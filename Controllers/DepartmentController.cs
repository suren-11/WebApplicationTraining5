using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTraining5.DB;
using WebApplicationTraining5.Entities;

namespace WebApplicationTraining5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly SqlDb _sqlDb;

        public DepartmentController(SqlDb sqlDb)
        {
            _sqlDb = sqlDb;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            List<Departmant> departmants = await _sqlDb.GetDepartments();
            if (departmants.Count != 0 && departmants != null)
            {
                return Ok(departmants);
            }

            return BadRequest("Departments Empty");
        }

        [HttpPost]
        public async Task<IActionResult> SaveDepartment([FromBody] Departmant departmant)
        {
            if (departmant == null)
            {
                return BadRequest("department is null");
            }
            bool success = await _sqlDb.SaveDepartment(departmant);
            if (success)
            {
                return Ok(new { message = "Department saved successfully", departmant = departmant.Name });
            }
            else
            {

                return BadRequest("Department Not Saved Successfully");
            }
        }

    }
}
