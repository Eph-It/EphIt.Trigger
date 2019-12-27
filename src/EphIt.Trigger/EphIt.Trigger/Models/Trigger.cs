using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EphIt.Job.Models
{
    public class Trigger
    {
        public Trigger()
        {
            Jobs = new HashSet<Job>();
        }
        /// <summary>
        /// Identity of the row
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TriggerId { get; set; }
        /// <summary>
        /// Type of trigger
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Last time this Trigger successfully ran
        /// </summary>
        public DateTime? LastSuccessfulRun { get; set; }
        /// <summary>
        /// Last time this Trigger ran
        /// </summary>
        public DateTime? LastRun { get; set; }
        /// <summary>
        /// Last time this trigger evaluated
        /// </summary>
        public DateTime? LastEvaluation { get; set; }
        /// <summary>
        /// Next evaluation
        /// </summary>
        public DateTime? NextEvaluation { get; set; }
        /// <summary>
        /// How often do we internally check this trigger - measured in ticks
        /// </summary>
        public long TriggerInterval { get; set; }
        /// <summary>
        /// RowVersion for concurrency check
        /// </summary>
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public Trigger_Interval Interval { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
    public class TriggerConfiguration : IEntityTypeConfiguration<Trigger>
    {
        public void Configure(EntityTypeBuilder<Trigger> builder)
        {
            
        }
    }
}
