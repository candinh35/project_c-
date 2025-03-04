using Framework.Core.Entities;

namespace Model.Core.Entities;

public class RefreshToken : BaseEntity
{
    public string Token { get; set; }
    public string Refresh { get; set; }
    public Guid UserId { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsRevoked { get; set; }
    
    public virtual User User { get; set; }
}