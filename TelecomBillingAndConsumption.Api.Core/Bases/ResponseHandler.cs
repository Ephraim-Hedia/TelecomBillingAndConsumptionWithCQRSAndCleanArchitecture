using Microsoft.Extensions.Localization;
using TelecomBillingAndConsumption.Core.Resources;

namespace TelecomBillingAndConsumption.Core.Bases
{
    public class ResponseHandler
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public ResponseHandler(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _localizer = stringLocalizer;
        }
        public Response<T> Updated<T>(T entity, object Meta = null)
        {
            return new Response<T>(entity)
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = _localizer[SharedResourcesKeys.Updated],
                Meta = Meta
            };
        }
        public Response<T> Deleted<T>(string? message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = message == null ? _localizer[SharedResourcesKeys.Deleted] : message,
            };
        }
        public Response<T> Success<T>(T entity, object Meta = null)
        {
            return new Response<T>(entity)
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = _localizer[SharedResourcesKeys.Success],
                Meta = Meta
            };
        }
        public Response<T> Unauthorized<T>(string? message)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = false,
                Message = message == null ? "Unauthorized" : message,
            };
        }
        public Response<T> UnprocessableEntity<T>(string? message)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = message == null ? "Unprocessable Entity" : message,
            };
        }
        public Response<T> BadRequest<T>(string? message)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = message == null ? "Bad Request" : message,
            };
        }
        public Response<T> NotFound<T>(string? message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message == null ? _localizer[SharedResourcesKeys.NotFound] : message,
            };
        }
        public Response<T> Updated<T>()
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = _localizer[SharedResourcesKeys.Updated],
            };
        }
        public Response<T> Created<T>(T entity, object Meta = null)
        {
            return new Response<T>(entity)
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = _localizer[SharedResourcesKeys.Created],
                Meta = Meta
            };
        }
    }

}
