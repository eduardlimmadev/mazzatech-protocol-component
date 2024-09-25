using Protocol.Publisher.Shared.FlowControl.Enums;
using System.Text.Json.Serialization;

namespace Protocol.Publisher.Shared.FlowControl.Models
{
    public class Error
    {
        public ErrorType Type { get; }
        public string Code { get; }
        public string Message { get; }

        [JsonConstructor]
        public Error(ErrorType type, string code, string message)
        {
            Type=type;
            Code=code;
            Message=message;
        }
    }
}
