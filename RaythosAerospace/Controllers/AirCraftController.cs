using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RaythosAerospace.Models.Repositories.AirCraftRepository;
using RaythosAerospace.Models.Repositories.CartRepository;
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
        public AirCraftController(IAirCraftRepository repo,IWebHostEnvironment hosting)
        {
            _repo = repo;
            _hosting = hosting;
        }
        // GET: AirCraftController
        public ViewResult Index()
        {
            return View();
        }

        public IActionResult AircraftCatalog()
        {
            ViewBag.aircrafts = _repo.GetAirCrafts();
            return View();
        }

        // GET: AirCraftController/Details/5
        public ViewResult Details(int id)
        {
            return View();
        }


        [HttpGet]
        // GET: AirCraftController/Create
        public ViewResult Create()
        {

            _FillDropDowns(ViewBag);
            return View();
        }

        public IActionResult AircraftPage(string airCraftId)
        {

            CartItemViewModel viewModel = new CartItemViewModel();
            viewModel.aircraft = _repo.Find(airCraftId);

            //prepare data
            _FillDropDowns(ViewBag);

            return View(viewModel);
        }

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


        [HttpPost]
        public IActionResult Create(AirCraftCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                _repo.Insert(model.AirCraftDet);

                int count = 0;
                //iterating photos
               foreach(IFormFile current in model.Photos)
                {
                    AirCraftPhoto currentPhoto = new AirCraftPhoto();

                    //adding the properties to the photo
                    string uniqueName = $"{current.FileName}_"+Guid.NewGuid().ToString()+"_"+count.ToString();
                    string rootpath = _hosting.WebRootPath;
                    string targetFolder = Path.Combine(rootpath, "images");
                    string filePath = Path.Combine(targetFolder, uniqueName);
                    current.CopyTo(new FileStream(filePath, FileMode.Create));


                    currentPhoto.AirCraftID = model.AirCraftDet.AircraftId;
                    currentPhoto.Date = DateTime.Now;
                    currentPhoto.FilePath = filePath;
                    currentPhoto.PhotoID ="P:"+Guid.NewGuid().ToString();

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
