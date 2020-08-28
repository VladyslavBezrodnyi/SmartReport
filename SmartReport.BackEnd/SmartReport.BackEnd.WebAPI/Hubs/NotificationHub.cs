using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SmartReport.BackEnd.BusinessLogicLayer.Interfaces;
using SmartReport.BackEnd.CrossCuttingConcern.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartReport.BackEnd.WebAPI.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        private static IHubContext<NotificationHub> hubContext;
        private readonly ICurrentUser _currentUser;
        private readonly IAccountService _accountService;
        public static Dictionary<String, DateTimeOffset> clientTimes = new Dictionary<String, DateTimeOffset>();

        public NotificationHub(IHubContext<NotificationHub> hubContext,
            ICurrentUser currentUser,
            IAccountService accountService)
        {
            NotificationHub.hubContext = hubContext;
            _currentUser = currentUser;
            _accountService = accountService;
        }
        public static async Task NotifyClientAsync(string userId, VisitDateDTO message)
        {
            if (hubContext != null)
            {
                await hubContext.Clients.Group(userId).SendAsync("ServerNotify", message);
            }
        }

        public static async Task AdminMonitorAsync(VisitDateDTO message)
        {
            if (hubContext != null)
            {
                await hubContext.Clients.Group("admin").SendAsync("AdminMonitor", message);
            }
        }

        public override async Task OnConnectedAsync()
        {
            var role = Context.User?.FindFirst(ClaimTypes.Role)?.Value;
            if (role == "admin")
            {
                await hubContext.Groups.AddToGroupAsync(this.Context.ConnectionId, "admin");
            }
            await hubContext.Groups.AddToGroupAsync(this.Context.ConnectionId, Context.UserIdentifier);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await hubContext.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, Context.UserIdentifier);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
