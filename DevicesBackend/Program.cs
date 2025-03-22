using DevicesBackend;
using DevicesBackend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


string? pathToDB = builder.Configuration.GetSection("PathToDB").Value;
if (string.IsNullOrEmpty(pathToDB))
    throw new ArgumentException("Path to DB is null or zero");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite($"Data Source={pathToDB}"));
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<DeviceService>();
//builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseBaseAuthorization();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseAuthorization();

app.MapControllers();

app.Run();
