using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heuristics.TechEval.Core.Models
{
	public class Member
	{

		public int Id { get; set; }
		[Required]
		public string Name { get; set; }

        [Required]
        [StringLength(255)]
		[Index(IsUnique = true)]
		public string Email { get; set; }
	}
}
