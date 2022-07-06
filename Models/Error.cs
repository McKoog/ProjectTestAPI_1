namespace ProjectTestAPI_1.Models
{
    public class Error
    {
        public Error(int errorCode, string errorMessage, string userErrorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            UserErrorMessage = userErrorMessage;
        }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string UserErrorMessage { get; set; }
    }
}