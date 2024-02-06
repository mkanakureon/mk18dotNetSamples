// クライアント側
using ConsoleSignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Main");
        GetId();

    }


    static void GetId()
    {
        Console.WriteLine("GetId");
        /***
        var cpuId = GetIdHelper.GetCpuId();
        Console.WriteLine(cpuId);

        var uuid = GetIdHelper.GetSystemUUID();
        Console.WriteLine(uuid);

        GetIdHelper.GetOsVersion();
        ***/

        var deviceId = GetIdHelper.GetDeviceId();
        Console.WriteLine(deviceId);

    }       

    static async Task testConnectionManager()
    {
        var connectionManager = new ConnectionManager("http://localhost:5157/chathub");
        await connectionManager.ConnectAsync();
        var user = "user";
        await connectionManager.SendMessageAsync(user, "message1");
        await connectionManager.SendMessageAsync(user, "message2");
        await connectionManager.SendMessageAsync(user, "message3");

    }

    static async Task test()
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