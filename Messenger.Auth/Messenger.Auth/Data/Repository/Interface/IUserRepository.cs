using Messenger.Auth.Core.Data.Repository;
using Messenger.Auth.Data.Entities;
using System.Linq.Expressions;

namespace Messenger.Auth.Data.Repository.Interface;

public interface IUserRepository : IRepository<User, MessengerAuthDbContext>
{
    public Task<User> GetByEmail(string email, CancellationToken cancellationToken,
            params Expression<Func<User, object>>[] includes);

    public Task<User> GetByRefreshToken(Guid refreshToken, CancellationToken cancellationToken);
}
