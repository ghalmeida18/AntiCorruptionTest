using AntiCorruption.Business.Interface;
using AntiCorruption.Data.Interface;
using AntiCorruption.Model;
using AntiCorruption.Model.Exceptions;

namespace AntiCorruption.Business.Executor
{
    public class ListBrachExecutor : IListBrachExecutor
    {
        private readonly IGitHubRepository _gitHubRepository;

        public ListBrachExecutor(IGitHubRepository gitHubRepository)
        {
            _gitHubRepository = gitHubRepository;
        }

        public List<Branch> Execute(long id)
        {
            if (id <= 0)
                throw new ListBrachException("It was not possible to list branchs. Please, inform a valid repository id.");

            return _gitHubRepository.ListBranchs(id);
        }
    }
}
