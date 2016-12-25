using Core.Entities;
using Core.IRepositries;
using System.Data.Entity;

namespace Core.Repositories
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(DbContext context)
            : base(context)
        {
        }
    }
}
