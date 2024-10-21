using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ServerLibrary.Repositories.Implementation
{
    public class UserRepository(IOptions<JwtSection> config, AppDbContext appDbContext) : IUserRepository
    {
        public async Task<GeneralResponse> CreateAsync(Register user)
        {
            if (user is null)
            {
                return new GeneralResponse(false, "Model is empty");
            }

            var checkUserEmail = await FindUserByEmail(user.Email!);
            if (checkUserEmail is not null) return new GeneralResponse(false, "User registered already");

            var checkUserName = await FindUserByName(user.FullName!);
            if (checkUserName is not null) return new GeneralResponse(false, "User registered already");

            var newUser = await AddToDatabase(new User()
            {
                Name = user.FullName,
                Email = user.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password)
            });

            // check, create and assign role
            var checkAdminRole = await appDbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.Equals(Constants.Admin));
            if (checkAdminRole is null)
            {
                var createAdminRole = await AddToDatabase(new SystemRole() { Name = Constants.Admin });
                await AddToDatabase(new UserRole() { RoleId = createAdminRole.Id, UserId = newUser.Id });
                return new GeneralResponse(true, "Account created!");
            }

            var checkUserRole = await appDbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.Equals(Constants.User));
            SystemRole response = new();
            if (checkUserRole is null)
            {
                response = await AddToDatabase(new SystemRole() { Name = Constants.User });
                await AddToDatabase(new UserRole() { RoleId = response.Id, UserId = newUser.Id });
            }
            else
            {
                await AddToDatabase(new UserRole() { RoleId = checkUserRole.Id, UserId = newUser.Id });
            }
            return new GeneralResponse(true, "Account created!");
        }

        public async Task<LoginResponse> SignInAsync(Login user)
        {
            if (user is null) return new LoginResponse(false, "Model is empty");

            var newUser = await FindUserByEmail(user.Email!);
            if (newUser is null) return new LoginResponse(false, "User not found!");

            if (!BCrypt.Net.BCrypt.Verify(user.Password, newUser.PasswordHash)) return new LoginResponse(false, "Email/Password not valid!");

            var getUserRole = await FindUserRole(newUser.Id!);
            if (getUserRole is null) return new LoginResponse(false, "User role not found");

            var getRoleName = await FindRoleName(getUserRole.RoleId!);
            if (getUserRole is null) return new LoginResponse(false, "User role not found");

            string jwtToken = GenerateToken(newUser, getRoleName!.Name!);
            string refreshToken = GenerateRefreshToken();

            var findUser = await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(_ => _.UserId == newUser.Id);
            if (findUser is not null)
            {
                findUser!.Token = refreshToken;
                await appDbContext.SaveChangesAsync();
            }
            else
            {
                await AddToDatabase(new RefreshTokenInfo() { Token = refreshToken, UserId = newUser.Id });
            }
            return new LoginResponse(true, "Login successfully", jwtToken, refreshToken);
        }

        private async Task<T> AddToDatabase<T>(T model)
        {
            var result = appDbContext.Add(model);
            await appDbContext.SaveChangesAsync();
            return (T)result.Entity;
        }

        #region FindUser

        private async Task<User> FindUserByEmail(string email) => await appDbContext.Users.FirstOrDefaultAsync(u => u.Email!.ToLower()!.Equals(email!.ToLower()));

        private async Task<User> FindUserByName(string name) => await appDbContext.Users.FirstOrDefaultAsync(u => u.Name!.ToLower()!.Equals(name!.ToLower()));

        private async Task<UserRole> FindUserRole(Guid userId)
            => await appDbContext.UserRoles.FirstOrDefaultAsync(_ => _.UserId == userId);

        private async Task<SystemRole> FindRoleName(Guid roleId)
            => await appDbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Id == roleId);

        #endregion FindUser
        #region GenerateToken

        private string GenerateToken(User user, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Value.Key!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, role!)
            };
            var token = new JwtSecurityToken(
                issuer: config.Value.Issuer,
                audience: config.Value.Audience,
                claims: userClaims,
                expires: DateTime.Now.AddSeconds(2),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion GenerateToken
        #region

        private static string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        #endregion GenerateRefreshToken
        #region RefreshTokenAsync

        public async Task<LoginResponse> RefreshTokenAsync(RefreshToken token)
        {
            if (token is null) return new LoginResponse(false, "Model is empty");

            var findToken = await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(_ => _.Token!.Equals(token.Token));
            if (findToken is null) return new LoginResponse(false, "Refresh token is required");

            //get user details
            var user = await appDbContext.Users.FirstOrDefaultAsync(_ => _.Id == findToken.UserId);
            if (user is null) return new LoginResponse(false, "Refresh token could not be generated because user not found");

            var userRole = await FindUserRole(user.Id);
            var roleName = await FindRoleName(userRole.RoleId);
            string jwtToken = GenerateToken(user, roleName.Name!);
            string refreshToken = GenerateRefreshToken();

            var updateRefreshToken = await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(_ => _.UserId == user.Id);
            if (updateRefreshToken is null) return new LoginResponse(false, "Refresh token could not be generated because user has not signed in");

            updateRefreshToken.Token = refreshToken;
            await appDbContext.SaveChangesAsync();
            return new LoginResponse(true, "Token refreshed successfully", jwtToken, refreshToken);
        }

        #endregion
    }
}