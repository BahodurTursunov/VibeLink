using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using ServerLibrary.Repositories.Implementation;
using System.Text;
using VibeLink_Server.Hubs.Implementation;

namespace VibeLink_Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();


            builder.Services.AddSignalR();
            builder.Services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    ["application/octet-stream"]);
            });


            #region Cors
            /*    builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll",
                        builder =>
                        {
                            builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                        });
                });*/

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazorWasm",
                builder => builder.WithOrigins("https://localhost:7214;http://localhost:5020")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
            #endregion 

            #region DbConnect

            builder.Services.AddDbContext<AppDbContext>(con =>
          con
              .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
              .LogTo(Console.Write, LogLevel.Information)
              .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            #endregion

            #region JwtSection and Authentication
            builder.Services.Configure<JwtSection>(builder.Configuration.GetSection("JwtSection"));
            var jwtSection = builder.Configuration.GetSection(nameof(JwtSection)).Get<JwtSection>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSection!.Issuer,
                    ValidAudience = jwtSection.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.Key!))
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthenticatedUser", policy =>
                    policy.RequireAuthenticatedUser());
            });

            #endregion


            #region Services

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            #endregion

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            app.UseResponseCompression();
            app.MapHub<ChatHub>("/chat");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}