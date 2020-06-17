namespace SmartReport.BackEnd.CrossCuttingConcern.Responses
{
    public class BaseResponse
    {

        public int StatusCode { get; set; }

        public BaseResponse(int statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
