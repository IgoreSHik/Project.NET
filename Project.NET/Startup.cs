using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MiddlewareBalyka
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
            //services.AddRazorPages();
            services.AddMvc();
        }

        public String GiveMeName(HttpContext httpContext)
        {
            var browserInfo = httpContext.Request.Headers["User-Agent"].ToString();
            string answer = "I don't know";
            if ((browserInfo.Contains("MSIE"))|| (browserInfo.Contains("Trident"))|| (browserInfo.Contains("rv"))) answer = "Przegladarka IE nie jest obslugiwana";
            if ((browserInfo.Contains("Firefox")) && !(browserInfo.Contains("Seamonkey"))) answer = "Firefox";
            if ((browserInfo.Contains("Seamonkey"))) answer = "Seamonkey";
            if ((browserInfo.Contains("Chrome")) && !(browserInfo.Contains("Chromium"))) answer = "Chrome";
            if ((browserInfo.Contains("Chromium"))) answer = "Chromium";
            if ((browserInfo.Contains("Safari")) && !(browserInfo.Contains("Chromium")) && !(browserInfo.Contains("Chrome"))) answer = "Safari";
            if ((browserInfo.Contains("OPR")) || (browserInfo.Contains("Opera"))) answer = "Opera";
            if (browserInfo.Contains("Edg")) answer = "Przegladarka Edge nie jest obslugiwana";
            string s = "<p>User-agent: " + browserInfo + "</p><p>Browser:" + answer + "</p>";
            return s;
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync(GiveMeName(context));
                await next();
            });


            //app.UseMvc();
            /*
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });*/
        }
    }
}
