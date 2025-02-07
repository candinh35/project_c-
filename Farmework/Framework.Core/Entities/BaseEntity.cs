using System.ComponentModel.DataAnnotations;
using Framework.Core.Abstractions;

namespace Framework.Core.Entities;

public abstract class BaseEntity : IAudit, ISoftDelete, IEntity<Guid>
{
    [Key]
    public Guid id { get; set; }
    public DateTime create_date { get; set; }
    public Guid create_user { get; set; }
    public DateTime? update_date { get; set; }
    public Guid? update_user { get; set; }
    public bool del_flg { get; set; }
}