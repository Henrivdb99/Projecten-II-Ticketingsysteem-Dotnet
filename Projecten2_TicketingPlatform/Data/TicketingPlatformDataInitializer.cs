using Microsoft.AspNetCore.Identity;
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

                //Nog aan te vullen verdere data later in project.

                _dbcontext.SaveChanges();
            }
        }

        private async Task InitializeUsers()
        {
            string eMailAddress = "klant@hogent.be";
            IdentityUser user = new IdentityUser { UserName = eMailAddress, Email = eMailAddress };
            await _userManager.CreateAsync(user, "P@ssword1");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "klant"));
        }
    }
}
