using MediatR;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace MicroserviceProj.Shared
{
    public interface IRequestByServiceResult<T> : IRequest<ServiceResult<T>>;
    public interface IRequestByServiceResult : IRequest<ServiceResult>;

    public class ServiceResult
    {
        [JsonIgnore]
        public HttpStatusCode Status { get; set; }
        public ProblemDetails? Fail { get; set; }

        [JsonIgnore]
        public bool IsSuccess => Fail is null;

        [JsonIgnore]
        public bool IsFail => !IsSuccess;



        //Static Factory Methods
        public static ServiceResult SuccessAsNoContent()
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.NoContent            
            };
        }
        public static ServiceResult ErrorAsNotFound() 
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.NotFound,
                Fail = new ProblemDetails
                {
                    Title = "Error",
                    Detail = "The request resource was not found"
                }
            };
        }
        public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode statusCode)
        {
            return new ServiceResult
            {
                Fail = problemDetails,
                Status = statusCode
            };
        }

        public static ServiceResult Error(HttpStatusCode statusCode, string title, string detail)
        {
            return new ServiceResult
            {
                Status = statusCode,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Detail = detail,
                    Status = statusCode.GetHashCode()
                }
            };
        }

        public static ServiceResult Error(HttpStatusCode statusCode, string title)
        {
            return new ServiceResult
            {
                Status = statusCode,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Status = statusCode.GetHashCode()
                }
            };
        }

        public static ServiceResult ErrorFromValidation(IDictionary<string, object?> errors)
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails()
                {
                    Title = "Validation error occured",
                    Detail = "Please check the error property for more details",
                    Status = HttpStatusCode.BadRequest.GetHashCode(),
                    Extensions = errors
                }
            };
        }

        public static ServiceResult ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult
                {
                    Fail = new ProblemDetails()
                    {
                        Title = exception.Message
                    },
                    Status = exception.StatusCode
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

            return new ServiceResult
            {
                Fail = problemDetails,
                Status = exception.StatusCode
            };
        }
    }


    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        [JsonIgnore]
        public string? UrlAsCreated { get; set; }


        public static ServiceResult<T> SuccessAsOk(T data)
        {
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.OK,
                Data = data,
            };
        }

        public static ServiceResult<T> SuccessAsCreated(T data, string url)
        {
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.Created,
                Data = data,
                UrlAsCreated = url
            };
        }

        public new static ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode statusCode)
        {
            return new ServiceResult<T>
            {
                Fail = problemDetails,
                Status = statusCode
            };
        }

        public new static ServiceResult<T> Error(HttpStatusCode statusCode, string title, string detail)
        {
            return new ServiceResult<T>
            {
                Status = statusCode,
                Fail = new ProblemDetails() 
                {
                    Title = title,
                    Detail = detail,
                    Status = statusCode.GetHashCode()
                }
            };
        }

        public new static ServiceResult<T> Error(HttpStatusCode statusCode, string title)
        {
            return new ServiceResult<T>
            {
                Status = statusCode,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Status = statusCode.GetHashCode()
                }
            };
        }

        public new static ServiceResult<T> ErrorFromValidation(IDictionary<string,object?> errors)
        {
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails()
                {
                    Title = "Validation error occured",
                    Detail = "Please check the error property for more details",
                    Status = HttpStatusCode.BadRequest.GetHashCode(),
                    Extensions = errors
                }
            };
        }

        public new static ServiceResult<T> ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult<T>
                {
                    Fail = new ProblemDetails()
                    {
                        Title = exception.Message
                    },
                    Status = exception.StatusCode
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

            return new ServiceResult<T>
            {
                Fail = problemDetails,
                Status = exception.StatusCode
            };
        }
    }
}
