using System.ComponentModel.DataAnnotations;

namespace API.Entities.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public UserTypeDTO UserType { get; set; }
        public UserGroupDTO UserGroup { get; set; }
        public string FullName { get; set; }
    }
}
