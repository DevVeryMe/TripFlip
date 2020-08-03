using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public ActionResult<IEnumerable<Person>> Get()
        {
            person = new SqlPersonRepo(ConnectionString);

            var result = person.Read();

            if (result.ResultType != ResultType.Ok)
            {
                if (result.ResultType == ResultType.Exception) ; // todo exception handling
                if (result.ResultType == ResultType.Error)
                    return NotFound(result.ErrorMsg);
            }

            return Ok(result.ValuesCollection);
        }

        /// <summary>
        /// Returns an entry from 'Person' table with a given id
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<Person> Get(int id)
        {
            person = new SqlPersonRepo(ConnectionString);

            var result = person.Read(id);

            if (result.ResultType != ResultType.Ok)
            {
                if (result.ResultType == ResultType.Exception) ; // todo exception handling
                if (result.ResultType == ResultType.Error)
                    return NotFound(result.ErrorMsg);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Adds a new table entry with a given object
        /// </summary>
        [HttpPost]
        public ActionResult<Person> Post(Person p)
        {
            person = new SqlPersonRepo(ConnectionString);

            var result = person.Create(p);

            if (result.ResultType != ResultType.Ok)
            {
                if (result.ResultType == ResultType.Exception) ; // todo exception handling
                if (result.ResultType == ResultType.Error)
                    return BadRequest(result.ErrorMsg);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Modifies a table entry by the given id with a given object
        /// </summary>
        [HttpPut]
        public ActionResult<Person> Put(int id, Person p)
        {
            person = new SqlPersonRepo(ConnectionString);

            var result = person.Update(id, p);

            if (result.ResultType != ResultType.Ok)
            {
                if (result.ResultType == ResultType.Exception) ; // todo exception handling
                if (result.ResultType == ResultType.Error)
                    return BadRequest(result.ErrorMsg);
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Deletes a table entry by the given id
        /// </summary>
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            person = new SqlPersonRepo(ConnectionString);

            var result = person.Delete(id);

            if (result.ResultType != ResultType.Ok)
            {
                if (result.ResultType == ResultType.Exception) ; // todo exception handling
                if (result.ResultType == ResultType.Error)
                    return BadRequest(result.ErrorMsg);
            }

            return Ok($"Entry #{id} is now deleted");
        }
    }
}
