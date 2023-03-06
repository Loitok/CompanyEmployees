using CompanyEmployees.Data.Contexts;
using CompanyEmployees.Data.Models;
using CompanyEmployees.Data.Models.Result.Generics;
using CompanyEmployees.Data.Models.Result.Implementations;
using CompanyEmployees.Data.Models.Result.Interfaces;
using CompanyEmployees.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployees.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeContext _context;

        public EmployeeService(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<IResult<IList<EmployeeModel>>> GetAllEmployeesAsync()
        {
            try
            {
                var result = await _context.Employees
                    .AsNoTracking()
                    .ToListAsync();

                return Result<List<EmployeeModel>>.CreateSuccess(result);
            }
            catch (Exception e)
            {
                return Result<IList<EmployeeModel>>.CreateFailure("Get Employees Error", e);
            }
        }

        public async Task<IResult<int>> CreateEmployeeAsync(EmployeeModel model)
        {
            try
            {
                await _context.Employees.AddAsync(model);

                return Result<int>.CreateSuccess(model.EmployeeId);
            }
            catch (Exception e)
            {
                return Result<int>.CreateFailure("Create Employee Error", e);
            }
        }

        public async Task<IResult<EmployeeModel>> GetEmployeeAsync(int employeeId)
        {
            try
            {
                var employee = await _context.Employees
                    .AsNoTracking()
                    .FirstAsync(x => x.EmployeeId == employeeId);

                return Result<EmployeeModel>.CreateSuccess(employee);
            }
            catch (Exception e)
            {
                return Result<EmployeeModel>.CreateFailure("Get Employee Error", e);
            }
        }

        public async Task<IResult> UpdateEmployeeAsync(int employeeId, EmployeeModel employeeModel)
        {
            try
            {
                var employee = await _context.Employees
                    .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);

                //_mapper.Map(employeeModel, employee);
                await _context.SaveChangesAsync();

                return Result.CreateSuccess();
            }
            catch (Exception e)
            {
                return Result.CreateFailure("Update Employee Error", e);
            }
        }

        public async Task<IResult> DeleteEmployeeAsync(IReadOnlyCollection<int> employeeIds)
        {
            try
            {
                var employees = await _context.Employees
                    .Where(x => employeeIds.Contains(x.EmployeeId))
                    .ToListAsync();


                _context.Employees.RemoveRange(employees);

                await _context.SaveChangesAsync();

                return Result.CreateSuccess();
            }
            catch (Exception e)
            {
                return Result.CreateFailure("Delete Employee Error", e);
            }
        }

    }
}
