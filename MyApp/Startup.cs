using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using MyApp.Areas.Identity.Data;
using MyApp.Models;
using System;

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

        services.AddControllersWithViews(); // Exemple : Ajouter des services MVC à l'application
        services.AddRazorPages();
        services.AddDbContext<MyappContext>(Options =>
        Options.UseSqlServer(Configuration.GetConnectionString("MyAppContextConnection")));

        services.AddDistributedMemoryCache(); // Use in-memory cache for session
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

       

        /* services.AddIdentity<MyAppUser, IdentityRole>()
         .AddEntityFrameworkStores<MyappContext>()
         .AddDefaultTokenProviders();*/
        /* services.AddDefaultIdentity<MyAppUser>(options =>
         {
             // Ensure that the user is required to confirm their account
             options.SignIn.RequireConfirmedAccount = true;
         }).AddEntityFrameworkStores<MyAppContext>();

     */
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
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication(); 
        app.UseAuthorization();

        app.UseSession();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
               
        });

        
    }
}
