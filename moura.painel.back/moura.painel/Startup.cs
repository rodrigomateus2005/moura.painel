using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using moura.painel.Auth;
using moura.painel.Services;
using moura.painel.SismouraAPI;
using Swashbuckle.AspNetCore.Swagger;

namespace moura.painel
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<LoginTokenService>();
            services.AddSingleton<ILoginRepository>(new SismouraAPILoginRepository());
            services.AddScoped<IPainelRepository>(serv => new NHibernate.NHibernatePainelRepository());

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Custom Scheme";
                options.DefaultChallengeScheme = "Custom Scheme";
            }).AddCustomAuth(options => { });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Painel Lupo",
                        Version = "v1",
                        Description = "Painel para escolher um portal Lupo",
                        Contact = new OpenApiContact
                        {
                            Name = "Rodrigo Alves",
                            Url = new Uri("https://github.com/rodrigomateus2005")
                        }
                    });

                //string caminhoAplicacao =
                //    PlatformServices.Default.Application.ApplicationBasePath;
                //string nomeAplicacao =
                //    PlatformServices.Default.Application.ApplicationName;
                //string caminhoXmlDoc =
                //    Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                //c.IncludeXmlComments(caminhoXmlDoc);
            });
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
                app.UseHttpsRedirection(); // debug do vscode não funciona em https
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            // app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portal Lupo");
            });

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller}/{action}",
                //    defaults: new { controller = "Default", action = "Index" });

                routes.MapRoute(
                    name: "CatchAll",
                    template: "{*url}",
                    defaults: new { controller = "Default", action = "Index" });
            });
        }
    }
}
