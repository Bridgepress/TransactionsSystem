using MediatR;

namespace TransactionsSystem.Domain.Commands.Transactions
{
    public record UpdateTransactionCommand
    (
        Guid Id,
        string Status
    ) : IRequest;
}
