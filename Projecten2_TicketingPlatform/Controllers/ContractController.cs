using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projecten2_TicketingPlatform.Models.ContractViewModels;
using Projecten2_TicketingPlatform.Models.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten2_TicketingPlatform.Controllers
{
    public class ContractController : Controller
    {
        private readonly IContractRepository _contractRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public ContractController(IContractRepository contractRepository, UserManager<IdentityUser> userManager)
        {
            _contractRepository = contractRepository;
            _userManager = userManager;
        }
        public IActionResult Index(ContractStatus contractStatus = ContractStatus.Standaard)
        {
            IEnumerable<Contract> contracten;
            if (contractStatus== ContractStatus.Standaard)
            {
                contracten = _contractRepository.GetByStatusByClientId(_userManager.GetUserId(User), new List<ContractStatus> {ContractStatus.Actief, ContractStatus.InBehandeling } );
            }
            else
            {
                contracten = _contractRepository.GetByStatusByClientId(_userManager.GetUserId(User), new List<ContractStatus> { contractStatus });
            }
            ViewData["ContractStatussen"] = new SelectList(new List<ContractStatus> { ContractStatus.Actief, ContractStatus.Afgelopen, ContractStatus.InBehandeling, ContractStatus.NietActief, ContractStatus.Stopgezet });       
            return View(contracten);
        }

        public IActionResult Create()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Create(EditViewModel viewModel) {
            throw new NotImplementedException();
        }

    }
}
