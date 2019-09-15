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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ChatApp.Pages
{
    [Authorize]
    public class SelectUser : PageModel
    {
        private readonly ApplicationDbContext _db;  // baza

        private readonly UserManager<ApplicationUser> _userManager;

        public IList<ApplicationUser> Users;
        public ApplicationUser currentUser;

        public bool UserIsSelected { get; set; }
        public int IndexOfSelectedUser { get; set; }

        public SelectUser(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            UserIsSelected = false;
            _db = db;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            Users = await _db.Users.ToListAsync(); // upis svih usera iz baze

            var userId = _userManager.GetUserId(HttpContext.User); // id trenutnog usera
            currentUser = await _userManager.FindByIdAsync(userId); // trenutni user
        }

    }
}