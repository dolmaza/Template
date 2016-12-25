using Core.Entities;
using Core.IRepositries;
using System.Data.Entity;

namespace Core.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context)
        {
        }
    }
}
