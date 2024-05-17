using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using task7_paa.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace task7_paa.Controllers{

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
        [HttpGet("person")]
        public ActionResult<Person> ListPerson()
        {
            PersonContext context = new PersonContext(this.__constr);
            List<Person> ListPerson = context.ListPerson();
            return Ok(ListPerson);
        }
        // [HttpGet("{id}")]
        // public ActionResult<Person> getPersonById(int id_person)
        // {
        //     PersonContext context = new PersonContext(this.__constr);
        //     Person person = context.getPersonById(id_person);
        //     return Ok(person);
        // }
    }
}