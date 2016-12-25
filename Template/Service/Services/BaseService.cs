using Core.DB;
using Service.IServices;

namespace Service.Services
{
    public class BaseService : IBaseService
    {
        public IUnitOfWork.IUnitOfWork UnitOfWork { get; private set; }

        public BaseService()
        {
            UnitOfWork = new UnitOfWork.UnitOfWork(new DbCoreDataContext());
        }
    }
}
