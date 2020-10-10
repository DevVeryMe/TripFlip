using Newtonsoft.Json;
using System;

namespace GoogleAuthentication.Root.ExceptionHandlingExtensions.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public Exception DroppedException { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
