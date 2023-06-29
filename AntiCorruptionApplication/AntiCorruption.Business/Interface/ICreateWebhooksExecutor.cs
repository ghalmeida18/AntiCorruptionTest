using AntiCorruption.Model;

namespace AntiCorruption.Business.Interface
{
    public interface ICreateWebhooksExecutor
    {
        public Task<RepositoryHookModel> Execute(long repositoryId, RepositoryHookModel hook, string repositoryName);
    }
}
