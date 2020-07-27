using System;
using System.Collections.Generic;

namespace SqlPersonRepository
{
    public class QueryResult<T>
    {
        public ResultType ResultType { get; set; }

        public T Value { get; set; }

        public IEnumerable<T> ValuesCollection { get; set; }

        public string ErrorMsg { get; set; }
    }

    public enum ResultType
    {
        Ok,
        Error,
        Exception
    }
}
