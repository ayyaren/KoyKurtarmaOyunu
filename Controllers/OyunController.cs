using Microsoft.AspNetCore.Mvc;
using KoyKurtarmaOyunu.Models;
using System.Collections.Generic;
using System.Linq;

namespace KoyKurtarmaOyunu.Controllers
{
    public class OyunController : Controller
    {
        // Oyun durumunu uygulama süresi boyunca saklayacağımız statik örnek
        // (Basitlik için, ileride kalıcı saklama veya Session ile değiştirebiliriz)
        private static GameState oyunDurumu;

        // İlk defa oyun başlatıldığında 7 köyü kuyruğa ekle
        private void OyunBaslat()
        {
            if (oyunDurumu == null)
            {
                oyunDurumu = new GameState();

                // Örnek 7 köy ve içindeki envanter (en az 3 item)
                var koyler = new List<Koy>
                {
                    new Koy{ Id = 1, Ad = "Köy 1", Envanter = new List<Item> {
                        new Item { Id = 1, Ad = "Kılıç", Guç = 10 },
                        new Item { Id = 2, Ad = "Harita", Guç = 5 },
                        new Item { Id = 3, Ad = "İksir", Guç = 7 }
                    }},
                    new Koy{ Id = 2, Ad = "Köy 2", Envanter = new List<Item> {
                        new Item { Id = 4, Ad = "Balta", Guç = 8 },
                        new Item { Id = 5, Ad = "Yiyecek", Guç = 4 },
                        new Item { Id = 6, Ad = "Altın", Guç = 6 }
                    }},
                    // Diğer 5 köyü benzer şekilde ekleyebiliriz
                };

                // Kuyruğa sırayla ekle
                foreach (var koy in koyler)
                    oyunDurumu.KurtarilacakKoyler.Enqueue(koy);

                oyunDurumu.MevcutKoy = oyunDurumu.KurtarilacakKoyler.Peek();
            }
        }

        // Ana sayfa: mevcut durum ve köyler
        [HttpGet]
        public IActionResult Index()
        {
            OyunBaslat();

            ViewBag.MevcutKoy = oyunDurumu.MevcutKoy;
            ViewBag.KurtarilanKoyler = oyunDurumu.KurtarilanKoyler;
            ViewBag.KurtarilacakKoyler = oyunDurumu.KurtarilacakKoyler.ToList();
            ViewBag.Canta = oyunDurumu.OyuncuCantasi.GetItems();

            return View();
        }

        // Köyü kurtarma aksiyonu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult KoyKurtar()
        {
            if (oyunDurumu == null || oyunDurumu.KurtarilacakKoyler.Count == 0)
                return RedirectToAction("Index");

            var koy = oyunDurumu.KurtarilacakKoyler.Dequeue();

            // Envanter maddelerini çantaya ekle (kapasite kontrolü yap)
            foreach (var item in koy.Envanter)
            {
                bool eklendi = oyunDurumu.OyuncuCantasi.Push(item);
                if (!eklendi)
                {
                    // Kapasite doldu, hata mesajı verebiliriz ya da eskilerden çıkarıp ekleme yapılabilir
                    TempData["Hata"] = "Çanta dolu! Yeni öğe eklenemedi: " + item.Ad;
                    break;
                }
            }

            oyunDurumu.KurtarilanKoyler.Add(koy);

            // Yeni mevcut köyü güncelle (kuyruğun ilk elemanı)
            oyunDurumu.MevcutKoy = oyunDurumu.KurtarilacakKoyler.Count > 0 ? oyunDurumu.KurtarilacakKoyler.Peek() : null;

            TempData["Mesaj"] = koy.Ad + " başarıyla kurtarıldı!";

            return RedirectToAction("Index");
        }

        // Çantayı görüntüleme
        [HttpGet]
        public IActionResult CantaGoruntule()
        {
            ViewBag.Canta = oyunDurumu?.OyuncuCantasi.GetItems() ?? new List<Item>();
            return View();
        }

        // Çantadaki öğeyi kullanma
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ItemKullan(string itemAdi)
        {
            if (oyunDurumu == null)
                return RedirectToAction("Index");

            bool kullanildi = oyunDurumu.OyuncuCantasi.UseItem(itemAdi);
            if (kullanildi)
            {
                TempData["Mesaj"] = $"{itemAdi} başarıyla kullanıldı.";
            }
            else
            {
                TempData["Hata"] = $"{itemAdi} çantada bulunamadı.";
            }

            return RedirectToAction("Index");
        }
    }
}

