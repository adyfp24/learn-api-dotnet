using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using task3_paa.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace task3_paa.Controllers{
    public class LoginController : Controller
    {
        private string __constr;
        private IConfiguration _config;

        public LoginController(IConfiguration configuration)
        {
            __config = configuration;
            __constr = configuration.GetConnectionString("WebApiDatabase");
        }

        public IActionResult index()
        {
            return View();
        }

        [HttpPost("api/Login")]
        public IEnumerable<Login> LoginUser(string namaUser, string password)
        {
            LoginContext context = new LoginContext(this.__constr);
            List<Login> listLogin = context.Authentifikasi(namaUser, password, __config)
            return (listLogin).ToArray();
        }
    }
}