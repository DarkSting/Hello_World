using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RaythosAerospace.Models.Repositories.AirCraftRepository;
using RaythosAerospace.Models.Repositories.CartRepository;
using RaythosAerospace.Models.Repositories.OrderRepository;
using RaythosAerospace.Models.Repositories.ProductRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Controllers
{
    public class AirCraftController : Controller
    {

        private readonly IAirCraftRepository _repo;
        private readonly IWebHostEnvironment _hosting;
        private readonly IProductRepository _productRepo;

        public AirCraftController(IAirCraftRepository repo,IWebHostEnvironment hosting, IProductRepository productRepo)
        {
            _repo = repo;
            _hosting = hosting;
            _productRepo = productRepo;
        }



        // getting the aircraft catelog view
        public IActionResult AircraftCatalog()
        {

            //  retreive all aircrafts
            ViewBag.aircrafts = _repo.GetAirCrafts();

            // getting the sold count for a particular aircraft
            ViewBag.soldCount = new Dictionary<string, int>();

            //assign the sold count for an aircraft
            foreach (AirCraftModel current in ViewBag.AirCrafts)
            {
                if (!ViewBag.soldCount.ContainsKey(current.AircraftId))
                {
                    int soldcount = GetSoldCount(current.AircraftId);
                    ViewBag.soldCount.Add(current.AircraftId, soldcount);
                }
            }

            return View();
        }



        [HttpGet]

        public ViewResult Create()
        {

            _FillDropDowns(ViewBag);
            return View();
        }


        //  gets the aircraft details requested id
        public IActionResult AircraftPage(string airCraftId)
        {

            CartItemViewModel viewModel = new CartItemViewModel();
            viewModel.aircraft = _repo.Find(airCraftId);

            //prepare data
            _FillDropDowns(ViewBag);

            return View(viewModel);
        }

        // view all the aircrafts
        public ViewResult ViewAirCrafts()
        {
            AirCraftViewModel vm = new AirCraftViewModel();
            vm.AirCrafts = _repo.GetAirCrafts();
            

            return View(vm);
        }

        //fills the dropdowns
        private dynamic _FillDropDowns (dynamic bag)
        {
            bag.Engines = _repo.GetEngineTypes();
            bag.Seats = _repo.GetSeatTypes();
            var enumList = Enum.GetValues(typeof(AirCraftTypeEnum))
                            .Cast<AirCraftTypeEnum>()
                            .Select(e => new SelectListItem
                            {
                                Value = e.ToString(),
                                Text = e.ToString()
                            }).ToList();

            bag.AircraftTypes = enumList;
            bag.Colors = _repo.GetAllColors();
            return bag;
        }

        // gets the soldcount based on the provided aircraft id
        public int GetSoldCount(string aircraftid)
        {
            
            return _productRepo.GetSoldAircraftCount(aircraftid); ;
        }

        // create an aircraft model
        [HttpPost]
        public IActionResult Create(AirCraftCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                _repo.Insert(model.AirCraftDet);

                int count = 1;
                // iterating photos
               foreach(IFormFile current in model.Photos)
                {
                    AirCraftPhoto currentPhoto = new AirCraftPhoto();

                    // adding the properties to the photo
                    string uniqueName = $"{count.ToString()}_"+Guid.NewGuid().ToString()+"_"+ current.FileName;
                    string rootpath = _hosting.WebRootPath;
                    string targetFolder = Path.Combine(rootpath, "images");
                    string filePath = Path.Combine(targetFolder, uniqueName);
                    current.CopyTo(new FileStream(filePath, FileMode.Create));


                    currentPhoto.AirCraftID = model.AirCraftDet.AircraftId;
                    currentPhoto.Date = DateTime.Now;
                    currentPhoto.FilePath = filePath;
                    currentPhoto.PhotoID ="P:"+Guid.NewGuid().ToString();
                    currentPhoto.fileName = uniqueName;

                    _repo.UploadPhoto(currentPhoto);
                }

                return RedirectToAction("Success", new { id = model.AirCraftDet.AircraftId });
            }

            _FillDropDowns(ViewBag);

            return View();

        }

        [HttpGet]
        public ViewResult Success(string id)
        {
            AirCraftModel model = _repo.Find(id);
            return View(model);
        }

   

    }
}
