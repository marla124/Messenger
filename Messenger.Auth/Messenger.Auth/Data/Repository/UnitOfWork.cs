using Messenger.Auth.Core.Data.Repository;
using Messenger.Auth.Data.Entities;
using Messenger.Auth.Data.Repository.Interface;

namespace Messenger.Auth.Data.Repository;

public class UnitOfWork(
    MessengerAuthDbContext dbContext,
    IUserRepository userRepository,
    IRepository<UserRole, MessengerAuthDbContext> userRoleRepository,
    IAuthRepository authRepository)
    : IUnitOfWork
{

    public IUserRepository UserRepository { get; } = userRepository;
    public IRepository<UserRole, MessengerAuthDbContext> UserRoleRepository { get; } = userRoleRepository;
    public IAuthRepository AuthRepository { get; } = authRepository;

    public async Task<int> Commit(CancellationToken cancellationToken)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}
