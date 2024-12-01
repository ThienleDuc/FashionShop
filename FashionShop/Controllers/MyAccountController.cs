using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionShop.Models.LeDucThien.ThienProcessData;

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

            pd_KhachHang khachHangProcess = new pd_KhachHang();

            // Truy vấn thông tin người dùng từ cơ sở dữ liệu theo tên đăng nhập
            var user = khachHangProcess.GetKhachHangWhere(username).FirstOrDefault();

            // Lưu trữ các giá trị vào ViewBag để gửi sang View
            ViewBag.Username = user.Username;
            ViewBag.FirstName = user.FirstName;
            ViewBag.LastName = user.LastName;
            ViewBag.Gender = user.Gender;
            ViewBag.Day = user.Day;
            ViewBag.Month = user.Moth;
            ViewBag.Year = user.Year;
            ViewBag.Avatar = user.Anh;

            return View();
        }

        public ActionResult NganHang()
        {
            // Lấy thông tin username từ Cookie
            HttpCookie usernameCookie = Request.Cookies["Username"];
            var username = usernameCookie != null ? usernameCookie.Value : string.Empty;

            // Khởi tạo lớp xử lý dữ liệu ngân hàng
            pd_TaiKhoanNganHangCuaToi taiKhoanNganHangCuaToiProcees = new pd_TaiKhoanNganHangCuaToi();
            pd_NganHangDuocLienKet nganHangDuocLienKetProcess = new pd_NganHangDuocLienKet();

            var myBank = taiKhoanNganHangCuaToiProcees.GetTaiKhoanNganHangCuaToiWhere(username).FirstOrDefault();
            int maNganHangLienKet = myBank.MaNganHangLienKet;
            var bankLink = nganHangDuocLienKetProcess.GetNganHangDuocLienKetWhereMaNganHangLienKet(maNganHangLienKet).FirstOrDefault();


            ViewBag.BankName = bankLink.TenNganHang;
            ViewBag.BankLogo = bankLink.AnhNganHang;
            ViewBag.OwnerName = myBank.TenChuSoHuu;
            ViewBag.Branch = myBank.TenChiNhanh;

            return View();
        }


        public ActionResult DiaChi()
        {
            // Giả sử bạn lấy thông tin từ Cookie hoặc Database
            HttpCookie usernameCookie = Request.Cookies["Username"];
            var username = usernameCookie != null ? usernameCookie.Value : string.Empty;

            pd_DiaChiGiaoHang diaChiGiaoHangProcess = new pd_DiaChiGiaoHang();
            pd_TinhThanh tinhThanhProcess = new pd_TinhThanh();
            pd_XaPhuong xaPhuongProcess = new pd_XaPhuong();
            pd_QuanHuyen quanHuyenProcess = new pd_QuanHuyen();
            

            var diaChiGiaoHang = diaChiGiaoHangProcess.GetDiaChiGiaoHangWhere(username).FirstOrDefault();

            int maTinhThanh = diaChiGiaoHang.MaTinhThanh;
            int maQuanHuyen = diaChiGiaoHang.MaQuanHuyen;
            int maXaPhuong = diaChiGiaoHang.MaXaPhuong;

            ViewBag.TenTinhThanh = tinhThanhProcess.GetTenTinhThanh(maTinhThanh);
            ViewBag.TenQuanHuyen = quanHuyenProcess.GetTenQuanHuyen(maQuanHuyen);
            ViewBag.TenXaPhuong = xaPhuongProcess.GetTenXaPhuong(maXaPhuong);

            ViewBag.TenKhachHang = diaChiGiaoHang.TenKhachHang;
            ViewBag.SDT = diaChiGiaoHang.SDT;
            ViewBag.DiaChi = diaChiGiaoHang.DiaChiGiaoHang;

            return View();
        }

        public ActionResult MatKhau()
        {
            return View();
        }
    }
}