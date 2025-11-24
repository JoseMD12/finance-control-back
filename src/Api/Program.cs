using Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

PostgresContext.CreateDbContext(builder.Services, builder.Configuration);
Infrastructure.DependencyInjection.Configure.Execute(builder.Services);
Application.DependencyInjection.Configure.Execute(builder.Services);

builder.Services.AddAuthentication("AppCookie")
    .AddCookie("AppCookie", options =>
    {
        options.LoginPath = "/login";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("CommonAccess", policy =>
    {
        policy.RequireAuthenticatedUser();
    });

builder.Services.AddControllers();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

