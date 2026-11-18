using Nine.Identity.Domain.Contracts.Users.ValueObjects;
using Nine.Shared.Application.Abstractions.Messaging;

namespace Nine.Identity.Application.Users.Commands.RegisterUserWithPassword;

public record RegisterUserWithPasswordCommandV1(EmailAddress EmailAddress, PlainPassword Password)
    : ICommand;
