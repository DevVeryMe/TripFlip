using System.Collections.Generic;
using Horoscope.models;

namespace Repository
{
    public class BookRepository : IRepository<Book>
    {
        //db context
        private string _sqlConnectionString;

        public BookRepository(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Book> GetItems()
        {
            throw new System.NotImplementedException();
        }

        public Book GetBook(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Create(Book item)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Book item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
