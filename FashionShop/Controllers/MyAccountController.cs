using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FashionShop.Controllers
{
    public class MyAccountController : Controller
    {
        // GET: MyAccount
        public ActionResult HoSo()
        {
            // Giả sử bạn lấy thông tin từ Cookie hoặc Database
            HttpCookie usernameCookie = Request.Cookies["Username"];
            var username = usernameCookie != null ? usernameCookie.Value : string.Empty;

            // Lưu trữ các giá trị vào ViewBag
            ViewBag.Username = username;
            ViewBag.FirstName = "Lê Đức";
            ViewBag.LastName = "Thiện";
            ViewBag.Gender = "Nam"; 
            ViewBag.Day = "02";
            ViewBag.Month = "09";
            ViewBag.Year = "2004";
            ViewBag.Avatar = "https://via.placeholder.com/120";

            return View();
        }

        public ActionResult NganHang()
        {
            ViewBag.BankName = "Ngân Hàng Công Thương";
            ViewBag.Branch = "Đà Nẵng";
            ViewBag.OwnerName = "LE DUC THIEN";
            return View();
        }

        public ActionResult DiaChi()
        {
            ViewBag.Name = "Lê Đức Thiện";
            ViewBag.PhoneNumber = "0862972248";
            ViewBag.Location = "KTX trướng Sư Phạm Kỹ Thuật - Đai Học Đà Nẵng";
            return View();
        }

        public ActionResult MatKhau()
        {
            return View();
        }
    }
}