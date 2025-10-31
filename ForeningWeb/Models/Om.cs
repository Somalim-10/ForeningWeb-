using System;
using System.ComponentModel.DataAnnotations;

namespace ForeningWeb.Models
{
    public class Om
    {
        private int _id;
        private string _indhold = string.Empty;
        private string? _billedePath;

        public int Id
        {
            get => _id;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Id), "Id skal være positivt.");
                _id = value;
            }
        }

        public string Indhold
        {
            get => _indhold;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Indhold må ikke være tom.", nameof(Indhold));
                _indhold = value;
            }
        }

        public string? BilledePath
        {
            get => _billedePath;
            set => _billedePath = value;
        }
    }
}
