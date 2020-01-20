using ChatApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ChatApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        public IndexModel( UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // TODO: Maybe should be removed
        public ApplicationUser CurrentUser;

        public bool IsAuthenticated => CurrentUser != null;

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(HttpContext.User); // Current User Id
            CurrentUser = await _userManager.FindByIdAsync(userId); // Current User
        }
    }
}
