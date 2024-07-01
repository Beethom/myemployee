using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyEmployeeMVCApp.Models;
using Employee.DataAccess.Models;
using Employee.DataAccess.Controllers;
using Employee.DataAccess;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyEmployeeMVCApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeConfigManager _configuration;
        public EmployeesController(IEmployeeConfigManager configuration)
        {
            _configuration = configuration;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            EmployeesViewModel model = new EmployeesViewModel(_configuration);
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(int empId, string empfName, string emplName, string empssn, string empdob, string empemail, string emprole, int empsalary, string empphone, string hiringdate, string manager)
        {
            if (empId > 0)
            {
                EmployeeController.UpdateEmployee(empId, empfName, emplName, empssn, empdob, empemail, emprole, empsalary, empphone, hiringdate, manager, _configuration);

            }
            else
            {
                EmployeeController.CreateEmployee(empfName, emplName, empssn, empdob, empemail, emprole, empsalary, empphone, hiringdate, manager, _configuration);
            }
            EmployeesViewModel model = new EmployeesViewModel(_configuration);
            model.IsActionSuccess = true;
            model.ActionMessage = "Employee has been saved";

            return View(model);
        }
        public IActionResult Update (int id)
        {
            EmployeesViewModel model = new EmployeesViewModel(_configuration, id);
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                EmployeeController.DeleteEmployee(id, _configuration);
                
            }

            EmployeesViewModel model = new EmployeesViewModel(_configuration);
            model.IsActionSuccess = true;
            model.ActionMessage = "Employee has been deleted successfully";
            return View("Index", model);
        }


    }
}



   

      
       

