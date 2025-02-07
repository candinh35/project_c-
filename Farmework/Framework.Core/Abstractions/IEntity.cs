namespace Framework.Core.Abstractions
{
    public interface IEntity<Guid>
    {
        Guid id { get; set; }
    }
}
