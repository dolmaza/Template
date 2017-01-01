using Core.Entities;
using System.Collections.Generic;

namespace Service.IServices
{
    public interface IDictionaryService : IBaseService
    {
        IEnumerable<Dictionary> GetAllTreeItems();
        Dictionary GetByID(int? ID);

        int? Add(Dictionary role);
        IEnumerable<int?> AddRange(List<Dictionary> roles);

        void Update(Dictionary role);

        void Remove(Dictionary role);
        void RemoveRange(List<Dictionary> roles);
    }
}
