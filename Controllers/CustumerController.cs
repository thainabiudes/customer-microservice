using Custumers.API.Data.ValueObjects;
using Custumers.API.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Custumers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustumerController : ControllerBase
    {

        private ICustumerRepository _repository;

        public CustumerController(ICustumerRepository repository)
        {
            _repository = repository ?? throw new
                ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustumerVO>>> FindAll()
        {
            var products = await _repository.FindAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustumerVO>> FindById(long id)
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
        public async Task<ActionResult<IEnumerable<CustumerVO>>> FindByName(string name)
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
        public async Task<ActionResult<CustumerVO>> Create([FromBody] CustumerVO vo)
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
        public async Task<ActionResult<CustumerVO>> Update(long id, [FromBody] CustumerVO vo)
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