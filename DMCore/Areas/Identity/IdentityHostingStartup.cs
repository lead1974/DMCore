using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(DMCore.Areas.Identity.IdentityHostingStartup))]
namespace DMCore.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}