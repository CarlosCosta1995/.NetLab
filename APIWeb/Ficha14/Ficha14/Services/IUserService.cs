using Ficha14.Models;

namespace Ficha14.Services
{
    public interface IUserService
    {
        //CRUD Operations
        public abstract IEnumerable<User> GetAll();
        public abstract User GetUser(string userName, string password);

        public abstract User Create(User user);

        public abstract User FindByName(string userName);

        //public abstract void DeleteById(int id);

        //public abstract User Update(int id, User book);


    }
}
