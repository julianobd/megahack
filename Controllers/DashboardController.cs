using MegaHack5.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MegaHack5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetDashboard")]
        public object GetDashboard(Guid planningId)
        {
            var planning = _context.Planning
                    .Include(x => x.Company)
                    .Include(x => x.Company.Departments)
                    .Include(x => x.BusinessOccupation)
                    .Include(x => x.Maturity)
                    .Include(x => x.Files)
                    .Include(x => x.InternalInvestments)
                    .Include(x => x.Status)
                    .Where(x => x.Id == planningId).FirstOrDefault();
            var ValueByMonth = planning.InternalInvestments.GroupBy(x => x.Month.ToString("MMM/yy")).Select(x => new { Month = x.Key, Value = x.Sum(y => y.InvestmentValue) });
            var ValueByDepartment = planning.InternalInvestments.GroupBy(x => x.Department).Select(x => new { Department = x.Key.Description, Value = x.Sum(y => y.InvestmentValue) });
            var TotalInvested = ValueByMonth.Sum(x => x.Value);
            var TotalPlanned = planning.InvestmentValue;
            return new
            {
                ValueByMonth = ValueByMonth,
                ValueByDepartment = ValueByDepartment,
                TotalInvested = TotalInvested,
                TotalPlanned = TotalPlanned
            };

        }
    }
}
