using Core.Entities;
using System.Collections.Generic;

namespace Service.IServices
{
    public interface IPermissionService : IBaseService
    {
        IEnumerable<Permission> GetAllTreeItems();
        IEnumerable<Permission> GetAllMenuItems();
        Permission GetByID(int? ID);

        int? Add(Permission role);
        IEnumerable<int?> AddRange(List<Permission> roles);

        void Update(Permission role);

        void Remove(Permission role);
        void RemoveRange(List<Permission> roles);
    }
}
