namespace Usermanagement.Domain
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Data { get; }
        public string Code { get; }
        public string Message { get; set; }

        public Result(bool isSuccess, T value, string code, string message)
        {
            IsSuccess = isSuccess;
            Data = value;
            Code = code;
            Message = message;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value, null, null);
        }

        public static Result<T> Failure(string code, string message)
        {
            return new Result<T>(false, default, code, message);
        }
    }
}