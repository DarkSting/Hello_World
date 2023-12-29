using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RaythosAerospace.Models.Repositories.AirCraftRepository;
using RaythosAerospace.Models.Repositories.CartRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Controllers
{
    public class AirCraftController : Controller
    {

        private readonly IAirCraftRepository _repo;

        public AirCraftController(IAirCraftRepository repo)
        {
            _repo = repo;
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
        public IActionResult Create(AirCraftModel model)
        {
            if (ModelState.IsValid)
            {
                _repo.Insert(model);
                return RedirectToAction("Success", new { id = model.AircraftId });
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

        [HttpPost]
        public RedirectToActionResult Inst(AirCraftModel model)
        {

            return RedirectToAction();
        }



    }
}
