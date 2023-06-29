using AntiCorruption.Business.Interface;
using AntiCorruption.Data.Interface;
using AntiCorruption.Model;
using AntiCorruption.Model.Exceptions;
using Newtonsoft.Json;

namespace AntiCorruption.Business.Executor
{
    public class ListWebHookExecutor : IListWebHookExecutor
    {
        private readonly IGitHubRepository _gitHubRepository;

        public ListWebHookExecutor(IGitHubRepository gitHubRepository)
        {
            _gitHubRepository = gitHubRepository;
        }

        public async Task<List<RepositoryHookModel>> Execute(string repositoryName)
        {
            if (String.IsNullOrEmpty(repositoryName))
                throw new ListWebHookException("It was not possible to list the WebHookers. The repository name shouldn't be Null or Empty. Please, check the name informed.");

            var response = _gitHubRepository.ListWebHook(repositoryName);

            if (response == null || !response.IsSuccessStatusCode)
                throw new ListWebHookException("It was not possible to list the WebHookers. Please, check the name informed.");

            List<RepositoryHookModel>? hooks = new();

            if (response != null)
                hooks = JsonConvert.DeserializeObject<List<RepositoryHookModel>>(await response.Content.ReadAsStringAsync());

            return hooks;
        }
    }
}
