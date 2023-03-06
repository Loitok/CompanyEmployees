using CompanyEmployees.Data.Models.Result.Implementations;

namespace CompanyEmployees.Data.Models.Result.Interfaces
{
    public interface IResult
    {
        bool Success { get; }
        ResponseMessage ErrorMessage { get; }
    }
}
