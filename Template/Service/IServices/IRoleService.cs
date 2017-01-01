using Core.Entities;
using Service.Utilities;
using System.Collections.Generic;

namespace Service.IServices
{
    public interface IRoleService : IBaseService
    {
        IEnumerable<Role> GetAllGridItems();
        IEnumerable<SimpleKeyValue<int?, string>> GetAllDropDownItems(int? selectedID = null);
        IEnumerable<int?> GetRolePermissions(int? ID);
        Role GetByID(int? ID);

        int? Add(Role role);
        IEnumerable<int?> AddRange(List<Role> roles);

        void Update(Role role);
        void UpdateRolePermissions(int? roleID, IEnumerable<int?> permissionIDs);

        void Remove(Role role);
        void RemoveRange(List<Role> roles);
    }
}
