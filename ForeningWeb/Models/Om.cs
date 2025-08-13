using System.ComponentModel.DataAnnotations;

namespace ForeningWeb.Models
{
    public class Om
    {
        public int Id { get; set; }

        [Required]
        public string Indhold { get; set; } = string.Empty;

        // Valgfrit billede (fx af foreningen)
        public string? BilledePath { get; set; }
    }
}
