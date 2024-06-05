using Messenger.Auth.Data.Entities;

namespace Messenger.Auth.Data.Repository.Interface
{
    public interface IAuthRepository
    {
        public Task CreateRefreshToken(RefreshToken refToken, CancellationToken cancellationToken);
        public Task DeleteRefreshToken(Guid id, CancellationToken cancellationToken);
        public Task<RefreshToken> GetRefreshToken(Guid id, CancellationToken cancellationToken);
    }
}
