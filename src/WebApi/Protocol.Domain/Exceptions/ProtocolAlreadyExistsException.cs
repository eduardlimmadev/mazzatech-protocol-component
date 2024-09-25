namespace Protocol.Domain.Exceptions
{
    public class ProtocolAlreadyExistsException : Exception
    {
        public ProtocolAlreadyExistsException()
        { }

        public ProtocolAlreadyExistsException(string message)
            : base(message)
        { }

        public ProtocolAlreadyExistsException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
