namespace DNC.Example.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json.Serialization;
    using Swashbuckle.AspNetCore.Swagger;

    using DNC.Example.DAL.Context;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Get connection string from appsetting.json.
            var connection = Configuration.GetConnectionString("Database");
            services.AddDbContext<MovieListDbContext>(options => options.UseSqlServer(connection, x => x.MigrationsAssembly("DNC.Example.DAL")));

            //Add for all project to be response of api camelCase formating.
            services.AddMvcCore()
                 .AddApiExplorer()
                 .AddJsonFormatters()
                 .AddJsonOptions(options =>
                 {
                     options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                 }).AddAuthorization();

            // Registering Swagger Generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Swagger ACSS API",
                    Version = "v1",
                    Description = "TBD",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "John Doe", Email = "john@xyzmail.com", Url = "www.example.com" },
                    License = new License() { Name = "License Terms", Url = "www.example.com" }
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Create Db if not exist automatically.
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<MovieListDbContext>();
                context.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Serves generated swagger document as JSON endpoint. 
                app.UseSwagger();

                // Serves the Swagger UI
                app.UseSwaggerUI(c =>
                {
                    // specifying the Swagger JSON endpoint.
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger ACSS API");
                    c.RoutePrefix = string.Empty;
                });
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
