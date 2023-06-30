using AntiCorruption.Model.Validator;
using AntiCorruption.Test.Builder;

namespace AntiCorruption.Test
{
    [TestClass]
    public class RepositoryModelValidatorTest
    {
        [TestMethod]
        public void RepositoryModelValidator_OK()
        {
            #region Builder
            var builder = new RepositoryModelBuilder();
            builder.WithId(10);
            builder.WithAutoInit(true);
            builder.WithPrivate(true);
            builder.WithDescription("description");
            builder.WithLicenseTemplate("license");
            builder.WithName("name");

            var repositoryModel = builder.Build();
            #endregion

            #region Validator
            var validator = new RepositoryModelValidator();
            var result = validator.Validate(repositoryModel);
            #endregion

            #region Asserts
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 0);
            #endregion
        }
    }
}