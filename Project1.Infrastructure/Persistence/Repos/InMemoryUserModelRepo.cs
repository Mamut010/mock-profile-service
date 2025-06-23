using Project1.Infrastructure.Persistence.Contracts;
using Project1.Infrastructure.Persistence.Models;

namespace Project1.Infrastructure.Persistence.Repos
{
    public class InMemoryUserModelRepo : IUserModelRepo
    {
        private static readonly Dictionary<int, UserModel> _users = new()
        {
            // In practice, you would not hard-code user data like this. Moreoever, passwords should be hashed and not stored in plain text.
            {
                1,
                new UserModel() { Id = 1, Name = "user1", DisplayedName = "User 1", Username = "username1", Password = "password1", Email = "user1@example.com" }
            },
            {
                2,
                new UserModel() { Id = 2, Name = "user2", Username = "username2", Password = "password2", Email = "user2@example.com" }
            },
            {
                3,
                new UserModel() { Id = 3, Name = "user3", DisplayedName = "User 3", Username = "username3", Password = "password3" }
            }
        };

        public Task<IEnumerable<UserModel>> GetAll()
        {
            return Task.FromResult(_users.Values.AsEnumerable());
        }

        public Task<UserModel?> FindOneById(int id)
        {
            return Task.FromResult(_users.GetValueOrDefault(id));
        }
    }
}
