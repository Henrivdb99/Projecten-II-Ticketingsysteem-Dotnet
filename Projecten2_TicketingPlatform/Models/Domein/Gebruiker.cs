using Projecten2_TicketingPlatform.Models.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projecten2_TicketingPlatform.Models.Domein
{
    public abstract class Gebruiker
    {
        private String _gebruikersnaam;
        private int _wachtwoord;

        public Status Status
        {
            get => default;
            set
            {
            }
        }
    }
}