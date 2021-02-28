using Projecten2_TicketingPlatform.Models.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projecten2_TicketingPlatform.Models.Domein
{
    public abstract class Gebruiker
    {
        private string _gebruikersnaam;
        private int _wachtwoord;

        public IEnumerable<Ticket> Tickets { get; set; }

        public string Gebruikersnaam
        {
            get
            {
                return _gebruikersnaam;
            }
            private set
            {
                _gebruikersnaam = value;
            }
        }
        public Status Status
        {
            get;
            set;
        }


    }
}