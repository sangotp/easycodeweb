using EasyCodeAcademy.Web.Models;
using EasyCodeAcademy.Web.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
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
            // Mail Service
            services.AddOptions();                                        // Kích hoạt Options
            var mailsettings = Configuration.GetSection("MailSettings");  // đọc config
            services.Configure<MailSettings>(mailsettings);               // đăng ký để Inject

            services.AddTransient<IEmailSender, SendMailService>();        // Đăng ký dịch vụ Mail
            //services.AddSingleton<IEmailSender, SendMailService>();

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

            // Register Identity Services
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<EasyCodeContext>()
                    .AddDefaultTokenProviders();
            //services.AddDefaultIdentity<AppUser>()
            //        .AddEntityFrameworkStores<EasyCodeContext>()
            //        .AddDefaultTokenProviders();

            // Truy cập IdentityOptions
            services.Configure<IdentityOptions>(options => {
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lần thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
                options.SignIn.RequireConfirmedAccount = true; // Xác Thực Email, sau đó cho phép người dùng đăng nhập

            });

            // Register Identity Error Service
            services.AddSingleton<IdentityErrorDescriber, AppIdentityErrorDescriber>();

            // Register Identity Login Service
            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Identity/Account/Login";
                option.LogoutPath = "/Identity/Account/Logout";
                option.AccessDeniedPath = "/accessDenied.html";
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
            app.UseAuthentication();
            app.UseAuthorization();
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
