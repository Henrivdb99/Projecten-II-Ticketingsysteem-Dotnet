using Projecten2_TicketingPlatform.Models.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_21_22
{
    public class Klant : Gebruiker
    {
        public IEnumerable<Ticket> Tickets { get; set; }
    }

}