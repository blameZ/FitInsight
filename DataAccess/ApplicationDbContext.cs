using System;
using FitInsight.Models;
using FitInsight.Models.ActivityModels;
using FitInsight.Models.ActivityModels.cs;
using FitInsight.Models.EquipmentModels;
using FitInsight.Models.UserModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitInsight.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityMetric> ActivityMetrics { get; set; }
        public DbSet<ActivityRoute> ActivityRoutes { get; set; }
        public DbSet<ActivityComment> ActivityComments { get; set; }
        public DbSet<ActivityLike> ActivityLikes { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
    }
}

