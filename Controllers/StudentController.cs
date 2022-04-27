using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University_MGS_API.Data;
using University_MGS_API.Models;

namespace University_MGS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly DataContext _db;

        public StudentController(DataContext db)
        {
            _db = db;
        }

        //Retrive Student
        [HttpGet]
        public async Task<ActionResult<List<Student>>> Get()
        {
            return Ok(await _db.students.ToListAsync());
        }

        //Retrive Single Student base on StuID
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            var student = await _db.students.FindAsync(id);

            if(student == null)
            {
                return BadRequest("Student Not Found!");
            }

            return Ok(student);
        }

        //Create Student
        [HttpPost]
        public async Task<ActionResult<List<Student>>> AddStudent(Student student)
        {
            _db.students.Add(student);
            await _db.SaveChangesAsync();

            return Ok(await _db.students.ToListAsync());
        }

        //Update Student
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(Student request)
        {
            var student = await _db.students.FindAsync(request.StuID);

            if(student == null)
            {
                return BadRequest("Student Not Found");
            }

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.Address = request.Address;
            await _db.SaveChangesAsync();

            return Ok(await _db.students.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }

            var student = await _db.students.FindAsync(id);

             _db.students.Remove(student);
            await _db.SaveChangesAsync();

            return Ok(student);
        }
    }
}
