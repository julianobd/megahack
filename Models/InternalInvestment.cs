using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MegaHack5.Models
{
    public class InternalInvestment
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public virtual Department Department { get; set; }
        [Required]
        public DateTime Month { get; set; }
        [Required]
        public decimal InvestmentValue { get; set; }
    }
}
