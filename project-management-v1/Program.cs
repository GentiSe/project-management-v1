using project_management_v1.Infrastructure;
using project_management_v1.Infrastructure.Data;
using project_management_v1.Infrastructure.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));

builder
    .AddDbContext()
    .AddIdentityService()
    .AddIoC()
    .UseAuth()
    .UseAuthorization();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

SeedDatabase();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


void SeedDatabase() //can be placed at the very bottom under app.Run()
{
    using var scope = app.Services.CreateScope();
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    dbInitializer.Initialize().GetAwaiter().GetResult();
}