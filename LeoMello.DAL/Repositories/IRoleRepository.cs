using LeoMello.DAL.Entities;

namespace LeoMello.DAL.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<ApplicationRole>> GetAllAsync();
        Task<ApplicationRole> GetRoleAsync(string role);
    }
}
