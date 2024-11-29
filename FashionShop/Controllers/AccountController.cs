using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionShop.Models.LeDucThien.Entity;
using FashionShop.Models.LeDucThien.ProcessData;
using FashionShop.Models.OtherMethods;

namespace FashionShop.Controllers
{
    public class AccountController : Controller
    {
        private pd_AccountUser accountUserProcess = new pd_AccountUser();

        // Phương thức đăng nhập
        [HttpPost]
        public JsonResult Login(string username, string password)
        {
            // Kiểm tra thông tin đăng nhập
            var accountUsers = accountUserProcess.GetAccountUsers();
            var user = accountUsers.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null) // Đăng nhập thành công
            {
                // Lưu trạng thái đăng nhập và tên người dùng vào cookie
                CookieHelper.SetLoginCookies(username);

                return Json(new { success = true });
            }
            else
            {
                // Nếu thông tin đăng nhập không hợp lệ
                return Json(new { success = false });
            }
        }

        // Trang đăng nhập
        public ActionResult Login()
        {
            return View();
        }

        // Phương thức đăng xuất
        public ActionResult Logout()
        {
            // Xóa cookie khi người dùng đăng xuất
            CookieHelper.ClearLoginCookies();

            return RedirectToAction("Index", "Home");
        }

        // Phương thức đăng ký
        [HttpPost]
        public JsonResult Register(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password)) // Đảm bảo thông tin không rỗng
            {
                // Lưu thông tin đăng ký vào cookie thông qua CookieHelper
                CookieHelper.SetRegisterCookies(username, password);

                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        // Trang đăng ký
        public ActionResult Register()
        {
            return View();
        }

    }
}