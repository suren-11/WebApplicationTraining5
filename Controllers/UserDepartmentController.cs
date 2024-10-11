using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTraining5.DB;
using WebApplicationTraining5.Entities;

namespace WebApplicationTraining5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDepartmentController : ControllerBase
    {
        private readonly SqlDb _sqlDb;

        public UserDepartmentController(SqlDb sqlDb)
        {
            _sqlDb = sqlDb;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserDepartments()
        {
            List<UserDepartments> userDepartments = await _sqlDb.GetUserDepartments();
            if (userDepartments.Count != 0 && userDepartments != null)
            {
                return Ok(userDepartments);
            }

            return BadRequest("UserDepartment Empty");
        }

        [HttpPost]
        public async Task<IActionResult> SaveUserDepartment([FromBody] UserDepartments userDepartments)
        {
            if (userDepartments == null)
            {
                return BadRequest("userDepartment is null");
            }
            bool success = await _sqlDb.SaveUserDepartment(userDepartments);
            if (success)
            {
                return Ok(new { message = "UserDepartment saved successfully", userDepartments = userDepartments.Id });
            }
            else
            {

                return BadRequest("UserDepartment Not Saved Successfully");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserDepartment([FromBody] UserDepartments userDepartment)
        {
            bool success = await _sqlDb.UpdateUserDepartment(userDepartment);
            if (success)
            {
                return Ok(new { message = "UserDepartment updated successfully", userDepartment = userDepartment.Id });
            }
            return BadRequest("UserDepartment Not Updated Successfully");
        }
    }
}

