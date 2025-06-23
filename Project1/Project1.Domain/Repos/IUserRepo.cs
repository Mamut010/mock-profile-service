using Project1.Domain.Entities;

namespace Project1.Domain.Repos
{
    public interface IUserRepo
    {
        Task<IEnumerable<User>> GetAll();

        Task<User?> GetById(int id);
    }
}
