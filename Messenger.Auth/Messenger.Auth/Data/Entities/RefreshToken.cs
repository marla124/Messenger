namespace Messenger.Auth.Data.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiringAt { get; set; }
        public string AssociateDeviceName { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
