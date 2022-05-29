using Custumers.API.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Custumers.API.Repository
{
    public interface ICustumerRepository
    {
        Task<IEnumerable<CustumerVO>> FindAll();
        Task<CustumerVO> FindById(long id);
        Task<IEnumerable<CustumerVO>> FindByName(string name);
        Task<CustumerVO> Create(CustumerVO vo);
        Task<CustumerVO> Update(long id, CustumerVO vo);
        Task<bool> Delete(long id);
    }
}
