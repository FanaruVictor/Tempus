using System.Text.Json.Serialization;

namespace Tempus.Core.Commons;

public class BaseResponse<T>
{
    public T? Resource { get; set; }

    [JsonIgnore] public StatusCodes StatusCode { get; set; }

    public List<string>? Errors { get; set; }

    public static BaseResponse<T> Ok(T resource = default)
    {
        return new BaseResponse<T>
        {
            Resource = resource,
            StatusCode = StatusCodes.Ok
        };
    }
    
    public static BaseResponse<T> Created(T resource = default)
    {
        return new BaseResponse<T>
        {
            Resource = resource,
            StatusCode = StatusCodes.Created
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

    public static BaseResponse<T> BadRequest(List<string> message)
    {
        return new BaseResponse<T>
        {
            StatusCode = StatusCodes.BadRequest,
            Errors = message
        };
    }
    
    public static BaseResponse<T> Unauthorized()
    {
        return new BaseResponse<T>
        {
            StatusCode = StatusCodes.Unauthorized,
            Errors = new List<string>
            {
                "Unauthorized"
            }
        };
    }
}