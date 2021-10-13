using MDFSchoolAppV2.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDFSchoolAppV2.Core
{
    public class MDFSchoolAppV2DbContext : DbContext
    {
        public DbSet<Student> Student { get; set; }
        public DbSet<Course> Course { get; set; }

        public MDFSchoolAppV2DbContext(DbContextOptions<MDFSchoolAppV2DbContext> contextOptions)
            :base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region tables

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable(name: "Student", schema: "dbo");
                entity.HasKey(s => s.Id);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable(name: "Course", schema: "dbo");
                entity.HasKey(c => c.Id);
            });

            #endregion
        }
    }
}
