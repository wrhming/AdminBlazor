using System;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AntDesign.ProLayout;
using Microsoft.EntityFrameworkCore;
using AdminBlazor.Data;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Authorization;
using AdminBlazor.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace AdminBlazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddRazorPages();
            //显示错误详细错误信息 AddCircuitOptions(options => { options.DetailedErrors = true; })
            //services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

            services.AddServerSideBlazor(); 
            services.AddAntDesign();

            services.AddScoped<ProtectedSessionStorage>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddScoped<UserAccountService>();
            

            services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(sp.GetService<NavigationManager>().BaseUri)
            });
            services.Configure<ProSettings>(Configuration.GetSection("ProSettings"));

            //添加策略权限
            services.AddAuthorization(options =>
            {

                var permissions = Permissions.GetPermissions();
                Permissions.GetPermissionNames(permissions).ForEach(key => {
                    options.AddPolicy(key, policy => policy.AddRequirements(new PermissionAuthorizationRequirement(key)));
                }); ;
            });
            //services.Configure<IdentityOptions>(options => {

            //    // Password settings.
            //    options.Password.RequireDigit = false;//必须有数字
            //    options.Password.RequireLowercase = false;//必须有小写字母
            //    options.Password.RequireNonAlphanumeric = false;//必须包含非字母数字字符
            //    options.Password.RequireUppercase = false;//必须有小写字母
            //    options.Password.RequiredLength = 6;//最小长度
            //    options.Password.RequiredUniqueChars = 1;//获取或设置密码必须包含的最小唯一字符数

            //    options.SignIn.RequireConfirmedAccount = false;

            //    // Lockout settings.
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);//当锁定发生时，用户被锁定。默认为5分钟。
            //    options.Lockout.MaxFailedAccessAttempts = 5;//如果启用了锁定，则在用户被锁定之前允许的失败访问尝试次数。
            //    options.Lockout.AllowedForNewUsers = true;//如果新创建的用户可以被锁定，则为True，否则为false。

            //    // User settings.
            //    options.User.AllowedUserNameCharacters ="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";//用于验证用户名的用户名中允许的字符列表。
            //    options.User.RequireUniqueEmail = false;//是否要求邮箱
            //});

            //services.ConfigureApplicationCookie(options =>
            //{
            //    // Cookie settings
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            //    options.LoginPath = "/Account/Login";
            //    options.AccessDeniedPath = "/Account/AccessDenied";
            //    options.SlidingExpiration = true;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //认证 一定要先认证后授权
            app.UseAuthentication();
            //授权
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
