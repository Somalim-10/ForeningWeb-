using System.ComponentModel.DataAnnotations;

namespace ForeningWeb.Models
{
    public class Kontakt
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Navn { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? Telefon { get; set; }

        public string? Adresse { get; set; }
    }
}
