using FluentValidation;
using Heuristics.TechEval.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heuristics.TechEval.Web.Validators
{
    internal class NewMemberValidator : AbstractValidator<NewMember>
    {
        public NewMemberValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}