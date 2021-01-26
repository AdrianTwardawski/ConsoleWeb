using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ConsoleWeb.Models;
using ConsoleWeb.Database;

namespace ConsoleWeb.Controllers
{
    [Route("[controller]")]
    public class MessagesController : Controller
    {
        private readonly ILogger<MessagesController> _logger;
        private readonly MessageRepository _messageRepository;


        public MessagesController(ILogger<MessagesController> logger)
        {
            _logger = logger;
            _messageRepository = new MessageRepository();
        }

        [HttpPost]
        public MessageModel PostMessage([FromBody] MessageModel input)
        {
            var message = _messageRepository.SendMessage(input);
            return message;
            
            
        }

        [HttpGet]
        public IActionResult GetMessages()
        {
            var message = _messageRepository.GetMessages();
            if (message.Count == 0)
            {
                return NoContent();
            }
            return Ok(message);
        }

        [HttpGet("{id}")]
        public IActionResult GetMessage(int id)
        {
            var message = _messageRepository.GetMessage(id);
            if(message.Text == null)
            {
                return NotFound();
            }
            return Ok(message);
           
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            var message =  _messageRepository.DeleteMessage(id);
            if (message.Text == null)
            {
                return NotFound();
            }
            return Ok(message);

        }

        [HttpDelete()]
        public string DeleteMessages()
        {
            return _messageRepository.DeleteMessages();
        }
    }
}
