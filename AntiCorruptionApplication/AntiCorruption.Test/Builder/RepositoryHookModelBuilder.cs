using AntiCorruption.Model;
using AntiCorruption.Test.Interface;

namespace AntiCorruption.Test.Builder
{
    public class RepositoryHookModelBuilder : IBuilder<RepositoryHookModel>
    {
        private readonly RepositoryHookModel _repositoryHookModel;

        public RepositoryHookModelBuilder()
        {
            _repositoryHookModel = new();
        }

        public RepositoryHookModel Build()
        {
            return _repositoryHookModel;
        }

        public void WithId(long id)
        {
            _repositoryHookModel.Id = id;
        }

        public void WithName(string name)
        {
            _repositoryHookModel.Name = name;
        }
        public void WithActive(bool active)
        {
            _repositoryHookModel.Active = active;
        }

        public void WithEvents(List<string>? events)
        {
            _repositoryHookModel.Events = events;
        }

        public void WithConfig(Dictionary<string, string>? config)
        {
            _repositoryHookModel.Config = config;
        }
    }
}
