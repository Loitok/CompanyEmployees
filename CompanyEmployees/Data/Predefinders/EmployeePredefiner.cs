using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Bogus;
using CompanyEmployees.Data.Contexts;
using CompanyEmployees.Data.Models;
using CompanyEmployees.Data.Models.Result.Generics;
using CompanyEmployees.Data.Models.Result.Implementations;
using CompanyEmployees.Data.Models.Result.Interfaces;
using EFCore.BulkExtensions;

namespace CompanyEmployees.Data.Predefinders
{
    public class EmployeePredefiner : IEmployeePredefiner
    {
        private readonly EmployeeContext _context;

        public EmployeePredefiner(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<IResult> SetAsync()
        {
            try
            {
                await GetEmployees(1000, null, 20000, 50000);
                await GetEmployees(4000, 1000, 15000, 25000);
                await GetEmployees(10000, 5000, 12500, 17000);
                await GetEmployees(15000, 15000, 7500, 14000);
                await GetEmployees(20000, 30000, 2500, 8000);

                return Result.CreateSuccess();
            }
            catch (Exception e)
            {
                return Result.CreateFailure("Get Employees Error", e);
            }
        }


        public async Task<IResult<IList<EmployeeModel>>> GetEmployees(int generatedNumber, int? generatedChiefEmploymentId, int minSalary, int maxSalary)
        {
            try
            {
                var entity = new Faker<EmployeeModel>()
                    .RuleFor(x => x.Name, y => y.Name.FirstName())
                    .RuleFor(x => x.Surname, y => y.Name.LastName())
                    .RuleFor(x => x.MiddleName, y => y.Name.FirstName())
                    .RuleFor(x => x.Position, y => y.Name.JobTitle())
                    .RuleFor(x => x.EmploymentDate,
                        y => y.Date.Between(new DateTime(2010, 1, 1), DateTime.Today))
                    .RuleFor(x => x.SalaryAmount, y => y.Finance.Amount(minSalary, maxSalary))
                    .RuleFor(x => x.Name, y => y.Name.FirstName())
                    .RuleFor(x => x.ChiefEmployeeId, y => generatedChiefEmploymentId.HasValue
                        ? new Random().Next(generatedChiefEmploymentId.Value)
                        : (int?)null);

                await _context.BulkInsertAsync(entity.Generate(generatedNumber));

                return Result<IList<EmployeeModel>>.CreateSuccess();
            }
            catch (Exception e)
            {
                return Result<IList<EmployeeModel>>.CreateFailure("Get Employees Error", e);
            }
        }
    }
}
