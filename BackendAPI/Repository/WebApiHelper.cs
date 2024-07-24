namespace BackendAPI.Repository
{
    public class WebApiHelper
    {
        public class ApiResponseObj
        {
            public string? message { get; set; }
            public object? data { get; set; }
            public string? transactionId { get; set; }
            public bool status { get; set; }

        }
    }
}
