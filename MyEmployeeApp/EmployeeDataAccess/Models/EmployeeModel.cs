namespace Employee.DataAccess.Models
{
    public class EmployeeModel
    {
        public int EmpID { get; set; }
        public string EmpfName { get; set; } = "";
        public string EmplName { get; set; } = "";
        public int EmpSsn { get; set; }
        public DateTime EmpDOB { get; set; }
        public string EmpEmail { get; set; } = "";
        public string EmpRole { get; set; } = "";
        public int EmpSalary { get; set; }
        public string EmpPhone { get; set; } = "";
        public DateTime HiringDate { get; set; }
        public string Manager { get; set; } = "";
            }
}