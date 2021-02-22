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
            ViewData["IsEdit"] = false;
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
                    _ticketRepository.Add(ticket);
                    _ticketRepository.SaveChanges();
                }
                catch (ArgumentException e)
                {
                    //nada
                }
                return RedirectToAction(nameof(Index)); 
            }
            ViewData["IsEdit"] = false;
            return View(ticketVm);

        }
        #endregion

        #region == Edit Methodes ==
        public IActionResult Edit(int ticketId)
        {
            Ticket ticket = _ticketRepository.GetById(ticketId);
            ViewData["IsEdit"] = true;
            return View(new EditViewModel(ticket));
        }
        [HttpPost]
        public IActionResult Edit(int ticketId, EditViewModel ticketVm)
        {
            Ticket ticket = _ticketRepository.GetById(ticketId);
            ticket.EditTicket(ticketVm.DatumAanmaken, ticketVm.Titel, ticketVm.Omschrijving, ticketVm.TypeTicket, ticketVm.Technieker, ticketVm.Opmerkingen, ticketVm.Bijlage, "1"); //!!!!!
            _ticketRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        } 
        #endregion
    }
}
