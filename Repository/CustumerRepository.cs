using AutoMapper;
using Custumers.API.Data.ValueObjects;
using Custumers.API.Model;
using Custumers.API.Model.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Custumers.API.Repository
{
    public class CustumerRepository : ICustumerRepository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public CustumerRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustumerVO>> FindAll()
        {
            List<Custumer> Custumers = await _context.Custumer.ToListAsync();
            return _mapper.Map<List<CustumerVO>>(Custumers);
        }

        public async Task<IEnumerable<CustumerVO>> FindByName(string name)
        {
            List<Custumer> Custumers = await _context.Custumer.Where(p => p.Name.Contains(name)).ToListAsync();
            return _mapper.Map<List<CustumerVO>>(Custumers);
        }

        public async Task<CustumerVO> FindById(long id)
        {
            Custumer custumer =
                await _context.Custumer.Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            return _mapper.Map<CustumerVO>(custumer);
        }

        public async Task<CustumerVO> Create(CustumerVO vo)
        {
            Custumer custumer = _mapper.Map<Custumer>(vo);
            custumer.Id = null;
            _context.Custumer.Add(custumer);
            await _context.SaveChangesAsync();
            return _mapper.Map<CustumerVO>(custumer);
        }
        public async Task<CustumerVO> Update(long id, CustumerVO vo)
        {
            try
            {
                Custumer custumer = _mapper.Map<Custumer>(vo);
                custumer.Id = id;
                _context.Custumer.Update(custumer);
                await _context.SaveChangesAsync();
                return _mapper.Map<CustumerVO>(custumer);
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
                Custumer custumer =
                await _context.Custumer.Where(p => p.Id == id)
                    .FirstOrDefaultAsync();
                if (custumer == null) return false;
                _context.Custumer.Remove(custumer);
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
