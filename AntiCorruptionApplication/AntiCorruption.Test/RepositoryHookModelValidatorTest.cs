using AntiCorruption.Model.Validator;
using AntiCorruption.Test.Builder;

namespace AntiCorruption.Test
{
    [TestClass]
    public class RepositoryHookModelValidatorTest
    {
        [TestMethod]
        public void RepositoryHookModelValidator_OK()
        {
            #region Builder
            var builder = new RepositoryHookModelBuilder();
            builder.WithId(10);
            builder.WithName("name");
            builder.WithActive(true);
            builder.WithEvents(new List<string>() { "push" });
            builder.WithConfig(new Dictionary<string, string>() { { "test", "push" } });

            var hookModel = builder.Build();
            #endregion

            #region Validator
            var validator = new RepositoryHookModelValidator();
            var result = validator.Validate(hookModel);
            #endregion

            #region Asserts
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
            #endregion
        }

        [TestMethod]
        public void RepositoryHookModelValidator_InvalidName_Empty_NOK()
        {
            #region Builder
            var builder = new RepositoryHookModelBuilder();
            builder.WithId(10);
            builder.WithName("");
            builder.WithActive(true);
            builder.WithEvents(new List<string>() { "push" });
            builder.WithConfig(new Dictionary<string, string>() { { "test", "push" } });

            var hookModel = builder.Build();
            #endregion

            #region Validator
            var validator = new RepositoryHookModelValidator();
            var result = validator.Validate(hookModel);
            #endregion

            #region Asserts
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count != 0);
            #endregion
        }

        [TestMethod]
        public void RepositoryHookModelValidator_InvalidName_Null_NOK()
        {
            #region Builder
            var builder = new RepositoryHookModelBuilder();
            builder.WithId(10);
            builder.WithName(null);
            builder.WithActive(true);
            builder.WithEvents(new List<string>() { "push" });
            builder.WithConfig(new Dictionary<string, string>() { { "test", "push" } });

            var hookModel = builder.Build();
            #endregion

            #region Validator
            var validator = new RepositoryHookModelValidator();
            var result = validator.Validate(hookModel);
            #endregion

            #region Asserts
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count != 0);
            #endregion
        }

        [TestMethod]
        public void RepositoryHookModelValidator_InvalidEvents_Empty_NOK()
        {
            #region Builder
            var builder = new RepositoryHookModelBuilder();
            builder.WithId(10);
            builder.WithName("name");
            builder.WithActive(true);
            builder.WithEvents(new List<string>() { });
            builder.WithConfig(new Dictionary<string, string>() { { "test", "push" } });

            var hookModel = builder.Build();
            #endregion

            #region Validator
            var validator = new RepositoryHookModelValidator();
            var result = validator.Validate(hookModel);
            #endregion

            #region Asserts
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count != 0);
            #endregion
        }

        [TestMethod]
        public void RepositoryHookModelValidator_InvalidEvents_Null_NOK()
        {
            #region Builder
            var builder = new RepositoryHookModelBuilder();
            builder.WithId(10);
            builder.WithName("name");
            builder.WithActive(true);
            builder.WithEvents(null);
            builder.WithConfig(new Dictionary<string, string>() { { "test", "push" } });

            var hookModel = builder.Build();
            #endregion

            #region Validator
            var validator = new RepositoryHookModelValidator();
            var result = validator.Validate(hookModel);
            #endregion

            #region Asserts
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count != 0);
            #endregion
        }

        [TestMethod]
        public void RepositoryHookModelValidator_InvalidConfig_Null_NOK()
        {
            #region Builder
            var builder = new RepositoryHookModelBuilder();
            builder.WithId(10);
            builder.WithName("name");
            builder.WithActive(true);
            builder.WithEvents(new List<string>() { "push" });
            builder.WithConfig(null);

            var hookModel = builder.Build();
            #endregion

            #region Validator
            var validator = new RepositoryHookModelValidator();
            var result = validator.Validate(hookModel);
            #endregion

            #region Asserts
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count != 0);
            #endregion
        }

        [TestMethod]
        public void RepositoryHookModelValidator_InvalidConfig_Empty_NOK()
        {
            #region Builder
            var builder = new RepositoryHookModelBuilder();
            builder.WithId(10);
            builder.WithName("name");
            builder.WithActive(true);
            builder.WithEvents(new List<string>() { "push" });
            builder.WithConfig(new Dictionary<string, string>());

            var hookModel = builder.Build();
            #endregion

            #region Validator
            var validator = new RepositoryHookModelValidator();
            var result = validator.Validate(hookModel);
            #endregion

            #region Asserts
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count != 0);
            #endregion
        }

    }
}