using AntiCorruption.Business.Executor;
using AntiCorruption.Data.Interface;
using AntiCorruption.Model;
using AntiCorruption.Model.Exceptions;
using Moq;

namespace AntiCorruption.Test
{
    [TestClass]
    public class ListBrachExecutorTest
    {
        private readonly Mock<IGitHubRepository> _gitHubRepository = new();

        [TestMethod]
        public void ListBrachExecutorTest_OK()
        {
            _gitHubRepository.Setup(s => s.ListBranchs(It.IsAny<long>()))
                .Returns(CreateDefaultBranch());

            var executor = new ListBrachExecutor(_gitHubRepository.Object);

            var result = executor.Execute(10);

            #region Asserts
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
            #endregion
        }

        [TestMethod]
        public void ListBrachExecutorTest_InvalidId_NOK()
        {
            var executor = new ListBrachExecutor(_gitHubRepository.Object);

            #region Asserts
            Assert.ThrowsException<ListBrachException>(() => Task.Run(() => executor.Execute(0)).GetAwaiter().GetResult());
            #endregion
        }

        #region Test Builders

        private static List<Branch> CreateDefaultBranch()
        {
            return new List<Branch>()
            {
                { new Branch() { Name = "master"}  },
                { new Branch() { Name = "QA"}  }
            };
        }

        #endregion
    }
}