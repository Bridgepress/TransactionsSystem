using MediatR;
using TransactionsSystem.Domain.Responses.Authentication;

namespace TransactionsSystem.Domain.Commands.Authentication
{
    public record LoginUserCommand
    (
        string UserName,
        string Password
    ) : IRequest<LoginUserResponse>;
}
