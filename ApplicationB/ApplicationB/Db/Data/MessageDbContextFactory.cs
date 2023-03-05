using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AppB.Db.Data;

public class MessageDbContextFactory : IDesignTimeDbContextFactory<MessagesDbContext>
{
    public MessagesDbContext CreateDbContext(string[] args)
    {
        var iConfigRoot = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();

        var optionsBuilder = new DbContextOptionsBuilder<MessagesDbContext>();
        optionsBuilder
            .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .UseSqlServer(iConfigRoot.GetConnectionString("DefaultConnection"));
        
        return new MessagesDbContext(optionsBuilder.Options);
    }
}