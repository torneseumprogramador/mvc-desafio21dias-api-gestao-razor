using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using mvc_desafio21dias_api_gestao_razor.Servico.ServicoRefatorado;
using mvc_desafio21dias_api_gestao_razor.Servico.ServicoRefatorado.Interfaces;
using mvc_desafio21dias_api_gestao_razor.Servico.ServicoRefatorado.Services;
using web_renderizacao_server_side.Models;

namespace web_renderizacao_server_side
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
            Program.AdministradoresAPI = Configuration.GetConnectionString("AdministradoresAPI");
            Program.AlunosAPI = Configuration.GetConnectionString("AlunosApi");
            Program.MateriaisAPI = Configuration.GetConnectionString("MateriaisAPI");
            Program.PaisAPI = Configuration.GetConnectionString("PaisAPI");

            services.AddScoped<IMaterialHttpClientService, MaterialHttpClientService>();
            
            services.AddControllersWithViews();
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
