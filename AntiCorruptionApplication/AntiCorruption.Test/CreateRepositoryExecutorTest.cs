using AntiCorruption.Business.Executor;
using AntiCorruption.Data.Interface;
using AntiCorruption.Model;
using AntiCorruption.Test.Builder;
using Moq;

namespace AntiCorruption.Test
{
    [TestClass]
    public class CreateRepositoryExecutorTest
    {
        private readonly Mock<IGitHubRepository> _gitHubRepository = new();

        [TestMethod]
        public void CreateRepositoryExecutor_OK()
        {
            _gitHubRepository.Setup(s => s.CreateRepository(It.IsAny<RepositoryModel>()))
                .Returns(10);

            var executor = new CreateRepositoryExecutor(_gitHubRepository.Object);

            var result = executor.Execute(CreateDefaultRepositoryModel("name"));

            #region Asserts
            Assert.IsTrue(result.Equals(10));
            #endregion
        }

        #region Test Builders
        private static RepositoryModel CreateDefaultRepositoryModel(string name)
        {
            var builder = new RepositoryModelBuilder();
            builder.WithId(10);
            builder.WithAutoInit(true);
            builder.WithPrivate(true);
            builder.WithDescription("desc");
            builder.WithLicenseTemplate("temp");
            builder.WithName(name);

            return builder.Build();
        }

        #endregion
    }
}