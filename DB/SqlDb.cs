using System.Data.SqlClient;
using WebApplicationTraining5.Entities;
using WebApplicationTraining5.Enums;

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
                        employee.Gender = Enum.Parse<GenderEnum>(reader.GetString(5), true);
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
                    sqlCommand.Parameters.AddWithValue("@CreatedDate", employee.created);
                    sqlCommand.Parameters.AddWithValue("@LastUpdatedDate", employee.updated);

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
                    sqlCommand.Parameters.AddWithValue("@LastUpdatedDate", employee.updated);
                    sqlCommand.Parameters.AddWithValue("@Id", employee.Id);

                    int record = await sqlCommand.ExecuteNonQueryAsync();
                    return record > 0;
                }
            }
        }

        public async Task<List<Departmant>> GetDepartments()
        {
            List<Departmant> departmants = new List<Departmant>();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                string sql = "SELECT * FROM departments";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        Departmant departmant = new Departmant();
                        departmant.Id = reader.GetInt32(0);
                        departmant.Name = reader.GetString(1);
                        departmant.Description = reader.GetString(2);
                        departmant.Code = reader.GetString(3);

                        departmants.Add(departmant);
                    }
                }
                sqlConnection.Close();
            }
            return departmants;
        }

        public async Task<bool> SaveDepartment(Departmant departmant)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                string sql = @"INSERT INTO departments ( Name, Description, Code) 
                       VALUES (@Name, @Description, @Code);";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Name", departmant.Name);
                    sqlCommand.Parameters.AddWithValue("@Description", departmant.Description);
                    sqlCommand.Parameters.AddWithValue("@Code", departmant.Code);

                    int record = await sqlCommand.ExecuteNonQueryAsync();
                    return record > 0;
                }
            }
        }
        
        public async Task<List<UserDepartments>> GetUserDepartments()
        {
            List<UserDepartments> userDepartmants = new List<UserDepartments>();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                string sql = "SELECT * FROM userdepartments";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        UserDepartments userDepartment = new UserDepartments();
                        userDepartment.Id = reader.GetInt32(0);
                        userDepartment.UserId = reader.GetInt32(1);
                        userDepartment.DepartmentId = reader.GetInt32(2);
                        userDepartment.CreatedDate = reader.GetDateTime(3);
                        userDepartment.LastUpdatedDate = reader.GetDateTime(4);

                        userDepartmants.Add(userDepartment);
                    }
                }
                sqlConnection.Close();
            }
            return userDepartmants;
        }

        public async Task<bool> SaveUserDepartment(UserDepartments userDepartment)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                string sql = @"INSERT INTO userdepartments ( UserId, DepartmentId, CreatedDate, LastUpdatedDate) 
                       VALUES (@UserId, @DepartmentId, @CreatedDate, @LastUpdatedDate);";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@UserId", userDepartment.UserId);
                    sqlCommand.Parameters.AddWithValue("@DepartmentId", userDepartment.DepartmentId);
                    sqlCommand.Parameters.AddWithValue("@CreatedDate", DateTime.UtcNow);
                    sqlCommand.Parameters.AddWithValue("@LastUpdatedDate", DateTime.UtcNow);

                    int record = await sqlCommand.ExecuteNonQueryAsync();
                    return record > 0;
                }
            }
        }

        public async Task<bool> UpdateUserDepartment(UserDepartments userDepartments)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                string sql = @"UPDATE userdepartments SET UserId = @UserId, DepartmentId = @DepartmentId, LastUpdatedDate = @LastUpdatedDate 
                       WHERE Id = @Id;";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@UserId", userDepartments.UserId);
                    sqlCommand.Parameters.AddWithValue("@DepartmentId", userDepartments.DepartmentId);
                    sqlCommand.Parameters.AddWithValue("@LastUpdatedDate", DateTime.UtcNow);
                    sqlCommand.Parameters.AddWithValue("@Id", userDepartments.Id);

                    int record = await sqlCommand.ExecuteNonQueryAsync();
                    return record > 0;
                }
            }
        }


    }
}
