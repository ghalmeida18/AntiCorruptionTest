using AntiCorruption.Model;
using AntiCorruption.Test.Interface;

namespace AntiCorruption.Test.Builder
{
    public class RepositoryModelBuilder : IBuilder<RepositoryModel>
    {
        private readonly RepositoryModel _repositoryModel;

        public RepositoryModelBuilder()
        {
            _repositoryModel = new RepositoryModel();
        }

        public RepositoryModel Build()
        {
            return _repositoryModel;
        }

        public void WithId(long id)
        {
            _repositoryModel.Id = id;
        }

        public void WithAutoInit(bool autoInit)
        {
            _repositoryModel.AutoInit = autoInit;
        }
        public void WithDescription(string? description)
        {
            _repositoryModel.Description = description;
        }

        public void WithLicenseTemplate(string? licenseTemplate)
        {
            _repositoryModel.LicenseTemplate = licenseTemplate;
        }

        public void WithName(string? name)
        {
            _repositoryModel.Name = name;
        }
    }
}
