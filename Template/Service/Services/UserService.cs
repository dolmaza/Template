using Core.Entities;
using Service.IServices;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services
{
    public class UserService : BaseService, IUserService
    {
        public IEnumerable<User> GetAllGridItems()
        {
            var users = UnitOfWork.UserRepository.GetAll(orderBy: ob => ob.OrderByDescending(u => u.CreateTime)).ToList();

            return users;
        }

        public User GetByID(int? ID)
        {
            var user = UnitOfWork.UserRepository.GetByID(ID);

            return user;
        }

        public User GetByEmailAndPassword(string email, string passwordMd5)
        {
            var user = UnitOfWork.UserRepository.GetOne
                (
                    u => u.Email == email && u.Password == passwordMd5,
                    u => u.Role, u => u.Role.Permissions
                );

            return user;
        }

        public int? Add(User user)
        {
            UnitOfWork.UserRepository.Add(user);
            UnitOfWork.Complate();
            IsError = UnitOfWork.IsError;

            return user.ID;
        }

        public IEnumerable<int?> AddRange(List<User> users)
        {
            UnitOfWork.UserRepository.AddRange(users);
            UnitOfWork.Complate();
            IsError = UnitOfWork.IsError;

            return users.Select(u => u.ID).ToList();
        }

        public void Update(User user)
        {
            UnitOfWork.UserRepository.Update(user);
            UnitOfWork.Complate();
            IsError = UnitOfWork.IsError;
        }

        public void Remove(User user)
        {
            UnitOfWork.UserRepository.Remove(user);
            UnitOfWork.Complate();
            IsError = UnitOfWork.IsError;

        }

        public void RemoveRange(List<User> users)
        {
            UnitOfWork.UserRepository.RemoveRange(users);
            UnitOfWork.Complate();
            IsError = UnitOfWork.IsError;

        }
    }
}
