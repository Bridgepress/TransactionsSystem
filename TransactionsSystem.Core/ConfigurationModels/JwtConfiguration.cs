namespace TransactionsSystem.Core.ConfigurationModels
{
    public class JwtConfiguration
    {
        public string Section { get; init; } = "JwtSettings";
        public string ValidIssuer { get; set; } = default!;
        public string ValidAudience { get; set; } = default!;
        public string Expires { get; set; } = default!;
        public string TokenKey { get; set; } = default!;
        public int RefreshTokenExpiresDays { get; set; }
    }
}
