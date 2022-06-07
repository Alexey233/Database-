using Database_201_721;
using Database_201_721.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;  
    options.Password.RequireLowercase = false; 
    options.Password.RequireUppercase = false; 
    options.Password.RequireDigit = false; 
    options.User.RequireUniqueEmail = false;
})

    .AddEntityFrameworkStores<ApplicationContext>();



// Add services to the container.
builder.Services.AddControllersWithViews();



var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();;
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");


using var scope = app.Services.CreateScope();

var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
var rolesManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
await DefaultUsers.InitializeAsync(userManager, rolesManager);

app.Run();
