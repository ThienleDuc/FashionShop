using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult order()
        {
            return View();
        }

        public ActionResult ChiTietDonHang()
        {
            ViewBag.TrangThai = "Chờ xác nhận";
            return View();
        }

        public ActionResult ChiTietHuyDonHang()
        {
            ViewBag.TrangThai = "Đã hủy";
            return View();
        }
    }
}