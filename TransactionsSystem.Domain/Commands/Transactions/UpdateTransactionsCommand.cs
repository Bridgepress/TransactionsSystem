using MediatR;

namespace TransactionsSystem.Domain.Commands.Transactions
{
    public class UpdateTransactionsCommand : IRequest
    {
        public string FileData { get; set; } = default!;
    }
}
