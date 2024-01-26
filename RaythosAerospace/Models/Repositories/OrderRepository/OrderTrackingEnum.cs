using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.OrderRepository
{
    public static class OrderTrackingEnum
    {

        public enum DesignEngineeringStatus
        {
            NotStarted,
            Pending,
            InProgress,
            Completed,

        }

        public enum PrototypingTestingStatus
        {
            NotStarted,
            Pending,
            InProgress,
            Completed,
            
        }

        public enum ComponentAssemblyStatus
        {
            NotStarted,
            Pending,
            InProgress,
            Completed,
            
        }

        public enum TestingCertificationStatus
        {
            NotStarted,
            Pending,
            InProgress,
            Completed,
           
        }

        public enum OrderProcessingStatus
        {
            NotStarted,
            Pending,
            InProgress,
            Completed,
            
        }

        public enum DeliveredStatus
        {
            Pending,
            Delivering,
            Delivered,
            NotStarted
        }

        public enum ShippedStatus
        {
            NotStarted,
            Pending,
            Shipping,
            Shipped
            
        }
    }
}
