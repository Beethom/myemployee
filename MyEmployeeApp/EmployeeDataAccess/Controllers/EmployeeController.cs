using Employee.DataAccess;
using Employee.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Employee.DataAccess.Controllers
{
    public class EmployeeController
    {
        public static int CreateEmployee(string empfName, string emplname, string empssn, string empdob, string empemail, string emprole, int empsalary, string empphone, string hiringdate, string manager, IEmployeeConfigManager configManager)
        {
            string sqlConnectionString = configManager.EmployeesConnection;
            int empId = 0;

            string insertSqlCommand = @"INSERT INTO EMPLOYEES(EMPFNAME,

EMPLNAME,
EMPSSN,
EMPDOB,EMPEMAIL,EMPROLE,EMPSALARY,EMPPHONE,HIRINGDATE,MANAGER)
OUTPUT INSERTED.EMPID
VALUES
(@EMPFNAME,@EMPLNAME,@EMPSSN,@EMPDOB,@EMPEMAIL,@EMPROLE,@EMPSALARY,@EMPPHONE,@HIRINGDATE,@MANAGER

                            





                                            )";

            using(SqlConnection sqlConnection =new SqlConnection(sqlConnectionString))
            {
                using(SqlCommand sqlCommand = new SqlCommand(insertSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@EMPFNAME", empfName));
                    sqlCommand.Parameters.Add(new SqlParameter("@EMPLNAME", emplname));
                    sqlCommand.Parameters.Add(new SqlParameter("@EMPSSN", empssn));
                    sqlCommand.Parameters.Add(new SqlParameter("@EMPDOB", empdob));
                    sqlCommand.Parameters.Add(new SqlParameter("@EMPEMAIL", empemail));
                    sqlCommand.Parameters.Add(new SqlParameter("@EMPROLE", emprole));
                    sqlCommand.Parameters.Add(new SqlParameter("@EMPSALARY", empsalary));
                    sqlCommand.Parameters.Add(new SqlParameter("EMPPHONE", empphone));
                    sqlCommand.Parameters.Add(new SqlParameter("@HIRINGDATE", hiringdate));
                    sqlCommand.Parameters.Add(new SqlParameter("MANAGER", manager));


                    sqlCommand.Connection.Open();
                    empId = (int)sqlCommand.ExecuteScalar();
                    sqlCommand.Connection.Close();
                }
            }
            return empId;
        }
        public static int UpdateEmployee(int empId, string empfName, string emplname, string empssn, string empdob, string empemail, string emprole, int empsalary, string empphone, string hiringdate, string manager, IEmployeeConfigManager configManager)
        {
            string sqlConnectionString = configManager.EmployeesConnection;
            string updateSqlCommand = @"UPDATE EMPLOYEES
                            SET EMPFNAME  =@EMPFNAME,
                                EMPLNAME =@EMPLNAME,
EMPSSN =@EMPSSN,
EMPDOB =@EMPDOB,
EMPEMAIL =@EMPEMAIL,
EMPSALARY =@EMPSALARY,
EMPPHONE =@EMPPHONE,
HIRINGDATE =@HIRINGDATE,
MANAGER =@MANAGER";

            using(SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
               using(SqlCommand sqlCommand = new SqlCommand(updateSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@EMPFNAME", empfName));
                    sqlCommand.Parameters.Add(new SqlParameter("@EMPLNAME", emplname));
                    sqlCommand.Parameters.Add(new SqlParameter("@EMPSSN", empssn));
                    sqlCommand.Parameters.Add(new SqlParameter("@EMPDOB", empdob));
                    sqlCommand.Parameters.Add(new SqlParameter("@EMPEMAIL", empemail));
                    sqlCommand.Parameters.Add(new SqlParameter("@EMPSALARY", empsalary));
                    sqlCommand.Parameters.Add(new SqlParameter("EMPPHONE", empphone));
                    sqlCommand.Parameters.Add(new SqlParameter("@HIRINGDATE", hiringdate));
                    sqlCommand.Parameters.Add(new SqlParameter("MANAGER", manager));


                    sqlCommand.Connection.Open();
                    empId = (int)sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return empId;
        }

        public static bool DeleteEmployee(int empId, IEmployeeConfigManager configManager)
        {
            string sqlConnectionString = configManager.EmployeesConnection;
            string deleteSqlCommand = @"DELETE FROM EMPLOYEES WHERE EMPID = @EMPID";
            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(deleteSqlCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@EMPID", empId));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
            }
            return true;
        }
        public static List<EmployeeModel>? GetAllEmployees(IEmployeeConfigManager configManager)
        {
            string sqlConnectionString = configManager.EmployeesConnection;
            List<EmployeeModel> employeeList = new();

            string querySql = "SELECT * FROM EMPLOYEES ORDER BY EMPID DESC";
            using (SqlConnection sqlConnection = new(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new(querySql, sqlConnection))
                {
                    using (SqlDataAdapter sqlDataAdapter = new(sqlCommand))
                    {
                        using (DataTable dataTable = new())
                        {
                            sqlDataAdapter.Fill(dataTable);

                            EmployeeModel employeeModel;

                            foreach (DataRow dataRow in dataTable.Rows)
                            {
                                employeeModel = new();

                                employeeModel.EmpID = Convert.ToInt32(dataRow["EMPID"]);
                                employeeModel.EmpfName = dataRow["EMPFNAME"].ToString() ?? "";
                                employeeModel.EmplName = dataRow["EMPLNAME"].ToString() ?? "";
                                employeeModel.EmpSsn = Convert.ToInt32(dataRow["EMPSSN"]);
                                employeeModel.EmpDOB = Convert.ToDateTime(dataRow["EMPDOB"]);
                                employeeModel.EmpEmail = dataRow["EMPEMAIL"].ToString() ?? "";
                                employeeModel.EmpSalary = Convert.ToInt32(dataRow["EMPSALARY"]);
                                employeeModel.EmpPhone = dataRow["EMPPHONE"].ToString() ?? "";
                                employeeModel.HiringDate = Convert.ToDateTime(dataRow["HIRINGDATE"]);
                                employeeModel.Manager = dataRow["MANAGER"].ToString() ?? "";

                                employeeList.Add(employeeModel);

                                
                            }
                        }
                    }
                }

            }
            return employeeList;

        }

          

        public static EmployeeModel? GetEmployeeById(int empId, IEmployeeConfigManager configManager)
        {
            string sqlConnectionString = configManager.EmployeesConnection;
            EmployeeModel employeeModel = new();

            string querySql = "SELECT * FROM EMPLOYEES WHERE EMPID = @EMPID";

            using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(querySql, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@EMPID", empId));

                    sqlConnection.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            employeeModel.EmpID = Convert.ToInt32(reader["EMPID"]);
                            employeeModel.EmpfName = reader["EMPFNAME"].ToString() ?? "";
                            employeeModel.EmplName = reader["EMPLNAME"].ToString() ?? "";
                            employeeModel.EmpSsn = Convert.ToInt32(reader["EMPSSN"]);
                            employeeModel.EmpEmail = reader["EMPEMAIL"].ToString() ?? "";
                            employeeModel.EmpSalary = Convert.ToInt32(reader["EMPSALARY"]);
                            employeeModel.EmpPhone = reader["EMPPHONE"].ToString() ?? "";
                            employeeModel.HiringDate = Convert.ToDateTime(reader["HIRINGDATE"]);
                            employeeModel.Manager = reader["MANAGER"].ToString() ?? "";

                          
                        }
                        else
                        {
                            throw new Exception("No rows found.");
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return employeeModel;
        }
    }
}