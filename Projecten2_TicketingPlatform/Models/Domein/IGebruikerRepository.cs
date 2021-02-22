using System;

namespace Projecten2_TicketingPlatform.Models.Domein
{
    public interface IGebruikerRepository
    {
        Gebruiker GetBy(String gebruikersnaam);
        void SaveChanges();
    }
}