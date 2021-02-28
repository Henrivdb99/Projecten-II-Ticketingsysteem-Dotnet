using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten2_TicketingPlatform.Models.Domein
{
    public class Contract
    {    
        public int ContractId { get; set; }
        public DateTime StartDatum { get; set; }
        public int ContractType { get; set; }
        public DateTime EindDatum { get; set; }
        public string ClientId { get; set; }

        public ContractStatus ContractStatus { get; set;  }

        public Contract() 
        {
        }

        public Contract(int contractId, DateTime startDatum, int contractType, DateTime eindDatum, string clientId)
        {
            ContractId = contractId;
            StartDatum = startDatum;
            ContractType = contractType;
            EindDatum = eindDatum;
            ClientId = clientId;
        }
    }
}
