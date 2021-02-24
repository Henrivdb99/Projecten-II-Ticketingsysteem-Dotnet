using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten2_TicketingPlatform.Models.Domein
{
    public class Ticket
    {
        private string _titel;

        public string Titel
        {
            get => _titel;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Ticket must have a name");
                _titel = value;
            }
        }

        public int Ticketid { get; set; }
        public TicketStatus Status { get; set; }
        public DateTime DatumAanmaken { get; set; }
        public string Omschrijving { get; set; }
        public int TypeTicket { get; set; }
        public string KlantId { get; set; }
        public string Technieker { get; set; }
        public string Opmerkingen { get; set; }
        public string Bijlage { get; set; }

        public Ticket()
        {

        }
        public Ticket(string titel, int ticketId, TicketStatus ticketStatus, DateTime date, string omschrijving, int typeTicket, string klantId, string technieker, string opmerkingen, string bijlage)
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
            Valideer();
        }

        public void EditTicket(DateTime date, string titel, string omschrijving, int typeTicket, string technieker, string opmerkingen, string bijlage, string klantId)
        {
            DatumAanmaken = date;
            Titel = titel;
            Omschrijving = omschrijving;
            TypeTicket = typeTicket;
            Technieker = technieker;
            Opmerkingen = opmerkingen;
            Bijlage = bijlage;
            KlantId = klantId;
            Valideer();
        }

        public void Valideer()
        {
            if (Titel == null || DatumAanmaken == null || Omschrijving == null || TypeTicket == null)
                throw new ArgumentException("Datum aanmaken, titel, omschrijving en type zijn verplicht.");
        }

        //Nice to have
        //public int Waardering { get; set; }
        //public bool ViaKnowledgebase { get; set; }
        //public bool SupportNodig { get; set; }
    }

}
