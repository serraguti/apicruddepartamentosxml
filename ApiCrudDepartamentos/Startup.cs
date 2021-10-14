using ApiCrudDepartamentos.Data;
using ApiCrudDepartamentos.Repositories;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudDepartamentos
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            String cadena = 
                this.Configuration.GetConnectionString("cadenahospital");
            services.AddTransient<RepositoryDepartamentos>();
            services.AddDbContext<DepartamentosContext>
                (options => options.UseSqlServer(cadena));
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(name: "v1", new OpenApiInfo
                {
                    Title = "Api Crud Departamentos Core",
                    Version = "v1",
                    Description = "Api Crud departamentos 2021"
                });
            });
            services.AddControllers();
            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            //});
            services.AddCors(
                o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));


            services.AddMvc().AddXmlDataContractSerializerFormatters();
            //services.Configure<MvcOptions>(options =>
            //{
            //    options.Filters.Add(new 
            //        CorsAuthorizationFilterFactory("MyPolicy"));
            //});
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(
                    url: "/swagger/v1/swagger.json", name: "Api v1");
                options.RoutePrefix = "";
            });
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("MyPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
