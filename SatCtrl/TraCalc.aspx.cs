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
using System.Net.Cache;
using System.Net.Cache;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace SatCtrl
{
    public partial class _TraCalc : System.Web.UI.Page
    {
        public long intMaxSessionN;
        double dX;
        double dY;
        string TraStatus;
        string szUsername = "Main";
        protected void Page_Load(object sender, EventArgs e)
        {
            String strEW;
            String strLongitude;
            String strLatitude;
            String strNS;
            if (Page.User.Identity.IsAuthenticated)
            {
                szUsername = Page.User.Identity.Name.ToString();
            }
            LabelUserName.Text = szUsername;
            object IsList = HttpContext.Current.Application["TargetLongitude" + szUsername];
            if (IsList == null)
            {
                String xml = null;
                String NameFile = "InitTraget" + szUsername + ".xml";
                String MapPath = Server.MapPath(NameFile);
                int iDirAccound = MapPath.IndexOf("\\SatCtrl\\");
                if (iDirAccound > 0) // it is dir "account"
                {
                    MapPath = MapPath.Substring(0, iDirAccound);
                    MapPath += "\\SatCtrl\\\\SatCtrl\\\\" + NameFile;
                }

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
                    strLongitude = SatCtrl.Global.Common.GetValue(xml, "Targetlongitude", 0);
                    strEW = strLongitude.Substring(0, 1);
                    if (strEW == "-") // east
                        strLongitude = "E" + strLongitude.Substring(1);
                    else
                        strLongitude = "W" + strLongitude;
                    HttpContext.Current.Application["Targetlongitude" + szUsername] = strLongitude;

                    strLatitude = SatCtrl.Global.Common.GetValue(xml, "Targetlatitude", 0);
                    strNS = strLatitude.Substring(0, 1);
                    if (strNS == "-") // south
                        strLatitude = "S" + strLatitude.Substring(1);
                    else
                        strLatitude = "N" + strLatitude;
                    HttpContext.Current.Application["Targetlatitude" + szUsername] = strLatitude;

                }
                else // file with initial data do not exsists
                {
                    TextBoxLongitude.Text = "E15";
                    HttpContext.Current.Application["TargetLongitude" + szUsername] = TextBoxLongitude.Text.ToString();
                    TextBoxLatitude.Text = "S2";
                    HttpContext.Current.Application["TargetLatitude"] = TextBoxLatitude.Text.ToString();
                }
                IsList = HttpContext.Current.Application["TargetLongitude" + szUsername];
            }

            if (IsList != null)
            {
                strLongitude = IsList.ToString();// HttpContext.Current.Application["TargetLongitude" + szUsername].ToString();

                strEW = strLongitude.Substring(0, 1);
                if (strEW == "W")
                    dX = -Convert.ToDouble(strLongitude.Substring(1));
                else
                    dX = Convert.ToDouble(strLongitude.Substring(1));
                TextBoxLongitude.Text = strLongitude;
            }


            IsList = HttpContext.Current.Application["TargetLatitude" + szUsername];
            if (IsList != null)
            {
                strLatitude = IsList.ToString();// HttpContext.Current.Application["TargetLatitude" + szUsername].ToString();
                strNS = strLatitude.Substring(0, 1);
                if (strNS == "N")
                    dX = -Convert.ToDouble(strLatitude.Substring(1));
                else
                    dX = Convert.ToDouble(strLatitude.Substring(1));
                TextBoxLatitude.Text = strLatitude;
            }
            else /// just in case == nothong more
            {
                TextBoxLatitude.Text = "S2";
                HttpContext.Current.Application["TargetLatitude" + szUsername] = TextBoxLatitude.Text.ToString();
            }

            HttpContext.Current.Application["strPageUsed" + szUsername] = "TraCalc";
            // number of distributed nodes
            LabelNodes.Text = "0";
            // status of distributed calculations
            TraStatus = "non avalable";
            LabelStatus.Text = TraStatus;
            HttpContext.Current.Application["TraDistStatus" + szUsername] = TraStatus;
            if (TraStatus == "non avalable")
            {
                ButtonImpOptimization.Enabled = false;
                ButtonFindImp.Enabled = false;
            }
        }
       

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            double X = e.X;
            double Y = e.Y;
            double Height = ImageLunarMap.Height.Value;
            double Width = ImageLunarMap.Width.Value;
            X /= Width; X -= 0.5; X *=360;
            Y /= Height; Y -= 0.5; Y *=180;
            if (X < 0.0)
            {
                X = -X;
                TextBoxLongitude.Text = "W" + X.ToString("F5");
            }
            else
                TextBoxLongitude.Text = "E" + X.ToString("F5");
            if (Y < 0.0)
            {
                Y = -Y;
                TextBoxLatitude.Text = "N" + Y.ToString("F5");
            }
            else
                TextBoxLatitude.Text = "S" + Y.ToString("F5");
            HttpContext.Current.Application["TargetLatitude" + szUsername] = TextBoxLatitude.Text.ToString();
            HttpContext.Current.Application["TargetLongitude" + szUsername] = TextBoxLongitude.Text.ToString();

        }
    }
}
