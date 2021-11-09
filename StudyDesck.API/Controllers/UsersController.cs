using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyDesck.API.Domain.Services;
using StudyDesck.API.Domain.Services.Comunications;
using StudyDesck.API.Extentions;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [SwaggerOperation(Summary = "Verify credentials")]
        public IActionResult Authenticate([FromBody] AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var response = _userService.Authenticate(request);

            if (response.Message != null)
                return BadRequest(response.Message);

            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("{studentId}/converted")]
        [SwaggerOperation(Summary = "Convert student into tutor")]
        public async Task<IActionResult> Converted(int studentId,[FromQuery] int courseId)
        {
            var result = await _userService.ConvertStudentToTutor(studentId, courseId);

            if (result.Message != null)
                return BadRequest(result.Message);

            return Ok(new { message = "Successful conversion" });
        }
    }
}
