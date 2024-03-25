using System;
using TaskManager.Common.Application;
using TaskManager.Common.Dto;

namespace TaskManager.Profile.Domain.Dtos
{
    public class UserProfileIdentityRequest : ISvcCommand<UserProfileIdentityResponse>
    {
        public string UserId { get; set; }
    }

    public class UserProfileIdentityResponse
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string AvatarUrl { get; set; }
        public string Theme { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string NotificationPreferences { get; set; }
        public string Education { get; set; }
    }
}
