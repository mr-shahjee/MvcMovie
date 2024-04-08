using utilities;
using System.Data.SqlClient; 
using System.Data;
using Models;
namespace DataAccessLayer
{

    public class EmployeesDAL
    {
        string cs = ConnectionString.dbcs;
        public List<Employees> getAllEmployees()
        {
            List<Employees> empList = new List<Employees>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetAllEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Employees emp = new Employees();
                    emp.Id = Convert.ToInt32(reader["Id"]);
                    emp.Name = reader["name"].ToString() ?? "";
                    emp.Gender = reader["gender"].ToString() ?? "";
                    emp.Age = Convert.ToInt32(reader["age"]);
                    emp.Designation = reader["designation"].ToString() ?? "";
                    emp.City = reader["city"].ToString() ?? "";
                    empList.Add(emp);
                }
            }
            return empList;
        }
    }
}