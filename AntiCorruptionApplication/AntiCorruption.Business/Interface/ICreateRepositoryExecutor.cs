using AntiCorruption.Model;

namespace AntiCorruption.Business.Interface
{
    public interface ICreateRepositoryExecutor
    {
        public long Execute(RepositoryModel repository);
    }
}
