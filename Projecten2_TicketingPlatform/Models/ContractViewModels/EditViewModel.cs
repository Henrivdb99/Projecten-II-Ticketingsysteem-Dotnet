using Projecten2_TicketingPlatform.Models.Domein;
using System;
using System.ComponentModel.DataAnnotations;

namespace Projecten2_TicketingPlatform.Models.ContractViewModels
{
    public class EditViewModel
    {
        [Required]
        public int ContractType { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Startdatum { get; set; }
        [Required]
        public int Doorlooptijd { get; set; }
        public EditViewModel()
        {
        }
        public EditViewModel(Contract contract):this()
        {
            ContractType = contract.ContractType;
            Startdatum = contract.StartDatum;
            Doorlooptijd = contract.Doorlooptijd;
        }

       
    }
}
