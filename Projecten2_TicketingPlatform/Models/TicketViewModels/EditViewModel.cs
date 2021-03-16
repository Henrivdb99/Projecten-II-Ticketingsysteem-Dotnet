using Projecten2_TicketingPlatform.Models.Domein;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten2_TicketingPlatform.Models.TicketViewModels
{
    public class EditViewModel
    {
        public string KlantId { get; set; }

        [Required(ErrorMessage = "U moet een startdatum kiezen.")]
        [DataType(DataType.Date)]
        [Display(Name = "Datum aanmaken")]
        public DateTime DatumAanmaken { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "U moet een titel ingeven.")]
        public string Titel { get; set; }

        [Required(ErrorMessage = "U moet een omschrijving ingeven.")]
        public string Omschrijving { get; set; }

        [Required(ErrorMessage = "U moet een type kiezen.")]
        [Display(Name = "Type")]
        public int TypeTicket { get; set; }

        public string Bijlage { get; set; }

        public EditViewModel()
        {
        }

        public EditViewModel(Ticket ticket) : this()
        {
            KlantId = ticket.KlantId;
            DatumAanmaken = ticket.DatumAanmaken;
            Titel = ticket.Titel;
            Omschrijving = ticket.Omschrijving;
            TypeTicket = ticket.TypeTicket;
            /*Technieker = ticket.Technieker;
            Opmerkingen = ticket.Opmerkingen;*/
            Bijlage = ticket.Bijlage;
        }
    }
}
