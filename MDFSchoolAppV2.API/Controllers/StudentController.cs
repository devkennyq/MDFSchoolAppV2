using MDFSchoolAppV2.Core;
using MDFSchoolAppV2.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDFSchoolAppV2.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StudentController : ControllerBase
    {
        private MDFSchoolAppV2DbContext context { get; }
        public StudentController(MDFSchoolAppV2DbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                var students = await context.Student.ToListAsync();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            try
            {
                await context.AddAsync(student);
                context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            try
            {
                var student = await context.Student.FindAsync(id);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent([FromBody] Student student)
        {
            try
            {
                context.Update(student);
                await context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                //Get student by id
                var student = context.Student.Find(id);
                context.Student.Remove(student);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
