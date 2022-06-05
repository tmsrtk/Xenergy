using System.ComponentModel.DataAnnotations;

namespace API.Entities.DTO
{
    public class AccessRuleDTO
    {
        public int Id { get; set; }
        [Required]
        public string AccessRuleName { get; set; }
        [Required]
        public bool Permission { get; set; }
    }
}
