using Framework.Core.Entities;

namespace Model.Core.Entities;

public class User : BaseEntity
{
    /// <summary>
    /// identify for user
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// パスワード
    /// </summary>
    public string HashPass { get; set; }
    
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

    public User()
    {
        id = Guid.NewGuid();
    }
}