using System;

namespace TripFlip.Services.Exceptions
{
    public class NoGrantRolePermissionException : ApplicationException
    {
        public NoGrantRolePermissionException()
        {
        }

        public NoGrantRolePermissionException(string name) : base(name)
        {
        }
    }
}
