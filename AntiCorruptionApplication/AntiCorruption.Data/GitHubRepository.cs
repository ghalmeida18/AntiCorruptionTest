using AntiCorruption.Data.Interface;
using AntiCorruption.Model;
using AntiCorruption.Model.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Octokit;
using System.Net.Http.Headers;
using System.Text;

namespace AntiCorruption.Data
{
    public class GitHubRepository : IGitHubRepository
    {
        private readonly GitHubClient _gitHubClient;
        private static HttpClient _httpClient;
        private readonly string? _userName;

        public GitHubRepository(IConfiguration configuration)
        {
            _userName = configuration.GetValue<string>("AppConfig:UserName");

            _httpClient = new();

            _gitHubClient = new GitHubClient(new Octokit.ProductHeaderValue("AntiCorruptionApplication"))
            {
                Credentials = new Credentials(configuration.GetValue<string>("AppConfig:Bearer"))
            };

        }

        public long CreateRepository(RepositoryModel repository)
        {
            Repository? newRepository = new();

            var repo = new NewRepository(repository.Name)
            {
                AutoInit = repository.AutoInit,
                Description = repository.Description,
                LicenseTemplate = repository.LicenseTemplate,
                Private = repository.Private
            };

            try
            {
                newRepository = Task.Run(async () => await _gitHubClient.Repository.Create(repo)).GetAwaiter().GetResult();
            }
            catch (Octokit.RepositoryExistsException e)
            {
                throw new DuplicatedRepositoryException($"It was not possible to create a new reposiory with name { repository.Name }. Base error: {e.Message}");
            }

            return newRepository.Id;
        }


        public List<Model.Branch> ListBranchs(long id)
        {
            List<Model.Branch> ListBranchs = new();

            var branchsGitHub = Task.Run(async () => await _gitHubClient.Repository.Branch.GetAll(id)).GetAwaiter().GetResult();

            if (branchsGitHub != null && branchsGitHub.Any())
            {
                ListBranchs = branchsGitHub.ToList().ConvertAll(x => new Model.Branch { Name = x.Name });
            }

            return ListBranchs;
        }

        public HttpResponseMessage? CreateWebHook(long repositoryId, RepositoryHookModel hook, string repositoryName)
        {

            var json = JsonConvert.SerializeObject(hook);

            var request = new HttpRequestMessage(HttpMethod.Post, $"https://api.github.com/repos/{_userName}/{repositoryName}/hooks");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(json.ToLower(), Encoding.UTF8);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _gitHubClient.Credentials.Password);

            request.Headers.TryAddWithoutValidation("Accept", "text/html,application/json");
            request.Headers.TryAddWithoutValidation("User-Agent", "http://developer.github.com/v3/#user-agent-required");


            return _httpClient.Send(request);

        }

        public HttpResponseMessage? ListWebHook(string repositoryName)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.github.com/repos/{_userName}/{repositoryName}/hooks");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _gitHubClient.Credentials.Password);

            request.Headers.TryAddWithoutValidation("Accept", "text/html,application/json");
            request.Headers.TryAddWithoutValidation("User-Agent", "http://developer.github.com/v3/#user-agent-required");

            return _httpClient.Send(request);
        }
        
        public HttpResponseMessage? UpdateWebHook(RepositoryHookModel hook, string repositoryName)
        {
            var json = JsonConvert.SerializeObject(hook);

            var request = new HttpRequestMessage(HttpMethod.Patch, $"https://api.github.com/repos/{_userName}/{repositoryName}/hooks/{hook.Id}");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(json.ToLower(), Encoding.UTF8);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _gitHubClient.Credentials.Password);

            request.Headers.TryAddWithoutValidation("Accept", "text/html,application/json");
            request.Headers.TryAddWithoutValidation("User-Agent", "http://developer.github.com/v3/#user-agent-required");


            return _httpClient.Send(request);
        }
    }
}
