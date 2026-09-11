namespace JobySaasFrontend.Models;

public class ServiceResult<T>
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public string? Error { get; set; }

    public T? Data { get; set; }

    public static ServiceResult<T> Ok(
        string message,
        T? data = default)
    {
        return new ServiceResult<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    public static ServiceResult<T> Fail(string error)
    {
        return new ServiceResult<T>
        {
            Success = false,
            Error = error
        };
    }
}

public class PagedResult<T>
{
    public List<T> Items { get; set; } = [];
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public bool HasNextPage { get; set; }
}