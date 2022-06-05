using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xenergy.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [ForeignKey("UserTypeId")]
        public UserType UserType { get; set; }
        [ForeignKey("UserGroupId")]
        public UserGroup UserGroup { get; set; }
    }
}