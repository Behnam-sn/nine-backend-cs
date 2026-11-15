using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nine.Identity.Domain.Contracts.Users.ValueObjects;

namespace Nine.Identity.Infrastructure.DbContexts.Converters;

public sealed class UserIdValueConverter()
    : ValueConverter<UserId, Guid>(id => id.Value, guid => UserId.From(guid));
