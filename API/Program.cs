using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}
catch(Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

// In case if we in development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//make all our request firstly go through our exception middleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors(policy => 
{
    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
