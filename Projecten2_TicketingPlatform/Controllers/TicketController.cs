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
            IEnumerable<Ticket> tickets = _ticketRepository.GetAllByClientId('1');
            return View(tickets);
        }
        #region == Create Methodes ==
        public IActionResult Create()
        {
            return View(new EditViewModel());
        }
        [HttpPost]
        public IActionResult Create(EditViewModel ticketVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Ticket ticket = new Ticket
                    {
                        DatumAanmaken = ticketVm.DatumAanmaken,
                        Titel = ticketVm.Titel,
                        Omschrijving = ticketVm.Omschrijving,
                        TypeTicket = ticketVm.TypeTicket,
                        Technieker = ticketVm.Technieker,
                        Opmerkingen = ticketVm.Opmerkingen,
                        Bijlage = ticketVm.Bijlage,
                        KlantId = "1" //!!!!!!!!!!!!!!!!
                    };
                    ticket.Status = TicketStatus.Aangemaakt;
                    ticket.Valideer();
                    _ticketRepository.Add(ticket);
                    _ticketRepository.SaveChanges();
                }
                catch (ArgumentException e)
                {
                    //nada
                }
                return RedirectToAction(nameof(Index)); 
            }
            return View(ticketVm);

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
