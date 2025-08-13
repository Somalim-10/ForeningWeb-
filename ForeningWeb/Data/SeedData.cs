using System;
using System.Linq;
using ForeningWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ForeningWeb.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate();

            if (!db.Events.Any())
            {
                db.Events.Add(new Event
                {
                    Titel = "Velkomstmøde",
                    Dato = DateTime.Today.AddDays(7),
                    Beskrivelse = "Introduktion til foreningen"
                });
            }

            if (!db.Donationer.Any())
            {
                db.Donationer.Add(new Donation
                {
                    MobilePayNummer = "123456",
                    Besked = "Tak for støtten",
                    QrKodePath = "/img/qr.png"
                });
            }

            if (!db.Kontakter.Any())
            {
                db.Kontakter.Add(new Kontakt
                {
                    Navn = "Formand",
                    Email = "formand@example.com",
                    Telefon = "+4512345678"
                });
            }

            if (!db.Om.Any())
            {
                db.Om.Add(new Om
                {
                    Indhold = "Vi arbejder for fællesskabet."
                });
            }

            db.SaveChanges();
        }
    }
}
