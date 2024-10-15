using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Core.Configuration;
using WebApplicationTraining5.Entities;

namespace WebApplicationTraining5.DB
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Departmant> Departments { get; set; }
        public DbSet<UserDepartments> UserDepartments { get; set; }

        public async Task<List<Employee>> GetEmployees()
        {
            var emloyeesList = await (from employee in Employees 
                                      select employee).ToListAsync();
            return emloyeesList;
            /*return await Employees.ToListAsync();*/
        }

        public async Task<bool> SaveEmployee(Employee employee)
        {

            await Employees.AddAsync(employee);
            return await SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            var existingEmployee = await Employees.Where(e => e.Id == employee.Id).FirstOrDefaultAsync();
            if (existingEmployee == null)
            {
                return false;
            }
            existingEmployee.Name = employee.Name;
            existingEmployee.Email = employee.Email;
            existingEmployee.DateOfBirth = employee.DateOfBirth;
            existingEmployee.Nic = employee.Nic;
            existingEmployee.Gender = employee.Gender;
            existingEmployee.updated = employee.updated;

            Employees.Update(existingEmployee);
            return await SaveChangesAsync() > 0;
        }

        public async Task<List<Departmant>> GetDepartments()
        {
            var departmentsList = await (from Departmant in Departments 
                                         select Departmant).ToListAsync();
            return departmentsList;
            /*return await Departments.ToListAsync();*/
        }

        public async Task<bool> SaveDepartment(Departmant departmant)
        {
            await Departments.AddAsync(departmant);
            return await SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateDepartment(Departmant departmant)
        {
            var existingDepartment = (from department in Departments 
                                      where department.Id == departmant.Id 
                                      select department).FirstOrDefault();
            if (existingDepartment == null)
            {
                return false;
            }
            existingDepartment.Name = departmant.Name;
            existingDepartment.Description = departmant.Description;
            existingDepartment.Code = departmant.Code;
            
            Departments.Update(existingDepartment);
            return await SaveChangesAsync() > 0;
        }

        public async Task<List<UserDepartments>> GetUserDepartments()
        {
            var userDepartments = await (from useDepartment in UserDepartments 
                                         select useDepartment).ToListAsync();
            return userDepartments;
            /*return await UserDepartments.ToListAsync();*/
        }

        public async Task<bool> SaveUserDepartment(UserDepartments departmant)
        {
            /*var curr = (from userDepartment in UserDepartments 
                        where userDepartment.UserId == departmant.UserId 
                        && userDepartment.IsActive select userDepartment)
                        .FirstOrDefault();*/
            var curr = await UserDepartments
                .Where(ud=>ud.UserId == departmant.UserId && ud.IsActive)
                .FirstOrDefaultAsync();
            if (curr != null)
            {
                curr.IsActive = false;
                UserDepartments.Update(curr);
                await UserDepartments.AddAsync(departmant);
                return await SaveChangesAsync() > 0;
            }

            departmant.IsActive = true;
            await UserDepartments.AddAsync(departmant);
            return await SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUserDepartment(UserDepartments departmant)
        {
            UserDepartments.Update(departmant);
            return await SaveChangesAsync() > 0;
        }
    }
}
