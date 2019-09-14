using ChatApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatApp.Providers
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext context)
        {   
            var result = context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return result.Value;        
        }
    }
}
