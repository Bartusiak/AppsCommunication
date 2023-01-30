using ApplicationB.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace AppB.Db.Data;

public class MessagesDbContext : DbContext
{ 
    public MessagesDbContext() { }

    public MessagesDbContext(DbContextOptions<MessagesDbContext> options) : base(options){ }
    
    public DbSet<Messages> Message { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var iConfigRoot = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();

        optionsBuilder
            .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .UseSqlServer(iConfigRoot.GetConnectionString("DefaultConnection"));
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Messages>()
            .Property(p => p.MsgId)
            .HasColumnName("MsgId")
            .IsRequired()
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Messages>()
            .Property(p => p.EncodedMsg)
            .HasColumnName("EncodedMsg")
            .IsRequired()
            .HasColumnType("varbinary(MAX)");
    }
}