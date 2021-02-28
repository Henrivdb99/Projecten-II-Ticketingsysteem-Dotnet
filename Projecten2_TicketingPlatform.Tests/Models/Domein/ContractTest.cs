using Projecten2_TicketingPlatform.Models.Domein;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Projecten2_TicketingPlatform.Tests.Models.Domein
{
    public class ContractTest
    {
        [Fact]
        public void NieuwTicket_CorrectTicket_MaaktTicket()
        {
            //Arrange
            DateTime startDatum = DateTime.Now;
            var contract = new Contract(1, startDatum, 2, startDatum.AddDays(365), "bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8");
            //Assert
            Assert.Equal(1, contract.ContractId);
            Assert.Equal(startDatum, contract.StartDatum);
            Assert.Equal(2, contract.ContractType);
            Assert.Equal(startDatum.AddDays(365), contract.EindDatum);
            Assert.Equal("bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8", contract.ClientId);
        }
    }
}
