using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;

namespace SatCtrl
{

    public partial class _GroundStation : System.Web.UI.Page
    {
       
        public string LatitudeName;
        public string Latitude;
        public string Longitude;
        public string Weather;
        public string WeatherMore;
        public string WeatherTitle;
        public string WeatherTitle2;
        public string WeatherTitlepwscode;
        public string AntennaAzimuth;
        public string AntennaAltitude;
        public string CalulatedAzimuth;
        public string CalulatedAltitude;
        public string GrStnStatus;
        public string StationUrl;
        public String MyTestGrStn(String Location)
        {
            String RetStr = "OFFLINE";
            WebRequest request = null;
            Stream dataStream;
            String url = StationUrl + "cmd?g_station=" + Location + "&ping=1";
            request = WebRequest.Create(url);
            if (request != null)
            {
                request.Method = "GET";
                request.ContentType = "text/html";
                request.Timeout = 1000;
                string ResonseString = "";
                try
                {
                    WebResponse resp = request.GetResponse();
                    long sizeresp = resp.ContentLength;
                    dataStream = resp.GetResponseStream();
                    byte[] byteArray = new byte[sizeresp];

                    dataStream.Read(byteArray, 0, (int)sizeresp);
                    dataStream.Close();
                    UTF8Encoding encoding = new UTF8Encoding();
                    ResonseString = encoding.GetString(byteArray);
                    if (ResonseString.IndexOf("Page PING") > 0)
                    {
                        RetStr = "ONLINE";
                    }
                }
                catch (WebException)
                {
                }
            }
            return RetStr;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            String Tempo = HttpContext.Current.Application["strMaxSessionN"].ToString();
            
            Latitude = "49.257735";
            Longitude = "-123.123904";
            Weather = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:00000.1.71892&amp;bannertypeclick=wu_clean2day";
            WeatherMore = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:00000.1.71892&bannertypeclick=wu_clean2day";
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

            string strDefaultMainGrStn = HttpContext.Current.Application["DefaultMainGrStn"].ToString();

            //MainGroundStation.Checked = false;
            if (strDefaultMainGrStn == Location)
            {
                MainGroundStation.Checked = true;
            }
            if (Location == "1")
            {
                Latitude = "49.257735";
                Longitude = "-123.123904";


                WeatherMore = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:00000.1.71892&bannertypeclick=wu_clean2day";
                Weather = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=Vancouver Harbour, CA";
                WeatherTitlepwscode = "http://weathersticker.wunderground.com/weathersticker/sunandmoon_metric/language/english/global/stations/71201.gif";
                WeatherTitle = "Vancouver Harbour, CA Weather Forecast";
                WeatherTitle2 = "Find more about Weather in Vancouver Harbour, CA";
                LatitudeName = "Vancouver, BC";
                ImagePoint.ImageUrl = "~/Img/SDC10316.JPG";
                GrStnStatus = "OFFLINE";
                SationIpAddress.Text = StationUrl = HttpContext.Current.Application["Stn1URL"].ToString();
                
            }
            if (Location == "2")
            {
                Latitude = "27.306121";
                Longitude = "-82.569112";
                WeatherMore = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:34230.1.99999&bannertypeclick=wu_clean2day";
                Weather = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=Sarasota, FL";
                WeatherTitlepwscode = "http://weathersticker.wunderground.com/weathersticker/sunandmoon_metric/language/english/US/FL/Sarasota.gif";
                WeatherTitle = "Sarasota, FL Weather Forecast";
                WeatherTitle2 = "Find more about Weather in Sarasota, FL";
                LatitudeName = "Sarasota, Florida (TBD)";
                ImagePoint.ImageUrl = "~/Img/empty.JPG";
                GrStnStatus = "OFFLINE";
                SationIpAddress.Text = StationUrl = HttpContext.Current.Application["Stn2URL"].ToString();
            }
            if (Location == "3")
            {
                Latitude = "-7.153526";
                Longitude = "-34.88966";
                WeatherMore = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:00000.1.82798&bannertypeclick=wu_clean2day";
                Weather = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=Joao Pessoa, BZ";
                WeatherTitlepwscode = "http://weathersticker.wunderground.com/weathersticker/sunandmoon_metric/language/english/global/stations/82798.gif";
                WeatherTitle = "Joao Pessoa, BZ Weather Forecast";
                WeatherTitle2 = "Find more about Weather in Joao Pessoa, BZ";
                LatitudeName = "Joao Pessoa, East Brasilia (TBD)";
                ImagePoint.ImageUrl = "~/Img/empty.JPG";
                GrStnStatus = "OFFLINE";
                SationIpAddress.Text = StationUrl = HttpContext.Current.Application["Stn3URL"].ToString();
            }
            if (Location == "4")
            {
                Latitude = "-33.916743";
                Longitude = "18.402658";
                WeatherMore = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:00000.1.68816&bannertypeclick=wu_clean2day";
                Weather = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=Cape Town, ZA";
                WeatherTitlepwscode = "http://weathersticker.wunderground.com/weathersticker/sunandmoon_metric/language/english/global/stations/68816.gif";
                WeatherTitle = "Cape Town, ZA Weather Forecast";
                WeatherTitle2 = "Find more about Weather in Cape Town, ZA";
                LatitudeName = " Cape Town, South Africa(TBD)";
                ImagePoint.ImageUrl = "~/Img/empty.JPG";
                GrStnStatus = "OFFLINE";
                SationIpAddress.Text = StationUrl = HttpContext.Current.Application["Stn4URL"].ToString();
            }
            if (Location == "5")
            {
                Latitude = "47.971091";
                Longitude = "37.712352";
                WeatherMore = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:00000.1.WUKCC&bannertypeclick=wu_clean2day";
                Weather = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=Donetsk, UR";
                WeatherTitlepwscode = "http://weathersticker.wunderground.com/weathersticker/sunandmoon_metric/language/english/global/stations/WUKCC.gif";
                WeatherTitle = "Donetsk, UR Weather Forecast";
                WeatherTitle2 = "Find more about Weather in Donetsk, UR";
                LatitudeName = "Donetsk, Ukraine (TBD)";
                ImagePoint.ImageUrl = "~/Img/empty.JPG";
                GrStnStatus = "OFFLINE";
                SationIpAddress.Text = StationUrl = HttpContext.Current.Application["Stn5URL"].ToString();
            }
            if (Location == "6")
            {
                Latitude = "49.789355";
                Longitude = "73.070583";
                WeatherMore = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:00000.1.WUAKK&bannertypeclick=wu_clean2day";
                Weather = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=Karaganda, KZ";
                WeatherTitlepwscode = "http://weathersticker.wunderground.com/weathersticker/sunandmoon_metric/language/english/global/stations/35394.gif";
                WeatherTitle = "Karaganda, KZ Weather Forecast";
                WeatherTitle2 = "Find more about Weather in Karaganda, KZ";
                LatitudeName = "Karaganda, Kazakhstan (TBD)";
                ImagePoint.ImageUrl = "~/Img/empty.JPG";
                GrStnStatus = "OFFLINE";
                SationIpAddress.Text = StationUrl = HttpContext.Current.Application["Stn6URL"].ToString();
            }
            if (Location == "7")
            {
                Latitude = "-31.967891";
                Longitude = "115.883274";
                WeatherMore = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:00000.1.94610&bannertypeclick=wu_clean2day";
                Weather = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=Perth, AU";
                WeatherTitlepwscode = "http://weathersticker.wunderground.com/weathersticker/sunandmoon_metric/language/english/global/stations/94610.gif";
                WeatherTitle = "Perth, AU Weather Forecast";
                WeatherTitle2 = "Find more about Weather in Perth, AU";
                LatitudeName = "Perth, Western Australia (TBD)";
                ImagePoint.ImageUrl = "~/Img/empty.JPG";
                GrStnStatus = "OFFLINE";
                SationIpAddress.Text = StationUrl = HttpContext.Current.Application["Stn7URL"].ToString();
            }
            if (Location == "8")
            {
                Latitude = "19.707243";
                Longitude = "-155.064983";
                WeatherMore = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:96720.1.99999&bannertypeclick=wu_clean2day";
                Weather = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=Hilo, HI";
                WeatherTitlepwscode = "http://weathersticker.wunderground.com/weathersticker/sunandmoon_metric/language/english/US/HI/Hilo.gif";
                WeatherTitle = "Hilo, HI Weather Forecast";
                WeatherTitle2 = "Find more about Weather in Hilo, HI";
                LatitudeName = "Hilo, Hawaii (TBD)";
                ImagePoint.ImageUrl = "~/Img/empty.JPG";
                GrStnStatus = "OFFLINE";
                SationIpAddress.Text = StationUrl = HttpContext.Current.Application["Stn8URL"].ToString();
            }
            if (Location == "9")
            {
                Latitude = "-21.255302";
                Longitude = "-159.768333";

                WeatherMore = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:00000.1.91844&bannertypeclick=wu_clean2day";
                Weather = "http://www.wunderground.com/cgi-bin/findweather/getForecast?query=Rarotonga Aws, KU";
                WeatherTitlepwscode = "http://weathersticker.wunderground.com/weathersticker/sunandmoon_metric/language/english/global/stations/91844.gif";
                WeatherTitle = "Rarotonga Aws, KU Weather Forecast";
                WeatherTitle2 = "Find more about Weather in Rarotonga Aws, KU";
                LatitudeName = "Rarotonga Aws, Cook Islands (TBD)";
                ImagePoint.ImageUrl = "~/Img/empty.JPG";
                GrStnStatus = "OFFLINE";
                SationIpAddress.Text = StationUrl = HttpContext.Current.Application["Stn9URL"].ToString();
            }
            GrStnStatus =  MyTestGrStn(Location);
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
                    HttpContext.Current.Application["DefaultMainGrStn"] = Location;
                    String StationN = HttpContext.Current.Application["DefaultMainGrStn"].ToString();
                    if (StationN == "1")
                        HttpContext.Current.Application["StnURL"] = HttpContext.Current.Application["Stn1URL"];
                    else if (StationN == "2")
                        HttpContext.Current.Application["StnURL"] = HttpContext.Current.Application["Stn2URL"];
                    else if (StationN == "3")
                        HttpContext.Current.Application["StnURL"] = HttpContext.Current.Application["Stn3URL"];
                    else if (StationN == "4")
                        HttpContext.Current.Application["StnURL"] = HttpContext.Current.Application["Stn4URL"];
                    else if (StationN == "5")
                        HttpContext.Current.Application["StnURL"] = HttpContext.Current.Application["Stn5URL"];
                    else if (StationN == "6")
                        HttpContext.Current.Application["StnURL"] = HttpContext.Current.Application["Stn6URL"];
                    else if (StationN == "7")
                        HttpContext.Current.Application["StnURL"] = HttpContext.Current.Application["Stn7URL"];
                    else if (StationN == "8")
                        HttpContext.Current.Application["StnURL"] = HttpContext.Current.Application["Stn8URL"];
                    else if (StationN == "9")
                        HttpContext.Current.Application["StnURL"] = HttpContext.Current.Application["Stn9URL"];
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            String Location = Page.Request.QueryString["st"];
            GrStnStatus = MyTestGrStn(Location);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
