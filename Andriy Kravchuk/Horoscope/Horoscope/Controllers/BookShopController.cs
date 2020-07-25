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
        public IConfiguration Configuration { get; set; }

        public SqlConnection _sqlConnection;

        public BookShopController(IConfiguration configuration)
        {
            Configuration = configuration;
            _sqlConnection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet]
        [Route("/books")]
        public bool GetBooks()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/book")]
        public Book GetBook(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("/add-book")]
        public Book AddBook(Book book)
        {
            throw new NotImplementedException();
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
