using DevicesBackend;
using DevicesBackend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

int port = builder.Configuration.GetValue<int>("PORT");

builder.WebHost.ConfigureKestrel(serverOptions => 
{
    serverOptions.ListenAnyIP(port);
});


string? pathToDB = builder.Configuration.GetSection("PathToDB").Value;
if (string.IsNullOrEmpty(pathToDB))
    throw new ArgumentException("Path to DB is null or zero");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite($"Data Source={pathToDB}"));
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<DeviceService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();

    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}

app.UseBaseAuthorization();

app.MapControllers();
app.Run();
