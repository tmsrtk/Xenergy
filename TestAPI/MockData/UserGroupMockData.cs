using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xenergy.Entities;

namespace TestAPI.MockData
{
    internal class UserGroupMockData
    {
        public static ICollection<UserGroup> GetUserGroups()
        {
            return new List<UserGroup>
            {
                new UserGroup
                {
                    Id = 1,
                    UserGroupName = "User Group 1",
                },
                new UserGroup
                {
                    Id = 2,
                    UserGroupName = "User Group 2",
                },
                new UserGroup
                {
                    Id = 3,
                    UserGroupName = "User Group 3",
                }
            };
        }
    }
}
