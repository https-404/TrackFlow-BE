using TrackFlow.Domain.Entities;
using System.Threading.Tasks;

namespace TrackFlow.Domain.Interfaces.IRepository
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        Task<bool> ExistsByEmailAsync(string email);
    }
}