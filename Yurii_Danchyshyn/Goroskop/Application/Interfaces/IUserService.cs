using Domain.Models;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetById(long userId);
        void Update(User user);
        User Create(User user);
        void Delete(long userId);
    }
}
