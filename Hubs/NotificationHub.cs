using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Projet101.Hubs
{
    [Authorize]
    public class NotificationHub : Hub<INotification>
    {
        public override async Task OnConnectedAsync()
        {
            var user = Context.User;
            if (user == null || !user.Identity.IsAuthenticated) 
            {
                throw new HubException("Unauthorized");
            }

            var role = user.Claims.FirstOrDefault(c=> c.Type == ClaimTypes.Role)?.Value;

            if (!string.IsNullOrEmpty(role))
            {
                Groups.AddToGroupAsync(Context.ConnectionId, role);
            }


            await base.OnConnectedAsync();

          //  await Clients.All.ReceiveMessage($"{Context.ConnectionId} has joined");

        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = Context.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                throw new HubException("Unauthorized");
            }

            var role = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (!string.IsNullOrEmpty(role))
            {
                Groups.RemoveFromGroupAsync(Context.ConnectionId, role);
            }

            await base.OnDisconnectedAsync(exception);
        }

       

       
    }
}
