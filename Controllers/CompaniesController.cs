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
    public class CompaniesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CompaniesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetList")]
        public List<Company> GetList(int skip = 0, int take = int.MaxValue)
        {
            try
            {
                return _context.Company.Include(x => x.Departments).Skip(skip).Take(take).ToList();
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
                var obj = _context.Company.Include(x => x.Departments).Where(x => x.Id == id).FirstOrDefault();
                if (obj == null)
                {
                    return false;
                }
                _context.Company.Remove(obj);
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
        public bool Create(Company comp)
        {
            try
            {
                comp.Id = Guid.Empty;
                comp.Departments.ForEach(x => x.Id = Guid.Empty);
                _context.Company.Add(comp);

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
        public bool Edit(Company comp)
        {
            try
            {
                _context.Company.Update(comp);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpGet]
        [Route("GetListDepartment")]
        public List<Department> GetListDepartment(Guid companyId, int skip = 0, int take = int.MaxValue)
        {
            try
            {
                return _context.Company.Include(x => x.Departments).Where(x => x.Id == companyId).Select(x => x.Departments.Skip(skip).Take(take).ToList()).FirstOrDefault();
            }
            catch (Exception)
            {
                return new();
            }
        }

        [HttpDelete]
        [Route("RemoveDepartment")]
        public bool RemoveDepartment(Guid departmentId)
        {
            try
            {
                var obj = _context.Company.Include(x => x.Departments).Select(x => x.Departments.Where(y => y.Id == departmentId).FirstOrDefault()).FirstOrDefault();
                if (obj == null)
                {
                    return false;
                }
                _context.Department.Remove(obj);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("CreateDepartment")]
        public bool CreateDepartment(Guid companyId, Department department)
        {
            try
            {
                var company = _context.Company.Include(x => x.Departments).Where(x => x.Id == companyId).FirstOrDefault();
                department.Id = Guid.Empty;
                company.Departments.Add(department);

                _context.Company.Update(company);
                
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
