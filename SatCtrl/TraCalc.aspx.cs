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
            string strMaxSessionN = HttpContext.Current.Application["strMaxSessionN"].ToString();
            intMaxSessionN = Convert.ToInt32(strMaxSessionN);
            RadioButtonTargetLL.Checked = true;
            object IsList = HttpContext.Current.Application["TargetLongitude"];
            if (IsList != null)
            {
                string strLongitude = HttpContext.Current.Application["TargetLongitude"].ToString();

                string strEW = strLongitude.Substring(0, 1);
                if (strEW == "W")
                    dX = -Convert.ToDouble(strLongitude.Substring(1));
                else
                    dX = Convert.ToDouble(strLongitude.Substring(1));
            }
            else
            {
                TextBoxLongitude.Text = "E15";
                HttpContext.Current.Application["TargetLongitude"] = TextBoxLongitude.Text.ToString();
            }
            IsList = HttpContext.Current.Application["TargetLatitude"];
            if (IsList != null)
            {
                string strLatitude = HttpContext.Current.Application["TargetLatitude"].ToString();
                string strNS = strLatitude.Substring(0, 1);
                if (strNS == "N")
                    dX = -Convert.ToDouble(strLatitude.Substring(1));
                else
                    dX = Convert.ToDouble(strLatitude.Substring(1));
            }
            else
            {
                TextBoxLatitude.Text = "S2";
                HttpContext.Current.Application["TargetLatitude"] = TextBoxLatitude.Text.ToString();
            }
            
            HttpContext.Current.Application["strPageUsed"] = "TraCalc";
            // number of distributed nodes
            LabelNodes.Text = "0";
            // status of distributed calculations
            TraStatus = "non avalable";
            LabelStatus.Text = TraStatus;
            HttpContext.Current.Application["TraDistStatus"] = TraStatus;
            if (TraStatus == "non avalable")
            {
                ButtonImpOptimization.Enabled = false;
                ButtonFindImp.Enabled = false;
            }
            if (Page.User.Identity.IsAuthenticated)
            {
                szUsername = Page.User.Identity.Name.ToString();
            }
            LabelUserName.Text = szUsername;
        }
        protected String AddHexString(String Str2)
        {
            String Str1 = "";
            for (int i = 0; i < Str2.Length; i++)
            {
                Char CharS = Str2.ElementAt(i);
                if ((CharS >= '0') && ((CharS <= '9')) || (CharS >= 'a') && ((CharS <= 'z')) || (CharS >= 'A') && ((CharS <= 'Z')))
                {
                    Str1 += CharS;
                }
                else
                {
                    Str1 = Str1 + "%" + Convert.ToByte(CharS).ToString("x2");
                }
            }
            return Str1;
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
            HttpContext.Current.Application["TargetLatitude"] = TextBoxLatitude.Text.ToString();
            HttpContext.Current.Application["TargetLongitude"] = TextBoxLongitude.Text.ToString();

        }
    }
}
