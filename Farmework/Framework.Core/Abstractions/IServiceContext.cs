namespace Framework.Core.Abstractions
{
    public interface IServiceContext
    {
        Guid? _userId { get; set; }
    }
}
