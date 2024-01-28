using System.Net;

namespace Elastic.API.Models.DTOs
{
    public record ResponseDto<T>
    {
        public T? Data { get; set; }
        public List<String>? Errors { get; set; }
        public HttpStatusCode Status { get; set; }

        
        // Static Factory Method

        public static ResponseDto<T> Success(T data, HttpStatusCode status)
        {
            return new ResponseDto<T>
            {
                Data = data,
                Status = status
            };
        }

        public static ResponseDto<T> Fail(List<String> errors, HttpStatusCode status)
        {
            return new ResponseDto<T>
            {
                Errors = errors,
                Status = status
            };
        }
    }
}
