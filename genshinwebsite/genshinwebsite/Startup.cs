using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using genshinwebsite.Services;
using genshinwebsite.Models;
using genshinwebsite.ViewModels;
using genshinwebsite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;







namespace genshinwebsite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
       // 这个方法用来注册服务
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); // 其中一个预定义的服务注册
            services.AddDbContext<DataContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("SQLServerConnection"));
            });
            services.AddDbContext<IdentityDbContext>(
                option =>
                {
                    option.UseSqlServer(Configuration.GetConnectionString("SQLServerConnection"), b => b.MigrationsAssembly("genshinwebsite"));
                }
            );
            services.AddDbContext<MusicDataContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("SQLServerConnection"));
            });
            services.AddDbContext<EmailVCodeDataContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("SQLServerConnection"));
            });
            services.AddDbContext<CommentDataContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("SQLServerConnection"));
            });

            //services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<IdentityDbContext>();
            services.AddIdentity<UserModel, IdentityRole<int>>(
                options =>
                {
                    // Password settings.
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;

                    // Lockout settings.
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;

                    // User settings.
                    options.User.AllowedUserNameCharacters = null;
                    options.User.RequireUniqueEmail = true;
                }
                ).AddEntityFrameworkStores<DataContext>();

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(1); // 每一分钟检查一次用户状态
            });

            services.ConfigureApplicationCookie(options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromDays(3);
                    options.SlidingExpiration = true; // 过期前发送request则自动发送一个新的cookie
                }

                 );
            services.AddAuthorization(options =>
            {
                //options.AddPolicy("仅限管理员", policy => policy.RequireRole("Admin"));
                options.AddPolicy("仅限God", policy => policy.RequireRole("God"));
                options.AddPolicy("编辑乐谱", policy => policy.RequireClaim("EditMusic", "Edit Music"));
            });

            // Add framework services.
            services.AddMvc();

            //Add MailKit
            services.AddMailKit(optionBuilder =>
            {
                optionBuilder.UseMailKit(new MailKitOptions()
                {
                    //get options from sercets.json
                    Server = Configuration["Server"],
                    Port = Convert.ToInt32(Configuration["Port"]),
                    SenderName = Configuration["SenderName"],
                    SenderEmail = Configuration["SenderEmail"],

                    // can be optional with no authentication 
                    Account = Configuration["Account"],
                    Password = Configuration["Password"],
                    // enable ssl or tls
                    Security = true
                });
            });
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("仅限管理员", policy => { })
            //});


            // 以下是三种注册自定义的服务
            // services.AddSingleton<> : 单例模式，只会生成一次实例
            // services.AddTransient<>  : 每次请求都生成一个实例
            // services.AddScoped <> : 每次http请求生成一个实例，如果一次http请求中多次请求该实例，则不会产生新的实例

            //services.AddScoped<IRepository<UserModel>, UserRepository>(); // 每次http请求产生新的实例
            //services.AddScoped<IRepository<UserModel>, EFCoreRepository>();
            services.AddScoped<IMusicDB<MusicModel, MusicViewModel>, MusicDBService>(); // efcore不是线程安全的，不能用singleton
                                                                                        // 用scope让每次执行都在一个逻辑线程中
            services.AddScoped<IEmailVCodeDB, EmailVCodeDBService>();
            services.AddScoped<ICommentDB<CommentModel, CommentViewModel>, CommentDBService>();


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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();

            // 下面两个一定是这个顺序，不然authorize会无效
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
