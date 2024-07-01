using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee.DataAccess.Models;
using Employee.DataAccess.Controllers;
using Employee.DataAccess;
namespace MyEmployeeMVCApp.Models
{
	public class EmployeesViewModel
	{

        private IEmployeeConfigManager _configuration;

        public List<EmployeeModel> EmployeeList { get; set; }

        public EmployeeModel CurrentEmployee { get; set; }

        public bool IsActionSuccess { get; set; }

        public string ActionMessage { get; set; }
        public EmployeesViewModel(IEmployeeConfigManager configuration)
		{
            _configuration = configuration;
            EmployeeList = GetAllEmployees();
            CurrentEmployee = EmployeeList.FirstOrDefault()!;
        }
        public EmployeesViewModel(IEmployeeConfigManager configuration, int empId)
        {
            _configuration = configuration;
            EmployeeList = new List<EmployeeModel>();

            if (empId > 0)
            {
                CurrentEmployee = GetEmployee(empId);
            }
            else
            {
                CurrentEmployee = new EmployeeModel();
            }
        }
        public List<EmployeeModel> GetAllEmployees()
        {
            return EmployeeController.GetAllEmployees(_configuration)!;
        }

        public EmployeeModel GetEmployee(int empId)
        {
            return EmployeeController.GetEmployeeById(empId, _configuration)!;
        }
	}
}



       

       

      
