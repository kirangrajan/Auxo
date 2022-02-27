using Demo;
using Demo.Commands;
using Demo.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IClockingCommand, ClockingCommand>();
builder.Services.AddScoped<ILabourQuery, LabourQuery>();
builder.Services.AddScoped<IJobQuery, JobQuery>();
builder.Services.AddSingleton<IWorkshopRepository, WorkshopRepository>();

builder.Services.AddControllers();
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
