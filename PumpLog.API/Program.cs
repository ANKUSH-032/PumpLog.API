using PumpLog.Core.Auth;
using PumpLog.Core.PumpLog;
using PumpLog.Generic.Helper;
using PumpLog.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Authenticates.Initialize(builder.Configuration);

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPumpLogRepository, PumpLogRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
