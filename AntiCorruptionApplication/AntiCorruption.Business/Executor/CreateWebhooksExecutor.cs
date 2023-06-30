using AntiCorruption.Business.Interface;
using AntiCorruption.Data.Interface;
using AntiCorruption.Model;
using AntiCorruption.Model.Exceptions;
using AntiCorruption.Model.Validator;
using Newtonsoft.Json;

namespace AntiCorruption.Business.Executor
{
    public class CreateWebhooksExecutor : ICreateWebhooksExecutor
    {
        private readonly IGitHubRepository _gitHubRepository;

        public CreateWebhooksExecutor(IGitHubRepository gitHubRepository)
        {
            _gitHubRepository = gitHubRepository;
        }

        public async Task<RepositoryHookModel> Execute(long repositoryId, RepositoryHookModel hook, string repositoryName)
        {
            var validacao = (new RepositoryHookModelValidator()).Validate(hook);

            if (!validacao.IsValid)
                throw new CreateHookException($"It was not possible to create a new WebHook for repository {repositoryId}. Base error: {validacao.ToString().Replace("\r\n", " ; ")} ");

            var response = _gitHubRepository.CreateWebHook(repositoryId, hook, repositoryName);

            if(response == null || !response.IsSuccessStatusCode)
                throw new CreateHookException($"It was not possible to create a new WebHook for repository {repositoryId}. Please, check the values informed.");

            return JsonConvert.DeserializeObject<RepositoryHookModel>(await response.Content.ReadAsStringAsync());
        }
    }
}
