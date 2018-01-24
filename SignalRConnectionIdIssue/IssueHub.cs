using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRConnectionIdIssue
{
    public class IssueHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await this.Clients.Caller.SendAsync("setId", this.Context.ConnectionId);
        }
    }
}
