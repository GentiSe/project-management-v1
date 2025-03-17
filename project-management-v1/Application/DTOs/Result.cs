namespace project_management_v1.Application.DTOs
{
    /// <summary>
    /// Represents the result of an operation, indicating whether it was successful or failed.
    /// </summary>
    public class Result
    {
        public bool IsSuccess { get; }
        public string? ErrorMessage { get; }

        private Result(bool isSuccess, string? errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static Result Success() => new(true, null);
        public static Result Failure(string errorMessage) => new (false, errorMessage);
    }
}
