using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten2_TicketingPlatform.Models.Domein
{
    public interface IContractTypeRepository
    {
        IEnumerable<ContractType> GetAll();
        ContractType GetById(int id);
    }
}
