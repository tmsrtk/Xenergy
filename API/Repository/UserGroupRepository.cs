using API.Data;
using API.Repository.IRepository;
using Xenergy.Entities;

namespace API.Repository
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly DataContext _db;
        public UserGroupRepository(DataContext db)
        {
            _db = db;
        }
        public bool CreateUserGroup(UserGroup userGroup)
        {
            _db.Add(userGroup);
            return Save();
        }

        public bool DeleteUserGroup(UserGroup userGroup)
        {
            _db.Remove(userGroup);
            return Save();
        }

        public UserGroup GetUserGroup(int userGroupId)
        {
            return _db.UserGroups.FirstOrDefault(a => a.Id == userGroupId);
        }

        public ICollection<UserGroup> GetUserGroups()
        {
            return _db.UserGroups.OrderBy(a => a.UserGroupName).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateUserGroup(UserGroup userGroup)
        {
            _db.UserGroups.Update(userGroup);
            return Save();
        }

        public bool UserGroupExists(string name)
        {
            bool value = _db.UserGroups.Any(a => a.UserGroupName.ToLower().Trim().Equals(name.ToLower().Trim()));
            return value;
        }

        public bool UserGroupExists(int userGroupId)
        {
            return _db.UserGroups.Any(a => a.Id == userGroupId);
        }
    }
}
