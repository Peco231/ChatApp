using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private ApplicationDbContext _db;

        public ChatHub(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task SendMessage(string receiverId, string message)
        {
            var senderId = Context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            await Clients
                .Users(new [] { receiverId, senderId })
                .SendAsync("ReceiveMessage", senderId, receiverId, message);

            var meessage = new Message
            {
                Text = message,
                Time = DateTime.Now,
                SenderId = senderId,
                ReceiverId = receiverId
            };

            _db.Messages.Add(meessage);
            _db.SaveChanges();
        }
    }
}
