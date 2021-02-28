using Projecten2_TicketingPlatform.Models.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projecten2_TicketingPlatform.Models.Domein
{
    public class Klant : Gebruiker
    {
        public IEnumerable<Contract> Contract { get; set; }
    }

}