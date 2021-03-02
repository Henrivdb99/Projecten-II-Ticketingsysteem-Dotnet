using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten2_TicketingPlatform.Models.Domein
{
    public class Contract
    {
        private int _doorlooptijd;

        public int ContractId { get; set; }
        public DateTime StartDatum { get; set; }
        public int ContractType { get; set; }
        public DateTime EindDatum { get; set; }
        public string ClientId { get; set; }
        public int Doorlooptijd {
            get => _doorlooptijd;
            set
            {
                if (value < 4 && value > 0)
                    _doorlooptijd = value;
                else
                {
                    throw new ArgumentException("Doorlooptijd moet binnen het domein [1, 3] liggen");
                }
            }
        }

        public ContractStatus ContractStatus { get; set;  }

        public Contract() 
        {
        }

        public Contract(DateTime startDatum, int contractType, int doorlooptijd, string clientId, ContractStatus status = ContractStatus.Actief)
        {
            StartDatum = startDatum;
            ContractType = contractType;
            EindDatum = startDatum.AddYears(doorlooptijd);
            ClientId = clientId;
            ContractStatus = status;
        }
    }
}
