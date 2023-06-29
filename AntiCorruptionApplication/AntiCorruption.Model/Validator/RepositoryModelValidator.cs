using FluentValidation;

namespace AntiCorruption.Model.Validator
{
    public class RepositoryModelValidator : AbstractValidator<RepositoryModel>
    {
        public RepositoryModelValidator()
        {
            RuleFor(x => x.LicenseTemplate).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty().NotNull();
        }
    }
}
