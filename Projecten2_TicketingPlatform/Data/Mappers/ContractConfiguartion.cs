using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projecten2_TicketingPlatform.Models.Domein;

namespace Projecten2_TicketingPlatform.Data.Mappers
{
    public class ContractConfiguartion : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasKey(c => c.ContractId);

            builder.HasOne(c => c.ContractType).WithMany().IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
