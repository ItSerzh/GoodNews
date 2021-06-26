using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewsAnalyzer.Core.DataTransferObjects;
using NewsAnalyzer.Core.Interfaces.Services;
using NewsAnalyzer.Core.Services.Interfaces;
using NewsAnalyzer.Dal.Repositories.Implementation;
using NewsAnalyzer.Dal.Repositories.Interfaces;
using NewsAnalyzer.DAL.Core;
using NewsAnalyzer.DAL.Core.Entities;
using NewsAnalyzer.Services.Implementation;
using NewsAnalyzer.Services.Implementation.Mapping;
using RssSourceAnalyzer.Dal.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NewsAnalyzer.Services.Implementation.WebPageParse;

namespace NewsAnalyzer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NewsAnalyzerContext>(opt => 
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<INewsRepository, NewsRepository>();
            services.AddTransient<IRepository<RssSource>, RssSourceRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<Role>, RoleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IRssSourceService, RssSourceService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddTransient<IgromaniaParser>();
            services.AddTransient<ShazooParser>();
            services.AddTransient<OnlinerParser>();
            services.AddTransient<ForPdaParser>();
            services.AddTransient<WylsaParser>();

            services.AddControllersWithViews();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.LoginPath = new PathString("/Acount/Login");
                    opt.AccessDeniedPath = new PathString("/Acount/Login");
                });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapping());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public void RegisterWebPageParser(IServiceCollection services)
        {
            
            services.AddTransient<ServiceResolver>(serviceProvider => key =>
            {
                switch (key)
                {
                    case "Igromania":
                        return serviceProvider.GetService<IgromaniaParser>();
                    case "Shazoo":
                        return serviceProvider.GetService<ShazooParser>();
                    case "Onliner":
                        return serviceProvider.GetService<OnlinerParser>();
                    case "4Pda":
                        return serviceProvider.GetService<ForPdaParser>();
                    case "Wylsa":
                        return serviceProvider.GetService<WylsaParser>();
                    default:
                        throw new KeyNotFoundException(); // or maybe return null, up to you
                }
            });
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
