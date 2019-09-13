using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string userId, string message)
        {
            Clients.User(userId).SendAsync("RecieveMessage", userId, message);
        }
    }
}
