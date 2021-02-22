using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten2_TicketingPlatform.Models.Domein
{
    public class Ticket
    {
        public string Titel { get; set; }
        public int Ticketid { get; set; }
        public TicketStatus Status { get; set; }
        public DateTime DatumAanmaken { get; set; }
        public string Omschrijving { get; set; }
        public int TypeTicket { get; set; }
        public string KlantId { get; set; }
        public string Technieker { get; set; }
        public string[] Opmerkingen { get; set; }
        public string Bijlage { get; set; }

        public Ticket(string titel, int ticketId, TicketStatus ticketStatus, DateTime date, string omschrijving, int typeTicket, string klantId, string technieker, string[] opmerkingen, string bijlage)
        {
            Titel = titel;
            Ticketid = ticketId;
            Status = ticketStatus;
            DatumAanmaken = date;
            Omschrijving = omschrijving;
            TypeTicket = typeTicket;
            KlantId = klantId;
            Technieker = technieker;
            Opmerkingen = opmerkingen;
            Bijlage = bijlage;
        }

        //Nice to have
        //public int Waardering { get; set; }
        //public bool ViaKnowledgebase { get; set; }
        //public bool SupportNodig { get; set; }
    }

}
