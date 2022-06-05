using System.ComponentModel.DataAnnotations;

namespace API.Entities.DTO
{
    public class AccessRuleAssignDTO
    {
        [Required]
        public List<AccessRuleDTO> AccessRules { get; set; }
    }
}
