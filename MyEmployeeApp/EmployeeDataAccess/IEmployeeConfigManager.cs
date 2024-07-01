using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.DataAccess
{
    public interface IEmployeeConfigManager
    {
        string EmployeesConnection { get; }

        string GetConnectionString(string connectionName);
    }
}
