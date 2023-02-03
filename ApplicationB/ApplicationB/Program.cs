using AppB.Db.Data;
using ApplicationB.Db.Models;
using ApplicationB.Db.Repository;
using ApplicationB.Db.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.OpenApi.Models;

namespace ApplicationB;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddDbContext<MessagesDbContext>();
        builder.Services.AddHealthChecks();
        builder.Services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Application B API Documentation",
                Description = "An ASP.NET Core WebAPI documentation to explain how to use app"
            });
            x.EnableAnnotations();
        });
        builder.Services.AddScoped<IMessagesRepository ,MessageService>();
        var app = builder.Build();
        
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<MessagesDbContext>();

            context.Database.EnsureCreated();

        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(x =>
            {
                x.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("v1/swagger.json", "Application B API V1 Documentation");
            });
        }
        app.UseHttpsRedirection();
        app.MapHealthChecks("/healthz");
        app.MapControllers();
        app.UsePathBase("/healthz");

        app.Run();
    }
}
