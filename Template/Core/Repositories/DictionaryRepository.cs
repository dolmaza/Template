using Core.Entities;
using Core.IRepositries;
using System.Data.Entity;

namespace Core.Repositories
{
    public class DictionaryRepository : Repository<Dictionary>, IDictionaryRepository
    {
        public DictionaryRepository(DbContext context)
            : base(context)
        {
        }
    }
}
