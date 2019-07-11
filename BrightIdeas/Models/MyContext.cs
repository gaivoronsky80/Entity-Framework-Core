using Microsoft.EntityFrameworkCore; 

namespace BrightIdeas.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        
        // "users" table is represented by this DbSet "Users"
        public DbSet<User> Users {get;set;}
        public DbSet<Message> Messages {get;set;}
        public DbSet<Like> Likes {get;set;}
    }
}