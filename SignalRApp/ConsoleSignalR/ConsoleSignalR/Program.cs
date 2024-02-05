// クライアント側
using Microsoft.AspNetCore.SignalR.Client;
using System;

class Program
{
    static async Task Main(string[] args)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5157/chathub")
            .Build();

        connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            Console.WriteLine($"{user}: {message}");
        });

        try
        {
            await connection.StartAsync();
            Console.WriteLine("Connected to the hub.");

            // ここでユーザー入力を受け取り、サーバーにメッセージを送信するなどの処理
            while (true)
            {
                string message = Console.ReadLine();
                await connection.InvokeAsync("SendMessage", "User", message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}