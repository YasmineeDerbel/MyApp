//using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
//using MyApp.Areas.Identity.Data;

namespace MyApp.Models;

public class MyappContext :DbContext { 

    public MyappContext(DbContextOptions<MyappContext> options)
        : base(options)
    {
        //Console.WriteLine("MyAppContext Constructor");
    }
    public DbSet<Utilisateur> Utilisateurs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Response> Response { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
     {
        
       
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
       
    }
}
