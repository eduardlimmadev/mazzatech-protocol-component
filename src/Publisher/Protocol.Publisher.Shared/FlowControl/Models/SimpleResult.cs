namespace Protocol.Publisher.Shared.FlowControl.Models
{
    public class SimpleResult
    {
        public bool HasError => Errors.Any();
        public IEnumerable<Error> Errors { get; private set; }

        private SimpleResult()
        {
            Errors = new List<Error>();
        }

        public static SimpleResult Success()
            => new();

        public static SimpleResult Fail(IEnumerable<Error> errors)
            => new() { Errors = errors };

        public static SimpleResult Fail(Error error)
            => new() { Errors = new List<Error>() { error } };
    }
}
