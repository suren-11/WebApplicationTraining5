using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTraining5.DB;
using WebApplicationTraining5.DTO;
using WebApplicationTraining5.Entities;

namespace WebApplicationTraining5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly SqlDb _sqlDb;
        private readonly SqlDbContext _sqlDbContext;
        private readonly IMapper _mapper;

        public DepartmentController(SqlDb sqlDb, IMapper mapper, SqlDbContext sqlDbContext)
        {
            _sqlDb = sqlDb;
            _sqlDbContext = sqlDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            /*List<Departmant> departmants = await _sqlDb.GetDepartments();*/
            List<Departmant> departmants = await _sqlDbContext.GetDepartments();
            if (departmants.Count != 0 && departmants != null)
            {
                var departmentDtos = _mapper.Map<List<DepartmentDTO>>(departmants);
                return Ok(departmentDtos);
            }

            return BadRequest("Departments Empty");
        }

        [HttpPost]
        public async Task<IActionResult> SaveDepartment([FromBody] DepartmentDTO departmantDTO)
        {
            if (departmantDTO == null)
            {
                return BadRequest("department is null");
            }
            Departmant departmant = _mapper.Map<Departmant>(departmantDTO);
            bool success = await _sqlDbContext.SaveDepartment(departmant);
            /*bool success = await _sqlDb.SaveDepartment(departmant);*/
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
