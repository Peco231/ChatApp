using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Security.Claims;

namespace ChatApp.Providers
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext context)
        {   
            var result = context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return result?.Value;        
        }
    }
}
