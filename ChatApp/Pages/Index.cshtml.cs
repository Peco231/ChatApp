using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ApplicationUser currentUser;
        public bool Autentificiran { get; set; }

        public IndexModel( UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            Autentificiran = false;
        }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(HttpContext.User); // id trenutnog usera
            if(userId != null)
            {
                Autentificiran = true;
            }
            currentUser = await _userManager.FindByIdAsync(userId); // trenutni user
        }
    }
}
