using Customize.Contracts.Base;
using Customize.Contracts.Customer;
using Customize.Domain.DataObject;
using Customize.Domain.DataObject.Query;

namespace Customize.Contracts.Extensions
{
    public static class CustomerContractsMapping
    {
        public static Domain.Entities.Customer Map(this PostCustomerRequest request)
        {
            return new Domain.Entities.Customer(Guid.NewGuid().ToString(), request.Name, request.Cellphone, request.Email, DateTime.Now);
        }

        public static Domain.Entities.Customer Map(this PutCustomerRequest request)
        {
            return new Domain.Entities.Customer(request.Id, request.Name, request.Cellphone, request.Email);
        }

        public static CustomerQueryParam Map(this ListCustomerRequest request)
        {
            return new CustomerQueryParam(request.DateRange)
            {
                Id = request.Id,
                Name = request.Name,
                Limit = request.Limit,
                PaginationToken = request.PaginationToken,
            };
        }

        public static BaseResponse MapBaseResponse(this Result result, string? message = null) 
        {
            return new BaseResponse
            {
                Success = result.Sucess,
                Errors = result.Errors,
                Message = message
            };
        }

        public static BaseResponse<T> MapBaseResponse<T>(this Result<T> result, string? message = null)
        {
            return new BaseResponse<T>
            {
                Success = result.Sucess,
                Errors = result.Errors,
                Message = message,
                Data = result.Data
            };
        }

        public static ListCustomerRequest MapListCustomerRequest(this IDictionary<string, string> queryParams)
        {
            queryParams.TryGetValue("id", out var id);
            queryParams.TryGetValue("name", out var name);
            queryParams.TryGetValue("limit", out var limit);
            queryParams.TryGetValue("start", out var startDate);
            queryParams.TryGetValue("end", out var endDate);
            queryParams.TryGetValue("paginationToken", out var paginationToken);

            var dateRange =new  DateRangeQueryParam(Convert.ToDateTime(startDate), Convert.ToDateTime(endDate));    

            return new ListCustomerRequest(dateRange)
            {
                Id = id,
                Name = name,
                Limit = Convert.ToInt32(limit),
                PaginationToken = paginationToken
            };
        }
        public static CustomerQueryParam MapCustomerQueryParam(this ListCustomerRequest queryParams)
        {
            return new CustomerQueryParam(queryParams.DateRange)
            {
                Id = queryParams.Id,
                Name = queryParams.Name,
                Limit = queryParams.Limit,
                PaginationToken = queryParams.PaginationToken   
            };
        }
    }
}
