using System.ComponentModel.DataAnnotations;

namespace ForeningWeb.Models
{
    public class Donation
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string MobilePayNummer { get; set; } = string.Empty;

        // Tekst der vises ved siden af QR-koden
        public string? Besked { get; set; }

        // Sti til QR-kode billede
        public string? QrKodePath { get; set; }
        //11
    }
}
