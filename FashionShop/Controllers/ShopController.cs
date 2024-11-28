using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult product()
        {
            return View();
        }

        public ActionResult product_detail()
        {
            return View();
        }
    }
}