using System;
using System.Collections.Generic;

namespace CompanyEmployees.Data.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }

        public string Position { get; set; }

        public DateTime EmploymentDate { get; set; }
        public decimal SalaryAmount { get; set; }

        public int? ChiefEmployeeId { get; set; }
        public EmployeeModel ChiefEmployee { get; set; }

        public List<EmployeeModel> Subordinates { get; set; }
    }
}
