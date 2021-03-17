using Projecten2_TicketingPlatform.Models.Domein;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projecten2_TicketingPlatform.Tests.Data
{
    public class DummyApplicationDbContext
    {
        public Ticket Ticket { get; }
        public Contract ContractActief { get; }
        public Contract ContractNietActief { get; }
        public Contract ContractAfgelopen { get; }
        public ContractType Contract24_7 { get; set; }
        public ContractType ContractWerkuren { get; set; }

        public DummyApplicationDbContext()
        {
            Contract24_7 = new ContractType("Contract24/7", ContractEnContractTypeStatus.Actief, ManierVanAanmakenTicket.Applicatie, TijdstipTicketAanmaken.Altijd, 1, 10, 100);
            ContractWerkuren = new ContractType("ContractWerkuren", ContractEnContractTypeStatus.Actief, ManierVanAanmakenTicket.Applicatie, TijdstipTicketAanmaken.TijdensWerkdagen, 1, 10, 100);

            Ticket = new Ticket("Ticket20", TicketStatus.Aangemaakt, DateTime.Today, "Ik heb een probleem", "1", "bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8", "Jan de technieker", null, null);
            ContractActief = new Contract(DateTime.Today, Contract24_7, 2, "bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8", ContractEnContractTypeStatus.Actief);
            ContractNietActief = new Contract(DateTime.Today.AddDays(400), Contract24_7, 1, "bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8", ContractEnContractTypeStatus.NietActief);
            ContractAfgelopen = new Contract(DateTime.Today.AddDays(-300), Contract24_7, 1, "bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8", ContractEnContractTypeStatus.Afgelopen);
        }
    }
}
