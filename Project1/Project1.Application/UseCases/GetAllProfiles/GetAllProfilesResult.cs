using Project1.Application.Dtos;

namespace Project1.Application.UseCases.GetAllProfiles
{
    public record GetAllProfilesResult(IEnumerable<UserProfileDto> Profiles);
}
