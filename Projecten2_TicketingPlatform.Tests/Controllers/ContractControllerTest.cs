using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Projecten2_TicketingPlatform.Controllers;
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
    }
}
