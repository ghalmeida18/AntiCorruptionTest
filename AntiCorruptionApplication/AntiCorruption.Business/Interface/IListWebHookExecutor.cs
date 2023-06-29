using AntiCorruption.Model;

namespace AntiCorruption.Business.Interface
{
    public interface IListWebHookExecutor
    {
        public Task<List<RepositoryHookModel>> Execute(string repositoryName);
    }
}
