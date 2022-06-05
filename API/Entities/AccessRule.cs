using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Xenergy.Entities
{
    public class AccessRule
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AccessRuleName { get; set; }
        [Required]
        public bool Permission { get; set; }

    }
}