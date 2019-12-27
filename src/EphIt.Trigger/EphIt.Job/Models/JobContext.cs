using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EphIt.Job.Models
{
    public class JobContext : DbContext
    {
        public JobContext(DbContextOptions options) : base (options) { }
        public JobContext() { }
        public virtual DbSet<Trigger> Trigger { get; set; }
        public virtual DbSet<Trigger_Interval> Trigger_Interval { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TriggerConfiguration());
            modelBuilder.ApplyConfiguration(new Trigger_IntervalConfiguration());
        }
    }
}
