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
            IEnumerable<Contract> contracten = _contractRepository.GetAllByClientId(_userManager.GetUserId(User));

            if (contracten.Any(c=> c.ContractStatus.Equals(ContractStatus.Actief) || c.ContractStatus.Equals(ContractStatus.InBehandeling)))
            {
                return View(new EditViewModel());
            }
            else
            {
                return View(new EditViewModel(contracten.FirstOrDefault()));
            }
        }

        [HttpPost]
        public IActionResult Create(EditViewModel contractVm) {
            if (ModelState.IsValid)
            {
                try
                {
                    //Een klant kan per contracttype maar één contract met de status “in behandeling” of  “actief” hebben 
                    Contract contract = new Contract
                    {
                        StartDatum = contractVm.Startdatum,
                        ContractType = contractVm.ContractType
                    };
                    contract.Doorlooptijd = contract.Doorlooptijd;
                    contract.ClientId = _userManager.GetUserId(User);

                    IEnumerable<Contract> contracten = _contractRepository.GetByStatusByClientId(_userManager.GetUserId(User), new List<ContractStatus> { ContractStatus.Actief, ContractStatus.InBehandeling });

                    if (contracten.Any(c => c.ContractType == contractVm.ContractType))
                    {
                        throw new ArgumentException("Er is al een contract van dit type in behandeling");
                    }
                    else
                    {
                        _contractRepository.Add(contract);
                        _contractRepository.SaveChanges();
                        TempData["Boodschap"] = "Aanmaken contract gelukt!";

                    }
                }
                catch (ArgumentException ae)
                {
                    TempData["Boodschap"] = "Aanmaken ticket mislukt. " + ae.Message;
                }
                return RedirectToAction(nameof(Index));

            }
            return View("Create", contractVm);

        }

    }
}
