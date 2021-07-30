using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFrontLab.UI.MVC.Models;


namespace StoreFrontLab.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart -- This action will generate a view with a list of CartItemViewModel objects
        public ActionResult Index()
        {
            //pull session-based cart info intoa  local variable that we can pass to the view
            var shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            //Does the shoppingCart exist and have items in it?
            if (shoppingCart == null || shoppingCart.Count == 0)
            {
                //The user either hasn't put anything in the cart, or removed all, or the session expired
                shoppingCart = new Dictionary<int, CartItemViewModel>();

                //Create message about empty cart
                ViewBag.Message = "There are no items in your cart";
            }
            else
            {
                ViewBag.Message = null;//explicitly clear out the variable
            }

            return View(shoppingCart);
        }

        public ActionResult UpdateCart(int weaponID, int qty)
        {
            //get the cart from session and place its value in a local shoppingCart variable
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            //target correct cartItem using bookID for key - then change the Qty property with the qty parameter
            shoppingCart[weaponID].Qty = qty;

            //return the local cart tot he session and redirect the user back to shoppingCart to see their changes
            Session["cart"] = shoppingCart;

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int id)
        {
            //get the cart from session and place it in a local variable
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            //remove the item from the local cart
            shoppingCart.Remove(id);

            //Update session
            Session["cart"] = shoppingCart;

            return RedirectToAction("Index");
        }
    }
}