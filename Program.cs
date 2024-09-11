using FitInsight.DataAccess;
using FitInsight.Interfaces;
using FitInsight.Models;
using FitInsight.Repositories;
using FitInsight.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

//Register services and repositories
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
//    SeedDataAsync(context, userManager).Wait();
//}
app.Run();

//static async Task SeedDataAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
//{
//    var user = await userManager.FindByEmailAsync("bartosz@gmail.com");
//    if (user == null)
//    {
//        return; // If the user doesn't exist, exit seeding
//    }

//    var random = new Random();
//    var activities = new List<FitInsight.Models.ActivityModels.cs.Activity>();
//    var activityTypes = new[] { "Bieganie", "Jazda na rowerze", "Pływanie", "Spacer", "Wędrówka" };

//    for (int i = 0; i < 20; i++)
//    {
//        // Randomly pick an activity type
//        var activityType = activityTypes[random.Next(activityTypes.Length)];

//        // Generate a random distance and duration
//        var distance = random.Next(1, 20); // Distance in kilometers
//        var duration = TimeSpan.FromMinutes(random.Next(20, 120)); // Duration in minutes

//        // Simple formula for calories burned: distance * 60 (as a rough estimate)
//        var caloriesBurned = distance * 60;

//        activities.Add(new FitInsight.Models.ActivityModels.cs.Activity
//        {
//            UserId = Guid.Parse(user.Id),
//            ActivityType = activityType,
//            Distance = distance,
//            Duration = duration,
//            CaloriesBurned = caloriesBurned,
//            StartTime = DateTime.Now.AddDays(-random.Next(1, 100)).AddHours(-random.Next(1, 24)),
//            EndTime = DateTime.Now.AddDays(-random.Next(1, 100)).AddHours(-random.Next(1, 24)).Add(duration),
//            CreatedAt = DateTime.Now,
//            UpdatedAt = DateTime.Now
//        });
//    }

//    await context.Activities.AddRangeAsync(activities);
//    await context.SaveChangesAsync();
//}




