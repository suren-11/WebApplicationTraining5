using System.Data.SqlClient;
using WebApplicationTraining5.Entities;

namespace WebApplicationTraining5.DB
{
    public class SqlDb
    {
        private readonly string _connectionString = "Data Source=DESKTOP-5B2B64F\\SQLEXPRESS;Initial Catalog=webtraining5;Integrated Security=True;Encrypt=False";

        public async Task<List<Employee>> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                string sql = "SELECT * FROM employees";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        Employee employee = new Employee();
                        employee.Id = reader.GetInt32(0);
                        employee.Nic = reader.GetString(1);
                        employee.Name = reader.GetString(2);
                        employee.Email = reader.GetString(3);
                        employee.DateOfBirth = reader.GetDateTime(4);
                        employee.Gender = reader.GetString(5);
                        employee.created = reader.GetDateTime(6);
                        employee.updated = reader.GetDateTime(7);

                        employees.Add(employee);
                    }
                }
                sqlConnection.Close();
            }
            return employees;
        }

        public async Task<bool> SaveEmployee(Employee employee)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                string sql = @"INSERT INTO employees (NIC, Name, Email, DateOfBirth, Gender, CreatedDate, LastUpdatedDate) 
                       VALUES (@NIC, @Name, @Email, @DateOfBirth, @Gender, @CreatedDate, @LastUpdatedDate);";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@NIC", employee.Nic);
                    sqlCommand.Parameters.AddWithValue("@Name", employee.Name);
                    sqlCommand.Parameters.AddWithValue("@Email", employee.Email);
                    sqlCommand.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                    sqlCommand.Parameters.AddWithValue("@Gender", employee.Gender);
                    sqlCommand.Parameters.AddWithValue("@CreatedDate", DateTime.UtcNow);
                    sqlCommand.Parameters.AddWithValue("@LastUpdatedDate", DateTime.UtcNow);

                    int record = await sqlCommand.ExecuteNonQueryAsync();
                    return record > 0;
                }
            }
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                string sql = @"UPDATE employees SET NIC = @NIC, Name = @Name, Email = @Email, DateOfBirth = @DateOfBirth, Gender = @Gender, LastUpdatedDate = @LastUpdatedDate 
                       WHERE Id = @Id;";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@NIC", employee.Nic);
                    sqlCommand.Parameters.AddWithValue("@Name", employee.Name);
                    sqlCommand.Parameters.AddWithValue("@Email", employee.Email);
                    sqlCommand.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                    sqlCommand.Parameters.AddWithValue("@Gender", employee.Gender);
                    sqlCommand.Parameters.AddWithValue("@LastUpdatedDate", DateTime.UtcNow);
                    sqlCommand.Parameters.AddWithValue("@Id", employee.Id);

                    int record = await sqlCommand.ExecuteNonQueryAsync();
                    return record > 0;
                }
            }
        }


    }
}
