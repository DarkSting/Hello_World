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
            Pending,
            InProgress,
            Completed,
            NotStarted

        }

        public enum PrototypingTestingStatus
        {
            Pending,
            InProgress,
            Completed,
            NotStarted
        }

        public enum ComponentAssemblyStatus
        {
            Pending,
            InProgress,
            Completed,
            NotStarted
        }

        public enum TestingCertificationStatus
        {
            Pending,
            InProgress,
            Completed,
            NotStarted
        }

        public enum OrderProcessingStatus
        {
            Pending,
            InProgress,
            Completed,
            NotStarted
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
            Pending,
            Shipping,
            Shipped,
            NotStarted
        }
    }
}
