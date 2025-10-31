using System;
using System.ComponentModel.DataAnnotations;

namespace ForeningWeb.Models
{
    public class Donation
    {
        private int _id;
        private string _mobilePayNummer = string.Empty;
        private string? _besked;
        private string? _qrKodePath;

        public int Id
        {
            get => _id;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Id), "Id skal være positivt.");
                _id = value;
            }
        }

        public string MobilePayNummer
        {
            get => _mobilePayNummer;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("MobilePayNummer må ikke være tom.", nameof(MobilePayNummer));
                if (value.Length > 50)
                    throw new ArgumentException("MobilePayNummer må maks være 50 tegn.", nameof(MobilePayNummer));
                _mobilePayNummer = value;
            }
        }

        public string? Besked
        {
            get => _besked;
            set => _besked = value;
        }

        public string? QrKodePath
        {
            get => _qrKodePath;
            set => _qrKodePath = value;
        }
    }
}
