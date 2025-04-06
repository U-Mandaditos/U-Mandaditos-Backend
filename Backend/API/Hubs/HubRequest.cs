using Microsoft.AspNetCore.SignalR;

namespace API.Hubs;

public class HubRequest: Hub 
{
    public async Task JoinPostGroup(string postId)
    {    
        Console.WriteLine($"Cliente {Context.ConnectionId} uniéndose al grupo {postId}");
        await Groups.AddToGroupAsync(Context.ConnectionId, postId);
        Console.WriteLine($"Cliente {Context.ConnectionId} se unió al grupo {postId}");
    }

    public async Task LeavePostGroup(string postId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, postId);
    }
}