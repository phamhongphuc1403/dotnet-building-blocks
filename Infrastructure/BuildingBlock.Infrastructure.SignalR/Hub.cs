using Microsoft.AspNetCore.SignalR;

namespace BuildingBlock.Infrastructure.SignalR;

public class Hub : Microsoft.AspNetCore.SignalR.Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} joined the chat");
    }
}