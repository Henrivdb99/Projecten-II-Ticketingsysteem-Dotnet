using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Projecten2_TicketingPlatform.Controllers;
using Projecten2_TicketingPlatform.Models.Domein;
using Projecten2_TicketingPlatform.Tests.Data;
using Xunit;

namespace Projecten2_TicketingPlatform.Tests.Controllers
{
    public class ContractControllerTest
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
            _mockContractRepository.Setup(p => p.GetAllByClientId(1)).Returns(_dummyContext.Contract);
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
    }
}
