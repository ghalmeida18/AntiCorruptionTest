using AntiCorruption.Model;

namespace AntiCorruption.Data.Interface
{
    public interface IGitHubRepository
    {
        public long CreateRepository(RepositoryModel repository);
        public List<Branch> ListBranchs(long id);
        public HttpResponseMessage? CreateWebHook(long repositoryId, RepositoryHookModel hook, string repositoryName);
        public HttpResponseMessage? ListWebHook(string repositoryName);
        public HttpResponseMessage? UpdateWebHook(RepositoryHookModel hook, string repositoryName);
    }
}
