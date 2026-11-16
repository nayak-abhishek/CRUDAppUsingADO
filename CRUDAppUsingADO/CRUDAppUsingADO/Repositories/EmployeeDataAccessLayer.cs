using CRUDAppUsingADO.Dto;
using CRUDAppUsingADO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CRUDAppUsingADO.Repositories
{
    public class EmployeeDataAccessLayer
    {
        string cs = ConnectionString.dbcs;

        public async Task<List<Employees>> GetAllEmployeesAsync()
        {
            List<Employees> empList = new List<Employees>();

            // with using keyword we don't need to close the connection
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetAllEmployee", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                await con.OpenAsync();

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while(await reader.ReadAsync())
                {
                    //You need to convert values because SqlDataReader returns database values as object, while your
                    //Employees properties expect specific C# data types such as int and string.
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


        public async Task AddEmployeeAsync(EmployeeDto empDto)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spAddEmployee", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", empDto.Name);
                cmd.Parameters.AddWithValue("@gender", empDto.Gender);
                cmd.Parameters.AddWithValue("@age", empDto.Age);
                cmd.Parameters.AddWithValue("@designation", empDto.Designation);
                cmd.Parameters.AddWithValue("@city", empDto.City);

                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }


        public async Task<Employees> GetEmployeeByIdAsync(int? id)
        {
            Employees emp = new Employees();

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select * From Employees Where Id = @id", con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);

                await con.OpenAsync();

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
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


        public async Task UpdateEmployeeAsync(int id, UpdateEmployeeDto empDto)
        {
            Employees emp = await GetEmployeeByIdAsync(id);
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@name", string.IsNullOrWhiteSpace(empDto.Name) ? emp.Name : empDto.Name);
                cmd.Parameters.AddWithValue("@gender", string.IsNullOrWhiteSpace(empDto.Gender) ? emp.Gender : empDto.Gender);
                cmd.Parameters.AddWithValue("@age", empDto.Age ?? emp.Age);
                cmd.Parameters.AddWithValue("@designation", string.IsNullOrWhiteSpace(empDto.Designation) ? emp.Designation : empDto.Designation);
                cmd.Parameters.AddWithValue("@city", string.IsNullOrWhiteSpace(empDto.City) ? emp.City : empDto.City);

                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }


        public async Task DeleteEmployeeAsync(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

    }
}
