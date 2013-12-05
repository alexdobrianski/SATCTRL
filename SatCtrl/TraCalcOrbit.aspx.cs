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
    public partial class _TraCalcOrbit : System.Web.UI.Page
    {
        public long intMaxSessionN;
        double dX;
        double dY;
        string TraStatus;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string strMaxSessionN = HttpContext.Current.Application["strMaxSessionN"].ToString();
            intMaxSessionN = Convert.ToInt32(strMaxSessionN);
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
            }
            
            HttpContext.Current.Application["strPageUsed"] = "TraCalc";
            // number of distributed nodes
            //LabelNodes.Text = "0";
            // status of distributed calculations
            TraStatus = "non avalable";
            //LabelStatus.Text = TraStatus;
            //HttpContext.Current.Application["TraDistStatus"] = TraStatus;
            if (TraStatus == "non avalable")
            {
            //    ButtonImpOptimization.Enabled = false;
            //    ButtonFindImp.Enabled = false;
            }
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

    }
}
