using System;
using System.ComponentModel.DataAnnotations;

namespace ForeningWeb.Models
{
    public class Kontakt
    {
        private int _id;
        private string _navn = string.Empty;
        private string? _email;
        private string? _telefon;
        private string? _adresse;

        public int Id
        {
            get => _id;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Id), "Id skal være positivt.");
                _id = value;
            }
        }

        public string Navn
        {
            get => _navn;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Navn må ikke være tom.", nameof(Navn));
                if (value.Length > 100)
                    throw new ArgumentException("Navn må maks være 100 tegn.", nameof(Navn));
                _navn = value;
            }
        }

        public string? Email
        {
            get => _email;
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && !new EmailAddressAttribute().IsValid(value))
                    throw new ArgumentException("Ugyldig email.", nameof(Email));
                _email = value;
            }
        }

        public string? Telefon
        {
            get => _telefon;
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && !new PhoneAttribute().IsValid(value))
                    throw new ArgumentException("Ugyldigt telefonnummer.", nameof(Telefon));
                _telefon = value;
            }
        }

        public string? Adresse
        {
            get => _adresse;
            set => _adresse = value;
        }
    }
}
