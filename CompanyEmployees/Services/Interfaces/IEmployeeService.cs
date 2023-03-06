using CompanyEmployees.Data.Models;
using CompanyEmployees.Data.Models.Result.Generics;
using CompanyEmployees.Data.Models.Result.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyEmployees.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IResult<IList<EmployeeModel>>> GetAllEmployeesAsync();

        Task<IResult<int>> CreateEmployeeAsync(EmployeeModel model);
        Task<IResult<EmployeeModel>> GetEmployeeAsync(int employeeId);
        Task<IResult> UpdateEmployeeAsync(int employeeId, EmployeeModel employeeModel);
        Task<IResult> DeleteEmployeeAsync(IReadOnlyCollection<int> employeeIds);
    }
}
