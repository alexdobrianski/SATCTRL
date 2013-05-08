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
    public partial class _PostTra : System.Web.UI.Page
    {
        public string MAX_session_no;
        public string MAX_packet_no;
        
        private List<DateTime> MyList;
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
                object IsList = HttpContext.Current.Application["ListOfGetsTraVisualXML"];
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
            }
            
        }
    }
}
