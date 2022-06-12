using System.ComponentModel.DataAnnotations;

namespace API.Entities.DTO
{
    public class AccessRuleAssignDTO
    {
        [Required]
        public List<int> AccessRuleIDs { get; set; }
    }
}
