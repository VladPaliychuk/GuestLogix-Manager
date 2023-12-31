﻿using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Entities
{
    public class Order : EntityBase
    {
        public string UserName { get; set; } = null!;
        public decimal TotalPrice { get; set; }

        // BillingAddress
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public string City { get; set; } = null!;

        // Payment
        public string CardName { get; set; } = null!;
        public string CardNumber { get; set; } = null!;
        public string Expiration { get; set; } = null!;
        public string CVV { get; set; } = null!;
        public int PaymentMethod { get; set; }
    }
}
