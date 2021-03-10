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
        public string ContractType { get; set; }
        public DateTime EindDatum { get; set; }
        public string ClientId { get; set; }
        //public int Doorlooptijd { get; set; }
        //Zet deze code uit commentaar en het field bovenaar ook. Vergeet niet de property doorlooptijd op lijn 17 in commentaar te plaatsen
        public int Doorlooptijd
        {
            get => _doorlooptijd;
            set
            {
                if (value > 3 || value < 1)
                {
                    throw new ArgumentException("Doorlooptijd moet binnen het domein [1, 3] liggen");
                }
                else
                {
                    _doorlooptijd = value;
                }
            }
        }

        public ContractStatus ContractStatus { get; set;  }

        public Contract() 
        {
        }

        public Contract(DateTime startDatum, string contractType, int doorlooptijd, string clientId, ContractStatus status = ContractStatus.InBehandeling)
        {
            StartDatum = startDatum;
            ContractType = contractType;
            Doorlooptijd = doorlooptijd;
            EindDatum = startDatum.AddYears(doorlooptijd);
            ClientId = clientId;
            ContractStatus = status;
        }

        public void ZetStop()
        {
            this.ContractStatus = ContractStatus.Stopgezet;
        }
    }
}
