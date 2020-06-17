using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartReport.BackEnd.CrossCuttingConcern.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartReport.BackEnd.DataAccessLayer.Contexts
{
    public class ApplicationDbContext: IdentityDbContext<User>
    {
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<TaskReport> TaskReports { get; set; }
        public virtual DbSet<UserTask> UserTasks { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasKey(k => k.Id);
            builder.Entity<Report>()
                .HasKey(k => k.Id);
            builder.Entity<Task>()
                .HasKey(k => k.Id);
            builder.Entity<Place>()
               .HasKey(k => k.Id);
            builder.Entity<TaskReport>()
                .HasKey(k => new { k.ReportId, k.TaskId });
            builder.Entity<UserTask>()
                .HasKey(k => new { k.UserId, k.TaskId });

            builder.Entity<UserTask>()
                .HasOne(obj => obj.User)
                .WithMany(obj => obj.UserTasks)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserTask>()
               .HasOne(obj => obj.Task)
               .WithMany(obj => obj.UserTasks)
               .HasForeignKey(k => k.TaskId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TaskReport>()
               .HasOne(obj => obj.Report)
               .WithMany(obj => obj.TaskReports)
               .HasForeignKey(k => k.ReportId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TaskReport>()
               .HasOne(obj => obj.Task)
               .WithMany(obj => obj.TaskReports)
               .HasForeignKey(k => k.TaskId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Task>()
                .HasOne(obj => obj.Place)
                .WithMany(obj => obj.Tasks)
                .HasForeignKey(k => k.PlaceId)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(builder);
        }
    }
}
