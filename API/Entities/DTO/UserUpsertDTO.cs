using System.ComponentModel.DataAnnotations;

namespace API.Entities.DTO
{
    public class UserUpsertDTO
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
        public int UserTypeId { get; set; }
        public int UserGroupId { get; set; }
    }
}
