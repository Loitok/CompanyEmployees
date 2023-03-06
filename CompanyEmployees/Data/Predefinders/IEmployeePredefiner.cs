using CompanyEmployees.Data.Models;
using CompanyEmployees.Data.Models.Result.Generics;
using CompanyEmployees.Data.Models.Result.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyEmployees.Data.Predefinders
{
    public interface IEmployeePredefiner
    {
        Task<IResult> SetAsync();
        Task<IResult<IList<EmployeeModel>>> GetEmployees(int generatedNumber, int? generatedChiefEmploymentId, int minSalary, int maxSalary);
    }
}
