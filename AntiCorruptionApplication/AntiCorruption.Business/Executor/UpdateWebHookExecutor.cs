using AntiCorruption.Business.Interface;
using AntiCorruption.Data.Interface;
using AntiCorruption.Model;
using AntiCorruption.Model.Exceptions;
using AntiCorruption.Model.Validator;
using Newtonsoft.Json;

namespace AntiCorruption.Business.Executor
{
    public class UpdateWebHookExecutor : IUpdateWebHookExecutor
    {
        private readonly IGitHubRepository _gitHubRepository;

        public UpdateWebHookExecutor(IGitHubRepository gitHubRepository)
        {
            _gitHubRepository = gitHubRepository;
        }

        public async Task<RepositoryHookModel> Execute(RepositoryHookModel hook, string repositoryName)
        {

            if (hook.Id <= 0)
                throw new UpdateWebHookException($"It was not possible to update the WebHook. Please, inform a valid ID.");

            var response = _gitHubRepository.UpdateWebHook(hook, repositoryName);

            if (response == null || !response.IsSuccessStatusCode)
                throw new UpdateWebHookException($"It was not possible to update the WebHook { hook.Id }. Check the values informed");


            return JsonConvert.DeserializeObject<RepositoryHookModel>(await response.Content.ReadAsStringAsync());
        }
    }
}
