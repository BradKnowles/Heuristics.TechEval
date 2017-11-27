using FluentValidation.Attributes;
using Heuristics.TechEval.Web.Validators;

namespace Heuristics.TechEval.Web.Models
{

    /// <summary>
    /// DTO representing the creation of a new Member
    /// </summary>
    [Validator(typeof(NewMemberValidator))]
	public class NewMember {
		public string Name { get; set; }
		public string Email { get; set; }
	}
}