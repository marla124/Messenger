using Messenger.Auth.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Auth.Data;

public class MessengerAuthDbContext(DbContextOptions<MessengerAuthDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
}
