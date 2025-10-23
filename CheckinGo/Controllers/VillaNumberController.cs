using CheckinGo.Domain.Entities;
using CheckinGo.Infrastructure.Data;
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

        public IActionResult Create()
        {
            //To create a dropdown of Villa =>
            IEnumerable<SelectListItem> list = _db.Villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View();

        }

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
