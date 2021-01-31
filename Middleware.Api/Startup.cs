using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Middleware.Data;
using Middleware.Data.Access;
using Middleware.Data.Access.Interfaces;

namespace Middleware.Api
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
            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson();
            services.Configure<HttpMiddlewareInterceptor>(Configuration.GetSection(nameof(HttpMiddlewareInterceptor)));
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<DeviceObserver>();

            //var test = Configuration.GetConnectionString(nameof(HttpMiddlewareInterceptor.ConnectionString));
            //var test2 = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<Context>(options =>
               options.UseSqlServer(
                   //Configuration.GetConnectionString(nameof(HttpMiddlewareInterceptor.ConnectionString)), opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)));
                   Configuration.GetConnectionString("DefaultConnection"), opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DiagnosticListener diagnosticListener, DeviceObserver deviceObserver)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseIoTListenerMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Subscribe to the observer
            diagnosticListener.Subscribe(deviceObserver);
        }
    }
}
