using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using SqlPersonRepository;

namespace WebAPI_Horoscope.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        string ConnectionString;
        SqlPersonRepo person;

        public PersonController(IConfiguration config)
        {
            ConnectionString = config.GetConnectionString("Local_MSSQL_DB");
        }

        /// <summary>
        /// Returns all entries from 'Person' table
        /// </summary>
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            person = new SqlPersonRepo(ConnectionString);

            var result = person.Read();
            if (CheckResultOnErrors(result))
                return null;

            return result.ValuesCollection;
        }

        /// <summary>
        /// Returns an entry from 'Person' table with a given id
        /// </summary>
        [HttpGet("{id}")]
        public Person Get(int id)
        {
            person = new SqlPersonRepo(ConnectionString);

            var result = person.Read(id);
            if (CheckResultOnErrors(result))
                return null;

            return result.Value;
        }

        [HttpPost]
        public Person Post(Person p)
        {
            person = new SqlPersonRepo(ConnectionString);

            var result = person.Create(p);

            if (CheckResultOnErrors(result))
                return null;

            return result.Value;
        }

        [HttpPut]
        public Person Put(int id, Person p)
        {
            person = new SqlPersonRepo(ConnectionString);

            var result = person.Update(id, p);

            if (CheckResultOnErrors(result))
                return null;

            return result.Value;
        }

        [HttpDelete]
        public string Delete(int id)
        {
            person = new SqlPersonRepo(ConnectionString);

            var result = person.Delete(id);

            if (CheckResultOnErrors(result))
                return null;

            return $"Entry #{id} is now deleted";
        }

        /// <summary>
        /// Returns TRUE if ResultType is anything but 'OK' and sets up a corresponding HTTP Status Code
        /// </summary>
        bool CheckResultOnErrors(QueryResult<Person> result)
        {
            bool flag = false;

            if (result.ResultType == ResultType.Error)
            {
                //new ErrorHandler(person);
                flag = true;
                Response.StatusCode = 400;
            }
            else if (result.ResultType == ResultType.Exception)
            {
                //new ErrorHandler(person);
                flag = true;
                Response.StatusCode = 500;
            }

            return flag;
        }
    }
}
