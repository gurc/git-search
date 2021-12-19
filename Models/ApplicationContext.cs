using Microsoft.EntityFrameworkCore;

namespace GitSearch.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<SearchResult> SearchResults { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=DataBase.db");
        }
    }
}
