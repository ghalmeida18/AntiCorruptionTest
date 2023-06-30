using AntiCorruption.Business.Executor;
using AntiCorruption.Data.Interface;
using AntiCorruption.Model;
using AntiCorruption.Model.Exceptions;
using AntiCorruption.Test.Builder;
using Moq;

namespace AntiCorruption.Test
{
    [TestClass]
    public class CreateWebhooksExecutorTest
    {
        private readonly Mock<IGitHubRepository> _gitHubRepository = new();

        [TestMethod]
        public void CreateWebhooksExecutor_OK()
        {
            var webHook = CreateDefaultHookModel("name");

            _gitHubRepository.Setup(s => s.CreateWebHook(
                It.IsAny<long>(),
                It.IsAny<RepositoryHookModel>(),
                It.IsAny<string>()))
                .Returns(CreateHttpResponseMessage("name"));

            var executor = new CreateWebhooksExecutor(_gitHubRepository.Object);

            RepositoryHookModel result = Task.Run(() => executor.Execute(10, CreateDefaultHookModel("name"), "repoName")).GetAwaiter().GetResult();

            #region Asserts
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Events.Any());
            Assert.IsTrue(result.Config.Any());
            #endregion
        }

        [TestMethod]
        public void CreateWebhooksExecutor_InvalidHook_NOK()
        {
            var webHook = CreateDefaultHookModel("name");

            _gitHubRepository.Setup(s => s.CreateWebHook(
                It.IsAny<long>(),
                It.IsAny<RepositoryHookModel>(),
                It.IsAny<string>()))
                .Returns(CreateHttpResponseMessage("name"));

            var executor = new CreateWebhooksExecutor(_gitHubRepository.Object);


            #region Asserts
            Assert.ThrowsException<CreateHookException>(() => Task.Run(() => executor.Execute(10, CreateDefaultHookModel(""), "repoName")).GetAwaiter().GetResult());
            #endregion
        }

        [TestMethod]
        public void CreateWebhooksExecutor_InvalidHook2_NOK()
        {
            var webHook = CreateDefaultHookModel("name");

            _gitHubRepository.Setup(s => s.CreateWebHook(
                It.IsAny<long>(),
                It.IsAny<RepositoryHookModel>(),
                It.IsAny<string>()))
                .Returns(CreateHttpResponseMessage("name"));

            var executor = new CreateWebhooksExecutor(_gitHubRepository.Object);


            #region Asserts
            Assert.ThrowsException<CreateHookException>(() => Task.Run(() => executor.Execute(10, CreateDefaultHookModel(null), "repoName")).GetAwaiter().GetResult());
            #endregion
        }

        [TestMethod]
        public void CreateWebhooksExecutor_InvalidRepositoryResponse_NOK()
        {
            var webHook = CreateDefaultHookModel("name");
            var response = CreateHttpResponseMessage("name");

            response.StatusCode = System.Net.HttpStatusCode.Forbidden;

            _gitHubRepository.Setup(s => s.CreateWebHook(
                It.IsAny<long>(),
                It.IsAny<RepositoryHookModel>(),
                It.IsAny<string>()))
                .Returns(response);

            var executor = new CreateWebhooksExecutor(_gitHubRepository.Object);


            #region Asserts
            Assert.ThrowsException<CreateHookException>(() => Task.Run(() => executor.Execute(10, CreateDefaultHookModel(null), "repoName")).GetAwaiter().GetResult());
            #endregion
        }

        #region Test Builders
       
        private static RepositoryHookModel CreateDefaultHookModel(string name)
        {
            var builder = new RepositoryHookModelBuilder();
            builder.WithId(10);
            builder.WithName(name);
            builder.WithActive(true);
            builder.WithEvents(new List<string>() { "push" });
            builder.WithConfig(new Dictionary<string, string>() { { "test", "push" } });

            return builder.Build();
        }

        private static HttpResponseMessage CreateHttpResponseMessage(string name)
        {
            HttpResponseMessage message = new()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("{\"Name\":\"web\",\"Active\":true,\"Events\":[\"push\",\"pull_request\"],\"Config\":{\"url\":\"\",\"content_type\":\"json\",\"insecure_ssl\":\"0\"}}")
            };

            return message;
        }

        #endregion
    }
}