using AutoMapper;
using RedBusinessEssentialWeb.ConvertEntityModel;

namespace RedBusinessEssentialWeb
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
            Injection.ConfigureDependencies.Dependencias(services, Configuration.GetConnectionString("DefaultConnection"));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EntityToModel());
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllersWithViews();
            services.AddMemoryCache();

            services.AddSession();
        }

        public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
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
