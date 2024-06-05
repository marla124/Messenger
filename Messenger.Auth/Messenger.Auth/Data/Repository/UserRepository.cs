using Messenger.Auth.Core.Data.Repository;
using Messenger.Auth.Data.Entities;
using Messenger.Auth.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Messenger.Auth.Data.Repository;

public class UserRepository : Repository<User, MessengerAuthDbContext>, IUserRepository
{
    public UserRepository(MessengerAuthDbContext dBContext) : base(dBContext)
    {

    }
    public async Task<User> GetByEmail(string email, CancellationToken cancellationToken,
                params Expression<Func<User, object>>[] includes)
    {
        var resultQuery = _dbSet.AsQueryable();
        if (includes.Any())
        {
            resultQuery = includes.Aggregate(resultQuery,
                (current, include)
                    => current.Include(include));
        }
        return await resultQuery.FirstOrDefaultAsync(entity => entity.Email.Equals(email), cancellationToken);
    }

    public async Task<User> GetByRefreshToken(Guid refreshToken, CancellationToken cancellationToken)
    {
        var resultQuery = _dbSet.AsQueryable();
        return await resultQuery.FirstOrDefaultAsync(entity => entity.RefreshTokens.Any(rt => rt.Id == refreshToken), cancellationToken);
    }
}
