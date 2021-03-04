using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten2_TicketingPlatform
{
    public enum TicketStatus
    {
        Aangemaakt,
        [Display(Name ="In behandeling")]
        InBehandeling,
        Afgehandeld,
        Geannuleerd,
        [Display(Name = "Wachten op informatie klant")]
        WachtenOpInformatieKlant,
        [Display(Name = "Informatie klant ontvangen")]
        InformatieKlantOntvangen,
        [Display(Name = "In development")]
        InDevelopment,
        Standaard,
        [Display(Name = "Alle tickets")]
        Alle
    }
}
