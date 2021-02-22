using Microsoft.AspNetCore.Mvc;
using Projecten2_TicketingPlatform.Models.Domein;
using Projecten2_TicketingPlatform.Models.TicketViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten2_TicketingPlatform.Controllers
{
    public class TicketController : Controller
    {
        private ITicketRepository _ticketRepository;

        public TicketController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Ticket> tickets = null;
            return View(tickets);
            throw new NotImplementedException();
        }
        #region == Create Methodes ==
        public IActionResult Create()
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public IActionResult Create(EditViewModel ticketVm)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region == Edit Methodes ==
        public IActionResult Edit(int ticketId)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public IActionResult Edit(int ticketId, EditViewModel editViewModel)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
