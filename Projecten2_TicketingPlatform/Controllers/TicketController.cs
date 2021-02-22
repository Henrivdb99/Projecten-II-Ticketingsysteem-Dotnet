using Microsoft.AspNetCore.Mvc;
using Projecten2_TicketingPlatform.Models.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten2_TicketingPlatform.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<Ticket> tickets = null;
            return View(tickets);
            throw new NotImplementedException();
        }
    }
}
