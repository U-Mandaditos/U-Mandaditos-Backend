using Microsoft.AspNetCore.SignalR;

namespace API.Hubs;

public class HubRequest: Hub 
{
    public async Task JoinPostGroup(string postId)
    {    
        await Groups.AddToGroupAsync(Context.ConnectionId, postId);
    }

    public async Task LeavePostGroup(string postId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, postId);
    }
}