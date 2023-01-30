using AppB.Db.Data;
using ApplicationB.Db.Services;

namespace ApplicationB;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddDbContext<MessagesDbContext>();
        builder.Services.AddHealthChecks();
        builder.Services.AddScoped<MessageService>();
        var app = builder.Build();
        
        app.UseHttpsRedirection();
        app.MapHealthChecks("/healthz");
        app.MapControllers();
        app.UsePathBase("/healthz");

        app.Run();
    }
}
