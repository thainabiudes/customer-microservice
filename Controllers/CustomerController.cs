using Customers.API.Data.ValueObjects;
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

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository ?? throw new
                ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerVO>>> FindAll()
        {
            var products = await _repository.FindAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerVO>> FindById(long id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest("ID is required");
            }

            var product = await _repository.FindById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<CustomerVO>>> FindByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name is required");
            }

            var product = await _repository.FindByName(name);
            if (product == null) return NotFound();
            return Ok(product);
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

            var product = await _repository.Create(vo);
            return Ok(product);
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
            
            var product = await _repository.Update(id, vo);
            if (product == null) return NotFound("ID not found");
            return Ok(product);
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
    }
}