using Project1.Infrastructure.Persistence.Models;

namespace Project1.Infrastructure.Persistence.Contracts
{
    public interface IUserModelRepo
    {
        Task<IEnumerable<UserModel>> GetAll();

        Task<UserModel?> FindOneById(int id);
    }
}
