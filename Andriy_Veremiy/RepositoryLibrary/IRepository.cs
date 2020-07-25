using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLibrary
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        IEnumerable<T> GetObjectList();
        T GetObject(int id); 
        T Create(T item); 
        T Update(int id, T item); 
        void Delete(int id); 
        void Save();
    }
}
