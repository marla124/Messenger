using Messenger.Auth.Data;
using Messenger.Auth.Data.Entities;
using Messenger.Auth.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Messenger.Auth.IntegrationTests;

public class BaseIntegrationTest : IDisposable
{
    private readonly MessengerAuthDbContext? _dbContextForAuth;

    protected readonly HttpClient _httpClient;
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public BaseIntegrationTest()
    {
        _webApplicationFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptorAuth = services.SingleOrDefault(d => d.ServiceType ==
                        typeof(DbContextOptions<MessengerAuthDbContext>));
                if (descriptorAuth != null)
                    services.Remove(descriptorAuth);
                services.AddDbContext<MessengerAuthDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryTest");
                });
            });
        });

        var serviceProvider = _webApplicationFactory.Services.CreateScope().ServiceProvider;

        _httpClient = _webApplicationFactory.CreateClient();

        _dbContextForAuth = serviceProvider.GetService<MessengerAuthDbContext>()!;
        _dbContextForAuth?.Database.EnsureCreated();
    }

    protected async Task<RefreshToken> PopulateTokenToDatabase()
    {
        var user = await PopulateUserToDatabase();
        var token = new RefreshToken
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            ExpiringAt = DateTime.Now,
            AssociateDeviceName = "name",
            UserId = user.Id,
        };

        _dbContextForAuth!.RefreshTokens.Add(token);
        await _dbContextForAuth.SaveChangesAsync();

        return token;
    }
    protected async Task<User> PopulateUserToDatabase()
    {
        var user = new User
        {
            Email = "testemail2@gmail.com",
            CreatedAt = DateTime.Now,
            Id = Guid.NewGuid(),
            UpdatedAt = DateTime.Now,
            PasswordHash = "5F4DCC3B5AA765D61D8327DEB882CF99",
            UserRoleId = Guid.Parse("52a9905e-2041-4fd9-9d94-4dfc24e735cf")
        };

        _dbContextForAuth!.Users.Add(user);
        await _dbContextForAuth.SaveChangesAsync();

        return user;
    }

    public void Dispose()
    {
        _dbContextForAuth?.Database.EnsureDeleted();
        _dbContextForAuth?.Dispose();
        _httpClient.Dispose();

        GC.SuppressFinalize(this);
    }
}
