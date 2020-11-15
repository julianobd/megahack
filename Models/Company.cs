using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MegaHack5.Models
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }
        public string FantasyName { get; set; }
        public string Cnpj { get; set; }
        public virtual List<Department> Departments { get; set; }
    }
}
