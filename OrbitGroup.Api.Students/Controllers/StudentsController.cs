using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrbitGroup.Api.Students.Interfaces;
using OrbitGroup.Api.Students.Models;

namespace OrbitGroup.Api.Students.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsProvider studentsProvider;

        public StudentsController(IStudentsProvider studentsProvider)
        {
            this.studentsProvider = studentsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentsAsync()
        {
            try
            {
                var result = await studentsProvider.GetStudentsAsync();
                if (result.IsSuccess)
                {
                    return Ok(result.Students);
                }
                return NotFound(result.ErrorMsg);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentAsync(int id)
        {
            try
            {
                var result = await studentsProvider.GetStudentByIdAsync(id);
                if (result.IsSuccess)
                {
                    return Ok(result.Student);
                }
                return NotFound(result.ErrorMsg);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        // POST: api/Students
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] MStudent student)
        {
            
            try
            {
                var result = await studentsProvider.AddStudentAsync(student);
                if (result.IsSuccess)
                {
                    return Created("Student", result.Id);
                }

                if (result.ErrorMsg.Equals(CustomErrorMessage.DataError) || result.ErrorMsg.Equals(CustomErrorMessage.ExistingUsername))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.ErrorMsg); 
                }
                return NotFound(result.ErrorMsg);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT: api/students
        [HttpPut]
        public async Task<IActionResult> PutStudent([FromBody] MStudent student)
        {
            try
            {
                var result = await studentsProvider.UpdateStudentAsync(student);
                if (result.IsSuccess)
                {
                    return Ok();
                }

                if (result.ErrorMsg.Equals(CustomErrorMessage.DataError) || result.ErrorMsg.Equals(CustomErrorMessage.ExistingUsername))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.ErrorMsg);
                }

                return NotFound(result.ErrorMsg);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentAsync(int id)
        {
            try
            {
                var result = await studentsProvider.DeleteStudentAsync(id);
                if (result.IsSuccess)
                {
                    return Ok();
                }
                return NotFound(result.ErrorMsg);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}