using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EphIt.Job.Models
{
    public class Job
    {
        /// <summary>
        /// Identity of the row
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobId { get; set; }

        /// <summary>
        /// Id of the action
        /// </summary>
        public int ActionId { get; set; }
        /// <summary>
        /// Id of the trigger
        /// </summary>
        public int TriggerId { get; set; }

        public Trigger Trigger { get; set; }
        public Action Action { get; set; }
    }
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder
                .HasOne(p => p.Trigger)
                .WithMany(c => c.Jobs)
                .HasForeignKey(p => p.TriggerId);
            builder
                .HasOne(p => p.Action)
                .WithMany(c => c.Jobs)
                .HasForeignKey(p => p.ActionId);
        }
    }
}
