using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddDbContext<MusicStoreEntities>(options => options.UseSqlServer(Configuration.
               GetConnectionString("DefaultConnection")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();
            app.UseMvc();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseMvcWithDefaultRoute();
          

            using (var scope = app.ApplicationServices.CreateScope())
            {
                MusicStoreEntities storeEntities = scope.ServiceProvider.GetRequiredService<MusicStoreEntities>();
                ModelBuilderExtensions.Initialize(storeEntities);
            }

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapRazorPages();
                });
            
        }
    }
}
