using TameAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace TameAPI.Context
{
    public class DBContext : DbContext
    {

        public DbSet<EFSchedule> Schedules { get; set; }
        public DbSet<EFUsers> Users { get; set; }
        public DBContext(string cnnString)
        {
            ConnectionString = cnnString;
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);

        }
    }
}
