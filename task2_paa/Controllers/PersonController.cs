using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using task2_paa.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace task2_paa.Controllers
{
    [ApiController]
    [Route("api/person")]
    public class PersonController : Controller
    {
        private string __constr;
        public PersonController(IConfiguration configuration){
            __constr = configuration.GetConnectionString("WebApiDatabase");
        }
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult<Person> ListPerson()
        {
            PersonContext context = new PersonContext(this.__constr);
            List<Person> ListPerson = context.ListPerson();
            return Ok(ListPerson);
        }
        [HttpGet("id_person")]
        public ActionResult<Person> getPersonById(int id_person)
        {
            PersonContext context = new PersonContext(this.__constr);
            Person person = context.getPersonById(id_person);
            return Ok(person);
        }
        [HttpPost]
        public ActionResult<Person> addPerson([FromBody]Person person)
        {
            PersonContext context = new PersonContext(this.__constr);
            context.addPerson(person);
            return CreatedAtAction(nameof(getPersonById), new { id = person.id_person }, person);
        }
        [HttpPut("id_person")]
        public ActionResult<Person> updatePerson(int id_person, [FromBody]Person person)
        {
            PersonContext context = new PersonContext(this.__constr);
            var existedPerson = context.getPersonById(id_person);
            if(existedPerson == null){
                return NotFound();
            }
            context.updatePerson(id_person, person);
            return NoContent();
        }
        // [HttpDelete]
        // public ActionResult<Person> deletePerson(int id_person, [FromBody]Person person)
        // {

        // }
    }
}