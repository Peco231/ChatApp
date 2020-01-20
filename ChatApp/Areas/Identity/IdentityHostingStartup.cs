using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ChatApp.Areas.Identity.IdentityHostingStartup))]
namespace ChatApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}