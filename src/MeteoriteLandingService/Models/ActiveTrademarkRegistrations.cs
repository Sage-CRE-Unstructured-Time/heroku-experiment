using System;
using System.Collections.Generic;

#nullable disable

namespace Sage.MeteoriteLandingService.Models
{
    public partial class ActiveTrademarkRegistrations
    {
        public Guid Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string RegistrationDate { get; set; }
        public string TrademarkDescription { get; set; }
        public string CorrespondentName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string ImageLink { get; set; }
    }
}
