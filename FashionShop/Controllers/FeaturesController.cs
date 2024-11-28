using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class FeaturesController : Controller
    {
        // GET: Features
        public ActionResult shoping_cart()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }
    }
}