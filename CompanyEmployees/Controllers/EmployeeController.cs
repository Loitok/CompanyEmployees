using CompanyEmployees.Data.Models;
using CompanyEmployees.Data.Predefinders;
using CompanyEmployees.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CompanyEmployees.Data.Contexts;
using CompanyEmployees.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace CompanyEmployees.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeePredefiner _employeePredefiner;

        public EmployeeController(IEmployeeService employeeService, IEmployeePredefiner employeePredefiner)
        {
            _employeeService = employeeService;
            _employeePredefiner = employeePredefiner;
        }

        public ActionResult EmployeesTree()
            => View();
        
        public async Task<ActionResult> Nodes()
        {
            var result = await _employeeService.GetAllEmployeesAsync();
            if (!result.Success)
                ViewBag.error = result.ErrorMessage.Message;

            var nodes = new List<GetEmployeeViewModel>();

            foreach (var item in result.Data)
            {
                nodes.Add(new GetEmployeeViewModel
                {
                    Id = item.EmployeeId,
                    FullName = $"{item.Name} {item.Surname}",
                    Position = item.Position,
                    Parent = item.ChiefEmployeeId.HasValue ? item.ChiefEmployeeId.ToString() : "#"
                });
            }

            return Json(nodes);
        }

        public async Task<ActionResult> EmployeesList()
        {
            var result = await _employeeService.GetAllEmployeesAsync();
            if (!result.Success)
                ViewBag.error = result.ErrorMessage.Message;

            return View(result.Data);
        }

        public async Task<ActionResult> SetPredefiner()
        {
            var result = await _employeePredefiner.SetAsync();
            if (!result.Success)
                ViewBag.error = result.ErrorMessage.Message;

            return View();
        }

        public async Task<ActionResult> CreateEmployee(EmployeeModel model)
        {
            var result = await _employeeService.CreateEmployeeAsync(model);
            if (!result.Success)
                ViewBag.error = result.ErrorMessage.Message;

            return Json(result.Data);
        }

        public async Task<ActionResult> GetEmployee(int employeeId)
        {
            var result = await _employeeService.GetEmployeeAsync(employeeId);
            if (!result.Success)
                ViewBag.error = result.ErrorMessage.Message;

            return Json(result.Data);
        }

        public async Task<ActionResult> UpdateEmployee(int employeeId, EmployeeModel model)
        {
            var result = await _employeeService.UpdateEmployeeAsync(employeeId, model);
            if (!result.Success)
                ViewBag.error = result.ErrorMessage.Message;

            return Json("Update Employee Success!");
        }

        public async Task<ActionResult> DeleteEmployee(IReadOnlyCollection<int> employeeIds)
        {
            var result = await _employeeService.DeleteEmployeeAsync(employeeIds);
            if (!result.Success)
                ViewBag.error = result.ErrorMessage.Message;

            return Json("Delete Employees Success!");
        }
    }
}
