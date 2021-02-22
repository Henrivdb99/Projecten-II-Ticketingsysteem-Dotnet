using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten2_TicketingPlatform.Models.Domein
{
    public class Ticket
    {
        public string Titel { 
            get => Titel;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Ticket moet een titel bevatten");
                Titel = value;
            }
        }
        public int Ticketid { get; set; }
        public TicketStatus Status { get; set; }
        public DateTime DatumAanmaken {
            get => DatumAanmaken;
            set
            {
                if (value == null)
                    throw new ArgumentException("Datum mag niet null zijn");
                DatumAanmaken = value;
            }
        }
        public string Omschrijving {
            get => Omschrijving;
            set
            {
                if (value == null)
                    throw new ArgumentException("Omschrijving mag niet null zijn");
                Omschrijving = value;
            }
        }
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
        }

        public void EditTicket(DateTime date, string titel, string omschrijving, int typeTicket, string technieker, string opmerkingen, string bijlage, string klantId) {
            DatumAanmaken = date;
            Titel = titel;
            Omschrijving = omschrijving;
            TypeTicket = typeTicket;
            Technieker = technieker;
            Opmerkingen = opmerkingen;
            Bijlage = bijlage;
            KlantId = klantId;    
        }


        //Nice to have
        //public int Waardering { get; set; }
        //public bool ViaKnowledgebase { get; set; }
        //public bool SupportNodig { get; set; }
    }

}
