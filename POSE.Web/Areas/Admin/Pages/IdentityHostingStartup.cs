namespace POSE.Web.Areas.Admin
{
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// Defines the <see cref="IdentityHostingStartup" />
    /// </summary>
    public class IdentityHostingStartup : IHostingStartup
    {
        /// <summary>
        /// The Configure
        /// </summary>
        /// <param name="builder">The builder<see cref="IWebHostBuilder"/></param>
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {

            });
        }
    }
}
