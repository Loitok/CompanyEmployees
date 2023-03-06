using System.Collections.Generic;

namespace CompanyEmployees.ViewModels
{
    public class GetEmployeeViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Parent { get; set; }
    }
}
