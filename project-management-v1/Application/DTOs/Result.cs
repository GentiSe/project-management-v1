﻿namespace project_management_v1.Application.DTOs
{
    /// <summary>
    /// Represents the result of an operation, indicating whether it was successful or failed.
    /// </summary>
    public class Result
    {
        public bool IsSuccess { get; }
        public string? ErrorMessage { get; }

        protected Result(bool isSuccess, string? errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static Result Success() => new(true, null);
        public static Result Failure(string errorMessage) => new (false, errorMessage);
    }

    public class Result<T> : Result
    {       
        public T? Data { get; }

        private Result(bool isSuccess, T? data, string? errorMessage)
            : base(isSuccess, errorMessage)
        {
            Data = data;
        }

        public static Result<T> Success(T data) => new(true, data, null);
        public static Result<T> Failure(string errorMessage) => new(false, default, errorMessage);
    }
}
