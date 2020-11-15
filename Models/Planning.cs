using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MegaHack5.Models
{
    public class Planning
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public virtual Company Company { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public decimal InvestmentValue { get; set; }
        [NotMapped]
        public string InvestmentValueLabel
        {
            get
            {
                return $"{InvestmentValue.ToString("C2", CultureInfo.GetCultureInfo("pt-BR"))}";
            }
        }
        [Required]
        public decimal CashOnHand { get; set; }
        [NotMapped]
        public string CashOnHandLabel
        {
            get
            {
                return $"{CashOnHand.ToString("C2", CultureInfo.GetCultureInfo("pt-BR"))}";
            }
        }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [NotMapped]
        public string PeriodLabel
        {
            get
            {
                return $"{StartDate.ToString("MMM. yyyy", CultureInfo.GetCultureInfo("pt-BR"))} a {EndDate.ToString("MMM. yyyy", CultureInfo.GetCultureInfo("pt-BR"))}";
            }
        }

        [Required]
        public virtual BusinessOccupation BusinessOccupation { get; set; }
        [Required]
        public virtual Maturity Maturity { get; set; }
        public virtual List<File> Files { get; set; }
        [Required]
        public virtual List<InternalInvestment> InternalInvestments { get; set; }
        [Required]
        public virtual Status Status { get; set; }
    }
}
