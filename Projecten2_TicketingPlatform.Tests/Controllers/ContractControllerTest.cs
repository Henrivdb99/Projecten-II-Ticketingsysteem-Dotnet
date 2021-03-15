using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Projecten2_TicketingPlatform.Controllers;
using Projecten2_TicketingPlatform.Models.ContractViewModels;
using Projecten2_TicketingPlatform.Models.Domein;
using Projecten2_TicketingPlatform.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using Xunit;

namespace Projecten2_TicketingPlatform.Tests.Controllers
{
    public class ContractControllerTest
    {
        private readonly ContractController _contractController;
        private readonly Mock<IContractRepository> _mockContractRepository;
        private readonly Contract _contract1;
        private readonly Contract _contract2;
        private readonly DummyApplicationDbContext _dummyContext;
        private readonly List<Contract> _contracts;
        private readonly Mock<UserManager<IdentityUser>> _mockUserManager;

        public static readonly string USERID = "bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8";

        public ContractControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _contract1 = _dummyContext.Contract1;
            _contract2 = _dummyContext.Contract2;

            var store = new Mock<IUserStore<IdentityUser>>();
            store.Setup(x => x.FindByIdAsync("123", CancellationToken.None))
                .ReturnsAsync(new IdentityUser()
                {
                    UserName = "test@email.com",
                    Id = "123"
                });

            _mockUserManager = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
            
            _mockContractRepository = new Mock<IContractRepository>();
            _contracts = new List<Contract>() { _contract1, _contract2 };

