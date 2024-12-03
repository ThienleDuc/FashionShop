using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionShop.Models.LeMinhToan.ProcessData;
using FashionShop.Models.LeMinhToan.Entity;

namespace FashionShop.Controllers
{
    public class HomeController : Controller
    {
        private pd_SanPham sanPhamProcess = new pd_SanPham();
        private pd_SanPham newSanPham = new pd_SanPham();

        // GET: Home
        public ActionResult Index()
        {
            List<ent_SanPham> TopSanPham = sanPhamProcess.GetTopSanPham();
            List<ent_SanPham> NewSanPham = sanPhamProcess.GetNewSanPham();
            ViewBag.TopSanPham = TopSanPham;
            ViewBag.NewSanPham = NewSanPham;
            return View();
        }
    }
}