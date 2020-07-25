using System.Collections.Generic;
using Repository.models;

namespace Repository
{
    public interface IBooksRepository
    {
        IEnumerable<Book> GetBooks();

        Book GetBookById(int id);

        Book Create(Book item);

        Book Update(Book book);

        bool Delete(int id);
    }
}
