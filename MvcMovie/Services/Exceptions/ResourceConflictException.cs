// File: Exceptions/ResourceConflictException.cs

namespace MvcMovie.Services.Exceptions
{
    public class ResourceConflictException : Exception
    {
        public ResourceConflictException(string message) : base(message)
        {
        }
    }
}