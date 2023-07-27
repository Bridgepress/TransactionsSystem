namespace TransactionsSystem.Domain.Responses
{
    public record PaginationResponse<T>(int TotalAmount, IEnumerable<T> Data)
    {
        public static PaginationResponse<T> Empty(int totalAmount) => new(totalAmount, Array.Empty<T>());
    }
}
