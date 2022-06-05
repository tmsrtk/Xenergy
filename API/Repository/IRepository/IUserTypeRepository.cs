using Xenergy.Entities;

namespace API.Repository.IRepository
{
    public interface IUserTypeRepository
    {
        ICollection<UserType> GetUserTypes();
        UserType GetUserType(int userTypeId);
        bool UserTypeExists(string name);
        bool UserTypeExists(int userTypeId);
        bool CreateUserType(UserType userType);
        bool UpdateUserType(UserType userType);
        bool DeleteUserType(UserType userType);
        bool Save();

    }
}
