using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EphIt.Trigger.Models
{
    public class TriggerContext : DbContext
    {
        public TriggerContext(DbContextOptions options) : base (options) { }
        public TriggerContext() { }
        public virtual DbSet<Trigger> Trigger { get; set; }
        public virtual DbSet<Trigger_Interval> Trigger_Interval { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TriggerConfiguration());
            modelBuilder.ApplyConfiguration(new Trigger_IntervalConfiguration());
        }
    }
}
