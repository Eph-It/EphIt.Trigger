using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EphIt.Trigger.Models
{
    public class Trigger_Interval
    {
        public int Interval { get; set; }
        public Trigger Trigger { get; set; }
    }
    public class Trigger_IntervalConfiguration : IEntityTypeConfiguration<Trigger_Interval>
    {
        public void Configure(EntityTypeBuilder<Trigger_Interval> builder)
        {
            builder.HasOne(p => p.Trigger)
                .WithOne(c => c.Interval)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
