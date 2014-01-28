using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.IO;


namespace SatCtrl
{
    public partial class _PostCalcResult : System.Web.UI.Page
    {
        public string MAX_session_no;
        public string MAX_packet_no;
        
        private List<DateTime> MyList;
        string szUsername = "Main";

        public class Common
        {
            public static  String GenXMLForOptimization(String szUsername)
            {
                String xml = null;
                String TextBoxTotalDays_Text;
                String SVal1 =  null;
                String SVal2 = null;
                String SVal3 = null;
                object IsList = HttpContext.Current.Application["ProbM" + szUsername];

                object IsStartDate = HttpContext.Current.Application["StartDate" + szUsername];

                object IsITotalDays = HttpContext.Current.Application["TotalDays" + szUsername];
                if (IsITotalDays != null)
                {
                    TextBoxTotalDays_Text = IsITotalDays.ToString();
                }
                else
                {
                    TextBoxTotalDays_Text = "1";
                }
                object IsIt = HttpContext.Current.Application["InitKeplerLine1" + szUsername];
                object IsIt2 = HttpContext.Current.Application["InitKeplerLine2" + szUsername];
                object IsIt3 = HttpContext.Current.Application["InitKeplerLine3" + szUsername];
                if ((IsIt != null) && (IsIt2 != null) && (IsIt3 != null))
                {
                    SVal1 = IsIt.ToString();
                    SVal2 = IsIt2.ToString();
                    SVal3 = IsIt3.ToString();
                }
                if ((IsList != null) && (IsStartDate != null) && (IsIt != null))
                {
                    _TraCalcCraft.CraftParam MyCraft;
                    MyCraft.ListEngineImpuse = new List<double>();
                    MyCraft.ListWeight = new List<double>();
                    MyCraft.ListFrameWeight = new List<double>();
                    MyCraft.ListEngineType = new List<double>();
                    MyCraft.ListFireTime = new List<double>();
                    MyCraft.ListThrottle = new List<double>();
                    MyCraft.ListDeltaT = new List<double>();
                    MyCraft.ListImpulseVal = new List<double>();
                    MyCraft.szUsername = szUsername;
                    MyCraft.TotalWeight = 0.0;
                    MyCraft.strProbM = "";
                    MyCraft.strProbMTarget = "";
                    MyCraft.strEngineXML = "";
                    MyCraft.TextBoxEngineXML1_Text = "";
                    MyCraft.TextBoxEngineXML2_Text = "";
                    MyCraft.TextBoxEngineXML3_Text = "";
                    MyCraft.TextBoxEngineXML4_Text = "";
                    MyCraft.TextBoxEngineXML5_Text = "";
                    MyCraft.iTotalIteration = 0;

                    MyCraft = _TraCalcCraft.Common.GetFromApplication(MyCraft, IsList);
                    xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\r\n" +
                          "<data version=\"1.00\" xmlns:TRA=\"http://www.adobri.com/tra\">\r\n" +
                          "<TRA:section name=\"TraInfo\">\r\n" +
                          "<!-- starting date (1 jan 2000) = 2451544.5JD \r\n" +
                          "    2001 == 2451910.5 2002 =  2452275.5 2003 == 2452640.5 2004 == 2453005.5\r\n" +
                          "    2005 == 2453371.5 2006 == 2453736.5 2007 == 2454101.5 2008 == 2454466.5\r\n" +
                          "    2009 == 2454832.5 2010 == 2455197.5 2011 == 2455562.5 2012 == 2455927.5\r\n" +
                          "    2013 == 2456293.5 2014 == 2456658.5 2015 == 2457023.5 \r\n" +
                          "    or set as in keplers epoch: 11291.79166666\r\n" +
                          "    or set as normal date and time DD/MM/YY HH:MM:SS:MLS\r\n" +
                          "    or negativge value set current date\r\n" +
                          "    or (not implemented et) set as normal date and time DD/MM/YY HH:MM:SS:MLS-->\r\n" +
                          "    <TRA name=\"dStartJD\" value=\"" + IsStartDate.ToString() + "\" />\r\n" +
                          "    <TRA name=\"TRAVisual\" value=\"" +
                          Global.Common.GetWebSiteURLAdress() + "/SatCtrl/PostCaclResult.aspx\" />\r\n" +
                          "    <TRA name=\"RGBImageW\" value=\"1280\" />\r\n" +
                          "    <TRA name=\"RGBImageH\" value=\"720\" />\r\n" +
                          "<!-- int iProfile = 0; \r\n" +
                          "              // 0 == XY , 1 == YZ, 2 == XZ 3 == -YZ 4 == -XZ 5==-XY\r\n" +
                          "              // 0 or XY is a view from North to south, 5 (- XY) is a view from south to north\r\n" +
                          "              // 1 or YZ is a view to easter -->\r\n" +
                          "    <TRA name=\"RGBView\" value=\"0\" />\r\n" +
                          "<!--  EARTH 2 MOON  9 -->\r\n" +
                          "    <TRA name=\"RGBReferenceBody\" value=\"2\" />\r\n" +
                          "<!-- max amount of pictures -->\r\n" +
                          "    <TRA name=\"RGBMaxPictures\" value=\"128\" />\r\n" +
                          "<!-- one picture per sec -->\r\n" +
                          "    <TRA name=\"RGBSecPerPictures\" value=\"8640\" />\r\n" +
                          "<!-- scale in m -->\r\n" +
                          "    <TRA name=\"RGBScale\" value=\"90000000\" />\r\n" +
                          "    <TRA name=\"IterPerSec\" value=\"32\" />\r\n" +
                          "    <TRA name=\"StartLandingIteraPerSec\" value=\"772050\" />\r\n" +
                          "    <TRA name=\"TotalDays\" value=\"" + TextBoxTotalDays_Text + "\" />\r\n" +
                          "    <TRA name=\"AU\" value=\"149597870697.39999\" />\r\n" +
                          "    <TRA name=\"ProbM\" value=\"67.5\" />\r\n" +
                          "    <TRA name=\"ProbKeplerLine1\" value=\"" + SVal1 + "\" />\r\n" +
                          "    <TRA name=\"ProbKeplerLine2\" value=\"" + SVal2 + "\" />\r\n" +
                          "    <TRA name=\"ProbKeplerLine3\" value=\"" + SVal3 + "\" />\r\n" +
                          "    <TRA name=\"GRSTNNAME\" value=\"vancouver\" />\r\n" +
                          "    <TRA name=\"GRSTNLat\" value=\"49.257735\" />\r\n" +
                          "    <TRA name=\"GRSTNLong\" value=\"-123.123904\" />\r\n" +
                          "    <TRA name=\"GRSTNNAME\" value=\"Sarasota\" />\r\n" +
                          "    <TRA name=\"GRSTNLat\" value=\"27.306121\" />\r\n" +
                          "    <TRA name=\"GRSTNLong\" value=\"-82.569112\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNNAME\" value=\"Joao Pessoa\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNLat\" value=\"-7.153526\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNLong\" value=\"-34.88966\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNNAME\" value=\"Cape Town\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNLat\" value=\"-33.916743\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNLong\" value=\"18.402658\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNNAME\" value=\"Donetsk\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNLat\" value=\"47.971091\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNLong\" value=\"37.712352\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNNAME\" value=\"Karaganda\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNLat\" value=\"49.789355\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNLong\" value=\"73.070583\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNNAME\" value=\"Perth\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNLat\" value=\"-31.967891\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNLong\" value=\"115.883274\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNNAME\" value=\"Hilo\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNLat\" value=\"19.707243\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNLong\" value=\"-155.064983\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNNAME\" value=\"Rarotonga\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNLat\" value=\"-21.255302\" />\r\n" +
                          "         <TRA:setting name=\"GRSTNLong\" value=\"-159.768333\" />\r\n" +
                          "<!-- target point on the Moon -->\r\n" +
                          "    <TRA:setting name=\"Targetlongitude\" value=\"-15\" />\r\n" +
                          "    <TRA:setting name=\"Targetlatitude\" value=\"-2\" />\r\n" +
                          "<!-- Mass and dimentions -->\r\n" +
                          "    <TRA:setting name=\"SunM\" value=\"1.9884158296478392e+030\" />\r\n" +
                          "    <TRA:setting name=\"EarthR\" value=\"6371000\" />\r\n" +
                          "    <TRA:setting name=\"EarthRP\" value=\"6356800\" />\r\n" +
                          "    <TRA:setting name=\"EarthRE\" value=\"6378100\" />\r\n" +
                          "    <TRA:setting name=\"EarthM\" value=\"5.9721863856475894e+024\" />\r\n" +
                          "    <TRA:setting name=\"MassRatioSunToEarthPlusMoon\" value=\"328900.56\" />\r\n" +
                          "    <TRA:setting name=\"EarthTSolSec\" value=\"86164.098903691003\" />\r\n" +
                          "    <TRA:setting name=\"EarthSmAx\" value=\"149597887500\" />\r\n" +
                          "    <TRA:setting name=\"EarthSmAxAU\" value=\"1.0000001124\" />\r\n" +
                          "    <TRA:setting name=\"EarthTDays\" value=\"365.25636600000001\" />\r\n" +
                          "    <TRA:setting name=\"MoonR\" value=\"1737100\" />\r\n" +
                          "    <TRA:setting name=\"MoonRP\" value=\"1735970\" />\r\n" +
                          "    <TRA:setting name=\"MoonRE\" value=\"1738140\" />\r\n" +
                          "    <TRA:setting name=\"MoonM\" value=\"7.3458114403351352e+022\" />\r\n" +
                          "<!--next line will force recalculation of SunM based on G constant -->\r\n" +
                          "    <TRA:setting name=\"GMSun\" value=\"1.327124400350198e+020\" />\r\n" +
                          "    <TRA:setting name=\"SunR\" value=\"696342000\" />\r\n" +
                          "    <TRA:setting name=\"GMEarthMoon\" value=\"403503241737999.87\" />\r\n" +
                          "    <TRA:setting name=\"GMEarth\" value=\"398600441499999.87\" />\r\n" +
                          "    <TRA:setting name=\"GMMoon\" value=\"4902800237999.998\" />\r\n" +
                          "<!-- next line will force calculation of a Earth and Moon mass -->\r\n" +
                          "<!-- it is not in use - value just for reference and fo trigger -->\r\n" +
                          "    <TRA:setting name=\"MassRatioEarthToMoon\" value=\"81.300567461545441\" />\r\n" +
                          "<!-- next line (value == \"1.0\") will force calculation\r\n" +
                          "                         of a Earth kepler position -->\r\n" +
                          "    <TRA:setting name=\"EarthCalcKepler\" value=\"0.0\" />\r\n" +
                          "<!-- next line will force assigning velocities and positions \r\n" +
                          "     based on JPL data -->\r\n" +
                          "    <TRA:setting name=\"UseJPLxyz\" value=\"1.0\" />\r\n" +
                          "<!-- optimization started from engine 0 -->\r\n" +
                          "    <TRA:setting name=\"StartOptim\" value=\"0\" />\r\n" +
                          "<!-- amount of engines to optimizel 0 - no optimization -->\r\n" +
                          "   <TRA:setting name=\"MaxOptim\" value=\"0\" />\r\n" +
                          "</TRA:section>\r\n" +
                          "            <!--     now all engines    -->\r\n" +
                          "<TRA:section name=\"Engine\" value=\"0.0\" >\r\n";
                    xml = xml + MyCraft.TextBoxEngineXML1_Text + MyCraft.TextBoxEngineXML2_Text + MyCraft.TextBoxEngineXML3_Text
                    + MyCraft.TextBoxEngineXML4_Text + MyCraft.TextBoxEngineXML5_Text;
                    xml = xml + "</TRA:section>\r\n" +
                    "</data>\r\n";
                }
                return xml;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string xml = null;
            //string ClientIp = Request.UserHostAddress;
            DateTime CutReq = DateTime.Now;
            if (Request.RequestType == "POST")
            {
                try
                {
                    using (StreamReader reader = new StreamReader(Request.InputStream))
                    {
                        xml = reader.ReadToEnd();
                        HttpContext.Current.Application["TraVisualXML"] = xml;
                    }
                }
                catch (Exception Exs) 
                {
                    xml = null;
                }
                Response.Clear();
                Response.ContentType = "text/html";
                Response.Write("OK");
            }
            else
            {
                if (Page.User.Identity.IsAuthenticated)
                {
                    szUsername = Page.User.Identity.Name.ToString();
                }
                xml = _PostCalcResult.Common.GenXMLForOptimization(szUsername);
                if (null != xml)
                {
                    Response.Write(xml);
                }
                else
                    Response.Write("bad bad");
                
                /*object IsList = HttpContext.Current.Application["ListOfGetsTraVisualXML"];
                int ListSize = 1;
                if (IsList == null)
                {
                    MyList = new List<DateTime>();
                    MyList.Add(CutReq);
                    HttpContext.Current.Application["ListOfGetsTraVisualXML"] = MyList;
                }
                else
                {
                    MyList = (List<DateTime>)IsList;
                    DateTime CutReqMinus100s = DateTime.Now.AddSeconds(-100);
                    int iCount = 0;
                    foreach (DateTime InList in MyList)
                    {
                        if (DateTime.Compare(InList, CutReqMinus100s) < 0) // requast was 100 long time ago
                            iCount ++;
                        else 
                            break;
                    }
                    if (iCount>0)
                        MyList.RemoveRange(0, iCount);
                    MyList.Add(CutReq);
                    ListSize = MyList.Count;
                }
                double WasTransfered = 40000 * ListSize;
                double CurTransferPerSec = WasTransfered / 100.0;
                double DelayNeeded = CurTransferPerSec / 40000; // 50K max
                int iDelayNeeded = Convert.ToInt32(DelayNeeded) + 1;
                String ReFreshSet = "<ReloadInSec>"+iDelayNeeded+"</ReloadInSec>";
                if (Page.User.Identity.IsAuthenticated)
                    ReFreshSet = "<ReloadInSec>1</ReloadInSec>";

                Response.Clear();
                Response.ContentType = "text/html";
                object IsIt = HttpContext.Current.Application["TraVisualXML"];
                if (IsIt == null)
                {
                    String MapPath = Server.MapPath("TraVisual.xml");
                    try
                    {
                        xml = File.ReadAllText(MapPath);
                    }
                    catch (Exception Exs)
                    {
                        xml = null;
                    }
                    if (xml != null)
                        HttpContext.Current.Application["TraVisualXML"] = xml;

                    IsIt = HttpContext.Current.Application["TraVisualXML"];
                }

                if (IsIt != null)
                {
                    xml = IsIt.ToString();
                    if (xml != null)
                    {
                        String dMinFromNow ;
                        object IsItIs = HttpContext.Current.Application["dMinFromNow"];
                        if (IsItIs != null)
                        {
                            dMinFromNow = IsItIs.ToString();
                            xml = xml.Replace("<dMinFromNow>50</dMinFromNow>","<dMinFromNow>" + dMinFromNow+ "</dMinFromNow>");
                        }
                        xml = xml.Replace("<ReloadInSec>00001</ReloadInSec>", ReFreshSet);
                        Response.Write(xml);
                    }
                    else
                    {
                        Response.Write("bad bad");
                    }
                }
                else
                    Response.Write("bad");
                 */
            }
            
        }
    }
}
