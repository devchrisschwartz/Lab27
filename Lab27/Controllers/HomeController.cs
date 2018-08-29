using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Lab27.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // 42.5720636,-83.451486 wbloomfield coordinates
            HttpWebRequest apiRequest = WebRequest.CreateHttp("https://forecast.weather.gov/MapClick.php?lat=42.5720636&lon=-83.451486&FcstType=json");

            apiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

            HttpWebResponse apiResponse = (HttpWebResponse)apiRequest.GetResponse();

            if (apiResponse.StatusCode == HttpStatusCode.OK) // check if we got an http status of 200
            {
                StreamReader responseData = new StreamReader(apiResponse.GetResponseStream());

                string weather = responseData.ReadToEnd(); // reads the data from the response and stores it in a string

                JObject jsonWeather = JObject.Parse(weather);

                ViewBag.weatherPicture = jsonWeather["data"]["iconLink"];
                ViewBag.weatherText = jsonWeather["data"]["text"];
                
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}