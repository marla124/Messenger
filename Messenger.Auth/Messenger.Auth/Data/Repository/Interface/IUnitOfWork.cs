using Messenger.Auth.Core.Data.Repository;
using Messenger.Auth.Data.Entities;

namespace Messenger.Auth.Data.Repository.Interface;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IRepository<UserRole, MessengerAuthDbContext> UserRoleRepository { get; }
    public IAuthRepository AuthRepository { get; }

    Task<int> Commit(CancellationToken cancellationToken);
}
