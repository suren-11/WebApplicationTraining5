using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTraining5.DB;
using WebApplicationTraining5.Entities;

namespace WebApplicationTraining5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly SqlDb _sqlDb;

        public EmployeeController(SqlDb sqlDb)
        {
            _sqlDb = sqlDb;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            List<Employee> employees = await _sqlDb.GetEmployees();
            if (employees.Count != 0 && employees != null)
            {
                return Ok(employees);
            }

            return BadRequest("Employees Empty");
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmployee([FromBody]Employee employee)
        {
            if (employee == null) 
            {
                return BadRequest("employee is null");
            }
            bool success = await _sqlDb.SaveEmployee(employee);
            if (success) 
            {
                return Ok(new { message = "Employee saved successfully", employee = employee.Name });
            }
            else
            {

            return BadRequest("Employee Not Saved Successfully");
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            bool success = await _sqlDb.UpdateEmployee(employee);
            if (success) 
            {
                return Ok(new { message = "Employee updated successfully", employee = employee.Name });
            }
            return BadRequest("Employee Not Updated Successfully");
        }
    }
}
