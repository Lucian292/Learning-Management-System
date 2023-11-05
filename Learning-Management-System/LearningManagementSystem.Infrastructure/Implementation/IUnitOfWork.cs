namespace Infrastructure.ComplexImplementation
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
    }

}
