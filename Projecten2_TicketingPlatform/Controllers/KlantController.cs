﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projecten2_TicketingPlatform.Models;

namespace Projecten2_TicketingPlatform.Controllers
{
    public class KlantController : Controller
    {
        public IActionResult Dashbord()
        {
            return View();
        }
    }
}
