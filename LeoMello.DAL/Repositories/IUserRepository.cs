using LeoMello.DAL.Entities;

namespace LeoMello.DAL.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser> GetUserByIdAsync(int id);
        Task<ApplicationUser> GetUserByNameAsync(string username);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
    }
}
