using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

namespace EasyCodeAcademy.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Setup ConfigureServices For Using Services
        public void ConfigureServices(IServiceCollection services)
        {
            // Use Razor Page Service
            services.AddRazorPages();

            // Use DbContext Service For EasyCodeContext
            services.AddDbContext<EasyCodeContext>(options =>
            {
                string connectString = Configuration.GetConnectionString("MyEasyCodeContext");

                options.UseSqlServer(connectString);
            });

            // Config Max Request Body Size to 409715200 400 MB
            services.Configure<FormOptions>(x =>
            {
                x.MultipartBodyLengthLimit = 409715200;
            });
        }

        // Setup Configure For Using HTTP Pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(!env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                // Razor Page Endpoint
                endpoints.MapRazorPages();

                // Default Endpoint
                endpoints.MapGet("/hello", () => "Hello World!");
            });
        }
    }
}
