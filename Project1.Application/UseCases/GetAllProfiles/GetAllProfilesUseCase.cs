using Project1.Application.Dtos;
using Project1.Domain.Repos;
using Project1.Shared.Contracts;

namespace Project1.Application.UseCases.GetAllProfiles
{
    public class GetAllProfilesUseCase(IUserRepo userRepo) : IQueryUseCase<GetAllProfilesQuery, GetAllProfilesResult>
    {
        private readonly IUserRepo _userRepo = userRepo;

        public async Task<GetAllProfilesResult> ExecuteAsync(GetAllProfilesQuery query)
        {
            var users = await _userRepo.GetAll();
            var profiles = users.Select((user) => new UserProfileDto(
                user.Id,
                user.Name,
                user.DisplayedName,
                user.Email
            ));
            return new GetAllProfilesResult(profiles);
        }
    }
}
