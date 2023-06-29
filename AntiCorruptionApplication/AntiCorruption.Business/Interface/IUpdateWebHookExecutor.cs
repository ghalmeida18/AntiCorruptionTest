using AntiCorruption.Model;

namespace AntiCorruption.Business.Interface
{
    public interface IUpdateWebHookExecutor
    {
        public Task<RepositoryHookModel> Execute(RepositoryHookModel hook, string repositoryName);
    }
}
