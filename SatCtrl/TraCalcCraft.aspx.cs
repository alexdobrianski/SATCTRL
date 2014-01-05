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

namespace SatCtrl
{
    public partial class _TraCalcCraft : System.Web.UI.Page
    {
        public long intMaxSessionN;
        string szUsername = "Main";
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
        protected void Page_Load(object sender, EventArgs e)
        {
            String strProbM;
            String strProbMTarget;
            string strMaxSessionN = HttpContext.Current.Application["strMaxSessionN"].ToString();
            intMaxSessionN = Convert.ToInt32(strMaxSessionN);
            if (Page.User.Identity.IsAuthenticated)
            {
                szUsername = Page.User.Identity.Name.ToString();
            }
            LabelUserName.Text = szUsername;
            object IsList = HttpContext.Current.Application["ProbM" + szUsername];
            if (IsList == null)
            {
                String xml = null;
                String NameFile = "InitCraft" + szUsername + ".xml";
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
                    strProbM = GetValue(xml, "ProbM", 0);
                    HttpContext.Current.Application["ProbM" + szUsername] = strProbM;

                    strProbMTarget = GetValue(xml, "ProbMTarget", 0);
                    HttpContext.Current.Application["ProbMTarget" + szUsername] = strProbMTarget;

                }
                else // file with initial data do not exsists
                {
                    strProbM = "255";
                    HttpContext.Current.Application["ProbM" + szUsername] = strProbM;
                    strProbMTarget = "4";
                    HttpContext.Current.Application["ProbMTarget"] = strProbMTarget;
                }
                IsList = HttpContext.Current.Application["ProbM" + szUsername];
            }

            if (IsList != null)
            {
                strProbM = IsList.ToString();
                TextBoxProbM.Text = strProbM;
            }


            IsList = HttpContext.Current.Application["ProbMTarget" + szUsername];
            if (IsList != null)
            {
                strProbMTarget = IsList.ToString();
                TextBoxProbMTarget.Text = strProbMTarget;
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
