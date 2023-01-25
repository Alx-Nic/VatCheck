using VatCheck.API.Helpers;
using VatCheck.Service.Abstract;
using VatCheck.Service.Concrete;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

String[] myAllowedOrigins = new string[1] {
    "http://localhost:8080"
    };

services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(myAllowedOrigins)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScoped<IVatCheckService, VatCheckService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();


app.UseAuthorization();

app.MapControllers();

app.Run();
