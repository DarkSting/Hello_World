﻿using RaythosAerospace.Models.Repositories.AirCraftRepository;
using RaythosAerospace.Models.Repositories.CartRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.PaymentRepository
{
    public class PaymentModel
    {
        public double Amount { get; set; }
        public string Token { get; set; }
        public IEnumerable<CartItemModel> cartitems { get; set; }

        public Dictionary<string, CheckoutViewModel> aircrafts { get; set; }
    }
}
