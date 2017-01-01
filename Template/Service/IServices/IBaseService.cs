namespace Service.IServices
{
    public interface IBaseService
    {
        IUnitOfWork.IUnitOfWork UnitOfWork { get; }
        bool IsError { get; set; }

    }
}
