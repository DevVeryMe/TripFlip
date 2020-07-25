using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        IEnumerable<T> GetItems();

        T GetBook(int id);

        void Create(T item);

        void Update(T item);

        void Delete(int id);

        void Save();
    }
}