            _contractController = new ContractController(_mockContractRepository.Object, _mockUserManager.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object
            };
        }
        #region == Index Methodes ==

        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfContracts()
        {
            _mockContractRepository.Setup(r => r.GetAllByClientId("bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8"))
                .Returns(_contracts);
            var result = _contractController.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Contract>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Index_NoContracts_ReturnsNoContracts()
        {
            _mockContractRepository.Setup(r => r.GetAllByClientId("bff6a934 - 0dca - 4965 - b9fc - onbestaande id"))
                .Returns(_contracts);
            var result = _contractController.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Contract>>(
                viewResult.ViewData.Model);
            Assert.Empty(model);
        } 
        #endregion

        #region == Create Methodes ==
        [Fact]
        public void CreateHttpGet_Contract_NoActiveContracts_PassesDetailsOfANewContractInEditViewModelToView()
        {
            _mockUserManager.Setup(u => u.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(USERID);
            _mockContractRepository.Setup(p => p.GetAllByClientId(USERID)).Returns(new List<Contract>() { _contract2 });

            var result = Assert.IsType<ViewResult>(_contractController.Create());
            var contractVm = Assert.IsType<EditViewModel>(result.Model);

            Assert.Equal(_contract2.Doorlooptijd, contractVm.Doorlooptijd);
            _mockContractRepository.Verify(mock => mock.GetAllByClientId("bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8"), Times.Once);
        }
        [Fact]
        public void CreateHttpGet_ActiveContract_PassesNoDetailsOfANewTicketInEditViewModelToView()
        {
            _mockContractRepository.Setup(p => p.GetAllByClientId("bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8")).Returns(_contracts);

            var result = Assert.IsType<ViewResult>(_contractController.Create());
            var contractVm = Assert.IsType<EditViewModel>(result.Model);
            Assert.Equal(0, contractVm.Doorlooptijd);
            _mockContractRepository.Verify(mock => mock.GetAllByClientId("bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8"), Times.Once);

        }


        
        [Fact]
        public void CreateHttpPost_ValidTicket_AddsNewContractToRepositoryAndRedirectsToIndex()
        {
            _mockContractRepository.Setup(p => p.Add(It.IsNotNull<Contract>()));
            _mockContractRepository.Setup(p => p.GetByStatusByClientId(It.IsNotNull<string>(), new List<ContractStatus> { ContractStatus.Actief, ContractStatus.InBehandeling }))
                .Returns(_contracts);
            var contractVm = new EditViewModel()
            {
                Startdatum = DateTime.Today,
                ContractType = "3",
                Doorlooptijd = 2
            };
            var result = Assert.IsType<RedirectToActionResult>(_contractController.Create(contractVm));
            Assert.Equal("Index", result.ActionName);
            _mockContractRepository.Verify(m => m.Add(It.IsNotNull<Contract>()), Times.Once);
            _mockContractRepository.Verify(m => m.GetByStatusByClientId(It.IsNotNull<string>(), new List<ContractStatus> { ContractStatus.Actief, ContractStatus.InBehandeling }), Times.Once);
            _mockContractRepository.Verify(m => m.SaveChanges(), Times.Once);
        }

        
        [Fact]
        public void CreateHttpPost_InvalidContract_DoesNotCreateNorPersistsContractAndRedirectsToActionIndex()
        {
            _mockContractRepository.Setup(p => p.Add(It.IsNotNull<Contract>()));
            _mockContractRepository.Setup(p => p.GetByStatusByClientId(It.IsNotNull<string>(), new List<ContractStatus> { ContractStatus.Actief, ContractStatus.InBehandeling }))
                .Returns(_contracts);
            var contractVm = new EditViewModel()
            {
                Startdatum = DateTime.Today,
                ContractType = "3",
                Doorlooptijd = 20000 //fout
            };
            var result = Assert.IsType<RedirectToActionResult>(_contractController.Create(contractVm));
            Assert.Equal("Index", result.ActionName);
            _mockContractRepository.Verify(m => m.Add(It.IsNotNull<Contract>()), Times.Never);
            _mockContractRepository.Verify(m => m.SaveChanges(), Times.Never);
        }

        
        [Fact]
        public void CreateHttpPost_ModelStateErrors_DoesNotChangeNorPersistContract()
        {
            var contractVm = new EditViewModel(_contract1);
            _contractController.ModelState.AddModelError("", "Any error");

            _contractController.Create(contractVm);

            Assert.Equal(_contract1.ContractType, contractVm.ContractType);
            Assert.Equal(_contract1.StartDatum, contractVm.Startdatum);
            Assert.Equal(_contract1.Doorlooptijd, contractVm.Doorlooptijd);
            _mockContractRepository.Verify(m => m.Add(It.IsNotNull<Contract>()), Times.Never);
            _mockContractRepository.Verify(m => m.SaveChanges(), Times.Never);
        }

        [Fact]
        public void CreateHttpPost_DomainErrors_AlreadyAnActiveContractsSameType_DoesNotPersistContract()
        {
            //dit zorgt ervoor dat er geen contracten van type1 mogen gemaakt worden
            _contracts.Add(new Contract(DateTime.Now, "1", 1, "bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8", ContractStatus.InBehandeling));

            _mockContractRepository.Setup(p => p.Add(It.IsNotNull<Contract>()));
            _mockContractRepository.Setup(p => p.GetByStatusByClientId(It.IsNotNull<string>(), new List<ContractStatus> { ContractStatus.Actief, ContractStatus.InBehandeling }))
                .Returns(_contracts);
            var contractVm = new EditViewModel()
            {
                Startdatum = DateTime.Today,
                ContractType = "1",
                Doorlooptijd = 2
            };
            var result = Assert.IsType<RedirectToActionResult>(_contractController.Create(contractVm));
            Assert.Equal("Index", result.ActionName);
            _mockContractRepository.Verify(m => m.Add(It.IsNotNull<Contract>()), Times.Never);
            _mockContractRepository.Verify(m => m.GetByStatusByClientId(It.IsNotNull<string>(), new List<ContractStatus> { ContractStatus.Actief, ContractStatus.InBehandeling }), Times.Once);
            _mockContractRepository.Verify(m => m.SaveChanges(), Times.Never);
        }

        #endregion
    }
}
