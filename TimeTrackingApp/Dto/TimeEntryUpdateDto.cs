using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrackingApp.Dto
{
    public class TimeEntryUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(0.01, 24)]
        public decimal Hours { get; set; }

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
    }
}
