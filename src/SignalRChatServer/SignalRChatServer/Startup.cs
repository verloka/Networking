using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalRChatServer.Services;

namespace SignalRChatServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();

            services.AddCors(options => options.AddPolicy("AllowLocalhost", builder => builder
                    .WithOrigins("http://localhost:3001")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("AllowLocalhost");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
