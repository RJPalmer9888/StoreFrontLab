using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreFrontLab.DATA.EF;//added for access to domain models (Book)
using System.ComponentModel.DataAnnotations;//Added for validation/display metadata

namespace StoreFrontLab.UI.MVC.Models
{
    //Added this ViewModel to combine Domain-related info (Book Product) with other info (int Qty) - Therefore this is a ViewModel
    public class CartItemViewModel
    {
        [Range(1, int.MaxValue)] //Ensure the values in the cart are greater than 0
        public int Qty { get; set; }

        public Weapon Product { get; set; }

        //fqctor
        public CartItemViewModel(int qty, Weapon product)
        {
            //prop = param
            Qty = qty;
            Product = product;
        }
    }
}