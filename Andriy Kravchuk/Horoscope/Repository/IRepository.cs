using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetItems();

        T GetItem(int id);

        T Create(T item);

        void Update(T item);

        void Delete(int id);

        void Save();
    }
}
