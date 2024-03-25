using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Common.Dto;
using TaskManager.Profile.Domain.dtos;

namespace TaskManager.Profile.Domain.dtos;

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
