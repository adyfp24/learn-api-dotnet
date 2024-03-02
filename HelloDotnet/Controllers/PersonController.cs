using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HelloDotnet.Models;
using System.Collections.Generic;

namespace PercobaanApi1.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("api/person")]
        public ActionResult<Person> ListPerson()
        {
            PersonContext context = new PersonContext();
            List<Person> ListPerson = context.ListPerson();
            return Ok(ListPerson);
        }

        [HttpGet("api/person/{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            PersonContext context = new PersonContext();
            Person person = context.GetPersonById(id);

            if (person == null)
            {
                return NotFound(); // Mengembalikan status 404 jika person tidak ditemukan
            }

            return Ok(person);
        }

    }
}