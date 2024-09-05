using System.Linq;
using System.Web.Mvc;
using YourNamespace.Models;

namespace YourNamespace.Controllers
{
    public class InsureeController : Controller
    {
        private InsuranceContext db = new InsuranceContext();

        // GET: Insuree
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insuree/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insuree/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Email,Age,CarYear,CarMake,CarModel,SpeedingTickets,HasDUI,CoverageType")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuree);
        }

        public ActionResult CalculateQuote(int id)
        {
            var insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }

            // Base quote
            decimal quote = 50m;

            // Age-based adjustment
            if (insuree.Age <= 18) quote += 100m;
            else if (insuree.Age >= 19 && insuree.Age <= 25) quote += 50m;
            else if (insuree.Age >= 26) quote += 25m;

            // Car year-based adjustment
            if (insuree.CarYear < 2000) quote += 25m;
            if (insuree.CarYear > 2015) quote += 25m;

            // Car make and model adjustments
            if (insuree.CarMake.ToLower() == "porsche")
            {
                quote += 25m;
                if (insuree.CarModel.ToLower() == "911 carrera")
                {
                    quote += 25m;
                }
            }

            // Speeding ticket adjustment
            quote += insuree.SpeedingTickets * 10m;

            // DUI adjustment
            if (insuree.HasDUI) quote *= 1.25m;

            // Coverage type adjustment
            if (insuree.CoverageType.ToLower() == "full") quote *= 1.50m;

            // Save quote
            insuree.Quote = quote;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Admin View to see all quotes
        public ActionResult Admin()
        {
            return View(db.Insurees.ToList());
        }
    }
}
