using Microsoft.AspNetCore.Identity;

namespace TransactionsSystem.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public ICollection<UserRole>? UserRoles { get; set; }
    }
}
