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
    public class BookShopController : ControllerBase
    {
        private string _sqlConnectionString;

        private BookRepository _bookRepository;

        public BookShopController(IConfiguration configuration)
        {
            _sqlConnectionString = configuration.GetConnectionString("DefaultConnection");
            _bookRepository = new BookRepository(_sqlConnectionString);
        }

        [HttpGet]
        [Route("/books")]
        public IEnumerable<Book> GetBooks()
        {
            return _bookRepository.GetItems();
        }

        [HttpGet]
        [Route("/book")]
        public Book GetBook(int id)
        {
            return _bookRepository.GetItem(id);
        }

        [HttpPost]
        [Route("/add-book")]
        public Book AddBook(Book book)
        {
            return _bookRepository.Create(book);
        }

        [HttpPut]
        [Route("/edit-book")]
        public Book EditBook(Book book)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("/delete-book")]
        public Book DeleteBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
