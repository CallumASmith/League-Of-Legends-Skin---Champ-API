using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevAPI;
using RevAPI.Objects;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using RevAPI.Functions;
using RevAPI.Requests;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace Rev_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LeagueOfLegendsController : ControllerBase
    {
        public LeagueOfLegendsController(IHostingEnvironment env)
        {
            Variables.environment = env;
        }
        //  GET: api/LeagueOfLegends
        [HttpGet]   
        public ActionResult Download()
        {
            string filePath = $"{Variables.environment.WebRootPath}\\{Core.PackedFile()}";
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", Core.PackedFile());
        }
        [HttpGet]
        public string Version(string id)
        {
            return JsonConvert.SerializeObject(Core.GetVersion());
        }
    }

}
