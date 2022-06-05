using API.Data;
using API.Repository.IRepository;
using Xenergy.Entities;

namespace API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _db;
        public UserRepository(DataContext db)
        {
            _db = db;
        }

        public bool CreateUser(User user)
        {
            _db.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _db.Remove(user);
            return Save();
        }

        public User GetUser(int userId)
        {
            return _db.Users.FirstOrDefault(a => a.Id == userId);
        }

        public ICollection<User> GetUsers()
        {
            return _db.Users.OrderBy(a => a.UserName).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _db.Users.Update(user);
            return Save();
        }

        public bool UserExists(string userName)
        {
            bool value = _db.Users.Any(a => a.UserName.ToLower().Trim().Equals(userName.ToLower().Trim()));
            return value;
        }

        public bool UserExists(int userId)
        {
            return _db.Users.Any(a => a.Id == userId);
        }
    }
}
