using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using task7_paa.Models;

namespace task7_paa.Controllers
{
    public class PDetailController : ControllerBase
    {
        private readonly string _constr;
        private readonly HttpClient _httpClient;

        public PDetailController(IConfiguration configuration)
        {
            _constr = configuration.GetConnectionString("WebApiDatabase");
            _httpClient = new HttpClient();
        }

        [HttpGet("import-data")]
        public async Task<IActionResult> ImportPersonDetails()
        {
            string apiUrl = "https://dummy-user-tan.vercel.app/user";
            List<PersonDetail> personDetails;

            try
            {
                var response = await _httpClient.GetStringAsync(apiUrl);
                personDetails = JsonConvert.DeserializeObject<List<PersonDetail>>(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving data from API: {ex.Message}");
            }

            try
            {
                PDetailContext context = new PDetailContext(_constr);
                context.InsertPersonDetails(personDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inserting data into database: {ex.Message}");
            }

            return Ok("Data imported successfully");
        }
    }
}
