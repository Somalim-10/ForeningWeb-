using System;
using System.ComponentModel.DataAnnotations;

namespace ForeningWeb.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required, MaxLength(160)]
        public string Titel { get; set; } = "";

        [Required]
        public DateTime Dato { get; set; }

        [MaxLength(40)]
        public string? Tidspunkt { get; set; }

        [MaxLength(1000)]
        public string? Beskrivelse { get; set; }


      
    }
}
