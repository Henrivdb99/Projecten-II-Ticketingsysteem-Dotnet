using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten2_TicketingPlatform.Models.Domein
{
    public interface ITicketRepository
    {
        Ticket GetById(int ticketId);
        IEnumerable<Ticket> GetAllByClientId(int klantId);
        void Add(Ticket ticket);
        void Delete(Ticket ticket);
        void SaveChanges();
    }
}
