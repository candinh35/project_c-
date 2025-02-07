using Framework.Core.Entities;

namespace Model.Core.Entities;

public class User : BaseEntity
{
    public string? Name { get; set; }
    public string? Email { get; set; }
}