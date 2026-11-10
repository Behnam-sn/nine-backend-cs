using Nine.Identity.Domain.Users.Entities;
using Nine.Profiles.Presentation.Profiles.WebApi.Endpoints;
using Nine.WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMessaging();
builder.Services.AddIdentity();
builder.Services.AddProfiles();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.ApplyIdentityMigrationsAsync();
await app.AddIdentityRolesAsync();

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapSwagger().RequireAuthorization();

app.MapIdentityApi<User>();
app.MapProfilesWebApi();

app.Run();

namespace Nine.WebApi
{
    public partial class Program;
}
