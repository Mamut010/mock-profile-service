using Project1.Domain.Entities;
using Project1.Domain.Repos;
using Project1.Infrastructure.Persistence.Contracts;
using Project1.Infrastructure.Persistence.Models;

namespace Project1.Infrastructure.Domain.Repos
{
    public class UserRepo(IUserModelRepo modelRepo) : IUserRepo
    {
        private readonly IUserModelRepo _modelRepo = modelRepo;

        public async Task<IEnumerable<User>> GetAll()
        {
            var models = await _modelRepo.GetAll();
            return models.Select((model) => MapModelToEntity(model));
        }

        public async Task<User?> GetById(int id)
        {
            var model = await _modelRepo.FindOneById(id);
            return model is not null ? MapModelToEntity(model) : null;
        }

        private static User MapModelToEntity(UserModel model)
        {
            return new()
            {
                Id = model.Id,
                Name = model.Name,
                DisplayedName = model.DisplayedName,
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
            };
        }
    }
}
