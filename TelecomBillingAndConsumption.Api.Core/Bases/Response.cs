using System.Net;

namespace TelecomBillingAndConsumption.Core.Bases
{
    public class Response<T>
    {
        public Response()
        {

        }
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public Response(string message, bool succeeded)
        {
            Succeeded = succeeded;
            Message = message;
        }
        public object Meta { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
