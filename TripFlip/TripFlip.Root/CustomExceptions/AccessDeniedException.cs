using System;

namespace TripFlip.Root.CustomExceptions
{
    [Serializable]
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException() { }

        public AccessDeniedException(string message)
            : base(message)
        {

        }

        public AccessDeniedException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
