using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult blog()
        {
            return View();
        }

        public ActionResult blog_detail()
        {
            return View();
        }
    }
}