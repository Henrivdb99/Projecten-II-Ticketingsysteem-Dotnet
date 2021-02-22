using Projecten2_TicketingPlatform.Models.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten2_TicketingPlatform.Models.TicketViewModels
{
    public class EditViewModel
    {
        public DateTime DatumAanmaken;
        public string Titel;
        public string Omschrijving;
        public int TypeTicket;
        public string Technieker;
        public string Opmerkingen;
        public string Bijlage;

        public EditViewModel()
        {
        }

        public EditViewModel(Ticket ticket)
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
