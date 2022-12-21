using Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shopping.BLL;
using Shopping.DAL;

namespace ShoppingApplication
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
            // Helper
            services.AddSingleton<IValidationHelper, ValidationHelper>();
            // BLL
            services.AddSingleton<IDiscountCalculator, DiscountCalculator>();
            services.AddSingleton<IShoppingBusiness, ShoppingBusiness>();
            // DAL
            services.AddSingleton<IShoppingProductRepository, ShoppingProductRepository>();
            services.AddSingleton<IFestivalRepository, FestivalRepository>();
            services.AddSingleton<IPromoCodeRepository, PromoCodeRepository>();

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
            }

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
