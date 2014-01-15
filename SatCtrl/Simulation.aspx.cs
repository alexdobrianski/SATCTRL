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
    public partial class _Simulation : System.Web.UI.Page
    {
        public string RecomendedClicks;
        public string ParamURLApplet = "http://24.84.33.246/SatCtrl";
        protected void Page_Load(object sender, EventArgs e)
        {
            string SVal1 = null;
            string SVal2 = null;
            string SVal3 = null;
            string UrlMy = Request.Url.ToString();
            int PtrLast = UrlMy.IndexOf("Simulation.aspx");
            if (PtrLast > 0)
            {
                UrlMy = UrlMy.Substring(0, PtrLast-1);
                ParamURLApplet = UrlMy;
            }
                
            //RecomendedClicks = "<a href=\"http://java3d.java.net/binary-builds.html\">Requare Java 3D - click to install</a>";
            //System.Web.HttpBrowserCapabilities browser = Request.Browser;
            //RecomendedClicks += "   browser:" + browser.Platform + " support for JavaApplet:" + browser.JavaApplets + " Screen size w:" + browser.ScreenPixelsWidth +
            //    " h:" + browser.ScreenPixelsHeight +"<br/>";
            object IsIt = HttpContext.Current.Application["ProbKeplerLine1"];
            if (IsIt == null)
            {
                String xml = null;
                String MapPath = Server.MapPath("Tra.xml");
                try
                {
                    xml = File.ReadAllText(MapPath);
                }
                catch (Exception Exs)
                {
                    xml = null;
                }
                if (xml != null)
                {
                    SVal1 = SatCtrl.Global.Common.GetValue(xml, "ProbKeplerLine1", 0);
                    HttpContext.Current.Application["ProbKeplerLine1"] = SVal1;
                    SVal2 = SatCtrl.Global.Common.GetValue(xml, "ProbKeplerLine2", 0);
                    HttpContext.Current.Application["ProbKeplerLine2"] = SVal2;
                    SVal3 = SatCtrl.Global.Common.GetValue(xml, "ProbKeplerLine3", 0);
                    HttpContext.Current.Application["ProbKeplerLine3"] = SVal3;
                }
                int iInteration = 1;
                do
                {
                    SVal1 = SatCtrl.Global.Common.GetValue(xml, "ProbKeplerLine1", iInteration);
                    SVal2 = SatCtrl.Global.Common.GetValue(xml, "ProbKeplerLine2", iInteration);
                    SVal3 = SatCtrl.Global.Common.GetValue(xml, "ProbKeplerLine3", iInteration);
                    if ((SVal1 != null) && (SVal2 != null) && (SVal3 != null))
                    {
                        HttpContext.Current.Application["GPS" + iInteration + "ProbKeplerLine1"] = SVal1;
                        HttpContext.Current.Application["GPS" + iInteration + "ProbKeplerLine2"] = SVal2;
                        HttpContext.Current.Application["GPS" + iInteration + "ProbKeplerLine3"] = SVal3;
                    }
                    else
                        break;
                    iInteration += 1;
                }
                while ((SVal1 != null) && (SVal2 != null) && (SVal3 != null));
            }
            else
            {
                SVal1 = IsIt.ToString();
                SVal2 = HttpContext.Current.Application["ProbKeplerLine2"].ToString();
                SVal3 = HttpContext.Current.Application["ProbKeplerLine3"].ToString();
            }
            if (SVal1 != null)
            {
                ProbKeplerLine1.Text = SVal1.ToString();
                ProbKeplerLine2.Text = SVal2.ToString();
                ProbKeplerLine3.Text = SVal3.ToString();
            }
            if (CheckBoxRealTime.Checked == true)
            {
                TextBoxdMinFromNow.Enabled = true;
                //Calendar1.Enabled = false;
                TextBoxTime.Enabled = false;
                String MinFromNow = TextBoxdMinFromNow.Text.ToString();
                long dMinFromNow = Convert.ToInt32(MinFromNow);
                if (dMinFromNow < 3)
                    dMinFromNow = 3;
                if (dMinFromNow > 55)
                    dMinFromNow = 55;
                HttpContext.Current.Application["dMinFromNow"] = Convert.ToString(dMinFromNow);
                HttpContext.Current.Application["dStartJD"] = "dStartJD\" value=\"-60";
            }
            else
            {
                TextBoxdMinFromNow.Enabled = false;
                //Calendar1.Enabled = true;
                TextBoxTime.Enabled = true;
                String MinFromNow = TextBoxdMinFromNow.Text.ToString();
                long dMinFromNow = Convert.ToInt32(MinFromNow);
                if (dMinFromNow < 3)
                    dMinFromNow = 3;
                if (dMinFromNow > 55)
                    dMinFromNow = 55;
                HttpContext.Current.Application["dMinFromNow"] = Convert.ToString(dMinFromNow);
                HttpContext.Current.Application["dStartJD"] = "dStartJD\" value=\"-60";
            }

        }
    }
}
