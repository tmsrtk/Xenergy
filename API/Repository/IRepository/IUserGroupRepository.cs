using Xenergy.Entities;

namespace API.Repository.IRepository
{
    public interface IUserGroupRepository
    {
        ICollection<UserGroup> GetUserGroups();
        UserGroup GetUserGroup(int userGroupId);
        bool UserGroupExists(string name);
        bool UserGroupExists(int userGroupId);
        bool CreateUserGroup(UserGroup userGroup);
        bool UpdateUserGroup(UserGroup userGroup);
        bool DeleteUserGroup(UserGroup userGroup);
        bool Save();

    }
}
