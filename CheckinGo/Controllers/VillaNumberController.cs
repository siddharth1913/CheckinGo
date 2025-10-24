using CheckinGo.Domain.Entities;
using CheckinGo.Infrastructure.Data;
using CheckinGo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace CheckinGo.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VillaNumberController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var VillaNumber = _db.VillaNumbers.ToList();
            return View(VillaNumber);
        }

        [HttpGet]
        public IActionResult Create()
        {

            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _db.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            #region ============== VIEWDATA or VIEWBAG METHOD ==============
            // ---------------- IF WE WANT TO USE VIEWDATE or VIEWBAG ----------------
            //To create a dropdown of Villa =>
            //IEnumerable<SelectListItem> list = _db.Villas.ToList().Select(u => new SelectListItem
            //{
            //    Text = u.Name,
            //    Value = u.Id.ToString()
            //});
            // We need to pass this collection (list) into view but can not pass directly because view is expecting VillaNumber model, instead of IEnumerable<SelectListItem>
            // So we can use ViewData dynamic property to pass this list to view.
            // ViewData = transfer data from controller to view using key-value pair.
            //ViewData["VillaList"] = list;
            #endregion

            return View(villaNumberVM);

        }

        [HttpPost]
        public IActionResult Create(VillaNumber villaNumber)
        {
            // modelstate does not gets value for (Public Villa Villa) - this is becasue its a navigation property.
            // To ignore this navigation property by ModelState We Can write =>
            // ModelState.Remove("Villa"); OR can add Attribute [ValidateNever] in Domain class VillaNumber.cs


            if (ModelState.IsValid)
            {
                _db.VillaNumbers.Add(villaNumber);
                _db.SaveChanges();
                TempData["success"] = "The villa number has been created successfully";
                return RedirectToAction("Index", "VillaNumber");
            }

            return View();
        }


    }
}
