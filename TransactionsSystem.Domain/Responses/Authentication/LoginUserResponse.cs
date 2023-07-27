namespace TransactionsSystem.Domain.Responses.Authentication
{
    public record LoginUserResponse(
        Guid Id,
        string UserName,
        string AccessToken, string RefreshToken);
}
