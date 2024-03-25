using MediatR;
using TaskManager.Profile.Domain;
using TaskManager.Profile.Domain.dtos;
using TaskManager.Profile.Repository;
using TaskManager.Profile.Repository;
using TaskManager.Profile.Repository.Repository;

namespace TaskManager.Profile.Application.Application
{
    public class UserProfileIdentityRequestHandler : IRequestHandler<UserProfileIdentityRequest, UserProfileIdentityResponse>
    {
        private readonly UserProfileDbContext _dbContext;

        public UserProfileIdentityRequestHandler(UserProfileDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserProfileIdentityResponse> Handle(UserProfileIdentityRequest request, CancellationToken cancellationToken)
        {
            // Fetch the user profile from the database using the provided UserId
            var userProfile = await _dbContext.UserProfiles.FindAsync(request.UserId);

            if (userProfile == null)
            {
                // User profile not found, handle accordingly (throw exception or return null)
                return null;
            }

            // Map the UserProfile entity to the UserProfileDto response
            var userProfileDto = new UserProfileIdentityResponse
            {
                UserId = userProfile.UserId,
                Username = userProfile.Username,
                AvatarUrl = userProfile.AvatarUrl,
                Theme = userProfile.Theme,
                EmailAddress = userProfile.EmailAddress,
                PhoneNumber = userProfile.PhoneNumber,
                NotificationPreferences = userProfile.NotificationPreferences,
                Education = userProfile.Education
            };

            return userProfileDto;
        }
    }
}
