using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten2_TicketingPlatform.Models.Domein
{
    public class ContractType
    {
        public string Naam { get; set; }
        public ContractStatus Status { get; set; }
        public ManierVanAanmakenTicket ManierVanAanmakenTicket { get; set; }
        public TijdstipTicketAanmaken TijdstipTicketAanmaken { get; set; }
        public int MinimaleDoorlooptijd { get; set; }
        public int MaximaleAfhandeltijd { get; set; }
        public double ContractPrijs { get; set; }

        public ContractType(string naam, ContractStatus status, ManierVanAanmakenTicket manierVanAanmakenTicket, TijdstipTicketAanmaken tijdstipTicketAanmaken, int minimaleDoorloopTijd, int maximaleAfhandeltijd, double contractPrijs)
        {
            Naam = naam;
            Status = status;
            ManierVanAanmakenTicket = manierVanAanmakenTicket;
            TijdstipTicketAanmaken = tijdstipTicketAanmaken;
            MinimaleDoorlooptijd = minimaleDoorloopTijd;
            MaximaleAfhandeltijd = maximaleAfhandeltijd;
            ContractPrijs = contractPrijs;
        }
       
    }
}
