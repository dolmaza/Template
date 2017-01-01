using Core.Entities;
using System.Collections.Generic;

namespace Service.IServices
{
    public interface IUserService : IBaseService
    {
        IEnumerable<User> GetAllGridItems();
        User GetByID(int? ID);
        User GetByEmailAndPassword(string email, string passwordMd5);

        int? Add(User user);
        IEnumerable<int?> AddRange(List<User> users);

        void Update(User user);

        void Remove(User user);
        void RemoveRange(List<User> users);
    }
}
