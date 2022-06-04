using Data.ValueObjects;
using Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {

        private ICustomerRepository _repository;
        private const string URL_CUSTOMER_API = "https://localhost:44300";

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository
                ?? throw new ArgumentNullException(nameof(repository));
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

        [HttpPost("invoice")]
        public async Task<ActionResult<InvoiceVO>> PostInvoice(InvoiceVO invoice)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL_CUSTOMER_API);

                string payload = JsonConvert.SerializeObject(invoice);

                var content = new StringContent(payload, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync("/Invoice", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        InvoiceVO vo = JsonConvert.DeserializeObject<InvoiceVO>(result);
                        return Ok(vo);
                    }

                    return BadRequest(response.Content.ReadAsStringAsync().Result);                    
                }
            }
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