namespace Kakadu.WebServer.Core
{
    public class NotSupportedHttpMethodException : ApplicationException
    {
        public NotSupportedHttpMethodException(string httpMethod)
            : base($"HttpMethod {httpMethod} is not supported")
        {
        }
    }
}
