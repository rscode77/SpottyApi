using Domain.Entities;
using Microsoft.AspNetCore.SignalR;

public class UserActivityHub : Hub
{
    public async Task SendUserActivity(UserActivity userActivity)
    {
        await Clients.All.SendAsync("ReceiveUserActivity", userActivity);
    }
}