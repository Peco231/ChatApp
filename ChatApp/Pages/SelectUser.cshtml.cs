using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Pages
{
    [Authorize]
    public class SelectUser : PageModel
    {
        private readonly ApplicationDbContext _db;  // baza

        private readonly UserManager<ApplicationUser> _userManager;

        public SelectUser(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;

            UserIsSelected = false;
        }

        public IList<ApplicationUser> Users;
        public ApplicationUser CurrentUser;

        public bool UserIsSelected { get; set; }
        public int IndexOfSelectedUser { get; set; }

        public async Task OnGetAsync()
        {
            Users = await _db.Users.ToListAsync(); // upis svih usera iz baze

            var userId = _userManager.GetUserId(HttpContext.User); // id trenutnog usera
            CurrentUser = await _userManager.FindByIdAsync(userId); // trenutni user
        }

    }
}