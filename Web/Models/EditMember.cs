using System;

namespace Heuristics.TechEval.Web.Models
{
    /// <summary>
    /// DTO representing editing of an existing Member
    /// </summary>
    public class EditMember
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
    }
}