using KoperasiTentera.DB;
using KoperasiTentera.Repositories;
using KoperasiTentera.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add DbContext
builder.Services.AddDbContext<KoperasiTenteraDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KoperasiTenteraDBConn")));

builder.Services.AddMemoryCache(); // For storing verification codes
builder.Services.AddScoped<IVerificationService, VerificationService>();

//reoisitories
builder.Services.AddScoped<ILoginRepository, RegistrationRepository>();

//services
builder.Services.AddScoped<IRegistrationService, RegistrationService>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
