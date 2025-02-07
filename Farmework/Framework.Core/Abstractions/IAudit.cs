namespace Framework.Core.Abstractions
{
    public interface IAudit
    {
        DateTime create_date { get; set; }
        Guid create_user { get; set; }
        DateTime? update_date { get; set; }
        Guid? update_user { get; set; }
    }
}
