using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Pages
{
    public class ChatModel : PageModel
    {
        private readonly ApplicationDbContext _db;  // baza

        private readonly UserManager<ApplicationUser> _userManager;

        public IList<ApplicationUser> Users;
        public ApplicationUser currentUser;

        public ChatModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            Users = await _db.Users.ToListAsync(); // upis svih usera iz baze

            var userId = _userManager.GetUserId(HttpContext.User); // id trenutnog usera
            currentUser = await _userManager.FindByIdAsync(userId); // trenutni user
            
        }

        
        public List<UserSearchResult> getUserSearchResult (string tmp)
        {
            List<UserSearchResult> users = new List<UserSearchResult>();
            
            foreach(ApplicationUser user in Users)
            {
                if(user.FullName.Contains(tmp))
                {
                    users.Add(new UserSearchResult(user.Id, user.FullName));
                }
            }

            return users;
        }
    }
}