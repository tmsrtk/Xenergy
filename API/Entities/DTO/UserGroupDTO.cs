using System.ComponentModel.DataAnnotations;

namespace API.Entities.DTO
{
    public class UserGroupDTO
    {
        public UserGroupDTO()
        {

        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserGroupName { get; set; }
    }
}
