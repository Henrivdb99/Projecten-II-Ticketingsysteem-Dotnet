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
        private readonly DbSet<Contract> _contracten;

        public ContractRepository(ApplicationDbContext context)
        {
            _context = context;
            _contracten = context.Contract;
        }

        public void Add(Contract contract)
        {
            _contracten.Add(contract);
        }

        public IEnumerable<Contract> GetAllByClientId(string klantId)
        {
            return _contracten.Where(t => t.ClientId.Equals(klantId)).OrderByDescending(c => c.StartDatum).AsNoTracking().ToList();
        }

        public Contract GetById(int contractId)
        {
            return _contracten.SingleOrDefault(c => c.ContractId == contractId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
