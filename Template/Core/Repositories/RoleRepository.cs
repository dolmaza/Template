using Core.Entities;
using Core.IRepositries;
using System.Data.Entity;

namespace Core.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context)
            : base(context)
        {
        }
    }
}
