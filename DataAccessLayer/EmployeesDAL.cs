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
                con.Open(); 
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

        public void AddEmployee(Employees obj)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spAddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", obj.Name);
                cmd.Parameters.AddWithValue("@gender", obj.Gender);
                cmd.Parameters.AddWithValue("@age", obj.Age);
                cmd.Parameters.AddWithValue("@designation", obj.Designation);
                cmd.Parameters.AddWithValue("@city", obj.City);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Employees getEmployeeById(int? id)
        {
            Employees emp = new Employees();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select * from Employees where id = @id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                { 
                    emp.Id = Convert.ToInt32(reader["Id"]);
                    emp.Name = reader["name"].ToString() ?? "";
                    emp.Gender = reader["gender"].ToString() ?? "";
                    emp.Age = Convert.ToInt32(reader["age"]);
                    emp.Designation = reader["designation"].ToString() ?? "";
                    emp.City = reader["city"].ToString() ?? ""; 
                }
            }
            return emp;
        }

        public void UpdateEmployee(Employees obj)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", obj.Id);
                cmd.Parameters.AddWithValue("@name", obj.Name);
                cmd.Parameters.AddWithValue("@gender", obj.Gender);
                cmd.Parameters.AddWithValue("@age", obj.Age);
                cmd.Parameters.AddWithValue("@designation", obj.Designation);
                cmd.Parameters.AddWithValue("@city", obj.City);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteEmployee(int? id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id); 
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}