namespace API.Errors
{
    public class ApiExceptionServices : ApiResponse
    {
        public ApiExceptionServices(int statusCode, string message = null, string details = null) 
            : base(statusCode, message)
        {
            Details = details;
        }

        public string Details { get; set; }
    }
}