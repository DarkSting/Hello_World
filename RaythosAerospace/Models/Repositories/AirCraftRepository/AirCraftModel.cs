using RaythosAerospace.Models.Repositories.CartRepository;
using RaythosAerospace.Models.Repositories.OrderRepository;
using RaythosAerospace.Models.Repositories.ProductRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.AirCraftRepository
{
    public class AirCraftModel
    {
        [Key]
        [Required(ErrorMessage = "Please provide a unique aircraft id")]
        [Display(Name ="Aircraft ID")]
        public string AircraftId { get; set; }

        [Required(ErrorMessage = "Please provide aircraft type")]
        [Display(Name = "Aircraft Type")]
        public string AircraftType { get; set; }

        [Required(ErrorMessage = "Please provide the registraion number")]
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }

        [Required(ErrorMessage = "Please provide the manufacture")]
        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "Please provide the YOM")]
        [Display(Name = "Year of manufacture")]
        public int YearOfManufacture { get; set; }

        [Required(ErrorMessage = "Please provide the seating capacity")]
        [Display(Name = "Seat Capacity")]

        public int SeatingCapacity { get; set; }

        [Required(ErrorMessage = "Please provide the range")]
        [Display(Name = "Maximum Range")]
        public double MaximumRange { get; set; }

        [Required(ErrorMessage = "Please provide a engine type")]
        [Display(Name = "Engine type")]
        public string EngineId { get; set; }
        public EngineModel EngineType { get; set; }

        [Required(ErrorMessage = "Please provide max allowed engines")]
        [Display(Name = "Max Allowed Engines")]
        public int MaxAllowedEngines { get; set; }

        [Required(ErrorMessage = "Please provide the fuel capacity")]
        [Display(Name = "Fuel capacity")]
        public double FuelCapacity { get; set; }

        [Required(ErrorMessage = "Please provide the weight")]
        [Display(Name = "Weight")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Please provide the length")]
        [Display(Name = "Length")]
        public double Length { get; set; }

        [Required(ErrorMessage = "Please provide the width")]
        [Display(Name = "Width")]
        public double Width { get; set; }

        [Required(ErrorMessage = "Please provide the height")]
        [Display(Name = "Height")]
        public double Height { get; set; }

        [Required(ErrorMessage = "Please provide the seat type")]
        [Display(Name = "Seat ID")]
        public string SeatID { get; set; }

        public SeatModel Seat { get; set; }

        public string ColorId { get; set; }

        public ColorModel Color { get; set; }


        [Required(ErrorMessage = "Please provide the craft count")]
        [Display(Name = "Item Count")]
        public int ItemCount { get; set; }

        [Display(Name = "Max Seats Allowed")]
        public int MaxSeatesAllowed { get; set; }

        [Display(Name = "Wing Span")]
        public int MaxWingSpan { get; set; }

        [Display(Name = "Manfactured Date")]
        [Required(ErrorMessage ="Please provide the manufactured date")]
        public DateTime ManfacturedDate { get; set; }

        [Required(ErrorMessage = "Please provide the aircraft price")]
        [Display(Name ="AirCraft price")]
        public double AirCraftPrice { get; set; }

        //navigation properties
        public ICollection<OrderAircraftModel> OrderAirCraft { get; set; }

        public ICollection<CartItemModel> CartItems { get; set; }

        public ICollection<ProductModel> Products { get; set; }

    
    }
}
