using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MegaHack5.Data;
using MegaHack5.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace MegaHack5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlanningsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlanningsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetList")]
        public List<Planning> GetList(int skip = 0, int take = int.MaxValue)
        {
            try
            {
                return _context.Planning
                    .Include(x => x.Company)
                    .Include(x => x.Company.Departments)
                    .Include(x => x.BusinessOccupation)
                    .Include(x => x.Maturity)
                    .Include(x => x.Files)
                    .Include(x => x.InternalInvestments)
                    .Include(x => x.Status)
                    .Skip(skip).Take(take).ToList();
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
                var obj = _context.Planning
                    .Include(x => x.Company)
                    .Include(x => x.BusinessOccupation)
                    .Include(x => x.Files)
                    .Include(x => x.InternalInvestments)
                    .Include(x => x.Status).Where(x => x.Id == id).FirstOrDefault();
                if (obj == null)
                {
                    return false;
                }
                _context.Planning.Remove(obj);
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
        public Guid Create(Planning obj)
        {
            try
            {
                obj.Id = Guid.NewGuid();
                obj.InternalInvestments.ForEach(x => { x.Id = Guid.NewGuid(); });

                _context.Entry(obj).State = EntityState.Modified;
                _context.Planning.Add(obj);

                obj.InternalInvestments.ForEach(i =>
                {
                    _context.Entry(i).State = EntityState.Modified;
                    _context.InternalInvestment.Add(i);
                });

                _context.SaveChanges();

                return obj.Id;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        [HttpPut]
        [Route("Edit")]
        public bool Edit(Planning obj)
        {
            try
            {
                _context.Planning.Update(obj);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        [HttpPost]
        [Route("AddInternalInvestments")]
        public bool AddInternalInvestments(Guid planningId, List<InternalInvestment> listInternalInvestment)
        {
            try
            {
                var planning = _context.Planning.Include(x => x.Company)
                    .Include(x => x.BusinessOccupation)
                    .Include(x => x.Files)
                    .Include(x => x.InternalInvestments)
                    .Include(x => x.Status).Where(x => x.Id == planningId).FirstOrDefault();



                listInternalInvestment.ForEach(x =>
                {
                    x.Id = Guid.Empty;
                    _context.Entry(x).State = EntityState.Added;
                    planning.InternalInvestments.Add(x);
                });

                _context.Entry(planning).State = EntityState.Modified;

                _context.SaveChanges();
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        [HttpDelete]
        [Route("RemoveInternalInvestments")]
        public bool RemoveInternalInvestments(Guid planningId, List<Guid> internalInvestmentsIds)
        {
            try
            {
                var internalInvestments = _context.InternalInvestment.Where(x => internalInvestmentsIds.Contains(x.Id)).ToList();

                _context.InternalInvestment.RemoveRange(internalInvestments);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpGet]
        [Route("GetInternalInvestments")]
        public List<InternalInvestment> GetInternalInvestments(Guid planningId)
        {
            try
            {
                var internalInvestmentsIds = _context.Planning.Include(x => x.Company)
                    .Include(x => x.BusinessOccupation)
                    .Include(x => x.Files)
                    .Include(x => x.InternalInvestments)
                    .Include(x => x.Status).Where(x => x.Id == planningId).Select(x => x.InternalInvestments.Select(x => x.Id)).FirstOrDefault();

                var internalInvestments = _context.InternalInvestment.Include(x => x.Department).Where(x => internalInvestmentsIds.Contains(x.Id)).ToList();
                return internalInvestments;
            }
            catch (Exception)
            {
                return new();
            }
        }

        [HttpPost]
        [Route("AddFile")]
        public bool AddFile(Guid planningId, IFormFile file)
        {
            try
            {
                Models.File f = new();
                f.Id = Guid.NewGuid();
                f.FileName = file.FileName;
                f.FileSize = file.Length;
                f.FileType = file.ContentType;
                f.Extension = System.IO.Path.GetExtension(file.FileName);
                var fileStream = new FileStream($@"c:\uploads\{f.Id}.{f.Extension}", FileMode.Create);
                file.CopyTo(fileStream);

                var planning = _context.Planning.Include(x => x.Company)
                     .Include(x => x.BusinessOccupation)
                     .Include(x => x.Files)
                     .Include(x => x.InternalInvestments)
                     .Include(x => x.Status).Where(x => x.Id == planningId).FirstOrDefault();

                _context.Entry(f).State = EntityState.Added;
                planning.Files.Add(f);

                _context.Entry(planning).State = EntityState.Modified;

                _context.SaveChanges();
                fileStream.Dispose();
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        [HttpDelete]
        [Route("RemoveFile")]
        public bool RemoveFile(Guid fileId)
        {
            try
            {
                var f = _context.File.Where(x => x.Id == fileId).FirstOrDefault();
                _context.File.Remove(f);
                _context.SaveChanges();
                System.IO.File.Delete($@"c:\uploads\{f.Id}.{f.Extension}");
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        [HttpGet]
        [Route("GetFiles")]
        public List<Models.File> GetFiles(Guid planningId)
        {
            try
            {
                var filesIds = _context.Planning.Include(x => x.Company)
                                    .Include(x => x.BusinessOccupation)
                                    .Include(x => x.Files)
                                    .Include(x => x.InternalInvestments)
                                    .Include(x => x.Status).Where(x => x.Id == planningId).Select(x => x.Files.Select(x => x.Id)).FirstOrDefault();

                var files = _context.File.Where(x => filesIds.Contains(x.Id)).ToList();
                return files;
            }
            catch (Exception err)
            {
                return new();
            }
        }
    }
}
