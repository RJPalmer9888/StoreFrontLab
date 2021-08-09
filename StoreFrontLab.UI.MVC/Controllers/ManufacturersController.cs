using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFrontLab.DATA.EF;

namespace StoreFrontLab.UI.MVC.Controllers
{
    public class ManufacturersController : Controller
    {
        private StoreFrontEntities1 db = new StoreFrontEntities1();

        // GET: Manufacturers
        public ActionResult Index()
        {
            return View(db.Manufacturers.ToList());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AjaxDelete(int id)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            db.Manufacturers.Remove(manufacturer);
            db.SaveChanges();

            string confirmMessage = string.Format("Deleted Manufacturer {0} from the database", manufacturer.Manufacturer1);
            return Json(new { id = id, message = confirmMessage });
        }

        [HttpGet]
        public PartialViewResult ManufacturerDetails(int id)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            return PartialView(manufacturer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AjaxCreate(Manufacturer manufacturer)
        {
            db.Manufacturers.Add(manufacturer);
            db.SaveChanges();
            return Json(manufacturer);
        }

        [HttpGet]
        public PartialViewResult ManufacturerEdit(int id)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            return PartialView(manufacturer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AjaxEdit(Manufacturer manufacturer)
        {
            db.Entry(manufacturer).State = EntityState.Modified;
            db.SaveChanges();
            return Json(manufacturer);
        }

        // GET: Manufacturers/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Manufacturer manufacturer = db.Manufacturers.Find(id);
        //    if (manufacturer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(manufacturer);
        //}

        //// GET: Manufacturers/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Manufacturers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ManufacturerID,Manufacturer1")] Manufacturer manufacturer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Manufacturers.Add(manufacturer);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(manufacturer);
        //}

        //// GET: Manufacturers/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Manufacturer manufacturer = db.Manufacturers.Find(id);
        //    if (manufacturer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(manufacturer);
        //}

        //// POST: Manufacturers/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ManufacturerID,Manufacturer1")] Manufacturer manufacturer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(manufacturer).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(manufacturer);
        //}

        //// GET: Manufacturers/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Manufacturer manufacturer = db.Manufacturers.Find(id);
        //    if (manufacturer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(manufacturer);
        //}

        //// POST: Manufacturers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Manufacturer manufacturer = db.Manufacturers.Find(id);
        //    db.Manufacturers.Remove(manufacturer);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
