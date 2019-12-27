using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EphIt.Job.Models
{
    public class Action
    {
        public Action()
        {
            Jobs = new HashSet<Job>();
        }
        /// <summary>
        /// Identity of the row
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActionId { get; set; }


        public ICollection<Job> Jobs { get; set; }
    }
}
