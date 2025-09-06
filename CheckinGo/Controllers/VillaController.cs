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
        public IActionResult CreateVilla(Villa _villaObj )
        {
            if (ModelState.IsValid)
            {
                _db.Villas.Add(_villaObj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Villa");
            }
            return View(_villaObj);
        }
    }


}
