﻿using Core.DB;
using Core.IRepositries;
using Core.Repositories;
using System;

namespace Service.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork.IUnitOfWork
    {
        private readonly DbCoreDataContext _context;

        public bool IsError { get; set; }

        public IUserRepository UserRepository { get; private set; }
        public IRoleRepository RoleRepository { get; private set; }
        public IPermissionRepository PermissionRepository { get; private set; }
        public IDictionaryRepository DictionaryRepository { get; private set; }

        public UnitOfWork(DbCoreDataContext context)
        {
            _context = context;
            DictionaryRepository = new DictionaryRepository(_context);
            UserRepository = new UserRepository(_context);
            RoleRepository = new RoleRepository(_context);
            PermissionRepository = new PermissionRepository(_context);

        }

        public void Dispose()
        {
            _context.Dispose();
        }


        public int Complate()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                IsError = true;
                return -1;
            }
        }
    }
}
