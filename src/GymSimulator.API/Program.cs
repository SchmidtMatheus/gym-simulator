using GymSimulator.Application.Abstractions;
using GymSimulator.Infrastructure.Data;
using GymSimulator.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// DbContext: InMemory for now; change to UseSqlServer with your connection string.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("GymSimulatorDb"));

// DI services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IPlanTypeService, PlanTypeService>();
builder.Services.AddScoped<IClassTypeService, ClassTypeService>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IBookingService, BookingService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
