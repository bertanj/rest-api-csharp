namespace RestWithASPNET.Exceptions
{
   
    public class RequiredObjectIsNullException : Exception
    {
        public RequiredObjectIsNullException() : base("It is not allowed to persist a null object!")
        {
        }

        public RequiredObjectIsNullException(string message) : base(message)
        {
        }
    }
}
