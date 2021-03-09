using Microsoft.AspNetCore.Identity;
using Projecten2_TicketingPlatform.Models.Domein;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Projecten2_TicketingPlatform.Data
{
    public class TicketingPlatformDataInitializer
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly UserManager<IdentityUser> _userManager;

        public TicketingPlatformDataInitializer(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbcontext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbcontext.Database.EnsureDeleted();
            if (_dbcontext.Database.EnsureCreated())
            {
                await InitializeUsers();

                ////Nog aan te vullen verdere data later in project.
                var tickets = new List<Ticket>
            {
                     new Ticket("TitelTicket4", TicketStatus.Geannuleerd, DateTime.Today.AddDays(-4), "Hoe maak ik een ticket aan?", "1", "210771fc - 21f2 - 47e4 - a902 - 986e2d199105", "Jef de Technieker"),
                     new Ticket("TitelTicket1", TicketStatus.Aangemaakt, DateTime.Today.AddDays(-3), "Blijkbaar heb ik een probleem", "1", "210771fc - 21f2 - 47e4 - a902 - 986e2d199105", "Jef de Technieker"),
                     new Ticket("TitelTicket2", TicketStatus.Aangemaakt, DateTime.Today.AddDays(-2), "Nog steeds hetzelfde probleem", "1", "210771fc - 21f2 - 47e4 - a902 - 986e2d199105", "Jef de Technieker"),
                     new Ticket("TitelTicket3", TicketStatus.Aangemaakt, DateTime.Today.AddDays(-1), "Het gaat niet weg", "1", "210771fc - 21f2 - 47e4 - a902 - 986e2d199105", "Bram de Technieker"),
                     new Ticket("TitelTicket4", TicketStatus.Aangemaakt, DateTime.Today, "Zijn jullie daar?", "1", "210771fc - 21f2 - 47e4 - a902 - 986e2d199105", "Jef de Technieker")
            };

                _dbcontext.Tickets.AddRange(tickets);

                var contractenVoorKlant = new List<Contract>
                {
                    new Contract(DateTime.Today.AddMonths(-13), "type 1", 1, "210771fc - 21f2 - 47e4 - a902 - 986e2d199105",ContractStatus.Afgelopen),
                    new Contract( DateTime.Today.AddMonths(-8), "type 2", 1, "210771fc - 21f2 - 47e4 - a902 - 986e2d199105", ContractStatus.InBehandeling),
                    new Contract(DateTime.Today.AddMonths(-1), "type 1", 2, "210771fc - 21f2 - 47e4 - a902 - 986e2d199105"),
                    new Contract(DateTime.Today.AddMonths(-26), "type 1", 2, "210771fc - 21f2 - 47e4 - a902 - 986e2d199105", ContractStatus.Afgelopen),
                    new Contract(DateTime.Today.AddMonths(-3), "type 1", 2, "210771fc - 21f2 - 47e4 - a902 - 986e2d199105", ContractStatus.Actief),
                    new Contract(DateTime.Today, "type 4", 3, "210771fc - 21f2 - 47e4 - a902 - 986e2d199105"),
                    new Contract(DateTime.Today.AddMonths(-4), "type 3", 3, "210771fc - 21f2 - 47e4 - a902 - 986e2d199105")
                };
                _dbcontext.Contracten.AddRange(contractenVoorKlant);




                _dbcontext.SaveChanges();
            }
        }

        private async Task InitializeUsers()
        {
            string eMailAddress1 = "klant@hogent.be";
            IdentityUser user1 = new IdentityUser { UserName = eMailAddress1, Email = eMailAddress1, Id = "210771fc - 21f2 - 47e4 - a902 - 986e2d199105" };
            await _userManager.CreateAsync(user1, "P@ssword1");
            await _userManager.AddClaimAsync(user1, new Claim(ClaimTypes.Role, "klant"));

            string eMailAddress1a = "klant2@hogent.be";
            IdentityUser user1a = new IdentityUser { UserName = eMailAddress1a, Email = eMailAddress1a, Id = "210771fc - 5d15 - 4e7c - a902 - a3a2fd4e2815" };
            await _userManager.CreateAsync(user1a, "P@ssword1");
            await _userManager.AddClaimAsync(user1a, new Claim(ClaimTypes.Role, "klant"));

            string eMailAddress2 = "technieker@hogent.be";
            IdentityUser user2 = new IdentityUser { UserName = eMailAddress2, Email = eMailAddress2, Id= "ff3bc350-5d15-47da-bb68-69ad26e059ae" };
            await _userManager.CreateAsync(user2, "P@ssword1");
            await _userManager.AddClaimAsync(user2, new Claim(ClaimTypes.Role, "technieker"));

            string eMailAddress3 = "sm@hogent.be";
            IdentityUser user3 = new IdentityUser { UserName = eMailAddress3, Email = eMailAddress3, Id= "f6d6bf42-2c83-4e7c-abbf-f207381286c2" };
            await _userManager.CreateAsync(user3, "P@ssword1");
            await _userManager.AddClaimAsync(user3, new Claim(ClaimTypes.Role, "supportmanager"));

            string eMailAddress4 = "admin@hogent.be";
            IdentityUser user4 = new IdentityUser { UserName = eMailAddress4, Email = eMailAddress4, Id= "9ea3de52 - 78c8 - 4cd5 - 877a - a3a2fd4e2815" };
            await _userManager.CreateAsync(user4, "P@ssword1");
            await _userManager.AddClaimAsync(user4, new Claim(ClaimTypes.Role, "administrator"));
        }
    }
}
