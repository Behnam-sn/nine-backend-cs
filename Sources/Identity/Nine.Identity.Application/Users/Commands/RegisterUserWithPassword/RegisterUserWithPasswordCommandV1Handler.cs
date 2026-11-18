using Microsoft.AspNetCore.Identity;

using Nine.Identity.Domain.Users.Entities;
using Nine.Identity.Domain.Users.UnitOfWorks;
using Nine.Shared.Application.Abstractions.Messaging;
using Nine.Shared.Domain;

namespace Nine.Identity.Application.Users.Commands.RegisterUserWithPassword;

public sealed class RegisterUserWithPasswordCommandV1Handler : ICommandHandler<RegisterUserWithPasswordCommandV1>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public RegisterUserWithPasswordCommandV1Handler(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task Handle(RegisterUserWithPasswordCommandV1 request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();
        
        var user = new User { UserName = request.EmailAddress.Value };

        var createUserResult = await _userManager.CreateAsync(user, request.Password.Value);

        if (!createUserResult.Succeeded)
        {
            //Todo: throw an exception and return errors
        }

        var addUserRoleResult = await _userManager.AddToRoleAsync(user, Roles.Member);

        if (!addUserRoleResult.Succeeded)
        {
            //Todo: throw an exception and return errors
        }

        await _unitOfWork.CommitTransactionAsync();
    }
}
