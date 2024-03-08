using E_Bus.Entities.DbContext;
using E_Bus.Entities.Entities;
using E_Bus.Presentation.SeedDataHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.TranspostationRepository;
using Repositories.TripRepository;
using RepositoryContracts;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDatabase"));
});


builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(
        options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
        }
    ).
    AddEntityFrameworkStores<ApplicationDbContext>().
    AddDefaultTokenProviders().
    AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>().
    AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

builder.Services.AddAuthorization(
    options =>
    {
        options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        options.AddPolicy("NoLogin",
            policy =>
            {
                policy.RequireAssertion(
                    context => { return !context.User.Identity!.IsAuthenticated; }
                    );
            }
            );
    }
    );

builder.Services.AddScoped<IAdderRepository<Trip>, TripAdderRepository>();
builder.Services.AddScoped<IDeleterRepository<Trip>, TripDeleterRepository>();
builder.Services.AddScoped<IGetterRepository<Trip>, TripGetterRepository>();
builder.Services.AddScoped<IUpdaterRepository<Trip>, TripUpdaterRepository>();

builder.Services.AddScoped<IGetterRepository<Transportation>, TransportationGetterRepository>();


builder.Services.AddScoped<ITripsService,TripsService>();
builder.Services.AddScoped<ITransportationsService,TransportationsService>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureApplicationCookie(
    options =>
    {
        options.LoginPath = "/Account/Login";
    }
    );
var app = builder.Build();
/*
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
    // requires using Microsoft.Extensions.Configuration;
    // Set password with the Secret Manager tool.
    // dotnet user-secrets set SeedUserPW <pw>

    var testUserPw = builder.Configuration.GetValue<string>("admin:password");
    var testUsername = builder.Configuration.GetValue<string>("admin:username");

    await SeedData.Initialize(services,testUsername, testUserPw);
}
*/
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
} else
{
    app.UseExceptionHandler("/Errors/ShowError");
}

app.UseStatusCodePagesWithRedirects("/Errors/ShowError?statusCode={0}");
app.UseHsts();
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization(

    );
app.MapControllers();

app.Run();
