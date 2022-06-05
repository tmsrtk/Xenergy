using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Xenergy.Entities
{
    public class UserType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserTypeName { get; set; }

    }
}