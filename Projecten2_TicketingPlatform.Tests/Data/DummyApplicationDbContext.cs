using Projecten2_TicketingPlatform.Models.Domein;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projecten2_TicketingPlatform.Tests.Data
{
    public class DummyApplicationDbContext
    {
        public Ticket Ticket { get; }
        public DummyApplicationDbContext()
        {
            Ticket= new Ticket("Ticket20", 1, TicketStatus.Aangemaakt, DateTime.Today, "Ik heb een probleem", 1, "bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8", "Jan de technieker", null, null);
        }
    }
}
