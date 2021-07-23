using System;
using System.Collections.Generic;

#nullable disable

namespace Sage.MeteoriteLandingService.Models
{
    public partial class SalariesOfStateAgencies
    {
        public Guid Id { get; set; }
        public string FiscalYear { get; set; }
        public string Agency { get; set; }
        public string Classification { get; set; }
        public decimal? SalaryAnnual { get; set; }
        public string FullOrPartTime { get; set; }
        public string ServiceType { get; set; }
        public string AgencyNumber { get; set; }
    }
}
