using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using task2_paa.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace task2_paa.Controllers
{
    [ApiController]
    [Route("api/murid")]
    public class StudentController : Controller
    {
        private string __constr;
        public StudentController(IConfiguration configuration){
            __constr = configuration.GetConnectionString("WebApiDatabase");
        }
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult<Student> ListStudent()
        {
            StudentContext context = new StudentContext(this.__constr);
            List<Student> ListStudent = context.ListStudent();
            return Ok(ListStudent);
        }
        [HttpGet("id_murid")]
        public ActionResult<Student> getStudentById(int id_murid)
        {
            StudentContext context = new StudentContext(this.__constr);
            Student student = context.getStudentById(id_murid);
            return Ok(student);
        }
        [HttpPost]
        public ActionResult<Student> addStudent([FromBody]Student student)
        {
            StudentContext context = new StudentContext(this.__constr);
            context.addStudent(student);
            return CreatedAtAction(nameof(getStudentById), new { id = student.id_murid }, student);
        }
        [HttpPut("id_murid")]
        public ActionResult<Student> updateStudent(int id_murid, [FromBody]Student student)
        {
            StudentContext context = new StudentContext(this.__constr);
            var existedStudent = context.getStudentById(id_murid);
            if(existedStudent == null){
                return NotFound();
            }
            context.updateStudent(id_murid, student);
            return NoContent();
        }
        [HttpDelete("id_murid")]
        public ActionResult<Student> deleteStudent(int id_murid)
        {
            StudentContext context = new StudentContext(this.__constr);
            var existedStudent = context.getStudentById(id_murid);
            if(existedStudent == null){
                return NotFound();
            }
            context.deleteStudent(id_murid);
            return NoContent();
        }
    }
}