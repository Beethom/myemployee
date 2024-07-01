using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.DataAccess
{
    public class EmployeeConfigManager : IEmployeeConfigManager
    {
        private readonly IConfiguration _configuration;

        public EmployeeConfigManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string EmployeesConnection
        {
            get
            {
                return _configuration["ConnectionStrings:Employee"]!;
            }
        }

        public string GetConnectionString(string connectionName)
        {
            return _configuration.GetConnectionString(connectionName)!;
        }
    }
}


