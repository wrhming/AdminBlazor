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
            //��ʾ������ϸ������Ϣ AddCircuitOptions(options => { options.DetailedErrors = true; })
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

            //��Ӳ���Ȩ��
            services.AddAuthorization(options =>
            {

                var permissions = Permissions.GetPermissions();
                Permissions.GetPermissionNames(permissions).ForEach(key => {
                    options.AddPolicy(key, policy => policy.AddRequirements(new PermissionAuthorizationRequirement(key)));
                }); ;
            });
            //services.Configure<IdentityOptions>(options => {

            //    // Password settings.
            //    options.Password.RequireDigit = false;//����������
            //    options.Password.RequireLowercase = false;//������Сд��ĸ
            //    options.Password.RequireNonAlphanumeric = false;//�����������ĸ�����ַ�
            //    options.Password.RequireUppercase = false;//������Сд��ĸ
            //    options.Password.RequiredLength = 6;//��С����
            //    options.Password.RequiredUniqueChars = 1;//��ȡ��������������������СΨһ�ַ���

            //    options.SignIn.RequireConfirmedAccount = false;

            //    // Lockout settings.
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);//����������ʱ���û���������Ĭ��Ϊ5���ӡ�
            //    options.Lockout.MaxFailedAccessAttempts = 5;//��������������������û�������֮ǰ�����ʧ�ܷ��ʳ��Դ�����
            //    options.Lockout.AllowedForNewUsers = true;//����´������û����Ա���������ΪTrue������Ϊfalse��

            //    // User settings.
            //    options.User.AllowedUserNameCharacters ="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";//������֤�û������û�����������ַ��б�
            //    options.User.RequireUniqueEmail = false;//�Ƿ�Ҫ������
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

            //��֤ һ��Ҫ����֤����Ȩ
            app.UseAuthentication();
            //��Ȩ
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
