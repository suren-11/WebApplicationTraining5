using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using WebApplicationTraining5.DB;
using WebApplicationTraining5.DTO;
using WebApplicationTraining5.Entities;

namespace WebApplicationTraining5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly SqlDb _sqlDb;
        private readonly SqlDbContext _sqlDbContext;
        private readonly IMapper _mapper;

        public EmployeeController(SqlDb sqlDb, IMapper mapper, SqlDbContext sqlDbContext)
        {
            _sqlDb = sqlDb;
            _mapper = mapper;
            _sqlDbContext = sqlDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            List<Employee> employees = await _sqlDbContext.GetEmployees();

            if (employees != null && employees.Any())
            {
                var employeesDtos = _mapper.Map<List<EmployeeDTO>>(employees);
                return Ok(employeesDtos);
            }

            return BadRequest("Employees Empty");
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmployee([FromBody]EmployeeDTO employeeDTO)
        {
            Console.WriteLine(employeeDTO);
            if (employeeDTO == null) 
            {
                return BadRequest("employee is null");
            }
            Employee employee = _mapper.Map<Employee>(employeeDTO);
            employee.created = DateTime.Now;
            employee.updated = DateTime.Now;
            bool success = await _sqlDbContext.SaveEmployee(employee);
            /*bool success = await _sqlDb.SaveEmployee(employee);*/
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
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDTO employeeDTO)
        {
            if (employeeDTO == null)
            {
                return BadRequest("employee is null");
            }

            Employee employee = _mapper.Map<Employee> (employeeDTO);
            employee.updated = DateTime.Now;
            /*bool success = await _sqlDb.UpdateEmployee(employee);*/
            bool success = await _sqlDbContext.UpdateEmployee(employee);
            if (success) 
            {
                return Ok(new { message = "Employee updated successfully", employee = employee.Name });
            }
            return BadRequest("Employee Not Updated Successfully");
        }
    }
}
