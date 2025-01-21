using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var hubConnection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5131/chat")
            .Build();

        hubConnection.On<string, string>("ReceiveMessage", DisplayReceivedMessage);

        try
        {
            await hubConnection.StartAsync();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Connected to the chat hub!");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Connection error: {ex.Message}");
            Console.ResetColor();
            return;
        }

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter your name: ");
            Console.ResetColor();
            var user = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter your message: ");
            Console.ResetColor();
            var message = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(message))
                continue;

            try
            {
                await SendMessageAsync(hubConnection, user, message);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error sending message: {ex.Message}");
                Console.ResetColor();
            }
        }
    }

    static async Task SendMessageAsync(HubConnection hubConnection, string user, string message)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Sending message...");
        Console.ResetColor();

        await hubConnection.InvokeAsync("SendMessage", user, message);

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Message sent: \"{message}\"");
        Console.ResetColor();
    }

    static void DisplayReceivedMessage(string user, string message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {user}: {message}");
        Console.ResetColor();
    }
}
