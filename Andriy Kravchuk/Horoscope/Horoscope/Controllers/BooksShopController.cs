using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horoscope.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Repository;
using System.Data.SqlClient;

namespace Horoscope.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksShopController : ControllerBase
    {
        private string _sqlConnectionString;

        private BookRepository _bookRepository;

        public BooksShopController(IConfiguration configuration)
        {
            _sqlConnectionString = configuration.GetConnectionString("DefaultConnection");
            _bookRepository = new BookRepository(_sqlConnectionString);
        }

        [HttpGet]
        [Route("/books")]
        public IEnumerable<Book> GetBooks()
        {
            return _bookRepository.GetBooks();
        }

        [HttpGet]
        [Route("/book")]
        public Book GetBook(int id)
        {
            return _bookRepository.GetBookById(id);
        }

        [HttpPost]
        [Route("/add-book")]
        public Book AddBook(Book book)
        {
            return _bookRepository.Create(book);
        }

        [HttpPut]
        [Route("/update-book")]
        public Book UpdateBook(Book book)
        {
            return _bookRepository.Update(book);
        }

        [HttpDelete]
        [Route("/delete-book")]
        public void DeleteBook(int id)
        {
            _bookRepository.Delete(id);
        }
    }
}
