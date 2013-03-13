using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SatCtrl
{
    public partial class _GroundStation : System.Web.UI.Page
    {
        public string LatitudeName;
        public string Latitude;
        public string Longitude;
        public string Weather;
        public string WeatherTitle;
        public string WeatherTitle2;
        public string WeatherTitlepwscode;
        public string AntennaAzimuth;
        public string AntennaAltitude;
        public string CalulatedAzimuth;
        public string CalulatedAltitude;
        public string GrStnStatus;
        protected void Page_Load(object sender, EventArgs e)
        {
            Latitude = "49.257735";
            Longitude = "-123.123904";
            Weather = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:00000.1.71892&amp;bannertypeclick=wu_clean2day";
            WeatherTitlepwscode = "http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_clean2day_metric_cond&amp;pwscode=IBRITSHC3&amp;ForcedCity=Vancouver&amp;ForcedState=Canada&amp;wmo=71892&amp;language=EN";
            WeatherTitle = "Vancouver, British Columbia Weather Forecast";
            WeatherTitle2 = "Find more about Weather in Vancouver, CA";
            LatitudeName = "Vancouver";
            ImagePoint.ImageUrl = "~/Img/SDC10316.JPG";
            String Location = Page.Request.QueryString["st"];
            if (Location == null)
                Location = "1";
            AntennaAzimuth = "000.000";
            AntennaAltitude = "00.000";
            CalulatedAzimuth = "000.000";
            CalulatedAltitude = "00.000";
            GrStnStatus = "OFFLINE";

            string strDefaultMainGrStn = "1";
            if (HttpContext.Current.Session["DefaultMainGrStn"] != null)
            {
                strDefaultMainGrStn = HttpContext.Current.Session["DefaultMainGrStn"].ToString();
            }
            else
            {
                strDefaultMainGrStn = System.Configuration.ConfigurationManager.AppSettings["DefaultMainGrStn"];
                HttpContext.Current.Session["DefaultMainGrStn"] = strDefaultMainGrStn;
            }
            //MainGroundStation.Checked = false;
            if (strDefaultMainGrStn == Location)
            {
                MainGroundStation.Checked = true;
            }
            if (Location == "1")
            {
                Latitude = "49.257735";
                Longitude = "-123.123904";
                Weather = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:00000.1.71892&amp;bannertypeclick=wu_clean2day";
                WeatherTitlepwscode = "http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_clean2day_metric_cond&amp;pwscode=IBRITSHC3&amp;ForcedCity=Vancouver&amp;ForcedState=Canada&amp;wmo=71892&amp;language=EN";
                WeatherTitle = "Vancouver, British Columbia Weather Forecast";
                WeatherTitle2 = "Find more about Weather in Vancouver, CA";
                LatitudeName = "Vancouver, BC";
                ImagePoint.ImageUrl = "~/Img/SDC10316.JPG";
                GrStnStatus = "OFFLINE";
                
            }
            if (Location == "2")
            {
                Latitude = "27.306121";
                Longitude = "-82.569112";
                Weather = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:34230.1.99999&bannertypeclick=wu_clean2day";
                WeatherTitlepwscode = "http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_clean2day_metric_cond&airportcode=KSRQ&ForcedCity=Sarasota&ForcedState=FL&zip=34230&language=EN";
                WeatherTitle = "Sarasota, Florida Weather Forecast";
                WeatherTitle2 = "Find more about Weather in Sarasota, FL";
                LatitudeName = "Sarasota, Florida (TBD)";
                ImagePoint.ImageUrl = "~/Img/empty.JPG";
                GrStnStatus = "OFFLINE";
            }
            if (Location == "3")
            {
                Latitude = "-7.153526";
                Longitude = "-34.88966";
                Weather = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:00000.1.82798&bannertypeclick=wu_clean2day";
                WeatherTitlepwscode = "http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_clean2day_metric_cond&airportcode=SBJP&ForcedCity=Joao Pessoa&ForcedState=Brazil&wmo=82798&language=EN";
                WeatherTitle = "Joao Pessoa, Brazil Weather Forecast";
                WeatherTitle2 = "Find more about Weather in Joao Pessoa, BZ";
                LatitudeName = "Joao Pessoa, East Brasilia (TBD)";
                ImagePoint.ImageUrl = "~/Img/empty.JPG";
                GrStnStatus = "OFFLINE";
            }
            if (Location == "4")
            {
                Latitude = "-33.916743";
                Longitude = "18.402658";
                Weather = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:00000.1.68816&bannertypeclick=wu_clean2day";
                WeatherTitlepwscode = "http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_clean2day_metric_cond&airportcode=FACT&ForcedCity=Cape Town&ForcedState=South Africa&wmo=68816&language=EN";
                WeatherTitle = "Cape Town, South Africa Weather Forecast";
                WeatherTitle2 = "Find more about Weather in Cape Town, ZA";
                LatitudeName = " Cape Town, South Africa(TBD)";
                ImagePoint.ImageUrl = "~/Img/empty.JPG";
                GrStnStatus = "OFFLINE";
            }
            if (Location == "5")
            {
                Latitude = "47.971091";
                Longitude = "37.712352";
                Weather = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:00000.1.WUKCC&bannertypeclick=wu_clean2day";
                WeatherTitlepwscode = "http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_clean2day_metric_cond&airportcode=UKCC&ForcedCity=Donetsk&ForcedState=Ukraine&zip=00000&language=EN";
                WeatherTitle = "Donetsk, Ukraine Weather Forecast";
                WeatherTitle2 = "Find more about Weather in Donetsk, UR";
                LatitudeName = "Donetsk, Ukraine (TBD)";
                ImagePoint.ImageUrl = "~/Img/empty.JPG";
                GrStnStatus = "OFFLINE";
            }
            if (Location == "6")
            {
                Latitude = "49.789355";
                Longitude = "73.070583";
                Weather = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:00000.1.WUAKK&bannertypeclick=wu_clean2day";
                WeatherTitlepwscode = "http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_clean2day_metric_cond&airportcode=UAKK&ForcedCity=Karaganda&ForcedState=Kazakhstan&zip=00000&language=EN";
                WeatherTitle = "Karaganda, Kazakhstan Weather Forecast";
                WeatherTitle2 = "Find more about Weather in Karaganda, KZ";
                LatitudeName = "Karaganda, Kazakhstan (TBD)";
                ImagePoint.ImageUrl = "~/Img/empty.JPG";
                GrStnStatus = "OFFLINE";
            }
            if (Location == "7")
            {
                Latitude = "-31.967891";
                Longitude = "115.883274";
                Weather = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:00000.1.94610&bannertypeclick=wu_clean2day";
                WeatherTitlepwscode = "http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_clean2day_metric_cond&airportcode=YPPH&ForcedCity=Perth&ForcedState=Australia&wmo=94610&language=EN";
                WeatherTitle = "Perth, Western Australia Weather Forecast";
                WeatherTitle2 = "Find more about Weather in Perth, AU";
                LatitudeName = "Perth, Western Australia (TBD)";
                ImagePoint.ImageUrl = "~/Img/empty.JPG";
                GrStnStatus = "OFFLINE";
            }
            if (Location == "8")
            {
                Latitude = "19.707243";
                Longitude = "-155.064983";
                Weather = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:96720.1.99999&bannertypeclick=wu_clean2day";
                WeatherTitlepwscode = "http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_clean2day_metric_cond&airportcode=PHTO&ForcedCity=Hilo&ForcedState=HI&zip=96720&language=EN";
                WeatherTitle = "Hilo, Hawaii Weather Forecast";
                WeatherTitle2 = "Find more about Weather in Hilo, HI";
                LatitudeName = "Hilo, Hawaii (TBD)";
                ImagePoint.ImageUrl = "~/Img/empty.JPG";
                GrStnStatus = "OFFLINE";
            }
            if (Location == "9")
            {
                Latitude = "-21.255302";
                Longitude = "-159.768333";

                Weather = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:00000.1.91844&bannertypeclick=wu_clean2day";
                WeatherTitlepwscode = "http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_clean2day_metric_cond&airportcode=NCRG&ForcedCity=Rarotonga Aws&ForcedState=Cook Islands&wmo=91844&language=EN";
                WeatherTitle = "Rarotonga Aws, Cook Islands Weather Forecast";
                WeatherTitle2 = "Find more about Weather in Rarotonga Aws, KU";
                LatitudeName = "Rarotonga Aws, Cook Islands (TBD)";
                ImagePoint.ImageUrl = "~/Img/empty.JPG";
                GrStnStatus = "OFFLINE";
            }
            if (GrStnStatus == "OFFLINE")
            {
                StatusText.ForeColor = System.Drawing.Color.Red;
            }
            if (GrStnStatus == "ONLINE")
            {
                StatusText.ForeColor = System.Drawing.Color.Blue; 
            }
            if (GrStnStatus == "COMLINK")
            {
                StatusText.ForeColor = System.Drawing.Color.Green; 
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            String Location = Page.Request.QueryString["st"];
            if (MainGroundStation.Checked)
            {
                if (Location != null)
                {
                    HttpContext.Current.Session["DefaultMainGrStn"] = Location;
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
