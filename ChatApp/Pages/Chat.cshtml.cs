using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Pages
{
    [Authorize]
    public class ChatModel : PageModel
    {
        /// <summary>
        /// Binding property for Other User Id
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public ApplicationUser OtherUser { get; set; }

        public List<Message> Messages { get; set; }

        public ApplicationUser CurrentUser { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(HttpContext.User); // Current User Id
            CurrentUser = await _userManager.FindByIdAsync(userId); // Current User

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