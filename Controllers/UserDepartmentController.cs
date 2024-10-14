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
    public class UserDepartmentController : ControllerBase
    {
        private readonly SqlDb _sqlDb;
        private readonly SqlDbContext _sqlDbContext;
        private readonly IMapper _mapper;

        public UserDepartmentController(SqlDb sqlDb, IMapper mapper, SqlDbContext sqlDbContext)
        {
            _sqlDb = sqlDb;
            _sqlDbContext = sqlDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserDepartments()
        {
            /*List<UserDepartments> userDepartments = await _sqlDb.GetUserDepartments();*/
            List<UserDepartments> userDepartments = await _sqlDbContext.GetUserDepartments();
            if (userDepartments.Count != 0 && userDepartments != null)
            {
                var userDepartmentDtos = _mapper.Map<List<UserDepartmentDTO>>(userDepartments);
                return Ok(userDepartmentDtos);
            }

            return BadRequest("UserDepartment Empty");
        }

        [HttpPost]
        public async Task<IActionResult> SaveUserDepartment([FromBody] UserDepartmentDTO userDepartmentDTO)
        {
            if (userDepartmentDTO == null)
            {
                return BadRequest("userDepartment is null");
            }
            UserDepartments departmant = _mapper.Map<UserDepartments>(userDepartmentDTO);
            departmant.CreatedDate = DateTime.Now;
            departmant.LastUpdatedDate = DateTime.Now;
            bool success = await _sqlDbContext.SaveUserDepartment(departmant);
            /*bool success = await _sqlDb.SaveUserDepartment(userDepartments);*/
            if (success)
            {
                return Ok(new { message = "UserDepartment saved successfully", userDepartments = departmant.Id });
            }
            else
            {

                return BadRequest("UserDepartment Not Saved Successfully");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserDepartment([FromBody] UserDepartmentDTO userDepartmentDTO)
        {
            if (userDepartmentDTO == null)
            {
                return BadRequest("userDepartment is null");
            }
            UserDepartments departmant = _mapper.Map<UserDepartments>(userDepartmentDTO);
            departmant.LastUpdatedDate = DateTime.Now;
            bool success = await _sqlDbContext.UpdateUserDepartment(departmant);
            /*bool success = await _sqlDb.UpdateUserDepartment(userDepartment);*/
            if (success)
            {
                return Ok(new { message = "UserDepartment updated successfully", userDepartment = departmant.Id });
            }
            return BadRequest("UserDepartment Not Updated Successfully");
        }
    }
}

