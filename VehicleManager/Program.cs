using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VehicleManager.Data;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("UsersContextConnection") ?? throw new InvalidOperationException("Connection string 'UsersContextConnection' not found.");

        builder.Services.AddDbContext<UsersContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<UsersContext>();
        builder.Services.AddAuthentication().AddGoogle(options =>
        {
            options.ClientId = builder.Configuration["Google:ClientId"]!;
            options.ClientSecret = builder.Configuration["Google:ClientSecret"]!;
        });

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromDays(1);
        });

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddHttpClient();
        builder.Services.AddAutoMapper(typeof(Program));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(name: "index2",
                        pattern: "/Book/{carId}/{pickupDate}/{returnDate}",
                        defaults: new { controller = "Rentals", action = "Create" });
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        //Seed Roles
        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] { "Admin", "Member" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

        }
        //Seed Users
        using (var scope = app.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string emailAdmin = "admin@admin.com";
            string passwordAdmin = "Admin123.";
            if (await userManager.FindByEmailAsync(emailAdmin) is null) 
            {
                var userAdmin = new IdentityUser
                {
                    Email = emailAdmin,
                    UserName = emailAdmin,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(userAdmin, passwordAdmin);

                await userManager.AddToRoleAsync(userAdmin, "Admin");
            }


            string emailMember = "member@member.com";
            string passwordMember = "Member123.";
            if (await userManager.FindByEmailAsync(emailMember) is null)
            {
                var userMember = new IdentityUser
                {
                    Email = emailMember,
                    UserName = emailMember,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(userMember, passwordMember);

                await userManager.AddToRoleAsync(userMember, "Member");
            }
        }

        app.Run();
    }
}