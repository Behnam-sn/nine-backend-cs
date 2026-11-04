using Nine.Profiles.Presentation.Profiles.WebApi.Endpoints;
using Nine.WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity();
builder.Services.AddMessaging();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapProfilesWebApi();

app.Run();

namespace Nine.WebApi
{
    public partial class Program;
}
