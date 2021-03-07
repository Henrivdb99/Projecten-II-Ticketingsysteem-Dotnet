using Projecten2_TicketingPlatform.Models.Domein;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projecten2_TicketingPlatform.Tests.Data
{
    public class DummyApplicationDbContext
    {
        public Ticket Ticket { get; }
        public Contract Contract1 { get; }
        public Contract Contract2 { get; }

        public DummyApplicationDbContext()
        {
            Ticket= new Ticket("Ticket20", 1, TicketStatus.Aangemaakt, DateTime.Today, "Ik heb een probleem", "1", "bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8", "Jan de technieker", null, null);
            Contract1 = new Contract(DateTime.Today,"1", 2, "bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8", ContractStatus.Actief);
            Contract2 = new Contract(DateTime.Today.AddDays(400), "1", 1, "bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8", ContractStatus.NietActief);

        }
    }
}
