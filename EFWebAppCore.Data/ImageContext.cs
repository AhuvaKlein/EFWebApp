using Microsoft.EntityFrameworkCore;

namespace EFWebApp.Data
{
    public class ImageContext : DbContext
    {
        private string _connectionString;

        public ImageContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_connectionString);
        }
    }
}
