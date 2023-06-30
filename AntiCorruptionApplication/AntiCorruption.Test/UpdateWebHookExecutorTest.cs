using AntiCorruption.Business.Executor;
using AntiCorruption.Data.Interface;
using AntiCorruption.Model;
using AntiCorruption.Model.Exceptions;
using AntiCorruption.Test.Builder;
using Moq;
using System.Text.Json;

namespace AntiCorruption.Test
{
    [TestClass]
    public class UpdateWebHookExecutorTest
    {
        private readonly Mock<IGitHubRepository> _gitHubRepository = new();

        [TestMethod]
        public void UpdateWebHookExecutor_OK()
        {
            _gitHubRepository.Setup(s => s.UpdateWebHook(It.IsAny<RepositoryHookModel>(), It.IsAny<string>()))
                .Returns(CreateHttpResponseMessage(false));

            var executor = new UpdateWebHookExecutor(_gitHubRepository.Object);

            var result = Task.Run(() => executor.Execute(CreateDefaultHookModel("name"), "repo-name")).GetAwaiter().GetResult();

            #region Asserts
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Active);
            Assert.IsTrue(result.Config.Any());
            Assert.IsTrue(result.Events.Any());
            #endregion
        }

        [TestMethod]
        public void UpdateWebHookExecutor_InvalidHookId_NOK()
        {
            var hook = CreateDefaultHookModel("name");
            hook.Id = 0;

            _gitHubRepository.Setup(s => s.UpdateWebHook(It.IsAny<RepositoryHookModel>(), It.IsAny<string>()))
                .Returns(CreateHttpResponseMessage(false));

            var executor = new UpdateWebHookExecutor(_gitHubRepository.Object);

            #region Asserts
            Assert.ThrowsException<UpdateWebHookException>(() => Task.Run(() => executor.Execute(hook, "repo-name")).GetAwaiter().GetResult());

            #endregion
        }

        [TestMethod]
        public void UpdateWebHookExecutor_InvalidResponse_NOK()
        {
            var response = CreateHttpResponseMessage(false);
            response.StatusCode = System.Net.HttpStatusCode.NotFound;

            _gitHubRepository.Setup(s => s.UpdateWebHook(It.IsAny<RepositoryHookModel>(), It.IsAny<string>()))
                .Returns(response);

            var executor = new UpdateWebHookExecutor(_gitHubRepository.Object);

            #region Asserts
            Assert.ThrowsException<UpdateWebHookException>(() => Task.Run(() => executor.Execute(CreateDefaultHookModel("name"), "repo-name")).GetAwaiter().GetResult());

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

        private static HttpResponseMessage CreateHttpResponseMessage(bool isList)
        {
            HttpResponseMessage message = new()
            {
                StatusCode = System.Net.HttpStatusCode.OK
            };

            if (isList)
                message.Content = new StringContent(JsonSerializer.Serialize(new List<RepositoryHookModel>() { CreateDefaultHookModel("name") }));
            else
                message.Content = new StringContent(JsonSerializer.Serialize(CreateDefaultHookModel("name")));

            return message;
        }

        #endregion
    }
}