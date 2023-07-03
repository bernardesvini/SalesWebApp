﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebApp.Data;

namespace SalesWebApp
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<SalesWebAppContext>(options =>
                     options.UseMySql(Configuration.GetConnectionString("SalesWebAppContext"), builder => 
                        builder.MigrationsAssembly("SalesWebApp")));

            services.AddScoped<SeedingService>(); // *Mycomments Registro da SeedingService na injeção de dependência

            // MSSQL
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddDbContext<SalesWebAppContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("SalesWebAppContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedingService seedingService) //*Mycomments  Add seeding, Configure aceita que coloque outros parametros. E se essa estiver registrado na injeção de dependência, automaticamente é resolvido uma instância desse objeto 
        {
            if (env.IsDevelopment()) // *Mycomments testa se está no perfil de desenvolvimento
            {
                app.UseDeveloperExceptionPage();
                seedingService.Seed(); // *Mycomments aplica-se o seed
            }
            else // *Mycomments App já está publicada
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
