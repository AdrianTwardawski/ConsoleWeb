using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ConsoleWeb.Models;

namespace ConsoleWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }
        //IActionResult opakowanie na rozbudowaną odpowiedź HTTP
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dupa()
        {
            return View();
        }

        public IActionResult Users()
        {
            return Ok(new List<string>(){"Mateusz", "Adrian"});
        }

        public string Users2()
        {
            return "costamcostam";
        }
    }
}
