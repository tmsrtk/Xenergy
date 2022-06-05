using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Xenergy.Entities
{
    public class UserGroupAccessRule
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("AccessRuleId")]
        public AccessRule AccessRule { get; set; }
        [ForeignKey("UserGroupId")]
        public UserGroup UserGroup { get; set; }

    }
}