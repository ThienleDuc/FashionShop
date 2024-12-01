using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionShop.Models.LeDucThien.ThienProcessData;

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

            pd_Voucher voucherProcess = new pd_Voucher();
            pd_DieuKienGiam dieuKienGiamProcess = new pd_DieuKienGiam();
            pd_TrangThaiGiam trangThaiGiamProccess = new pd_TrangThaiGiam();
            pd_VoucherCuaToi voucherCuaToiProcess = new pd_VoucherCuaToi();

            return View();
        }
    }
}