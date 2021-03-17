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
        public IActionResult Index(ContractEnContractTypeStatus contractStatus = ContractEnContractTypeStatus.Standaard)
        {
            IEnumerable<Contract> contracten;
            if (contractStatus== ContractEnContractTypeStatus.Standaard)
            {
                contracten = _contractRepository.GetByStatusByClientId(_userManager.GetUserId(User), new List<ContractEnContractTypeStatus> { ContractEnContractTypeStatus.Actief, ContractEnContractTypeStatus.InBehandeling } );
            }else
            if (contractStatus == ContractEnContractTypeStatus.Alle)
            {
                contracten = _contractRepository.GetAllByClientId(_userManager.GetUserId(User));
            }
            else
            {
                contracten = _contractRepository.GetByStatusByClientId(_userManager.GetUserId(User), new List<ContractEnContractTypeStatus> { contractStatus });
            }
            if (contracten.Count() == 0)
            {
                TempData["Waarschuwing"] = $"Uw account beschikt niet over contracten met status {contractStatus.GetDisplayAttributeFrom(typeof(ContractEnContractTypeStatus))}";
            }
            ViewData["ContractStatussen"] = new SelectList(new List<ContractEnContractTypeStatus> { ContractEnContractTypeStatus.Alle, ContractEnContractTypeStatus.Standaard, ContractEnContractTypeStatus.Actief, ContractEnContractTypeStatus.Afgelopen, ContractEnContractTypeStatus.InBehandeling, ContractEnContractTypeStatus.NietActief, ContractEnContractTypeStatus.Stopgezet });       
            return View(contracten);
        }

        public IActionResult Create()
        {
            IEnumerable<Contract> contracten = _contractRepository.GetAllByClientId(_userManager.GetUserId(User));
            IEnumerable<Contract> afgelopenContracten = contracten.Where(c => c.ContractStatus.Equals(ContractEnContractTypeStatus.Afgelopen));

            //als er al een contract
            if (contracten.Any(c=> c.ContractStatus.Equals(ContractEnContractTypeStatus.Actief) || c.ContractStatus.Equals(ContractEnContractTypeStatus.InBehandeling) ) || contracten.Count() == 0 || afgelopenContracten.Count() == 0)
            {
                return View(new EditViewModel());
            }
            else
            {   //Gegevens oude laatst afgelopen contract ophalen voor nieuw contract vooraf in te vullen.
                return View(new EditViewModel(afgelopenContracten.Last()));
            }
        }

        [HttpPost]
        public IActionResult Create(EditViewModel contractVm) {
            if (ModelState.IsValid)
            {
                try
                {
                    //Een klant kan per contracttype maar één contract met de status “in behandeling” of  “actief” hebben 
                    Contract contract = new Contract(contractVm.Startdatum, contractVm.ContractType, contractVm.Doorlooptijd, _userManager.GetUserId(User));

                    IEnumerable<Contract> contracten = _contractRepository.GetByStatusByClientId(_userManager.GetUserId(User), new List<ContractEnContractTypeStatus> { ContractEnContractTypeStatus.Actief, ContractEnContractTypeStatus.InBehandeling });

                    if (contracten.Any(c => c.ContractType.Equals(contractVm.ContractType)))
                    {
                        throw new ArgumentException("Er is al een contract van dit type in behandeling");
                    }
                    else
                    {
                        _contractRepository.Add(contract);
                        _contractRepository.SaveChanges();
                        TempData["Succes"] = "Aanvragen contract gelukt!";

                    }
                }
                catch (ArgumentException ae)
                {
                    TempData["FoutMelding"] = "Aanvragen contract mislukt. " + ae.Message;
                }
                return RedirectToAction(nameof(Index));

            }
            return View("Create", contractVm);

        }


        #region == Delete Methodes ==
        public IActionResult Annuleer(int contractId)
        {
            Contract contract = _contractRepository.GetById(contractId);
            //if (contract.ContractStatus.Equals(ContractStatus.Stopgezet))
            //{
            //    TempData["Boodschap"] = "Dit contract is reeds stopgezet";
            //    return RedirectToAction(nameof(Index));
            //}
            return View(contract);
        }

        [HttpPost]
        public IActionResult AnnuleerConfirmed(int contractId)
        {
            Contract contract = _contractRepository.GetById(contractId);
            contract.ZetStop();
            _contractRepository.SaveChanges();
            TempData["Succes"] = "Contract is stopgezet";
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
