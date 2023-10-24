using LeoMello.DAL.Entities;

namespace LeoMello.DAL.Repositories
{
    public interface IUserClaimRepository
    {
        Task<ApplicationUserClaim> GetAsync(string userId);
    }
}
