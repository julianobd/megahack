﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MegaHack5.Models
{
    public class Department
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
