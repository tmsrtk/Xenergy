using Xenergy.Entities;

namespace API.Repository.IRepository
{
    public interface IAccessRuleRepository
    {
        ICollection<AccessRule> GetAccessRules();
        AccessRule GetAccessRule(int accessRuleId);
        bool AccessRuleExists(string name);
        bool AccessRuleExists(int accessRuleId);
        bool CreateAccessRule(AccessRule accessRule);
        bool UpdateAccessRule(AccessRule accessRule);
        bool DeleteAccessRule(AccessRule accessRule);
        bool Save();
        bool AddUserGroupAccessRules(List<UserGroupAccessRule> userGroupAccessRules);
        bool DeleteUserGroupAccessRules(int userGroupId);
        ICollection<UserGroupAccessRule> GetUserGroupAccessRules(int userGroupId);
        bool UserGroupAccessRuleExists(int userGroupId);

    }
}
