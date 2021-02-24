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
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Datum aanmaken")]
        public DateTime DatumAanmaken { get; set; }

        [Required]
        public string Titel { get; set; }

        [Required]
        public string Omschrijving { get; set; }

        [Required]
        [Range(1, 10)]
        [Display(Name = "Type")]
        public int TypeTicket { get; set; }

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
            /*Technieker = ticket.Technieker;
            Opmerkingen = ticket.Opmerkingen;*/
            Bijlage = ticket.Bijlage;
        }
    }
}
