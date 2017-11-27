using FluentValidation;
using Heuristics.TechEval.Web.Models;

namespace Heuristics.TechEval.Web.Validators
{
    internal class EditMemberValidator : AbstractValidator<EditMember>
    {
        public EditMemberValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}