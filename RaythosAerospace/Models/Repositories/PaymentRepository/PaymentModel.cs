using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.PaymentRepository
{
    public class PaymentModel
    {
        public int Amount { get; set; }
        public string Token { get; set; }
    }
}
