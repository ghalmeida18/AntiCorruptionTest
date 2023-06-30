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
    public class ListWebHookExecutorTest
    {
        private readonly Mock<IGitHubRepository> _gitHubRepository = new();

        [TestMethod]
        public void ListWebHookExecutor_OK()
        {
            _gitHubRepository.Setup(s => s.ListWebHook(It.IsAny<string>()))
                .Returns(CreateHttpResponseMessage(true));

            var executor = new ListWebHookExecutor(_gitHubRepository.Object);

            var result = Task.Run(() => executor.Execute("repo")).GetAwaiter().GetResult();

            #region Asserts
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
            #endregion
        }

        [TestMethod]
        public void ListWebHookExecutor_InvalidName_Null_NOK()
        {
            var executor = new ListWebHookExecutor(_gitHubRepository.Object);

            #region Asserts
            Assert.ThrowsException<ListWebHookException>(() => Task.Run(() => executor.Execute(null)).GetAwaiter().GetResult());
            #endregion
        }

        [TestMethod]
        public void ListWebHookExecutor_InvalidName_Empty_NOK()
        {
            var executor = new ListWebHookExecutor(_gitHubRepository.Object);

            #region Asserts
            Assert.ThrowsException<ListWebHookException>(() => Task.Run(() => executor.Execute("")).GetAwaiter().GetResult());
            #endregion
        }

        [TestMethod]
        public void ListWebHookExecutor_InvalidRepositoryReturn_NOK()
        {
            _gitHubRepository.Setup(s => s.ListWebHook(It.IsAny<string>()))
                .Returns((HttpResponseMessage?)null);

            var executor = new ListWebHookExecutor(_gitHubRepository.Object);

            #region Asserts
            Assert.ThrowsException<ListWebHookException>(() => Task.Run(() => executor.Execute("Test")).GetAwaiter().GetResult());

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