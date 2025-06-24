using Microsoft.AspNetCore.Mvc;
using Project1.Application.Contracts;
using Project1.Application.UseCases.GetAllProfiles;
using Project1.Application.UseCases.GetProfile;

namespace Project1.Presentation.Controllers
{
    [Route("api/profiles")]
    [ApiController]
    public class ProfileController(
        ICurrentUser currentUser,
        GetProfileUseCase getProfileUseCase,
        GetAllProfilesUseCase getAllProfilesUseCase
        ) : ControllerBase
    {
        private readonly ICurrentUser _currentUser = currentUser;
        private readonly GetProfileUseCase _getProfileUseCase = getProfileUseCase;
        private readonly GetAllProfilesUseCase _getAllProfilesUseCase = getAllProfilesUseCase;

        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            var query = new GetAllProfilesQuery();
            var result = await _getAllProfilesUseCase.ExecuteAsync(query);
            return Ok(result.Profiles);
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUserProfile()
        {
            if (_currentUser.UserId is null)
            {
                throw new UnauthorizedAccessException();
            }

            var query = new GetProfileQuery((int)_currentUser.UserId);
            var result = await _getProfileUseCase.ExecuteAsync(query);
            var profile = result.Profile;
            return profile is not null ? Ok(profile) : NotFound();
        }
    }
}
