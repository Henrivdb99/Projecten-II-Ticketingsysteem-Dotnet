using Project_21_22;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projecten2_TicketingPlatform.Models.Domein
{
    public interface IGebruikerRepository
    {
        Gebruiker GetBy(String gebruikersnaam);
    }
}