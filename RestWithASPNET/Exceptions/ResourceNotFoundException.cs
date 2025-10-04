namespace RestWithASPNET.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException() : base("Not found!")
        {
        }
        public ResourceNotFoundException(string message) : base(message)
        {
        }
    }
}
