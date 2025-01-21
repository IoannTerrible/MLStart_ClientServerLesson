using Microsoft.AspNetCore.SignalR;

using System.Threading.Tasks;

namespace SignalRExample;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public override async Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;

        Console.WriteLine($"Client connected: {connectionId}");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connectionId = Context.ConnectionId;
        Console.WriteLine($"Client disconnected: {connectionId}");

        await base.OnDisconnectedAsync(exception);
    }
}
