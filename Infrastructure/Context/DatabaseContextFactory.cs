using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Infrastructure.Context;

public class DatabaseContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        const string connectionString = "Server=localhost;Database=citra_adit;Uid=root;Pwd='';";
        var mySqlVersion = ServerVersion.AutoDetect(connectionString);
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseMySql(connectionString,mySqlVersion);

        return new ApplicationDbContext(optionsBuilder.Options);
        
    }
}