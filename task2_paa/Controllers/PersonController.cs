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
        public 
    }
}