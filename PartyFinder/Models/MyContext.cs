using Microsoft.EntityFrameworkCore;

namespace PartyFinder.Models
{
    public class MyContext : DbContext
    {
       public MyContext(DbContextOptions options) : base(options) { }
       public DbSet<User> Users {get; set;}
       public DbSet<Party> Parties {get; set;}
       public DbSet<Join> Joins {get; set;}
    }
}