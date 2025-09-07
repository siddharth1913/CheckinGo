using CheckinGo.Domain.Entites;
using CheckinGo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace CheckinGo.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;
        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var villa = _db.Villas.ToList();
            return View(villa);
        }

        public IActionResult CreateVilla()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateVilla(Villa _villaObj)
        {
            if (_villaObj.Name == _villaObj.Description)
            {
                // Is key not specified then they it will show mssge on top of form because of ValidationSummary tag helper which is used in View
                ModelState.AddModelError("Name", "The Name and Description cannot be the same.");
            }

            if (ModelState.IsValid)
            {
                _db.Villas.Add(_villaObj);
                _db.SaveChanges();
                TempData["success"] = "The villa has been created successfully";
                return RedirectToAction("Index", "Villa");
            }
            return View(_villaObj);
        }

        // Get endpoint
        public IActionResult Update(int? villaId)
        {
            Villa? villaObj = _db.Villas.FirstOrDefault(c => c.Id == villaId);
            if (villaObj == null)
            {
                return RedirectToAction("Erro", "Home");
            }
            return View(villaObj);
        }

        [HttpPost]
        public IActionResult Update(Villa _villaObj)
        {

            if (ModelState.IsValid)
            {
                _db.Villas.Update(_villaObj);
                _db.SaveChanges();
                TempData["success"] = "The villa has been Updated successfully";
                return RedirectToAction("Index", "Villa");
            }
            return View(_villaObj);
        }

        public IActionResult Delete(int? villaId)
        {
            Villa? villaObj = _db.Villas.FirstOrDefault(c => c.Id == villaId);
            if (villaObj is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(villaObj);
        }

        [HttpPost]
        public IActionResult Delete(Villa _villaObj)
        {
            Villa? obj = _db.Villas.FirstOrDefault(c => c.Id == _villaObj.Id);
            if (obj is not null)
            {
                _db.Villas.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "The villa has been deleted successfully";
                return RedirectToAction("Index", "Villa");
            }
            TempData["error"] = "The villa could not be deleted";

            return View();
            //return View(obj);
        }
    }


}
