using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using task3_paa.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace task3_paa.Controllers{
    public class PersonController : Controller
    {
        private string __constr;
        public PersonController(IConfiguration configuration)
        {
            __constr = configuration.GetConnectionString("WebApiDatabase");
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("api/person")]
        public ActionResult<Person> ListPerson()
        {
            PersonContext context = new PersonContext(this.__constr);
            List<Person> ListPerson = context.ListPerson();
            return Ok(ListPerson);
        }
        [HttpPost("api/person_auth"), Authorize]
        public ActionResult<Person> ListPersonWithAuth()
        {
            PersonContext context = new PersonContext(this.__constr);
            List<Person> ListPerson = context.ListPerson();
            return Ok(ListPerson);
        }
        [HttpPut("api/person/{id}")]
        public ActionResult<Person> UpdatePerson(int id, Person person)
        {
            PersonContext context = new PersonContext(this.__constr);
            context.UpdatePerson(id, person);
            return Ok();
        }
        [HttpDelete("api/person/{id}")]
        public ActionResult<Person> DeletePerson(int id)
        {
            PersonContext context = new PersonContext(this.__constr);
            context.DeletePerson(id);
            return NoContent();
        }
    }
}