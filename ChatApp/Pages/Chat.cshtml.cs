using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace ChatApp.Pages
{
    public class ChatModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; } // id od receiver user

        public readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public ApplicationUser currentUser;
        public List<Message> messages;

        public ChatModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        
        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(HttpContext.User); // id trenutnog usera
            currentUser = await _userManager.FindByIdAsync(userId); // trenutni user 

            messages = await _db.Messages
                    .Where(m => ( m.SenderId == currentUser.Id && m.ReceiverId == Id)
                        || (m.SenderId == Id && m.ReceiverId == currentUser.Id))
                    .Include(m => m.Receiver)
                    .Include(m => m.Sender)
                    .OrderBy(m => m.Time)
                    .ToListAsync();
        }
        

    }
}