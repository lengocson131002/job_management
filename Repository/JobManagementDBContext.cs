using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class JobManagementDBContext : DbContext
    {

        public JobManagementDBContext(DbContextOptions<JobManagementDBContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<JobDescription> JobDescriptions { get; set; }

        public DbSet<Resume> Resumes { get; set; }

        public DbSet<Skill> Levels { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Application> Applications { get; set; }

        public DbSet<ResumeSkill> ResumeSkills { get; set; }

        public DbSet<JobDescriptionSkill> JobDescriptionSkills { get; set; }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        private void AddTimestamps()
        {
            var entites = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entites)
            {
                var now = DateTime.Now;

                var baseEntity = (BaseEntity)entity.Entity;

                if (entity.State == EntityState.Added)
                {
                    baseEntity.CreatedAt = now;
                }

                baseEntity.ModifiedAt = now;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // JobDescription Skill
            modelBuilder.Entity<JobDescription>()
             .HasMany(job => job.Skills)
             .WithMany(skill => skill.JobDescriptions)
             .UsingEntity<JobDescriptionSkill>(
                js => js.HasOne(prop => prop.Skill).WithMany().HasForeignKey(prop => prop.SkillId),
                js => js.HasOne(prop => prop.JobDescription).WithMany().HasForeignKey(prop => prop.JobDescriptionId),
                js => {});

            // Resume Skill
            modelBuilder.Entity<Resume>()
             .HasMany(resume => resume.Skills)
             .WithMany(skill => skill.Resumes)
             .UsingEntity<ResumeSkill>(
                js => js.HasOne(prop => prop.Skill).WithMany().HasForeignKey(prop => prop.SkillId),
                js => js.HasOne(prop => prop.Resume).WithMany().HasForeignKey(prop => prop.ResumeId),
                js => { });

            // Application
            modelBuilder.Entity<JobDescription>()
             .HasMany(job => job.Resumes)
             .WithMany(resume=> resume.JobDescriptions)
             .UsingEntity<Application>(
                js => js.HasOne(prop => prop.Resume).WithMany().HasForeignKey(prop => prop.ResumeId),
                js => js.HasOne(prop => prop.JobDescription).WithMany().HasForeignKey(prop => prop.JobDescriptionId),
                js => { });
        }
    }
}
