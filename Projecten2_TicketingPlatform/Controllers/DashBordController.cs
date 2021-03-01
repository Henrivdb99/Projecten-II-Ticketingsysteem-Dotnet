using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projecten2_TicketingPlatform.Models;
using Microsoft.AspNetCore.Authorization;

namespace Projecten2_TicketingPlatform.Controllers
{
    public class DashbordController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
