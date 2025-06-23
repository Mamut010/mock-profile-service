using Project1.Application.Dtos;
using Project1.Domain.Repos;
using Project1.Shared.Contracts;

namespace Project1.Application.UseCases.GetProfile
{
    public class GetProfileUseCase(IUserRepo userRepo) : IQueryUseCase<GetProfileQuery, GetProfileResult>
    {
        private readonly IUserRepo _userRepo = userRepo;

        public async Task<GetProfileResult> ExecuteAsync(GetProfileQuery query)
        {
            var userId = query.UserId;
            var user = await _userRepo.GetById(userId);
            if (user is null)
            {
                return new GetProfileResult(null);
            }

            var profile = new UserProfileDto(
                user.Id,
                user.Name,
                user.DisplayedName,
                user.Email
            );
            return new GetProfileResult(profile);
        }
    }
}
