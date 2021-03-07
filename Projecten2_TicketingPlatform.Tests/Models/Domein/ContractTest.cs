﻿using Projecten2_TicketingPlatform.Models.Domein;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Projecten2_TicketingPlatform.Tests.Models.Domein
{
    public class ContractTest
    {
        [Fact]
        public void Nieuw_Contract_CorrectContract_MaaktContract()
        {
            //Arrange
            DateTime startDatum = DateTime.Now;
            int doorlooptijd = 1; //aantal jaren
            var contract = new Contract(startDatum, 1, doorlooptijd, "bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8");
            //Assert
            Assert.Equal(1, contract.ContractId);
            Assert.Equal(startDatum, contract.StartDatum);
            Assert.Equal(2, contract.ContractType);
            Assert.Equal(startDatum.AddDays(1*365), contract.EindDatum);
            Assert.Equal("bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8", contract.ClientId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(4)]
        public void Nieuw_Contract_FoutDoorlooptijd(int doorlooptijd)
        {
            Assert.Throws<ArgumentException>(() => new Contract(DateTime.Now, doorlooptijd, doorlooptijd, "bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8"));
        }

    }
}
