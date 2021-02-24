using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projecten2_TicketingPlatform.Models.Domein;
using Projecten2_TicketingPlatform.Models.TicketViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Projecten2_TicketingPlatform.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public TicketController(ITicketRepository ticketRepository, UserManager<IdentityUser> userManager)
        {
            _ticketRepository = ticketRepository;
            _userManager = userManager;
        }
        
        public IActionResult Index()
        {
            IEnumerable<Ticket> tickets = _ticketRepository.GetAllByClientId(_userManager.GetUserId(User));
            if (tickets.Count() == 0)
            {
                TempData["GeenTickets"] = $"Uw account met ID {_userManager.GetUserId(User)} beschikt niet over tickets";
            }
            return View(tickets);
        }
        #region == Create Methodes ==
        public IActionResult Create()
        {
            ViewData["IsEdit"] = false;
            return View("Edit", new EditViewModel());
        }
        [HttpPost]
        public IActionResult Create(EditViewModel ticketVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Ticket ticket = new Ticket();
                    ticket.EditTicket(ticketVm.DatumAanmaken, ticketVm.Titel, ticketVm.Omschrijving, ticketVm.TypeTicket, ticketVm.Technieker, ticketVm.Opmerkingen, ticketVm.Bijlage, _userManager.GetUserId(User)); //!!!!!
                    ticket.Status = TicketStatus.Aangemaakt;  
                    _ticketRepository.Add(ticket);
                    _ticketRepository.SaveChanges();
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }
                return RedirectToAction(nameof(Index)); 
            }
            ViewData["IsEdit"] = false;
            return View("Edit", ticketVm);

        }
        #endregion

        #region == Edit Methodes ==
        public IActionResult Edit(int ticketId)
        {
            Ticket ticket = _ticketRepository.GetById(ticketId);
            if (ticket == null)
                return new NotFoundResult();
            ViewData["IsEdit"] = true;
            return View(new EditViewModel(ticket));
        }
        [HttpPost]
        public IActionResult Edit(int ticketId, EditViewModel ticketVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Ticket ticket = _ticketRepository.GetById(ticketId);
                    if (ticket == null)
                        return new NotFoundResult();
                    ticket.EditTicket(ticketVm.DatumAanmaken, ticketVm.Titel, ticketVm.Omschrijving, ticketVm.TypeTicket, ticketVm.Technieker, ticketVm.Opmerkingen, ticketVm.Bijlage, _userManager.GetUserId(User)); 
                    _ticketRepository.SaveChanges();
                }
                catch (ArgumentException)
                {
                    //nada
                }
                return RedirectToAction(nameof(Index)); 
            }
            ViewData["IsEdit"] = true;
            return View(ticketVm);
        } 
        #endregion
    }
}
