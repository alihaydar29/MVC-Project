using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.entity; //entity klasörüne ulaş

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLkategori.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKategori()  //sadece sayfayı gri dödürme işlemi yap diyor.
        {         
            return View();                                    
        }
                                                     // !!! htppget ve htppPost un temel amacı işlem yapılmayan durumlarda veriyi entegre etmemek .eğer edilirse gereksiz bir iş olur.
        [HttpPost] //örneğin kaydet butonuna bastıgım zaman kaydet işlemini gerçekleştir demek için kullanıyoruz.
         public ActionResult YeniKategori(TBLkategori p1)
        {
            if(!ModelState.IsValid)    //! ekledik
            {
                return View("YeniKategori");  //YeniKategori şeklinde yazdık
            }
            db.TBLkategori.Add(p1);
            db.SaveChanges();
            return View();
        }
        public ActionResult SIL(int id)
        {
            var kategori = db.TBLkategori.Find(id);
            db.TBLkategori.Remove(kategori);     // kategoriden gelen degeri silecek.
            db.SaveChanges();                   // değişiklikleri kaydet.
            return RedirectToAction("Index");  // yönlendrme yapar index sayfasına
     
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TBLkategori.Find(id);
            return View("KategoriGetir", ktgr);
        }
        public ActionResult Guncelle(TBLkategori p1)
        {
            var ktg = db.TBLkategori.Find(p1.KATEGORIID);
            ktg.KATEGORIAD = p1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}






