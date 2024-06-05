using Messenger.Auth.Core.Data;

namespace Messenger.Auth.Data.Entities;

public class UserRole : BaseEntity
{
    public string Role { get; set; }
    public List<User> Users { get; set; }
}
