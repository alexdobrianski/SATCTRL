using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace SatCtrl
{
    public partial class _SimulationGPS : System.Web.UI.Page
    {
        protected String GetValue(String xml, String SearchStr, int iInstance)
        {
            String MySearch = "\"" + SearchStr + "\"";
            int FirstLine = xml.IndexOf(MySearch);
            int iCount = 0;

            if (FirstLine > 0)
            {
                do
                {
                    if (iCount == iInstance)
                    {
                        int FirstValue = xml.IndexOf("value=", FirstLine) + 7;
                        int LastValue = xml.IndexOf('\"', FirstValue) - 1;
                        return xml.Substring(FirstValue, LastValue - FirstValue + 1);
                    }
                    FirstLine = xml.IndexOf(MySearch, FirstLine + 1);
                    iCount += 1;
                }
                while (FirstLine > 0);
            }
            return null;
        }

        public string ParamURLApplet = "http://24.84.33.246/SatCtrl";
        protected void Page_Load(object sender, EventArgs e)
        {
            string UrlMy = Request.Url.ToString();
            int PtrLast = UrlMy.IndexOf("SimulationGPS.aspx");
            if (PtrLast > 0)
            {
                UrlMy = UrlMy.Substring(0, PtrLast - 1);
                ParamURLApplet = UrlMy;
            }
            string SVal1 = null;
            string SVal2 = null;
            string SVal3 = null;
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
                    SVal1 = GetValue(xml, "ProbKeplerLine1", 0);
                    HttpContext.Current.Application["ProbKeplerLine1"] = SVal1;
                    SVal2 = GetValue(xml, "ProbKeplerLine2", 0);
                    HttpContext.Current.Application["ProbKeplerLine2"] = SVal2;
                    SVal3 = GetValue(xml, "ProbKeplerLine3", 0);
                    HttpContext.Current.Application["ProbKeplerLine3"] = SVal3;
                }
                int iInteration = 1;
                do
                {
                    SVal1 = GetValue(xml, "ProbKeplerLine1", iInteration);
                    SVal2 = GetValue(xml, "ProbKeplerLine2", iInteration);
                    SVal3 = GetValue(xml, "ProbKeplerLine3", iInteration);
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
            for (int iCount = 1; iCount < 6; iCount++)
            {
                IsIt = HttpContext.Current.Application["GPS" + iCount + "ProbKeplerLine1"];
                object IsIt2 = HttpContext.Current.Application["GPS" + iCount + "ProbKeplerLine2"];
                object IsIt3 = HttpContext.Current.Application["GPS" + iCount + "ProbKeplerLine3"];
                if ((IsIt != null) && (IsIt2 != null) && (IsIt3 != null))
                {
                    SVal1 = IsIt.ToString();
                    SVal2 = IsIt2.ToString();
                    SVal3 = IsIt3.ToString();
                    switch (iCount)
                    {
                        case 1: GPS1Line1.Text = SVal1.ToString(); GPS1Line2.Text = SVal2.ToString(); GPS1Line3.Text = SVal3.ToString(); break;
                        case 2: GPS2Line1.Text = SVal1.ToString(); GPS2Line2.Text = SVal2.ToString(); GPS2Line3.Text = SVal3.ToString(); break;
                        case 3: GPS3Line1.Text = SVal1.ToString(); GPS3Line2.Text = SVal2.ToString(); GPS3Line3.Text = SVal3.ToString(); break;
                        case 4: GPS4Line1.Text = SVal1.ToString(); GPS4Line2.Text = SVal2.ToString(); GPS4Line3.Text = SVal3.ToString(); break;
                        case 5: GPS5Line1.Text = SVal1.ToString(); GPS5Line2.Text = SVal2.ToString(); GPS5Line3.Text = SVal3.ToString(); break;
                        case 6: GPS6Line1.Text = SVal1.ToString(); GPS6Line2.Text = SVal2.ToString(); GPS6Line3.Text = SVal3.ToString(); break;
                    }
                }
                else
                    break;
            }
        }

        protected void GPS1Line1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void GPS1Line2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void GPS1Line3_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
