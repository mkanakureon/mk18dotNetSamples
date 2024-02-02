using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace BlazorSignalRAppServer.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            Debug.WriteLine($"SendMessage {user} {message}");
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

    }
}
