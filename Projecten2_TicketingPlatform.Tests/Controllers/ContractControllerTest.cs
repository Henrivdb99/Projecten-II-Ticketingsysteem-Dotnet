using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Projecten2_TicketingPlatform.Models.Domein;
using Projecten2_TicketingPlatform.Tests.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using Xunit;

namespace Projecten2_TicketingPlatform.Tests.Controllers
{
    class ContractControllerTest
    {
        private readonly ContractController _contractController;
        private readonly Mock<IContractRepository> _mockContractRepository;
        private readonly Contract _contract;
        private readonly DummyApplicationDbContext _dummyContext;
        private readonly Mock<UserManager<IdentityUser>> _mockUser;

        public ContractControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _mockContractRepository = new Mock<IContractRepository>();
            _mockContractRepository.Setup(p => p.GetAllByClientIdIncludingAnnuled(1)).Returns(_dummyContext.Contract);
            _contract = _dummyContext.Contract;
            _mockUser = new Mock<UserManager<IdentityUser>>();
            _contractController = new ContractController(_mockContractRepository.Object, _mockUser.Object);
        }

        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfContracts()
        {
            _mockContractRepository.Setup(r => r.GetAllByClientId(_mockUser.GetUserId(User)))
                .Returns(GetTestContracts());
            
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ContractViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        private List<Contract> GetTestContracts()
        {
            var contracts = new List<Contract>();
            contracts.Add(new Contract()
            {
                StartDatum = DateTime.Today,
                ContractType = 1,
                EindDatum = DateTime.Today.AddDays(14)
            });
            contracts.Add(new Contract()
            {
                StartDatum = DateTime.Today,
                ContractType = 2,
                EindDatum = DateTime.Today.AddDays(21)
            });
            return contracts;
        }
    }
}
