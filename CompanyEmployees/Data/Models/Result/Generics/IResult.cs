using CompanyEmployees.Data.Models.Result.Interfaces;

namespace CompanyEmployees.Data.Models.Result.Generics
{
    public interface IResult<out TData> : IResult
    {
        TData Data { get; }
    }
}
