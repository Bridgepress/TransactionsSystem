using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TransactionsSystem.Domain.Entities;


namespace TransactionsSystem.DataAccess.InitialDataCreate
{
    public class MigrationsService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public MigrationsService(IServiceProvider serviceProvider, ILogger logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                await context.Database.MigrateAsync(stoppingToken);
            }
            catch (Exception e)
            {
                _logger.Fatal(e, "Cannot migrate database");
                return;
            }

            await SeedAdmin(context, stoppingToken);
        }

        private static async Task SeedAdmin(ApplicationDbContext context, CancellationToken stoppingToken)
        {
            if (await context.Users.AnyAsync(stoppingToken))
            {
                return;
            }
            var admin = new User
            {
                UserName = "admin",
                NormalizedUserName = "admin",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var passwordHasher = new PasswordHasher<User>();
            admin.PasswordHash = passwordHasher.HashPassword(admin, "Pa$$w0rd");
            context.Users.Add(admin);
            await context.SaveChangesAsync(stoppingToken);
        }

    }
}
