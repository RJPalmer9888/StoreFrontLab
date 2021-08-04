using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFrontLab.DATA.EF;
using StoreFrontLab.UI.MVC.Models;
using StoreFrontLab.UI.MVC.Utilities;

namespace StoreFrontLab.UI.MVC.Controllers
{
    
    public class WeaponsController : Controller
    {
        private StoreFrontEntities1 db = new StoreFrontEntities1();

        // GET: Weapons
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var weapons = db.Weapons.Include(w => w.Archetype).Include(w => w.Element).Include(w => w.InStockStatus).Include(w => w.Manufacturer).Include(w => w.Rarity);
            return View(weapons.ToList());
        }

        // GET: Weapons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weapon weapon = db.Weapons.Find(id);
            if (weapon == null)
            {
                return HttpNotFound();
            }
            return View(weapon);
        }

        // GET: Weapons/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ArchetypeID = new SelectList(db.Archetypes, "ArchetypeID", "Archetype1");
            ViewBag.ElementID = new SelectList(db.Elements, "ElementID", "Element1");
            ViewBag.InStockID = new SelectList(db.InStockStatus1, "InStockID", "Status");
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "Manufacturer1");
            ViewBag.RarityID = new SelectList(db.Rarities, "RarityID", "Rarity1");
            return View();
        }

        // POST: Weapons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "WeaponID,WeaponName,Description,ArchetypeID,ElementID,RarityID,ManufacturerID,Image,InStockID")] Weapon weapon, HttpPostedFileBase weaponImage)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                string file = "NoImage.png";

                if (weapon != null)
                {
                    file = weaponImage.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    //Check that the uploaded file is in our list of acceptable exts and file size <= 4mb max from ASP.NET
                    if (goodExts.Contains(ext.ToLower()) && weaponImage.ContentLength <= 4194303)
                    {
                        //Create a new file name (using a GUID)
                        file = Guid.NewGuid() + ext;

                        #region Resize Image
                        string savePath = Server.MapPath("~/imgstore/weapons/");

                        Image convertedImage = Image.FromStream(weaponImage.InputStream);

                        int maxImageSize = 500;

                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                        #endregion
                    }

                    //no matter what, update the PhotoUrl witht he value of the file variable
                    weapon.Image = file;
                }
                #endregion
                db.Weapons.Add(weapon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArchetypeID = new SelectList(db.Archetypes, "ArchetypeID", "Archetype1", weapon.ArchetypeID);
            ViewBag.ElementID = new SelectList(db.Elements, "ElementID", "Element1", weapon.ElementID);
            ViewBag.InStockID = new SelectList(db.InStockStatus1, "InStockID", "Status", weapon.InStockID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "Manufacturer1", weapon.ManufacturerID);
            ViewBag.RarityID = new SelectList(db.Rarities, "RarityID", "Rarity1", weapon.RarityID);
            return View(weapon);
        }

        // GET: Weapons/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weapon weapon = db.Weapons.Find(id);
            if (weapon == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArchetypeID = new SelectList(db.Archetypes, "ArchetypeID", "Archetype1", weapon.ArchetypeID);
            ViewBag.ElementID = new SelectList(db.Elements, "ElementID", "Element1", weapon.ElementID);
            ViewBag.InStockID = new SelectList(db.InStockStatus1, "InStockID", "Status", weapon.InStockID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "Manufacturer1", weapon.ManufacturerID);
            ViewBag.RarityID = new SelectList(db.Rarities, "RarityID", "Rarity1", weapon.RarityID);
            return View(weapon);
        }

        // POST: Weapons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "WeaponID,WeaponName,Description,ArchetypeID,ElementID,RarityID,ManufacturerID,Image,InStockID")] Weapon weapon, HttpPostedFileBase weaponImageEdit)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                string file = "NoImage.png";

                if (weaponImageEdit != null)
                {
                    file = weaponImageEdit.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    //Check that the uploaded file is in our list of acceptable exts and file size <= 4mb max from ASP.NET
                    if (goodExts.Contains(ext.ToLower()) && weaponImageEdit.ContentLength <= 4194303)
                    {
                        //Create a new file name (using a GUID)
                        file = Guid.NewGuid() + ext;

                        #region Resize Image
                        string savePath = Server.MapPath("~/imgstore/weapons/");

                        Image convertedImage = Image.FromStream(weaponImageEdit.InputStream);

                        int maxImageSize = 500;

                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                        #endregion

                        if (weapon.Image != null && weapon.Image != "NoImage.png")
                        {
                            string path = Server.MapPath("~/imgstore/weapons/");
                            ImageUtility.Delete(path, weapon.Image);
                        }
                    }

                    //no matter what, update the PhotoUrl witht he value of the file variable
                    weapon.Image = file;
                }
                #endregion
                db.Entry(weapon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArchetypeID = new SelectList(db.Archetypes, "ArchetypeID", "Archetype1", weapon.ArchetypeID);
            ViewBag.ElementID = new SelectList(db.Elements, "ElementID", "Element1", weapon.ElementID);
            ViewBag.InStockID = new SelectList(db.InStockStatus1, "InStockID", "Status", weapon.InStockID);
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "ManufacturerID", "Manufacturer1", weapon.ManufacturerID);
            ViewBag.RarityID = new SelectList(db.Rarities, "RarityID", "Rarity1", weapon.RarityID);
            return View(weapon);
        }

        // GET: Weapons/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weapon weapon = db.Weapons.Find(id);
            if (weapon == null)
            {
                return HttpNotFound();
            }
            return View(weapon);
        }

        // POST: Weapons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Weapon weapon = db.Weapons.Find(id);
            db.Weapons.Remove(weapon);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region Add to Cart functionality (Called from the Details View)
        public ActionResult AddToCart(int qty, int weaponID)
        {
            //Create an empty shell of a collection for gaining access LOCALLY (in this action) to the Session cart variable
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            //Check if that session-cart exists...if so, use it to populate the local shoppingCart
            if (Session["cart"] != null)
            {
                //Session-cart exists - put its items in the LOCAL shoppingCart
                shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];
                //When we UNBOX Session object to its smaller, more specific type, we use explicit casting
            }
            else
            {
                //if session cart doesn't exist at this point, we need to instantiate it, or New It Up
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }
            //At this point, we now have a local variable that's ready to add things to it.

            //Find the product they reference by its ID
            Weapon product = db.Weapons.Where(b => b.WeaponID == weaponID).FirstOrDefault();
            if (product == null)
            {
                //it's a bad ID, kick them back to some page to try again
                return RedirectToAction("Index");
            }
            else
            {
                //if book is VALID, add the line-item to the cart
                CartItemViewModel item = new CartItemViewModel(qty, product);

                //put item in the local cart BUT if they have already added that specific product to the cart, then we will only update the qty. This is why we have the dictionary
                if (shoppingCart.ContainsKey(product.WeaponID))
                {
                    shoppingCart[product.WeaponID].Qty += qty;
                }
                else
                {
                    shoppingCart.Add(product.WeaponID, item);
                }

                //now update the SESSION version of the shoppingCart, so we can maintain that information between all of the requests/responses
                Session["cart"] = shoppingCart;//casting from a smaller container to a bigger container, no explicit cast is needed
            }

            //send the user to view their cart items - This action that we are sending the user to is housed in the ShoppingCartController
            return RedirectToAction("Index", "ShoppingCart");
        }
        #endregion
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
