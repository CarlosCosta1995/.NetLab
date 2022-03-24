using Ficha14.Models;

namespace Ficha14.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext usercontext;
        public UserService(UserContext context)
        {
            this.usercontext = context;
        }
        public IEnumerable<User> GetAll()
        {
            var users = usercontext.Users;
            return users;
        }

        public User GetUser(string userName, string password)
        {
            var user = usercontext.Users.FirstOrDefault(x => x.UserName == userName && x.Password == password);
            return user;
        }

        public User Create(User user)
        {
            if (user is null)
            {
                throw new NullReferenceException("Publisher does not exist");
            }
            else
            {
                usercontext.Users.Add(user);
                usercontext.SaveChanges();
                return user;
            }
        }

        public User FindByName(string userName)
        {
           var user = usercontext.Users.Find(userName);
            if(user is null)
            {
                throw new NullReferenceException("user does not exist");
            }
            else
            {
                return user;
            }
        }
    }
}
