using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.entity;
namespace MvcStok.Controllers

{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLurunler.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.TBLkategori.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()

                                             }
                                             ).ToList();
            ViewBag.dgr = degerler;                       
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(TBLurunler p1) 
        {
            var ktg = db.TBLkategori.Where(m => m.KATEGORIID == p1.TBLkategori.KATEGORIID).FirstOrDefault();   // seçmiş oldugumuz ilk degeri bize getirir.
            p1.TBLkategori = ktg;
            db.TBLurunler.Add(p1);             // add metodunu da ekleme için kullan.
            db.SaveChanges();

            return RedirectToAction("Index");  // bizi kaydetme işlemini gerçekleşirdikten sonra index sayfasına yönlendirsin
        }
        public ActionResult SIL(int id)
        {
            var urun = db.TBLurunler.Find(id);     // find komutu ile bul remove komutu ile kaldır
            db.TBLurunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLurunler.Find(id);

            List<SelectListItem> degerler = (from i in db.TBLkategori.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()

                                             }
                                            ).ToList();
            ViewBag.dgr = degerler;

            return View("UrunGetir", urun);
        }
        public ActionResult Guncelle(TBLurunler p)
        {

            var urun = db.TBLurunler.Find(p.URUNID);
            urun.URUNAD = p.URUNAD;
            urun.MARKA = p.MARKA;
            urun.STOK = p.STOK;
            urun.FIYAT =p.FIYAT;
            //urun katogori katası verilen değerler uyuşmuyor.
            urun.URUNKATEGORI = p.URUNKATEGORI;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}

