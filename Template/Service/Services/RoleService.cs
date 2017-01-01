using Core.Entities;
using Service.IServices;
using Service.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services
{
    public class RoleService : BaseService, IRoleService
    {
        public IEnumerable<Role> GetAllGridItems()
        {
            var roles = UnitOfWork.RoleRepository.GetAll(orderBy: ob => ob.OrderByDescending(r => r.CreateTime)).ToList();

            return roles;
        }

        public IEnumerable<SimpleKeyValue<int?, string>> GetAllDropDownItems(int? selectedID = null)
        {
            var roles = UnitOfWork.RoleRepository.GetAll(orderBy: ob => ob.OrderByDescending(r => r.CreateTime))
                .Select(r => new SimpleKeyValue<int?, string>
                {
                    Key = r.ID,
                    Value = r.Caption,
                    IsSelected = r.ID == selectedID
                }).ToList();

            return roles;
        }

        public IEnumerable<int?> GetRolePermissions(int? ID)
        {
            var rolePermissions = UnitOfWork.RoleRepository.Get(filter: r => r.ID == ID, includes: r => r.Permissions)
                .FirstOrDefault()?.Permissions
                .Select(r => r.ID)
                .ToList();

            return rolePermissions;
        }

        public Role GetByID(int? ID)
        {
            var role = UnitOfWork.RoleRepository.GetByID(ID);

            return role;
        }

        public int? Add(Role role)
        {
            UnitOfWork.RoleRepository.Add(role);
            UnitOfWork.Complate();
            IsError = UnitOfWork.IsError;

            return role.ID;
        }

        public IEnumerable<int?> AddRange(List<Role> roles)
        {
            UnitOfWork.RoleRepository.AddRange(roles);
            UnitOfWork.Complate();
            IsError = UnitOfWork.IsError;

            return roles.Select(u => u.ID).ToList();
        }

        public void Update(Role role)
        {
            UnitOfWork.RoleRepository.Update(role);
            UnitOfWork.Complate();
            IsError = UnitOfWork.IsError;

        }

        public void UpdateRolePermissions(int? roleID, IEnumerable<int?> permissionIDs)
        {
            var role = UnitOfWork.RoleRepository.GetOne(filter: r => r.ID == roleID, includes: r => r.Permissions);
            if (role == null || permissionIDs == null)
            {
                IsError = true;
            }
            else
            {
                var newPermissions = UnitOfWork.PermissionRepository.Get(filter: p => permissionIDs.Contains(p.ID ?? 0)).ToList();

                role.Permissions.Where(p => !newPermissions.Contains(p)).ToList().ForEach(permission =>
                {
                    role.Permissions.Remove(permission);
                });

                newPermissions.Where(p => !role.Permissions.Contains(p)).ToList().ForEach(permission =>
                {
                    role.Permissions.Add(permission);
                });
                UnitOfWork.RoleRepository.Update(role);
                UnitOfWork.Complate();
                IsError = UnitOfWork.IsError;

            }
        }

        public void Remove(Role role)
        {
            UnitOfWork.RoleRepository.Remove(role);
            UnitOfWork.Complate();
            IsError = UnitOfWork.IsError;

        }

        public void RemoveRange(List<Role> roles)
        {
            UnitOfWork.RoleRepository.RemoveRange(roles);
            UnitOfWork.Complate();
            IsError = UnitOfWork.IsError;

        }
    }
}
