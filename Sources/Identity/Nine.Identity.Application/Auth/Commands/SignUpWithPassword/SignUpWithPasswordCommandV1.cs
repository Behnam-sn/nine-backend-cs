using Nine.Identity.Domain.Contracts.Users.ValueObjects;
using Nine.Shared.Application.Abstractions.Messaging;

namespace Nine.Identity.Application.Auth.Commands.SignUpWithPassword;

public record SignUpWithPasswordCommandV1(EmailAddress EmailAddress, PlainPassword Password)
    : ICommand;
