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
    
    //+ poznać metody HTTP: w szczególności GET, POST, PATCH, DELETE
    //+ poznać REST

    //+ GET Zrobić odczyt wszystkich użytkowników, wybranego po id,
    //+ GET po id powinien zwrócić inny kod odpowiedzi HTTP gdy podamy zły id (nie 200 OK)

    //+- POST Dodanie nowego usera - sprawdzić czy id już istnieje. Jeżeli istnieje zwrócić odpowieni kod błędu HTTP (BadRequest)

    //+ DELETE usunąć użytkownika po przekazanym Id

    //+ PATCH to na końcu. Aktualizacja użytkownika (tylko username, staramy się ie zmieniać wartości Id)


    //- Jak Ci się będzie chciało to możesz zrobić podobny kontroler dla messages
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        // ILogger pokazuje w debug konsoli, że coś załadowało i opatrza dodatkowymi tagami - do diagnostyki
        private readonly UserRepository _userRepository;
        // readonly mówi że _userRepository można zainicjalizować tylko w konstruktorze - UsersController 
        
        //Konstruktor//
        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
            _userRepository = new UserRepository();
        }

        [HttpGet]
        public IEnumerable<UserModel> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        [HttpGet("{id}")]
        public IActionResult GetUsers(int id)
        {
            var user = _userRepository.GetUser(id);
            if(user.Username == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsers(int id)
        {
            var user = _userRepository.DeleteUser(id);
            if(user.Username == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


    [HttpPatch("{id}")]
        public IActionResult PatchUser(int id)
        {
            var user = _userRepository.PatchUser(id);
            if(user.Username == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public UserModel PostUser([FromBody]UserModel input)
        {
            return _userRepository.AddUser(input);
            
        }
    }
}
