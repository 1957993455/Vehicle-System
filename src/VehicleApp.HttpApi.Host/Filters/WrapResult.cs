namespace VehicleApp.HttpApi.Host.Filters;

public class WrapResult<T>
{
    public bool Success { get; set; }

    public string Message { get; set; } = null!;

    public T? Data { get; set; }

    public int Code { get; set; }



    public void SetSuccess(T data, string message = "Success", int code = 200)
    {
        Success = true;
        Data = data;
        Code = code;
    }

    public void SetFail(string message = "Fail", int code = 500)
    {
        Success = false;
        Message = message;
        Code = code;
    }
}