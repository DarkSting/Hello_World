using RaythosAerospace.Models.Repositories.OrderRepository;
using RaythosAerospace.Models.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.InvoiceRepository
{
    public class InvoiceModel
    {


        [Key]
        public string InvoiceId { get; set; }


        [Required(ErrorMessage = "Please provide the invoice date")]
        [Display(Name ="Invoice Date")]
        public DateTime InvoiceDate { get; set; }

        [Required(ErrorMessage = "Please provide the customer name")]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer ID")]
        public string CustomerId { get; set; }
        public UserModel Customer { get; set; }

        [Required(ErrorMessage = "Please provide the billing address")]
        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; }

        [Required(ErrorMessage = "Please provide the Invoice Number")]
        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }



        // Totals and Summary
        [Required(ErrorMessage = "Please provide the sub total")]
        [Display(Name = "Subtotal")]
        public double Subtotal { get; set; }

        public double Discounts { get; set; }

        [Required(ErrorMessage = "Please provide the Total Amount")]
        [Display(Name = "TotalAmount")]
        public double TotalAmount { get; set; }

        // Payment Information
        public string PaymentMethod { get; set; }

        public string TransactionId { get; set; }

        // Additional Information
        public string Notes { get; set; }

        // Status

        [Required(ErrorMessage = "Please provide the Invoice Status")]
        [Display(Name = "Invoice Status")]
        public string InvoiceStatus { get; set; }

        //navigation properties
        public string OrderId { get; set; }
        public OrderModel Order{ get; set; }
    }


}
