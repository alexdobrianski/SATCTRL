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
        List<double> ListEngineImpuse = new List<double>();
        List<double> ListWeight = new List<double>();
        List<double> ListFrameWeight = new List<double>();
        List<double> ListEngineType = new List<double>();
        List<double> ListThrottle = new List<double>();
        List<double> ListDeltaT = new List<double>();
        List<double> ListImpulseVal = new List<double>();
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
        void UpdateVisibility()
        {

            switch (ListEngineImpuse.Count)
            {
                case 1:
                    CheckBoxImp5.Visible = false;
                    ButtonDeleteImp5.Visible = false;
                    ButtonAddImp5.Visible = false;
                    ButtonUpdateImp5.Visible = false;
                    TextBoxEngineXML5.Visible = false;
                    LabelImpl5.Visible = false;

                    CheckBoxImp4.Visible = false;
                    ButtonDeleteImp4.Visible = false;
                    ButtonAddImp4.Visible = false;
                    ButtonUpdateImp4.Visible = false;
                    TextBoxEngineXML4.Visible = false;
                    LabelImpl4.Visible = false;

                    CheckBoxImp3.Visible = false;
                    ButtonDeleteImp3.Visible = false;
                    ButtonAddImp3.Visible = false;
                    ButtonUpdateImp3.Visible = false;
                    TextBoxEngineXML3.Visible = false;
                    LabelImpl3.Visible = false;

                    CheckBoxImp2.Visible = false;
                    ButtonDeleteImp2.Visible = false;
                    ButtonAddImp2.Visible = true;
                    ButtonUpdateImp2.Visible = false;
                    TextBoxEngineXML2.Visible = false;
                    LabelImpl2.Visible = false;
                    ButtonDeleteImp1.Visible = !CheckBoxImp1.Checked;
                    HyperLinkImpl5.Visible = false;
                    HyperLinkImpl4.Visible = false;
                    HyperLinkImpl3.Visible = false;
                    HyperLinkImpl2.Visible = false;
                    break;
                case 2:
                    CheckBoxImp5.Visible = false;
                    ButtonDeleteImp5.Visible = false;
                    ButtonAddImp5.Visible = false;
                    ButtonUpdateImp5.Visible = false;
                    TextBoxEngineXML5.Visible = false;
                    LabelImpl5.Visible = false;

                    CheckBoxImp4.Visible = false;
                    ButtonDeleteImp4.Visible = false;
                    ButtonAddImp4.Visible = false;
                    ButtonUpdateImp4.Visible = false;
                    TextBoxEngineXML4.Visible = false;
                    LabelImpl4.Visible = false;

                    CheckBoxImp3.Visible = false;
                    ButtonDeleteImp3.Visible = false;
                    ButtonAddImp3.Visible = true;
                    ButtonUpdateImp3.Visible = false;
                    TextBoxEngineXML3.Visible = false;
                    LabelImpl3.Visible = false;

                    CheckBoxImp2.Visible = true;
                    ButtonDeleteImp2.Visible = true;
                    ButtonAddImp2.Visible = false;
                    ButtonUpdateImp2.Visible = true;
                    TextBoxEngineXML2.Visible = true;
                    LabelImpl2.Visible = true;
                    ButtonDeleteImp1.Visible = !CheckBoxImp1.Checked;
                    ButtonDeleteImp2.Visible = !CheckBoxImp2.Checked;

                    HyperLinkImpl5.Visible = false;
                    HyperLinkImpl4.Visible = false;
                    HyperLinkImpl3.Visible = false;
                    HyperLinkImpl2.Visible = true;

                    break;
                case 3:
                    CheckBoxImp5.Visible = false;
                    ButtonDeleteImp5.Visible = false;
                    ButtonAddImp5.Visible = false;
                    ButtonUpdateImp5.Visible = false;
                    TextBoxEngineXML5.Visible = false;
                    LabelImpl5.Visible = false;

                    CheckBoxImp4.Visible = false;
                    ButtonDeleteImp4.Visible = false;
                    ButtonAddImp4.Visible = true;
                    ButtonUpdateImp4.Visible = false;
                    TextBoxEngineXML4.Visible = false;
                    LabelImpl4.Visible = false;

                    CheckBoxImp3.Visible = true;
                    ButtonDeleteImp3.Visible = true;
                    ButtonAddImp3.Visible = false;
                    ButtonUpdateImp3.Visible = true;
                    TextBoxEngineXML3.Visible = true;
                    LabelImpl3.Visible = true;

                    CheckBoxImp2.Visible = true;
                    ButtonDeleteImp2.Visible = true;
                    ButtonAddImp2.Visible = false;
                    ButtonUpdateImp2.Visible = true;
                    TextBoxEngineXML2.Visible = true;
                    LabelImpl2.Visible = true;
                    ButtonDeleteImp1.Visible = !CheckBoxImp1.Checked;
                    ButtonDeleteImp2.Visible = !CheckBoxImp2.Checked;
                    ButtonDeleteImp3.Visible = !CheckBoxImp3.Checked;

                    HyperLinkImpl5.Visible = false;
                    HyperLinkImpl4.Visible = false;
                    HyperLinkImpl3.Visible = true;
                    HyperLinkImpl2.Visible = true;

                    break;
                case 4:
                    CheckBoxImp5.Visible = false;
                    ButtonDeleteImp5.Visible = false;
                    ButtonAddImp5.Visible = true;
                    ButtonUpdateImp5.Visible = false;
                    TextBoxEngineXML5.Visible = false;
                    LabelImpl5.Visible = false;

                    CheckBoxImp4.Visible = true;
                    ButtonDeleteImp4.Visible = true;
                    ButtonAddImp4.Visible = false;
                    ButtonUpdateImp4.Visible = true;
                    TextBoxEngineXML4.Visible = true;
                    LabelImpl4.Visible = true;

                    CheckBoxImp3.Visible = true;
                    ButtonDeleteImp3.Visible = true;
                    ButtonAddImp3.Visible = false;
                    ButtonUpdateImp3.Visible = true;
                    TextBoxEngineXML3.Visible = true;
                    LabelImpl3.Visible = true;

                    CheckBoxImp2.Visible = true;
                    ButtonDeleteImp2.Visible = true;
                    ButtonAddImp2.Visible = false;
                    ButtonUpdateImp2.Visible = true;
                    TextBoxEngineXML2.Visible = true;
                    LabelImpl2.Visible = true;
                    ButtonDeleteImp1.Visible = !CheckBoxImp1.Checked;
                    ButtonDeleteImp2.Visible = !CheckBoxImp2.Checked;
                    ButtonDeleteImp3.Visible = !CheckBoxImp3.Checked;
                    ButtonDeleteImp4.Visible = !CheckBoxImp4.Checked;

                    HyperLinkImpl5.Visible = false;
                    HyperLinkImpl4.Visible = true;
                    HyperLinkImpl3.Visible = true;
                    HyperLinkImpl2.Visible = true;


                    break;
                case 5:
                    CheckBoxImp5.Visible = true;
                    ButtonDeleteImp5.Visible = true;
                    ButtonAddImp5.Visible = true;
                    ButtonUpdateImp5.Visible = true;
                    TextBoxEngineXML5.Visible = true;
                    LabelImpl5.Visible = true;

                    CheckBoxImp4.Visible = true;
                    ButtonDeleteImp4.Visible = true;
                    ButtonAddImp4.Visible = true;
                    ButtonUpdateImp4.Visible = true;
                    TextBoxEngineXML4.Visible = true;
                    LabelImpl4.Visible = true;

                    CheckBoxImp3.Visible = true;
                    ButtonDeleteImp3.Visible = true;
                    ButtonAddImp3.Visible = true;
                    ButtonUpdateImp3.Visible = true;
                    TextBoxEngineXML3.Visible = true;
                    LabelImpl3.Visible = true;

                    CheckBoxImp2.Visible = true;
                    ButtonDeleteImp2.Visible = true;
                    ButtonAddImp2.Visible = true;
                    ButtonUpdateImp2.Visible = true;
                    TextBoxEngineXML2.Visible = true;
                    LabelImpl2.Visible = true;
                    ButtonDeleteImp1.Visible = !CheckBoxImp1.Checked;
                    ButtonDeleteImp2.Visible = !CheckBoxImp2.Checked;
                    ButtonDeleteImp3.Visible = !CheckBoxImp3.Checked;
                    ButtonDeleteImp4.Visible = !CheckBoxImp4.Checked;
                    ButtonDeleteImp5.Visible = !CheckBoxImp5.Checked;

                    HyperLinkImpl5.Visible = true;
                    HyperLinkImpl4.Visible = true;
                    HyperLinkImpl3.Visible = true;
                    HyperLinkImpl2.Visible = true;

                    break;

            }

        }
        String MakeEngineXMLFile(List<double> ListVal, double dEngineImpuse, double dWeight,
            double dFrameWeight, double dEngineType, double dThrottle, double dDeltaT)
        {
            // <TRA:setting name="EngineImpuse" value="1.0" />
            //   <TRA:setting name="Weight" value="17.157" />
            //   <TRA:setting name="FrameWeight" value="7.0" />
            //   <!-- set engine type
            //     -1 = fixed 
            //      N = variable point on a plot describing impules value
            //     -->
            //   <TRA:setting name="EngineType" value="-1.0" />
            //   <!-- set engine Throttle  value (0.0 - 1.0) -->    
            //   <TRA:setting name="Throttle" value="1.0" />
            //   <!-- iteration per sec from engine's plot -->
            //    <TRA:setting name="DeltaT" value="5.0" />

            String strXML = "<TRA:setting name=\"EngineImpuse +\" value=\"" + dEngineImpuse + "\" />\r\n" +
            "   <TRA:setting name=\"Weight\" value=\"" + dWeight + "\" />\r\n" +
            "   <TRA:setting name=\"FrameWeight\" value=\"" + dFrameWeight + "\" />\r\n" +
            "   <!-- set engine type\r\n" +
            "     -1 = fixed \r\n" +
            "      N = variable point on a plot describing impules value\r\n" +
            "     -->\r\n" +
            "   <TRA:setting name=\"EngineType\" value=\"" + dEngineType + "\" />\r\n" +
            "   <!-- set engine Throttle  value (0.0 - 1.0) -->\r\n" +
            "   <TRA:setting name=\"Throttle\" value=\"" + dThrottle + "\" />\r\n" +
            "   <!-- iteration per sec from engine's plot -->\r\n" +
            "    <TRA:setting name=\"DeltaT\" value=\"" + dDeltaT + "\" />\r\n";
            foreach (double dd in ListVal)
            {
                strXML += "    <CT:setting name=\"ImplVal\" value=\"" + dd + "\" />\r\n";
            }
            return strXML;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            String strProbM;
            String strProbMTarget;
            String strImpNumber;
            double dEngineImpuse;
            double dWeight;
            double FrameWeight;
            double EngineType;
            double dThrottle;
            double dDeltaT;
            String strEngineImpuse;
            String strFrameWeight;
            String strWeight;
            String strEngineType;
            String strThrottle;
            String strDeltaT;
            String strEngineXML;
            double TotalWeight = 0.0;
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
                    TotalWeight = Convert.ToDouble(strProbMTarget);
                    // now need to read all engines profiles
                    String strImplVal = null;
                    double dImplVal = 0;
                    
                    int iIteration = 0;
                    int iTotalIteration = 0;
                    int iEngineImpuse = 0;
                    do
                    {
                        strImplVal = GetValue(xml, "ImplVal", iTotalIteration);
                        iTotalIteration += 1;
                        iIteration += 1;
                        if (strImplVal != null)
                        {
                            dImplVal = Convert.ToDouble(strImplVal);
                            if (dImplVal == 0.0 && iIteration > 1) // next engine's impulse
                            {
                                
                                ListImpulseVal.Add(dImplVal);
                                strImpNumber = "ImplVal" + Convert.ToString(iEngineImpuse) + szUsername;
                                HttpContext.Current.Application[strImpNumber] = ListImpulseVal;
                                
                                
                                //<TRA:setting name="EngineImpuse" value="1.0" />
                                strEngineImpuse = GetValue(xml, "EngineImpuse", iEngineImpuse);
                                dEngineImpuse = Convert.ToDouble(strEngineImpuse);
                                ListEngineImpuse.Add(dEngineImpuse);
                                // <TRA:setting name="Weight" value="17.157" />
                                strWeight = GetValue(xml, "Weight", iEngineImpuse);
                                dWeight = Convert.ToDouble(strWeight);
                                TotalWeight += dWeight;
                                ListWeight.Add(dWeight);
                                // <TRA:setting name="FrameWeight" value="7.0" />
                                strFrameWeight = GetValue(xml, "FrameWeight", iEngineImpuse);
                                FrameWeight = Convert.ToDouble(strFrameWeight);
                                ListFrameWeight.Add(FrameWeight);
                                // <!-- set engine type
                                //      -1 = fixed 
                                //      N = variable point on a plot describing impules value
                                //          -->
                                //<TRA:setting name="EngineType" value="-1.0" />
                                strEngineType = GetValue(xml, "EngineType", iEngineImpuse);
                                EngineType = Convert.ToDouble(strEngineType);
                                ListEngineType.Add(EngineType);
                                //<!-- set engine Throttle  value (0.0 - 1.0) -->    
                                //<TRA:setting name="Throttle" value="1.0" />
                                strThrottle = GetValue(xml, "Throttle", iEngineImpuse);
                                dThrottle = Convert.ToDouble(strThrottle);

                                ListThrottle.Add(dThrottle);
                                //<!-- iteration per sec from engine's plot -->
                                //<TRA:setting name="DeltaT" value="5.0" />
                                strDeltaT = GetValue(xml, "DeltaT", iEngineImpuse);
                                dDeltaT = Convert.ToDouble(strDeltaT);
                                ListDeltaT.Add(dDeltaT);
                                switch (iEngineImpuse)
                                {
                                    case 0:
                                        CheckBoxImp1.Checked = true;
                                        break;
                                    case 1:
                                        CheckBoxImp2.Checked = true;
                                        break;
                                    case 2:
                                        CheckBoxImp3.Checked = true;
                                        break;
                                    case 3:
                                        CheckBoxImp4.Checked = true;
                                        break;
                                    case 4:
                                        CheckBoxImp5.Checked = true;
                                        break;
                                }

                                iEngineImpuse += 1;
                                ListImpulseVal = new List<double>(); 
                                iIteration = 0;
                            }
                            else
                            {
                                ListImpulseVal.Add(dImplVal);
                            }

                        }

                    }
                    while (strImplVal != null);

                    HttpContext.Current.Application["EngineImpuse" + szUsername] = ListEngineImpuse;
                    HttpContext.Current.Application["Weight" + szUsername] = ListWeight;
                    HttpContext.Current.Application["FrameWeight" + szUsername] = ListFrameWeight;
                    HttpContext.Current.Application["EngineType" + szUsername] = ListEngineType;
                    HttpContext.Current.Application["Throttle" + szUsername] = ListThrottle;
                    HttpContext.Current.Application["DeltaT" + szUsername] = ListDeltaT;
                    strProbM = Convert.ToString(TotalWeight);
                    HttpContext.Current.Application["ProbM" + szUsername] = strProbM;
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
            else // value in memory
            {
            }
            // no take everything
            if (IsList != null)
            {
                strProbM = IsList.ToString();
                TextBoxProbM.Text = strProbM;
                IsList = HttpContext.Current.Application["ProbMTarget" + szUsername];
                if (IsList != null)
                {
                    strProbMTarget = IsList.ToString();
                    TextBoxProbMTarget.Text = strProbMTarget;
                }
                int iEngineImpuse = 0;
                ListEngineImpuse = (List<double>) HttpContext.Current.Application["EngineImpuse" + szUsername];
                if (ListEngineImpuse.Count > 0)
                {
                    for (iEngineImpuse = 0; iEngineImpuse < ListEngineImpuse.Count; iEngineImpuse++)
                    {
                        strImpNumber = "ImplVal" + Convert.ToString(iEngineImpuse) + szUsername;
                        ListImpulseVal = (List<double>)HttpContext.Current.Application[strImpNumber];
                        ListWeight = (List<double>)HttpContext.Current.Application["Weight" + szUsername];
                        ListFrameWeight = (List<double>)HttpContext.Current.Application["FrameWeight" + szUsername];
                        ListEngineType = (List<double>)HttpContext.Current.Application["EngineType" + szUsername];
                        ListThrottle = (List<double>)HttpContext.Current.Application["Throttle" + szUsername];
                        ListDeltaT = (List<double>)HttpContext.Current.Application["DeltaT" + szUsername];

                        strEngineXML = MakeEngineXMLFile(ListImpulseVal, ListEngineImpuse.ElementAt(iEngineImpuse), ListWeight.ElementAt(iEngineImpuse),
                            ListFrameWeight.ElementAt(iEngineImpuse), ListEngineType.ElementAt(iEngineImpuse), ListThrottle.ElementAt(iEngineImpuse), ListDeltaT.ElementAt(iEngineImpuse));
                        switch (iEngineImpuse)
                        {
                            case 0:
                                TextBoxEngineXML1.Text = strEngineXML;
                                ImageImpl1.ImageUrl = "EngineJpgGen.aspx?n=1";
                                //CheckBoxImp1.Checked = true;
                                break;
                            case 1:
                                TextBoxEngineXML2.Text = strEngineXML;
                                ImageImpl2.ImageUrl = "EngineJpgGen.aspx?n=2";
                                //CheckBoxImp2.Checked = true;
                                break;
                            case 2:
                                TextBoxEngineXML3.Text = strEngineXML;
                                ImageImpl3.ImageUrl = "EngineJpgGen.aspx?n=3";
                                //CheckBoxImp3.Checked = true;
                                break;
                            case 3:
                                TextBoxEngineXML4.Text = strEngineXML;
                                ImageImpl4.ImageUrl = "EngineJpgGen.aspx?n=4";
                                //CheckBoxImp4.Checked = true;
                                break;
                            case 4:
                                TextBoxEngineXML5.Text = strEngineXML;
                                ImageImpl5.ImageUrl = "EngineJpgGen.aspx?n=5";
                                //CheckBoxImp5.Checked = true;
                                break;
                        }

                    }
                }
            }
            UpdateVisibility();
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

        protected void CheckBoxImp1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateVisibility();
        }

        protected void ButtonUpdateImp1_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonAddImp1_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonDeleteImp1_Click(object sender, EventArgs e)
        {
        }

        protected void CheckBoxImp2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateVisibility();
        }

        protected void CheckBoxImp3_CheckedChanged(object sender, EventArgs e)
        {
            UpdateVisibility();
        }

        protected void CheckBoxImp4_CheckedChanged(object sender, EventArgs e)
        {
            UpdateVisibility();
        }

        protected void CheckBoxImp5_CheckedChanged(object sender, EventArgs e)
        {
            UpdateVisibility();
        }
    }
}
