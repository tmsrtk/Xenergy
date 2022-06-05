using API.Data;
using API.Repository.IRepository;
using Xenergy.Entities;

namespace API.Repository
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private readonly DataContext _db;
        public UserTypeRepository(DataContext db)
        {
            _db = db;
        }
        public bool CreateUserType(UserType userType)
        {
            _db.Add(userType);
            return Save();
        }

        public bool DeleteUserType(UserType userType)
        {
            _db.Remove(userType);
            return Save();
        }

        public UserType GetUserType(int userTypeId)
        {
            return _db.UserTypes.FirstOrDefault(a => a.Id == userTypeId);
        }

        public ICollection<UserType> GetUserTypes()
        {
            return _db.UserTypes.OrderBy(a => a.UserTypeName).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateUserType(UserType userType)
        {
            _db.UserTypes.Update(userType);
            return Save();
        }

        public bool UserTypeExists(string name)
        {
            bool value = _db.UserTypes.Any(a => a.UserTypeName.ToLower().Trim().Equals(name.ToLower().Trim()));
            return value;
        }

        public bool UserTypeExists(int userTypeId)
        {
            return _db.UserTypes.Any(a => a.Id == userTypeId);
        }
    }
}
