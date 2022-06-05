using API.Data;
using API.Repository.IRepository;
using Xenergy.Entities;

namespace API.Repository
{
    public class AccessRuleRepository : IAccessRuleRepository
    {
        private readonly DataContext _db;
        public AccessRuleRepository(DataContext db)
        {
            _db = db;
        }
        public bool CreateAccessRule(AccessRule accessRule)
        {
            _db.Add(accessRule);
            return Save();
        }

        public bool DeleteAccessRule(AccessRule accessRule)
        {
            _db.Remove(accessRule);
            return Save();
        }

        public AccessRule GetAccessRule(int accessRuleId)
        {
            return _db.AccessRules.FirstOrDefault(a => a.Id == accessRuleId);
        }

        public ICollection<AccessRule> GetAccessRules()
        {
            return _db.AccessRules.OrderBy(a => a.AccessRuleName).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateAccessRule(AccessRule accessRule)
        {
            _db.AccessRules.Update(accessRule);
            return Save();
        }

        public bool AccessRuleExists(string name)
        {
            bool value = _db.AccessRules.Any(a => a.AccessRuleName.ToLower().Trim().Equals(name.ToLower().Trim()));
            return value;
        }

        public bool AccessRuleExists(int accessRuleId)
        {
            return _db.AccessRules.Any(a => a.Id == accessRuleId);
        }



        public bool AddUserGroupAccessRules(List<UserGroupAccessRule> userGroupAccessRules)
        {
            _db.AddRange(userGroupAccessRules);
            return Save();
        }

        public bool DeleteUserGroupAccessRules(int userGroupId)
        {
            List<UserGroupAccessRule> groupAccessRules = (List<UserGroupAccessRule>) GetUserGroupAccessRules(userGroupId);
            _db.RemoveRange(groupAccessRules);
            return Save();
        }

        public ICollection<UserGroupAccessRule> GetUserGroupAccessRules(int userGroupId)
        {
            return _db.UserGroupAccessRule.Where(f => f.UserGroup.Id == userGroupId).ToList();
        }

        public bool UserGroupAccessRuleExists(int userGroupId)
        {
            bool value = _db.UserGroupAccessRule.Any(a => a.UserGroup.Id == userGroupId);
            return value;
        }
    }
}
