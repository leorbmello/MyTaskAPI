using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LeoMello.DAL.Migrations
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            string connectionString = "Server=localhost;Port=3306;Database=mytaskapi;User=root;Password=Mel130317;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new ApplicationDbContext(null, optionsBuilder.Options);
        }
    }
}
