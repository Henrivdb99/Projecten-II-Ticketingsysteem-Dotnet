using Projecten2_TicketingPlatform.Models.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten2_TicketingPlatform.Models.TicketViewModels
{
    public class EditViewModel
    {
        public DateTime DatumAanmaken { get; set; }
        public string Titel { get; set; }
        public string Omschrijving { get; set; }
        public int TypeTicket { get; set; }
        public string Technieker { get; set; }
        public string Opmerkingen { get; set; }
        public string Bijlage { get; set; }

        public EditViewModel()
        {
        }

        public EditViewModel(Ticket ticket) : this()
        {
            DatumAanmaken = ticket.DatumAanmaken;
            Titel = ticket.Titel;
            Omschrijving = ticket.Omschrijving;
            TypeTicket = ticket.TypeTicket;
            Technieker = ticket.Technieker;
            Opmerkingen = ticket.Opmerkingen;
            Bijlage = ticket.Bijlage;
        }
    }
}
