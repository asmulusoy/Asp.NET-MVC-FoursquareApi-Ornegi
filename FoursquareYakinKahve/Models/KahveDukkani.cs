using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoursquareYakinKahve.Models
{
    public class KahveDukkani
    {
        public string id { get; set; }
        public string name { get; set; }
        public string Telefon { get; set; } = "Girilmemiş";
        public string address { get; set; } = "Girilmemiş"; //formattedadress i çekeceğim buraya ayırarak
        public string url { get; set; } = "Girilmemiş";
        public decimal lat { get; set; }
        public decimal lng { get; set; }
        public string photoUrl { get; set; }

    }
}