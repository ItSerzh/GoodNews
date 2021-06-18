using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NewsAnalizer.Core.Services.Interfaces;
using NewsAnalizer.Dal.Repositories.Implementation;
using NewsAnalizer.Dal.Repositories.Interfaces;
using NewsAnalizer.DAL.Core;
using NewsAnalizer.DAL.Core.Entities;
using NewsAnalizer.Services.Implementation;
using NewsAnalizer.Services.Implementation.Mapping;
using NewsAnalyzer.DAL.CQRS.Queries;
using RssSourceAnalizer.Dal.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAnalyzer.WebAPI
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
            services.AddDbContext<NewsAnalizerContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<INewsRepository, NewsRepository>();
            services.AddTransient<IRepository<RssSource>, RssSourceRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<Role>, RoleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IRssSourceService, RssSourceCqsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapping());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMediatR(typeof(GetRssSourceByIdQuery));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NewsAnalyzer.WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NewsAnalyzer.WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
