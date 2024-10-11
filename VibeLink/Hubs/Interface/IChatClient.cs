namespace VibeLink_Server.Hubs.Interface
{
    public interface IChatClient
    {
        Task ReceiveMessage(string userName, string message);
    }
}