using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EphIt.Trigger.Models
{
    public class Trigger
    {
        public Trigger()
        {
            
        }
        /// <summary>
        /// Identity of the row
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
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
        /// The JobId from the last time this trigger ran
        /// </summary>
        public Guid? LastJobId { get; set; }
        /// <summary>
        /// How often do we internally check this trigger
        /// </summary>
        public int TriggerInterval { get; set; }
        public Trigger_Interval Interval { get; set; }
    }
    public class TriggerConfiguration : IEntityTypeConfiguration<Trigger>
    {
        public void Configure(EntityTypeBuilder<Trigger> builder)
        {
            
        }
    }
}
