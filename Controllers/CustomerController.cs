using Customers.API.Data.ValueObjects;
using Customers.API.Messages;
using Customers.API.RabbitMQSender;
using Customers.API.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {

        private ICustomerRepository _repository;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public CustomerController(ICustomerRepository repository,
            IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _repository = repository
                ?? throw new ArgumentNullException(nameof(repository));
            _rabbitMQMessageSender = rabbitMQMessageSender
                ?? throw new ArgumentNullException(nameof(rabbitMQMessageSender));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerVO>>> FindAll()
        {
            var customers = await _repository.FindAll();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerVO>> FindById(long id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest("ID is required");
            }

            var customer = await _repository.FindById(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<CustomerVO>>> FindByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name is required");
            }

            var customer = await _repository.FindByName(name);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerVO>> Create([FromBody] CustomerVO vo)
        {
            if (vo == null ||
                string.IsNullOrEmpty(vo.Name) ||
                string.IsNullOrEmpty(vo.LastName) ||
                string.IsNullOrEmpty(vo.Age.ToString()) ||
                string.IsNullOrEmpty(vo.Gender) 
                )
            {
                return BadRequest("All request body is required");
            }

            var customer = await _repository.Create(vo);
            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerVO>> Update(long id, [FromBody] CustomerVO vo)
        {
            if (string.IsNullOrEmpty(vo.Name) ||
                string.IsNullOrEmpty(vo.LastName) ||
                string.IsNullOrEmpty(vo.Age.ToString()) ||
                string.IsNullOrEmpty(vo.Gender) ||
                string.IsNullOrEmpty(id.ToString())
                )
            {
                return BadRequest("All request body or params are required");
            }
            
            var customer = await _repository.Update(id, vo);
            if (customer == null) return NotFound("ID not found");
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest("ID is required");
            }

            var status = await _repository.Delete(id);
            if (!status) return NotFound("ID not found");
            return Ok(status);
        }

        [HttpPost("send-email")]
        public async Task<ActionResult<MessageVO>> SendEmail(long id, string bodyEmail)
        {
            if (string.IsNullOrEmpty(id.ToString()) || string.IsNullOrEmpty(bodyEmail)) return BadRequest();
            
            var customer = await _repository.FindById(id);

            if (customer == null) return NotFound();

            MessageVO message = new MessageVO();

            message.Name = customer.Name;
            message.Gender = customer.Gender;
            message.Age = customer.Age;
            message.Email = customer.Email;
            message.LastName = customer.LastName;
            message.BodyEmail = bodyEmail;
            message.Id = id;

            _rabbitMQMessageSender.SendMessage(message, "emailqueue");

            return Ok(message);
        }

        [HttpPost("list")]
        public async Task<ActionResult<IEnumerable<CustomerVO>>> ListIds([FromBody] ListVO ids)
        {
            if (string.IsNullOrEmpty(ids.ToString())) return BadRequest();

            IEnumerable<CustomerVO> customers = await _repository.FindListIds(ids);

            if (customers == null) return NotFound();
                       
            return Ok(customers);
        }

    }
}