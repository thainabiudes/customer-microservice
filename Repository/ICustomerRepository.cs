using Customers.API.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.API.Repository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerVO>> FindAll();
        Task<CustomerVO> FindById(long id);
        Task<IEnumerable<CustomerVO>> FindByName(string name);
        Task<CustomerVO> Create(CustomerVO vo);
        Task<CustomerVO> Update(long id, CustomerVO vo);
        Task<bool> Delete(long id);
        Task<IEnumerable<CustomerVO>> FindListIds(ListVO vo);
    }
}
