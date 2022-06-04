using AutoMapper;
using Customers.API.Data.ValueObjects;
using Customers.API.Model;
using Customers.API.Model.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.API.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public CustomerRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerVO>> FindAll()
        {
            List<Customer> Customers = await _context.Customer.ToListAsync();
            return _mapper.Map<List<CustomerVO>>(Customers);
        }

        public async Task<IEnumerable<CustomerVO>> FindByName(string name)
        {
            List<Customer> Customers = await _context.Customer.Where(p => p.Name.Contains(name)).ToListAsync();
            return _mapper.Map<List<CustomerVO>>(Customers);
        }

        public async Task<IEnumerable<CustomerVO>> FindListIds(ListVO vo)
        {
            List<Customer> Customers = await _context.Customer.Where(p => vo.ids.Contains((long)p.Id)).ToListAsync();
            return _mapper.Map<List<CustomerVO>>(Customers);
        }

        public async Task<CustomerVO> FindById(long id)
        {
            Customer customer =
                await _context.Customer.Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            return _mapper.Map<CustomerVO>(customer);
        }

        public async Task<CustomerVO> Create(CustomerVO vo)
        {
            Customer customer = _mapper.Map<Customer>(vo);
            customer.Id = null;
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
            return _mapper.Map<CustomerVO>(customer);
        }
        public async Task<CustomerVO> Update(long id, CustomerVO vo)
        {
            try
            {
                Customer customer = _mapper.Map<Customer>(vo);
                customer.Id = id;
                _context.Customer.Update(customer);
                await _context.SaveChangesAsync();
                return _mapper.Map<CustomerVO>(customer);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                Customer customer =
                await _context.Customer.Where(p => p.Id == id)
                    .FirstOrDefaultAsync();
                if (customer == null) return false;
                _context.Customer.Remove(customer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
