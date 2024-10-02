﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenexCars.DataAccess.Models
{
    public class Operator : BaseEntity
    {
        public string? PhoneNumber { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyAddress { get; set; }
        public DateTime ContactDOB { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Zip { get; set; }
        public string? IdentificationDocument { get; set; }
        public string? CompanyRegistrationDocument { get; set; }
        public string? OperatorSubscriptionDuration { get; set; }
        public string? BusinessName { get; set; }
        public InsuranceOffering InsuranceOffering { get; set; }
        public MultipleSubscribers MultipleSubscribers { get; set; }
        public string? CompanyLogo { get; set; }
        public string? HeroGraphics { get; set; }
        public string? FAQLink { get; set; }
        public string? ContactLink { get; set; }
        public string? SupportContact1 { get; set; }
        public string? SupportContact2 { get; set; }

        public IList<Vehicle> Vehicles { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public List<Transaction>? Transactions { get; set; }

        //public string Email { get; set; }
    }
}
