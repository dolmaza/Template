using Core.Entities;
using Service.IServices;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services
{
    public class PermissionService : BaseService, IPermissionService
    {
        public IEnumerable<Permission> GetAllTreeItems()
        {
            var permissions = UnitOfWork.PermissionRepository.GetAll(orderBy: ob => ob.OrderBy(p => p.SortIndex)).ToList();
            return permissions;
        }

        public IEnumerable<Permission> GetAllMenuItems()
        {
            var permissions = UnitOfWork.PermissionRepository.Get
                (
                    filter: p => p.IsMenuItem,
                    orderBy: ob => ob.OrderBy(p => p.SortIndex)
                ).ToList();
            return permissions;
        }

        public Permission GetByID(int? ID)
        {
            var permission = UnitOfWork.PermissionRepository.GetByID(ID);

            return permission;
        }

        public int? Add(Permission permission)
        {
            UnitOfWork.PermissionRepository.Add(permission);
            UnitOfWork.Complate();
            IsError = UnitOfWork.IsError;

            return permission.ID;
        }

        public IEnumerable<int?> AddRange(List<Permission> permissions)
        {
            UnitOfWork.PermissionRepository.AddRange(permissions);
            UnitOfWork.Complate();
            IsError = UnitOfWork.IsError;

            return permissions.Select(u => u.ID).ToList();
        }

        public void Update(Permission permission)
        {
            UnitOfWork.PermissionRepository.Update(permission);
            UnitOfWork.Complate();
            IsError = UnitOfWork.IsError;

        }

        public void Remove(Permission permission)
        {
            UnitOfWork.PermissionRepository.Remove(permission);
            UnitOfWork.Complate();
            IsError = UnitOfWork.IsError;

        }

        public void RemoveRange(List<Permission> permissions)
        {
            UnitOfWork.PermissionRepository.RemoveRange(permissions);
            UnitOfWork.Complate();
            IsError = UnitOfWork.IsError;

        }
    }
}
