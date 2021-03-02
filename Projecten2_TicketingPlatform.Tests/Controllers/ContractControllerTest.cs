using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Projecten2_TicketingPlatform.Controllers;
using Projecten2_TicketingPlatform.Models.ContractViewModels;
using Projecten2_TicketingPlatform.Models.Domein;
using Projecten2_TicketingPlatform.Tests.Data;
using System.Collections.Generic;
using System.Linq;
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
        private readonly Mock<UserManager<IdentityUser>> _mockUser;
        private readonly List<Contract> _contracts;

        public ContractControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _mockContractRepository = new Mock<IContractRepository>();
            _contract1 = _dummyContext.Contract1;
            _contract2 = _dummyContext.Contract2;
            _mockUser = new Mock<UserManager<IdentityUser>>();
            _contractController = new ContractController(_mockContractRepository.Object, _mockUser.Object);
            _contracts = new List<Contract>() { _contract1, _contract2 };
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

            _mockContractRepository.Setup(p => p.GetAllByClientId("bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8")).Returns(new List<Contract>() { _contract2 });

            var result = Assert.IsType<ViewResult>(_contractController.Create());
            var contractVm = Assert.IsType<EditViewModel>(result.Model);

            Assert.Equal(_contract2.Doorlooptijd, contractVm.Doorlooptijd);
            Assert.Equal(_contract2.Doorlooptijd, contractVm.Doorlooptijd);
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
        public void CreateHttpGet_ActiveContract_PassesNoDetailsOfANewTicketInEditViewModelToView()
        {
            _mockContractRepository.Setup(p => p.GetAllByClientId("bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8")).Returns(_contracts);

            var result = Assert.IsType<ViewResult>(_contractController.Create());
            var contractVm = Assert.IsType<EditViewModel>(result.Model);
            Assert.Equal(0, contractVm.Doorlooptijd);
            _mockContractRepository.Verify(mock => mock.GetAllByClientId("bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8"), Times.Once);

        }

        /*
        [Fact]
        public void CreateHttpPost_ValidTicket_AddsNewTicketToRepositoryAndRedirectsToIndex()
        {
            _mockTicketRepository.Setup(p => p.Add(It.IsNotNull<Ticket>()));
            var ticketVm = new EditViewModel()
            {
                Titel = "Fout2098 Fase7",
                Omschrijving = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                TypeTicket = 1
            };
            var result = Assert.IsType<RedirectToActionResult>(_ticketController.Create(ticketVm));
            Assert.Equal("Index", result.ActionName);
            _mockTicketRepository.Verify(m => m.Add(It.IsNotNull<Ticket>()), Times.Once);
            _mockTicketRepository.Verify(m => m.SaveChanges(), Times.Once);
        }
        [Fact]
        public void CreateHttpPost_InvalidTicket_DoesNotCreateNorPersistsTicketAndRedirectsToActionIndex()
        {
            _mockTicketRepository.Setup(m => m.Add(It.IsAny<Ticket>()));
            var ticketVm = new EditViewModel()
            {
                Titel = null,
                Omschrijving = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                TypeTicket = 1
            };
            var action = Assert.IsType<RedirectToActionResult>(_ticketController.Create(ticketVm));
            Assert.Equal("Index", action?.ActionName);
            _mockTicketRepository.Verify(m => m.SaveChanges(), Times.Never());
            _mockTicketRepository.Verify(m => m.Add(It.IsAny<Ticket>()), Times.Never());
        }
        [Fact]
        public void CreateHttpPost_ModelStateErrors_DoesNotChangeNorPersistTicket()
        {
            var ticketVm = new EditViewModel(_ticket);
            _ticketController.ModelState.AddModelError("", "Any error");
            _ticketController.Create(ticketVm);
            Assert.Equal("Ticket20", ticketVm.Titel);
            Assert.Equal(1, ticketVm.TypeTicket);
            _mockTicketRepository.Verify(m => m.SaveChanges(), Times.Never);
        } */

        #endregion
    }
}
