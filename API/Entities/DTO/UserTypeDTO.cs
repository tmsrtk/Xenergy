using System.ComponentModel.DataAnnotations;

namespace API.Entities.DTO
{
    public class UserTypeDTO
    {
        public int Id { get; set; }
        [Required]
        public string UserTypeName { get; set; }
    }
}
