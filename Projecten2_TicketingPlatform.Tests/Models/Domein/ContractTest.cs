using Projecten2_TicketingPlatform.Models.Domein;
using Projecten2_TicketingPlatform.Tests.Data;
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
            ContractType contract24_7 = new ContractType("Contract24/7", ContractEnContractTypeStatus.Actief, ManierVanAanmakenTicket.EmailEnTelefonischEnApplicatie, TijdstipTicketAanmaken.Altijd, 1, 10, 100);
            int doorlooptijd = 1; //aantal jaren
            var contract = new Contract(startDatum, contract24_7, doorlooptijd, "bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8");
            //Assert
            Assert.Equal(startDatum, contract.StartDatum);
            Assert.Equal("Contract24/7", contract.ContractType.Naam);
            Assert.Equal(startDatum.AddDays(1*365), contract.EindDatum);
            Assert.Equal("bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8", contract.ClientId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(4)]
        public void Nieuw_Contract_FoutDoorlooptijd(int doorlooptijd)
        {
            ContractType contract24_7 = new ContractType("Contract24/7", ContractEnContractTypeStatus.Actief, ManierVanAanmakenTicket.EmailEnTelefonischEnApplicatie, TijdstipTicketAanmaken.Altijd, 1, 10, 100);

            Assert.Throws<ArgumentException>(() => new Contract(DateTime.Now, contract24_7, doorlooptijd, "bff6a934 - 0dca - 4965 - b9fc - 91c3290792c8"));
        }

        [Fact]
        public void StopzettenContract_VerlooptCorrect()
        {
            //Arrange
            Contract contract = new DummyApplicationDbContext().Contract1;
            //Act
            contract.ZetStop();
            //Assert
            Assert.Equal(ContractEnContractTypeStatus.Stopgezet, contract.ContractStatus);
        }

    }
}
