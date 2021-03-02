using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    

        public IActionResult Index(bool toonGeanulleerd = false)
        {
            IEnumerable<Ticket> tickets;
            if (!toonGeanulleerd) {
                tickets = _ticketRepository.GetAllByClientId(_userManager.GetUserId(User));
            } else
            {
                tickets = _ticketRepository.GetAllByClientIdIncludingAnnuled(_userManager.GetUserId(User));
            }
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
            ViewData["TicketType"] = TicketTypesAsSelectList();
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
                    ticket.DatumAanmaken = ticketVm.DatumAanmaken;
                    ticket.Titel = ticketVm.Titel;
                    ticket.Omschrijving = ticketVm.Omschrijving;
                    ticket.TypeTicket = ticketVm.TypeTicket;
                    /*ticketVm.Technieker, ticketVm.Opmerkingen,*/
                    ticket.Bijlage = ticketVm.Bijlage;
                    ticket.KlantId = _userManager.GetUserId(User); //!!!!!
                    ticket.Status = TicketStatus.Aangemaakt;
                    
                    _ticketRepository.Add(ticket);
                    _ticketRepository.SaveChanges();
                    TempData["Boodschap"] = "Aanmaken ticket gelukt!";
                }
                catch (ArgumentException ae)
                {
                    TempData["Boodschap"] = "Aanmaken ticket mislukt. " + ae.Message;
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
            ViewData["TicketType"] = TicketTypesAsSelectList();

            return View(new EditViewModel(ticket));
        }
        [HttpPost]
        public IActionResult Edit(EditViewModel ticketVm, int ticketId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Ticket ticket = _ticketRepository.GetById(ticketId);
                    if (ticket == null)
                        return new NotFoundResult();
                    ticket.DatumAanmaken = ticketVm.DatumAanmaken;
                    ticket.Titel = ticketVm.Titel;
                    ticket.Omschrijving = ticketVm.Omschrijving;
                    ticket.TypeTicket = ticketVm.TypeTicket;
                    /*ticketVm.Technieker, ticketVm.Opmerkingen,*/
                    ticket.Bijlage = ticketVm.Bijlage;
                    //ticket.KlantId = _userManager.GetUserId(User); //niet nodig denk ik, verandert niet
                    ticket.Status = TicketStatus.Aangemaakt; 
                    _ticketRepository.SaveChanges();
                    TempData["Boodschap"] = "Bewerken ticket gelukt!";
                }
                catch (ArgumentException ae)
                {
                    TempData["Boodschap"] = "Bewerken ticket mislukt. " + ae.Message;
                }
                return RedirectToAction(nameof(Index)); 
            }
            ViewData["IsEdit"] = true;
            ViewData["TicketType"] = TicketTypesAsSelectList();
            return View(ticketVm);
        }

        public IActionResult Annuleer(int ticketId) {
            Ticket ticket = _ticketRepository.GetById(ticketId);
            return View(ticket);
        }

        public IActionResult Details (int ticketId)
        {
            Ticket ticket = _ticketRepository.GetById(ticketId);
            return View(ticket);
        }

        [HttpPost] 
        public IActionResult AnnuleerConfirmed(int ticketId)
        {
            Ticket ticket = _ticketRepository.GetById(ticketId);
            ticket.Status = TicketStatus.Geannuleerd;
            _ticketRepository.SaveChanges();
            TempData["Boodschap"] = "Ticket is geannuleerd";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        private SelectList TicketTypesAsSelectList(int selected = 0)
        {
            SelectListItem selListItem = new SelectListItem() { Value = "1", Text = "Productie geïmpacteerd" };
            SelectListItem selListItem2 = new SelectListItem() { Value = "2", Text = "Productie zal binnen een tijd stilvallen" };
            SelectListItem selListItem3 = new SelectListItem() { Value = "3", Text = "Geen productie impact" };

            List<SelectListItem> newList = new List<SelectListItem>
            {
                selListItem,
                selListItem2,
                selListItem3
            };

            return new SelectList(newList, "Value", "Text", selected); ;
        }
    }
}
