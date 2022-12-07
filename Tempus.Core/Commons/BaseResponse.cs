using System.Text.Json.Serialization;

namespace Tempus.Core.Commons;

public class BaseResponse<T>
{
    public T? Resource { get; set; }

    [JsonIgnore] public StatusCodes StatusCode { get; set; }

    public List<string>? Errors { get; set; }

    public static BaseResponse<T> Ok(T resource)
    {
        return new BaseResponse<T>
        {
            Resource = resource,
            StatusCode = StatusCodes.Ok
        };
    }

    public static BaseResponse<T> NotFound(string resource)
    {
        return new BaseResponse<T>
        {
            StatusCode = StatusCodes.NotFound,
            Errors = new List<string>
            {
                $"{resource} not found"
            }
        };
    }

    public static BaseResponse<T> BadRequest(string message)
    {
        return new BaseResponse<T>
        {
            StatusCode = StatusCodes.BadRequest,
            Errors = new List<string>
            {
                message
            }
        };
    }
}