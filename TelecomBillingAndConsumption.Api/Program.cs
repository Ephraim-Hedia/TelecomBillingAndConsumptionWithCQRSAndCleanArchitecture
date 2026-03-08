using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using TelecomBillingAndConsumption.Core;
using TelecomBillingAndConsumption.Core.Middlewares;
using TelecomBillingAndConsumption.Data.Entities.Identity;
using TelecomBillingAndConsumption.Infrastructure;
using TelecomBillingAndConsumption.Infrastructure.DatabaseConntection;
using TelecomBillingAndConsumption.Infrastructure.Seeder;
using TelecomBillingAndConsumption.Service;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Cors Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
    });
});
#endregion

#region Database Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

#region Dependency Injection
builder.Services.AddInfrastructureDependencies()
    .AddServiceDependencies()
    .AddCoreDependencies()
    .AddServiceRegisteration(builder.Configuration);
#endregion

#region Localization
builder.Services.AddControllersWithViews();
builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    List<CultureInfo> supportedCultures = new List<CultureInfo>
    {
            new CultureInfo("en-US"),
            new CultureInfo("de-DE"),
            new CultureInfo("fr-FR"),
            new CultureInfo("ar-EG")
    };

    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
#endregion




var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}
#region Seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<Role>>();
    var dbContext = services.GetRequiredService<ApplicationDbContext>();

    // Identity Seed
    await RoleSeeder.SeedAsync(roleManager);
    await UsersSeeder.SeedAsync(userManager);

    // Domain Seed
    await PlansSeeder.SeedAsync(dbContext);
    await SubscribersSeeder.SeedAsync(dbContext);
    await TariffSeeder.SeedAsync(dbContext);
    await UsageRecordsSeeder.SeedAsync(dbContext);
}
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#region Localization Middleware
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
#endregion

app.UseMiddleware<ErrorHandlerMiddleware>();


app.UseHttpsRedirection();
app.UseCors("AllowAll");


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
