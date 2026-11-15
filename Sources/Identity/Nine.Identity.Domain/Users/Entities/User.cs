using Microsoft.AspNetCore.Identity;
using Nine.Identity.Domain.Contracts.Users.ValueObjects;

namespace Nine.Identity.Domain.Users.Entities;

public sealed class User : IdentityUser<UserId>
{
    public User()
    {
        Id = UserId.Create();
    }
}
