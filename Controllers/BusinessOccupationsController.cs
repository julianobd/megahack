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
    public class BusinessOccupationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BusinessOccupationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetList")]
        public List<BusinessOccupation> GetList(int skip = 0, int take = int.MaxValue)
        {
            try
            {
                return _context.BusinessOccupation.Skip(skip).Take(take).ToList();
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
                var obj = _context.BusinessOccupation.Where(x => x.Id == id).FirstOrDefault();
                if (obj == null)
                {
                    return false;
                }
                _context.BusinessOccupation.Remove(obj);
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
        public bool Create(BusinessOccupation obj)
        {
            obj.Id = Guid.Empty;
            _context.BusinessOccupation.Add(obj);
            _context.SaveChanges();
            return true;
        }

        [HttpPut]
        [Route("Edit")]
        public bool Edit(BusinessOccupation obj)
        {
            try
            {
                _context.BusinessOccupation.Update(obj);
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
