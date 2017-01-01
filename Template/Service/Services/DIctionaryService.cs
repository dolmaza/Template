using Core.Entities;
using Service.IServices;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services
{
    public class DictionaryService : BaseService, IDictionaryService
    {
        public IEnumerable<Dictionary> GetAllTreeItems()
        {
            var dictionaries = UnitOfWork.DictionaryRepository.GetAll(orderBy: ob => ob.OrderBy(d => d.SortIndex)).ToList();

            return dictionaries;
        }

        public Dictionary GetByID(int? ID)
        {
            var dictionary = UnitOfWork.DictionaryRepository.GetByID(ID);

            return dictionary;
        }

        public int? Add(Dictionary dictionary)
        {
            UnitOfWork.DictionaryRepository.Add(dictionary);
            UnitOfWork.Complate();
            IsError = UnitOfWork.IsError;

            return dictionary.ID;
        }

        public IEnumerable<int?> AddRange(List<Dictionary> dictionaries)
        {
            UnitOfWork.DictionaryRepository.AddRange(dictionaries);
            UnitOfWork.Complate();
            IsError = UnitOfWork.IsError;

            return dictionaries.Select(u => u.ID).ToList();
        }

        public void Update(Dictionary dictionary)
        {
            UnitOfWork.DictionaryRepository.Update(dictionary);
            UnitOfWork.Complate();
            IsError = UnitOfWork.IsError;

        }

        public void Remove(Dictionary dictionary)
        {
            UnitOfWork.DictionaryRepository.Remove(dictionary);
            UnitOfWork.Complate();
            IsError = UnitOfWork.IsError;

        }

        public void RemoveRange(List<Dictionary> dictionaries)
        {
            UnitOfWork.DictionaryRepository.RemoveRange(dictionaries);
            UnitOfWork.Complate();
            IsError = UnitOfWork.IsError;

        }
    }
}
