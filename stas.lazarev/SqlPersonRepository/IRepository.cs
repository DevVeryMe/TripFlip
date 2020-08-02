using System;
using System.Collections.Generic;

namespace SqlPersonRepository
{
    /// <summary>
    /// Generic interface that performs basic CRUD operations with a given type
    /// </summary>
    public interface IRepository<T>
    {
        public string ConnectionString { get; set; }

        /// <summary>
        /// Creates a new table entry
        /// </summary>
        /// <returns>a new entry</returns>
        public QueryResult<T> Create(T p);

        /// <summary>
        /// Reads table entry by the given id
        /// </summary>
        /// <returns>id and table entry</returns>
        public QueryResult<T> Read(int id);

        /// <summary>
        /// Updates a table entry with the given oject and id
        /// </summary>
        /// <returns>updated entry</returns>
        public QueryResult<T> Update(int id, T p);

        /// <summary>
        /// Removes a table entry by the given id
        /// </summary>
        /// <returns>a boolean result</returns>
        public QueryResult<T> Delete(int id);

        /// <summary>
        /// Returns all table entries
        /// </summary>
        public QueryResult<T> Read();
    }
}
