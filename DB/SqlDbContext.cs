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
            return await Employees.ToListAsync();
        }

        public async Task<bool> SaveEmployee(Employee employee)
        {
            await Employees.AddAsync(employee);
            return await SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            Employees.Update(employee);
            return await SaveChangesAsync() > 0;
        }
        
        public async Task<List<Departmant>> GetDepartments()
        {
            return await Departments.ToListAsync();
        }

        public async Task<bool> SaveDepartment(Departmant departmant)
        {
            await Departments.AddAsync(departmant);
            return await SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateDepartment(Departmant departmant)
        {
            Departments.Update(departmant);
            return await SaveChangesAsync() > 0;
        }
        
        public async Task<List<UserDepartments>> GetUserDepartments()
        {
            return await UserDepartments.ToListAsync();
        }

        public async Task<bool> SaveUserDepartment(UserDepartments departmant)
        {
            var curr = await UserDepartments.FirstOrDefaultAsync( du => du.UserId == departmant.UserId && du.IsActive);
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
