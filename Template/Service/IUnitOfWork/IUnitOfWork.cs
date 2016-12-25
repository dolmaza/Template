using Core.IRepositries;
using System;

namespace Service.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IPermissionRepository PermissionRepository { get; }
        IDictionaryRepository DictionaryRepository { get; }

        int Complate();
    }
}
