using FluentValidation;

namespace Discogs.Api.Models.Validators
{
    public class SearchCriteriaDTOValidator : AbstractValidator<SearchCriteriaDTO>
    {
        public SearchCriteriaDTOValidator()
        {
            RuleFor(c => c.Username).NotEmpty().Length(5, 100);
            RuleFor(c => c.PageSize).LessThanOrEqualTo(100);
        }
    }
}
