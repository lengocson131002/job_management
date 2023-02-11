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

        public DbSet<Level> Levels { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Application> Applications { get; set; }

        public DbSet<JobDescriptionLevel> JobDescriptionsLevel { get; set; }

        public DbSet<JobDescriptionTag> JobDescriptionTags { get; set; }

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
    }
}
