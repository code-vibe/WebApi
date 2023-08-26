using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZ.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentsName = new string[] { "James", "Dorcas", "Toheeb", "Lekan" };
            return Ok(studentsName);
           
        }



    }
}
