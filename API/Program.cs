using API.Extensions;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

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

app.Run();
