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

        public ApplicationUser OtherUser { get; set; }

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public List<Message> Messages { get; set; }

        public ApplicationUser CurrentUser { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(HttpContext.User); // id trenutnog usera
            CurrentUser = await _userManager.FindByIdAsync(userId); // trenutni user 

            Messages = await _db.Messages
                    .Where(m => ( m.SenderId == CurrentUser.Id && m.ReceiverId == Id)
                        || (m.SenderId == Id && m.ReceiverId == CurrentUser.Id))
                    .Include(m => m.Receiver)
                    .Include(m => m.Sender)
                    .OrderBy(m => m.Time)
                    .ToListAsync();

            OtherUser = await _db.Users.FirstOrDefaultAsync(u => u.Id == Id);
        }


    }
}