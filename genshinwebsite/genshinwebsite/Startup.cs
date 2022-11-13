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
       // �����������ע�����
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); // ����һ��Ԥ����ķ���ע��
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
                options.ValidationInterval = TimeSpan.FromMinutes(1); // ÿһ���Ӽ��һ���û�״̬
            });

            services.ConfigureApplicationCookie(options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromDays(3);
                    options.SlidingExpiration = true; // ����ǰ����request���Զ�����һ���µ�cookie
                }

                 );
            services.AddAuthorization(options =>
            {
                //options.AddPolicy("���޹���Ա", policy => policy.RequireRole("Admin"));
                options.AddPolicy("����God", policy => policy.RequireRole("God"));
                options.AddPolicy("�༭����", policy => policy.RequireClaim("EditMusic", "Edit Music"));
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
            //    options.AddPolicy("���޹���Ա", policy => { })
            //});


            // ����������ע���Զ���ķ���
            // services.AddSingleton<> : ����ģʽ��ֻ������һ��ʵ��
            // services.AddTransient<>  : ÿ����������һ��ʵ��
            // services.AddScoped <> : ÿ��http��������һ��ʵ�������һ��http�����ж�������ʵ�����򲻻�����µ�ʵ��

            //services.AddScoped<IRepository<UserModel>, UserRepository>(); // ÿ��http��������µ�ʵ��
            //services.AddScoped<IRepository<UserModel>, EFCoreRepository>();
            services.AddScoped<IMusicDB<MusicModel, MusicViewModel>, MusicDBService>(); // efcore�����̰߳�ȫ�ģ�������singleton
                                                                                        // ��scope��ÿ��ִ�ж���һ���߼��߳���
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

            // ��������һ�������˳�򣬲�Ȼauthorize����Ч
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
