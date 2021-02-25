using Microsoft.EntityFrameworkCore;
using Projecten2_TicketingPlatform.Models.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten2_TicketingPlatform.Data.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Contract> _contract;

        public ContractRepository(ApplicationDbContext context)
        {
            _context = context;
            _contract = context.Contract;
        }         

        IEnumerable<Contract> IContractRepository.GetAllByClientId(string contractId)
        {
            throw new NotImplementedException();
        }

        Contract IContractRepository.GetById(int contractId)
        {
            throw new NotImplementedException();
        }
    }
}
