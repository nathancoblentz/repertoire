using Microsoft.EntityFrameworkCore;
using CoblentzContext.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromSeconds(60 * 1));

builder.Services.AddDbContext<CoblentzContext.Models.CoblentzContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CoblentzContext")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5; // Lowered to allow "admin1" if needed
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<CoblentzContext.Models.CoblentzContext>().AddDefaultTokenProviders();

// --- REQUIREMENT: Enable Lowercase URLs and Trailing Slashes via Routing Settings ---
builder.Services.AddRouting(options => {
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});

// --- REQUIREMENT: Add Response Compression for performance boost ---
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

var app = builder.Build();

// Enable compression middleware
app.UseResponseCompression();

// --- STARTUP LOGIC: Database Seeding & User Management ---
// We use a scope here to ensure the Admin account exists on first launch
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    
    // Ensure Role Exists
    if (!await roleManager.RoleExistsAsync("Admin")) {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Check for user "admin"
    var adminUser = await userManager.FindByNameAsync("admin");
    if (adminUser != null) {
        // Reset password to "admin123" to guarantee it works
        await userManager.RemovePasswordAsync(adminUser);
        await userManager.AddPasswordAsync(adminUser, "admin123");
        
        // Ensure they have the role
        if (!await userManager.IsInRoleAsync(adminUser, "Admin")) {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
// --- END STARTUP LOGIC ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
