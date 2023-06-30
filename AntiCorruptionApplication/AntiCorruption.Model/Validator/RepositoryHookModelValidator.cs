using FluentValidation;

namespace AntiCorruption.Model.Validator
{
    public class RepositoryHookModelValidator : AbstractValidator<RepositoryHookModel>
    {
        public RepositoryHookModelValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Events).NotNull().NotEmpty();
            RuleFor(x => x.Config).NotNull().NotEmpty();
        }
    }
}
