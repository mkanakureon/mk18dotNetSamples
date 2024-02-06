using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSignalR
{
    public class ConnectionManager
    {
        private readonly HubConnection _connection;

        public ConnectionManager(string url)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            _connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Console.WriteLine($"{user}: {message}");
            });
        }

        public async Task ConnectAsync()
        {
            try
            {
                await _connection.StartAsync();
                Console.WriteLine("Connected to the hub.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while connecting: {ex.Message}");
            }
        }

        public async Task SendMessageAsync(string user, string message)
        {
            try
            {
                await _connection.InvokeAsync("SendMessage", user, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while sending a message: {ex.Message}");
            }
        }

        // Call this method when the application is closing
        public async Task DisconnectAsync()
        {
            await _connection.StopAsync();
            await _connection.DisposeAsync();
        }
    }
}
