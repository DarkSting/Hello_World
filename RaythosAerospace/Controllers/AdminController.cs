using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RaythosAerospace.Models.Repositories.AdminRepository;
using RaythosAerospace.Models.Repositories.AirCraftRepository;
using RaythosAerospace.Models.Repositories.OrderRepository;
using RaythosAerospace.Models.Repositories.ProductRepository;
using RaythosAerospace.Models.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepo;
        private readonly JWTController _jwt;
        private readonly IAirCraftRepository _aircraftRepo;
        private readonly IUserRepository _userRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;
        public AdminController(IAdminRepository adminrepo,JWTController jwt,IAirCraftRepository aircraftRepo,IUserRepository userRepo,
            IOrderRepository orderRepo,IProductRepository productRepo
            
            )
        {
            _adminRepo = adminrepo;
            _jwt = jwt;
            _aircraftRepo = aircraftRepo;
            _userRepo = userRepo;
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }

        // GET: 
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Admin/Register
        [HttpPost]
        public IActionResult Register(AdminLoginDTO admin)
        {
            if (admin.credintials.Password != admin.confirmedPass)
            {
                return View();
            }
            _adminRepo.RegisterAdmin(admin.credintials,admin.credintials.Password);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AircraftManagement()
        {
            return View();
        }

        // GET: /Admin/Login
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        // POST: /Admin/Login
        [HttpPost]
        public IActionResult Login(AdminModel viewmodel)
        {

            ViewBag.Validated = false;

            bool isValid = _adminRepo.ValidateLogin(viewmodel.Email, viewmodel.Password);

            if (isValid)
            {

                
                string token = _jwt.AssignToken(viewmodel.Email);
                _jwt.AttachToken(token, Response.Cookies);


                return RedirectToAction("Dashboard"); // Redirect to a dashboard or home page
            }
            else
            {
             
                ModelState.AddModelError(string.Empty, "Ivalid Login");
                
                return View();
            }
        }

        public IActionResult AddAircraft()
        {
            return View();
        }

        public IActionResult ManageAircrafts()
        {
            AirCraftCreateDTO DTO = new AirCraftCreateDTO();

            DTO.airCrafts = _aircraftRepo.GetAirCrafts();
            DTO.soldCount = new Dictionary<string, int>();

            foreach (AirCraftModel current in DTO.airCrafts)
            {
                if (!DTO.soldCount.ContainsKey(current.AircraftId))
                {
                    int soldcount = _productRepo.GetSoldAircraftCount(current.AircraftId);
                    DTO.soldCount.Add(current.AircraftId, soldcount);
                }
            }

            

            return View(DTO);
        }

        private dynamic _FillDropDownsOfManageAirCraftPage(dynamic bag)
        {
            bag.Engines = _aircraftRepo.GetEngineTypes();
            bag.Seats = _aircraftRepo.GetSeatTypes();
            var enumList = Enum.GetValues(typeof(AirCraftTypeEnum))
                            .Cast<AirCraftTypeEnum>()
                            .Select(e => new SelectListItem
                            {
                                Value = e.ToString(),
                                Text = e.ToString()
                            }).ToList();

            bag.AircraftTypes = enumList;
            bag.Colors = _aircraftRepo.GetAllColors();
            return bag;
        }

        [HttpGet]
        public IActionResult ManageAircraftsPage(string aircraftId)
        {
            AirCraftModel foundAircraft = _aircraftRepo.Find(aircraftId);
            _FillDropDownsOfManageAirCraftPage(ViewBag);
            return View(foundAircraft);
        }

        [HttpPost]
        public IActionResult ManageAircraftsPage(AirCraftModel aircraft)
        {
            AirCraftModel foundAircraft = _aircraftRepo.Update(aircraft);
            _FillDropDownsOfManageAirCraftPage(ViewBag);
            return View(foundAircraft);
        }

        public IActionResult InventoryManagement()
        {
            return View();
        }

        public IActionResult AddInventoryItem()
        {
            return View();
        }

        public IActionResult ManageInventory()
        {
            return View();
        }

        public IActionResult ManageInventoryItem()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewCustomer(string userId)
        {

            UserModel userModel = _userRepo.Find(userId);
            return View(userModel);

        }


        [HttpPost]
        public IActionResult OrderEditing(OrderDetailsDTO dto)
        {

            _productRepo.UpdateTheProducts(dto.products);
            return RedirectToAction("OrderEditing", new { orderId = dto.order.OrderId });
        }


        [HttpGet]
        public IActionResult OrderEditing(string orderId)
        {
            OrderDetailsDTO dto = new();
            dto.order=_orderRepo.Find(orderId);
            dto.products = _productRepo.GetAllProductsForAnOrder(dto.order.OrderId);

            dto.user = _userRepo.Find(dto.order.UserId);
            dto.user.Password = "";

            //filling dropdowns
            ViewBag.DesignEngineeringStatus = Enum.GetValues(typeof(OrderTrackingEnum.DesignEngineeringStatus))
                            .Cast<OrderTrackingEnum.DesignEngineeringStatus>()
                            .Select(e => new SelectListItem
                            {
                                Value = e.ToString(),
                                Text = e.ToString()
                            }).ToList();

            ViewBag.PrototypingTestingStatus= Enum.GetValues(typeof(OrderTrackingEnum.PrototypingTestingStatus))
                            .Cast<OrderTrackingEnum.PrototypingTestingStatus>()
                            .Select(e => new SelectListItem
                            {
                                Value = e.ToString(),
                                Text = e.ToString()
                            }).ToList(); 

            ViewBag.ComponentAssemblyStatus= Enum.GetValues(typeof(OrderTrackingEnum.ComponentAssemblyStatus))
                            .Cast<OrderTrackingEnum.ComponentAssemblyStatus>()
                            .Select(e => new SelectListItem
                            {
                                Value = e.ToString(),
                                Text = e.ToString()
                            }).ToList(); 

            ViewBag.TestingCertificationStatus= Enum.GetValues(typeof(OrderTrackingEnum.TestingCertificationStatus))
                            .Cast<OrderTrackingEnum.TestingCertificationStatus>()
                            .Select(e => new SelectListItem
                            {
                                Value = e.ToString(),
                                Text = e.ToString()
                            }).ToList(); 

            ViewBag.ShippedStatus = Enum.GetValues(typeof(OrderTrackingEnum.ShippedStatus))
                            .Cast<OrderTrackingEnum.ShippedStatus>()
                            .Select(e => new SelectListItem
                            {
                                Value = e.ToString(),
                                Text = e.ToString()
                            }).ToList(); 

            ViewBag.DeliveredStatus = Enum.GetValues(typeof(OrderTrackingEnum.DeliveredStatus))
                            .Cast<OrderTrackingEnum.DeliveredStatus>()
                            .Select(e => new SelectListItem
                            {
                                Value = e.ToString(),
                                Text = e.ToString()
                            }).ToList(); 


            return View(dto);
        }

        [HttpGet]
        public IActionResult DeletUser(string userId)
        {

            _userRepo.DeleteUser(userId);
            return RedirectToAction("CustomerManagement");

        }

        [HttpGet]
        public IActionResult ManageColors()
        {
            IList<ColorModel> colors = _aircraftRepo.GetAllColors();

            colors.Add(new ColorModel());

            return View(new ColorDTO { Colors = colors });
        }
        [HttpPost]
        public IActionResult ManageColors(ColorDTO colordto)
        {
            _aircraftRepo.UpdateColors(colordto.Colors);
            return RedirectToAction("ManageColors");
        }

        [HttpGet]
        public IActionResult DeleteColor(string colorId)
        {
            _aircraftRepo.DeleteColor(colorId);
            return RedirectToAction("ManageColors");
        }

        [HttpGet]
        public IActionResult ManageEngines()
        {
            IList<EngineModel> engines = _aircraftRepo.GetEngineTypes();

            engines.Add(new EngineModel());

            return View(new EngineDTO { Engines = engines });
        }

        [HttpPost]
        public IActionResult ManageEngines(EngineDTO enginedto)
        {
            _aircraftRepo.UpdateEngines(enginedto.Engines);
            return RedirectToAction("ManageEngines");
        }

        [HttpGet]
        public IActionResult DeleteEngine(string engineId)
        {
            _aircraftRepo.DeleteEngine(engineId);
            return RedirectToAction("ManageSeats");
        }

        [HttpGet]
        public IActionResult ManageSeats()
        {
            IList<SeatModel> seats = _aircraftRepo.GetAllSeats();

            seats.Add(new SeatModel());
            
            return View(new SeatDTO { existingSeats = seats });
        }

        [HttpGet]
        public IActionResult DeleteSeat(string seatId)
        {
            _aircraftRepo.DeleteSeat(seatId);
            return RedirectToAction("ManageSeats");
        }

        [HttpPost]
        public IActionResult ManageSeats(SeatDTO seats)
        {
            _aircraftRepo.UpdateSeats(seats.existingSeats);
            return RedirectToAction("ManageSeats");
        }

        [HttpPost]
        public IActionResult ViewCustomer(UserModel model)
        {
            
           _userRepo.UpdateUser(model);
            return View(model);

        }

        [HttpGet]
        public IActionResult CustomerManagement()
        {
            IList<UserModel> foundUsers = _userRepo.GetUsers();
            UserDTO DTO = new UserDTO
            {
                Users = foundUsers
            };
            return View(DTO);
        }
    }
}
