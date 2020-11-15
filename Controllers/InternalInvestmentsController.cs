using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MegaHack5.Data;
using MegaHack5.Models;

namespace MegaHack5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InternalInvestmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InternalInvestmentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetList")]
        public List<InternalInvestment> GetList(int skip = 0, int take = int.MaxValue)
        {
            try
            {
                return _context.InternalInvestment.Include(x => x.Department).Skip(skip).Take(take).ToList();
            }
            catch (Exception)
            { 
                return new();
            }
        }

        [HttpDelete]
        [Route("Remove")]
        public bool Remove(Guid id)
        {
            try
            {
                var obj = _context.InternalInvestment.Where(x => x.Id == id).FirstOrDefault();
                if (obj == null)
                {
                    return false;
                }
                _context.InternalInvestment.Remove(obj);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("Create")]
        public bool Create(InternalInvestment obj)
        {
            try
            {
                obj.Id = Guid.Empty;
                _context.Entry(obj).State = EntityState.Modified;
                _context.InternalInvestment.Add(obj);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPut]
        [Route("Edit")]
        public bool Edit(InternalInvestment obj)
        {
            try
            {
                _context.InternalInvestment.Update(obj);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
