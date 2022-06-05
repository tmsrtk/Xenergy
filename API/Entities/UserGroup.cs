using System.ComponentModel.DataAnnotations;

namespace Xenergy.Entities
{
    public class UserGroup
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserGroupName { get; set; }
        
    }
}