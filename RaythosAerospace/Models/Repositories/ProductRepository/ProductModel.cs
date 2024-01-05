using RaythosAerospace.Models.Repositories.AirCraftRepository;
using RaythosAerospace.Models.Repositories.CartRepository;
using RaythosAerospace.Models.Repositories.OrderRepository;
using RaythosAerospace.Models.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.ProductRepository
{
    public class ProductModel
    {

        [Key]
        public string ProductId { get; set; }

        public string AirCraftId { get; set; }

        public int UnitPrice { get; set; }
        public int Count { get; set; }
        public string UserId { get; set; }
        public string CustomizationId { get; set; }
        public DateTime AddedDate { get; set; }

        public string CartId { get; set; }

        public string OrderId { get; set; }

        public string DesignEngineeringStatus { get; set; }

        public string PrototypingTestingStatus { get; set; }

        public string ComponentAssemblyStatus { get; set; }
        public string TestingCertificationStatus { get; set; }

        public string OrderProcessingStatus { get; set; }

        public string DeliveredStatus { get; set; }

        public string ShippedStatus { get; set; }

        //navigation properties
        public AirCraftModel AirCraft { get; set; }

        public CustomizationModel Customize { get; set; }

        public UserModel User { get; set; }

        public CartModel Cart { get; set; }

        public OrderModel Order { get; set; }
    }
}
