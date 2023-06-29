using AntiCorruption.Business.Interface;
using AntiCorruption.Data.Interface;
using AntiCorruption.Model;
using Octokit;

namespace AntiCorruption.Business.Executor
{
    public class CreateRepositoryExecutor : ICreateRepositoryExecutor
    {
        private readonly IGitHubRepository _gitHubRepository;

        public CreateRepositoryExecutor(IGitHubRepository gitHubRepository)
        {
            _gitHubRepository = gitHubRepository;
        }

        public long Execute(RepositoryModel repository)
        {
            return _gitHubRepository.CreateRepository(repository);
        }
    }
}
