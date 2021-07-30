﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFrontLab.DATA.EF;
using StoreFrontLab.UI.MVC.Utilities;
//For access to the image functionality
using System.Drawing;
//Add the below using statement for access to CartItemViewModel
using StoreFrontLab.UI.MVC.Models;
using PagedList;
using System.Net.Mail;
using System.Collections.Generic;
using System.Configuration;


namespace StoreFrontLab.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private StoreFrontEntities1 db = new StoreFrontEntities1();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            // It is best practice to confirm the "state" of the model
            if (!ModelState.IsValid)
            {
                //  Send them back to the form, by passing the object to the view, 
                //the form returns with the original populated information.
                return View(cvm);
            }

            // Below code only executes if the form (object) passes model validation
            string returnMessage = $"You have received an email from {cvm.Name} with a subject " +
                $"{cvm.Subject}.  Please respond to {cvm.Email} with your response to " +
                $"the following message: <br />{cvm.Message}";

            Boolean isMailSetUp = true;

            if (isMailSetUp)
            {
                //Add using statements for the System Mail
                //Mailmessage Package is what sends the email (System.Net.Mail)
                MailMessage mm = new MailMessage(
                     // From
                     ConfigurationManager.AppSettings["EmailUser"].ToString(),

                    ConfigurationManager.AppSettings["EmailTo"].ToString(),
                    cvm.Subject, returnMessage
                    )
                {

                    //Mailmessage properties
                    //Allow HTML formatting
                    IsBodyHtml = true,

                    //Set Mail priority
                    Priority = MailPriority.High //Default is normal priority
                };

                //Set up the reply list
                mm.ReplyToList.Add(cvm.Email);

                //  SmtpClient - This is the information from the HOST (smarterAsp.net) 
                // that allows the email to actually be sent
                SmtpClient client = new SmtpClient(
                    ConfigurationManager.AppSettings["EmailClient"].ToString());

                //  Client credentials (smarterASP requires your user name and password)
                client.Credentials = new NetworkCredential(
                    ConfigurationManager.AppSettings["EmailUser"].ToString(),
                    ConfigurationManager.AppSettings["EmailPassword"].ToString());

                //  It is possible that the mailserver is down or we may have configuration 
                // issues, so we want to encapsulate our code in a try/catch
                try
                {
                    //Attempt to send the email
                    client.Send(mm);
                }
                catch (Exception ex)
                {
                    ViewBag.CustomerMessage = $"We're sorry your request could not be " +
                        $"completed at this time." +
                        $"  Please try again later.  Error Message: <br /> {ex.StackTrace}";
                    return View(cvm); //  Return the view with the entire message so that 
                                      //  users can copy/paste it for later


                }

            }

            return View("EmailConfirmation", cvm); //Upon successful email send users to
                                                   //a friendly confirmation page

        }

        [HttpGet]
        public ActionResult Products(string searchString, string elementID, int? page = 1)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            ViewBag.pageSize = pageSize;
            ViewBag.elementID = elementID;
            ViewBag.Elements = db.Elements.Select(x => x.Element1).ToList();

            var weapons = db.Weapons.Include(w => w.Archetype).Include(w => w.Element).Include(w => w.InStockStatus).Include(w => w.Manufacturer).Include(w => w.Rarity).OrderBy(w => w.WeaponName).ToList();

            #region Search functionality
            if (!String.IsNullOrEmpty(searchString))
            {
                if (!String.IsNullOrEmpty(elementID))
                {
                    return View((from w in weapons
                                where w.WeaponName.ToLower().Contains(searchString.ToLower()) && w.ElementID == Int32.Parse(elementID)
                                select w).ToPagedList(pageNumber, pageSize));
                }
                return View((from w in weapons
                            where w.WeaponName.ToLower().Contains(searchString.ToLower())
                            select w).ToPagedList(pageNumber, pageSize));
            }

            if (!String.IsNullOrEmpty(elementID))
            {
                return View((from w in weapons
                             where w.ElementID == Int32.Parse(elementID)
                             select w).ToPagedList(pageNumber, pageSize));
            }


            #endregion

            return View((from w in weapons
                         select w).ToPagedList(pageNumber, pageSize));
        }
    }
}
