using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace ChatApp.Pages
{
    public class ErrorModel : PageModel
    {
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
