using Microsoft.EntityFrameworkCore;

namespace MyApp.Models
{
    public class MyAppContext : DbContext 

    {
        public MyAppContext(DbContextOptions<MyAppContext>options)
            : base(options)
        {
            Console.WriteLine("MyAppContext Constructor");
        }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
    }
}
