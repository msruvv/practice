using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrackingApp.Entities
{
    internal class TimeEntry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(0.01, 24)]
        public int Hours { get; set; }

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int TaskId { get; set; }
    }
}
