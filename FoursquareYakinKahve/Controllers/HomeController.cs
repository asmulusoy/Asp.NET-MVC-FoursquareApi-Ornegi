using FoursquareYakinKahve.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FoursquareYakinKahve.Controllers
{
    public class HomeController : Controller
    {
        public const string clientID = "Client Id'niz";
        public const string clientSecret = "Client Secret'iniz";
        public string categoryCode = "4bf58dd8d48988d1e0931735"; //kahve dükkanı kategori kodu
        public const string apiUrl = "https://api.foursquare.com/v2/venues/search";
        public int radius = 500; // alan genişliği

        string latitude = 41.0205225.ToString().Replace(',', '.'); //rasgele koordinasyon verildi
        string longitude = 28.9325358.ToString().Replace(',', '.');

        // GET: Home
        public ActionResult Index()
        {
            string sorgu = apiUrl + "?client_id=" + clientID;
            sorgu += "&client_secret=" + clientSecret;
            sorgu += "&v=" + string.Format("{0:yyyyMMdd}", DateTime.Now);
            sorgu += "&ll=" + latitude + "," + longitude;
            sorgu += "&radius=" + radius.ToString();
            sorgu += "&categoryId=" + categoryCode;

            WebRequest baglanti = HttpWebRequest.Create(sorgu);
            string gelenveri = "";
            using (WebResponse GelenCevap = baglanti.GetResponse())
            {
                StreamReader CevapOku = new StreamReader(GelenCevap.GetResponseStream());
                gelenveri = CevapOku.ReadToEnd();
            }
            JObject obj = JObject.Parse(gelenveri);
            JToken response = obj["response"];
            JArray mekanlar = (JArray)response["venues"];
            List<KahveDukkani> mekanListesi = new List<KahveDukkani>();
            foreach (var item in mekanlar)
            {
                string gelenfotolar = "";
                KahveDukkani yeni = new KahveDukkani();
                yeni.id = item["id"].ToString();
                yeni.name = item["name"].ToString();
                yeni.lat = (decimal)item["location"]["lat"];
                yeni.lng = (decimal)item["location"]["lng"];
                //WebRequest resimbaglantisi = HttpWebRequest.Create("https://api.foursquare.com/v2/venues/" + item["id"].ToString() + "/photos/");
                //using (WebResponse photoGelen = resimbaglantisi.GetResponse())
                //{
                //    StreamReader resimoku = new StreamReader(photoGelen.GetResponseStream());
                //    gelenfotolar = resimoku.ReadToEnd();
                //}
                //yeni.photoUrl = gelenfotolar;

                yeni.address = item["location"]["address"] == null ? "Adres bilgisi girilmemiş." : item["location"]["address"].ToString();
                yeni.Telefon = item["contact"]["formattedPhone"] == null ? "Telefon bilgisi girilmemiş." : item["contact"]["formattedPhone"].ToString();
                yeni.url = item["url"] == null ? "Web sitesi girilmemiş." : item["url"].ToString();
                mekanListesi.Add(yeni);
            }
            ViewBag.deneme = gelenveri;
            return View("Index",mekanListesi);
        }
    }
}