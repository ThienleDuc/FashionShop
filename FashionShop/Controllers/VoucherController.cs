using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionShop.Models.LeDucThien.ThienProcessData;
using FashionShop.Models.LeDucThien.ThienEntity;

namespace FashionShop.Controllers
{
    public class VoucherController : Controller
    {
        // GET: Voucher
        public ActionResult voucher()
        {
            // Giả sử bạn lấy thông tin từ Cookie hoặc Database
            HttpCookie usernameCookie = Request.Cookies["Username"];
            var username = usernameCookie != null ? usernameCookie.Value : string.Empty;

            pd_VoucherCuaToi voucherProcess = new pd_VoucherCuaToi();

            List<ent_VoucherCuaToi> ent_VoucherCuaTois = voucherProcess.GetVoucherCuaToi(username);
;
            ViewBag.voucherCuaToi = ent_VoucherCuaTois;
            return View();
        }

        [HttpPost]
        public ActionResult ThemVoucher(string maVoucher)
        {
            if (!string.IsNullOrEmpty(maVoucher))
            {
                HttpCookie usernameCookie = Request.Cookies["Username"];
                var username = usernameCookie != null ? usernameCookie.Value : string.Empty;

                pd_VoucherCuaToi voucherProcess = new pd_VoucherCuaToi();
                voucherProcess.ThemMaVoucherCuaToi(maVoucher, username);

                // Sau khi thêm thành công, trả lại trang Index
                return RedirectToAction("voucher");
            }

            // Nếu mã voucher không hợp lệ, vẫn trả về trang Index
            return RedirectToAction("voucher");
        }
    }
}