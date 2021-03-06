﻿using DemoBlog.Backend.Controllers;
using DemoBlog.Backend.Services;
using DemoBlog.DataLib.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoBlog.Backend
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
            services.AddMvc()
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var optionsBuilder = new DbContextOptionsBuilder<BlogContext>();
            optionsBuilder.UseSqlite("Data Source=blog.db");

            var context = new BlogContext(optionsBuilder.Options);

            context.Database.EnsureCreated();
            context.SaveChanges();

            var dataService = new DataService(context);

            services.AddTransient(ctx => new PostsController(dataService));
            services.AddTransient(ctx => new SessionController(dataService));
            services.AddTransient(ctx => new UserController(dataService));
            services.AddTransient(ctx => new CommentController(dataService));
            services.AddTransient(ctx => new VoteController(dataService));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
