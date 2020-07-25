using System;
using System.Collections.Generic;
using Horoscope.models;

namespace Repository
{
    public interface IBooksRepository
    {
        IEnumerable<Book> GetBooks();

        Book GetBookById(int id);

        Book Create(Book item);

        Book Update(Book book);

        void Delete(int id);

        void Save();
    }
}
