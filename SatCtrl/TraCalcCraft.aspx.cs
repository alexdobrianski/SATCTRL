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

namespace SatCtrl
{
    public partial class _TraCalcCraft : System.Web.UI.Page
    {
        public struct CraftParam
        {
            public double TotalWeight;
            public List<double> ListEngineImpuse ;
            public List<double> ListWeight;
            public List<double> ListFrameWeight;
            public List<double> ListEngineType;
            public List<double> ListFireTime;
            public List<double> ListThrottle;
            public List<double> ListDeltaT;
            public List<double> ListImpulseVal;
            public string szUsername;
            public String strProbM;
            public String strProbMTarget;
            public String strEngineXML;
            public String TextBoxEngineXML1_Text;
            public String TextBoxEngineXML2_Text;
            public String TextBoxEngineXML3_Text;
            public String TextBoxEngineXML4_Text;
            public String TextBoxEngineXML5_Text;
            public int iTotalIteration;
        };
        public class Common
        {

            public static CraftParam GetFromApplication(CraftParam MyCraft, object IsList)
            {
                
                String strImpNumber;
                MyCraft.strProbM = IsList.ToString();
                //TextBoxProbM.Text = strProbM;
                IsList = HttpContext.Current.Application["ProbMTarget" + MyCraft.szUsername];
                if (IsList != null)
                {
                    MyCraft.strProbMTarget = IsList.ToString();
                    //TextBoxProbMTarget.Text = strProbMTarget;
                }
                int iEngineImpuse = 0;
                MyCraft.ListEngineImpuse = (List<double>)HttpContext.Current.Application["EngineImpuse" + MyCraft.szUsername];
                if (MyCraft.ListEngineImpuse.Count > 0)
                {
                    for (iEngineImpuse = 0; iEngineImpuse < MyCraft.ListEngineImpuse.Count; iEngineImpuse++)
                    {
                        strImpNumber = "ImplVal" + Convert.ToString(iEngineImpuse) + MyCraft.szUsername;
                        MyCraft.ListImpulseVal = (List<double>)HttpContext.Current.Application[strImpNumber];
                        MyCraft.ListWeight = (List<double>)HttpContext.Current.Application["Weight" + MyCraft.szUsername];
                        MyCraft.ListFrameWeight = (List<double>)HttpContext.Current.Application["FrameWeight" + MyCraft.szUsername];
                        MyCraft.ListEngineType = (List<double>)HttpContext.Current.Application["EngineType" + MyCraft.szUsername];
                        MyCraft.ListFireTime = (List<double>)HttpContext.Current.Application["FireTime" + MyCraft.szUsername];
                        MyCraft.ListThrottle = (List<double>)HttpContext.Current.Application["Throttle" + MyCraft.szUsername];
                        MyCraft.ListDeltaT = (List<double>)HttpContext.Current.Application["DeltaT" + MyCraft.szUsername];

                        MyCraft.strEngineXML = SatCtrl._TraCalcCraft.Common.MakeEngineXMLFile(MyCraft.ListImpulseVal, MyCraft.ListEngineImpuse[iEngineImpuse], 
                            MyCraft.ListWeight[iEngineImpuse],
                            MyCraft.ListFrameWeight[iEngineImpuse], MyCraft.ListEngineType[iEngineImpuse],
                            MyCraft.ListFireTime[iEngineImpuse],
                            MyCraft.ListThrottle[iEngineImpuse], MyCraft.ListDeltaT[iEngineImpuse]);
                        switch (iEngineImpuse)
                        {
                            case 0:
                                MyCraft.TextBoxEngineXML1_Text = MyCraft.strEngineXML;
                                //CheckBoxImp1.Checked = true;
                                break;
                            case 1:
                                MyCraft.TextBoxEngineXML2_Text = MyCraft.strEngineXML;
                                //CheckBoxImp2.Checked = true;
                                break;
                            case 2:
                                MyCraft.TextBoxEngineXML3_Text = MyCraft.strEngineXML;
                                //CheckBoxImp3.Checked = true;
                                break;
                            case 3:
                                MyCraft.TextBoxEngineXML4_Text = MyCraft.strEngineXML;
                                //CheckBoxImp4.Checked = true;
                                break;
                            case 4:
                                MyCraft.TextBoxEngineXML5_Text = MyCraft.strEngineXML;
                                //CheckBoxImp5.Checked = true;
                                break;
                        }

                    }
                }
                return MyCraft;
            }
            static public CraftParam ProcessOneImpl(CraftParam MyCraft, String xml, int iEngineImpuse, int iTotalIteration, int iEngineImpuseUpdt, bool update = false)
            {
                String strImpNumber;
                String strEngineImpuse;
                String strWeight;
                String strEngineType;
                String strFireTime;
                String strThrottle;
                String strDeltaT;
                String strFrameWeight;

                double dEngineImpuse;
                double dWeight;
                double FrameWeight;
                double EngineType;
                double FireTime;
                double dThrottle;
                double dDeltaT;

                {
                    // read engine profiles
                    String strImplVal = null;
                    double dImplVal = 0;

                    int iIteration = 0;
                    do
                    {
                        strImplVal = SatCtrl.Global.Common.GetValue(xml, "ImplVal", iTotalIteration);
                        iTotalIteration += 1;
                        iIteration += 1;
                        if (strImplVal != null)
                        {
                            dImplVal = Convert.ToDouble(strImplVal);
                            if (dImplVal == 0.0 && iIteration > 1) // next engine's impulse
                            {

                                MyCraft.ListImpulseVal.Add(dImplVal);
                                strImpNumber = "ImplVal" + Convert.ToString(iEngineImpuse) + MyCraft.szUsername;
                                HttpContext.Current.Application[strImpNumber] = MyCraft.ListImpulseVal;

                                //<TRA:setting name="EngineImpuse" value="1.0" />
                                strEngineImpuse = SatCtrl.Global.Common.GetValue(xml, "EngineImpuse", iEngineImpuseUpdt);
                                dEngineImpuse = Convert.ToDouble(strEngineImpuse);
                                if (update)
                                    MyCraft.ListEngineImpuse[iEngineImpuse] = dEngineImpuse;
                                else
                                    MyCraft.ListEngineImpuse.Add(dEngineImpuse);
                                // <TRA:setting name="Weight" value="17.157" />
                                strWeight = SatCtrl.Global.Common.GetValue(xml, "Weight", iEngineImpuseUpdt);
                                dWeight = Convert.ToDouble(strWeight);
                                MyCraft.TotalWeight += dWeight;
                                if (update)
                                    MyCraft.ListWeight[iEngineImpuse] = dWeight;
                                else
                                    MyCraft.ListWeight.Add(dWeight);
                                // <TRA:setting name="FrameWeight" value="7.0" />
                                strFrameWeight = SatCtrl.Global.Common.GetValue(xml, "FrameWeight", iEngineImpuseUpdt);
                                FrameWeight = Convert.ToDouble(strFrameWeight);
                                MyCraft.ListFrameWeight.Add(FrameWeight);
                                // <!-- set engine type
                                //      -1 = fixed 
                                //      N = variable point on a plot describing impules value
                                //          -->
                                //<TRA:setting name="EngineType" value="-1.0" />
                                strEngineType = SatCtrl.Global.Common.GetValue(xml, "EngineType", iEngineImpuseUpdt);
                                EngineType = Convert.ToDouble(strEngineType);
                                if (update)
                                    MyCraft.ListEngineType[iEngineImpuse] = EngineType;
                                else
                                    MyCraft.ListEngineType.Add(EngineType);



                                strFireTime = SatCtrl.Global.Common.GetValue(xml, "FireTime", iEngineImpuseUpdt);
                                FireTime = Convert.ToDouble(strFireTime);
                                if (update)
                                    MyCraft.ListFireTime[iEngineImpuse] = FireTime;
                                else
                                    MyCraft.ListFireTime.Add(FireTime);

                                //<!-- set engine Throttle  value (0.0 - 1.0) -->    
                                //<TRA:setting name="Throttle" value="1.0" />
                                strThrottle = SatCtrl.Global.Common.GetValue(xml, "Throttle", iEngineImpuseUpdt);
                                dThrottle = Convert.ToDouble(strThrottle);

                                if (update)
                                    MyCraft.ListThrottle[iEngineImpuse] = dThrottle;
                                else
                                    MyCraft.ListThrottle.Add(dThrottle);
                                //<!-- iteration per sec from engine's plot -->
                                //<TRA:setting name="DeltaT" value="5.0" />
                                strDeltaT = SatCtrl.Global.Common.GetValue(xml, "DeltaT", iEngineImpuseUpdt);
                                dDeltaT = Convert.ToDouble(strDeltaT);
                                if (update)
                                    MyCraft.ListDeltaT[iEngineImpuse] = dDeltaT;
                                else
                                    MyCraft.ListDeltaT.Add(dDeltaT);
                                MyCraft.iTotalIteration = iTotalIteration;
                                return MyCraft;
                            }
                            else
                            {
                                MyCraft.ListImpulseVal.Add(dImplVal);
                            }

                        }

                    }
                    while (strImplVal != null);
                }

                MyCraft.iTotalIteration = 0;
                return MyCraft;
            }
            
            public static String MakeEngineXMLFile(List<double> ListVal, double dEngineImpuse, double dWeight,
            double dFrameWeight, double dEngineType, double dFireTime, double dThrottle, double dDeltaT)
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

                String strXML = "<TRA name=\"EngineImpuse +\" value=\"" + dEngineImpuse + "\" />\r\n" +
                "   <TRA name=\"Weight\" value=\"" + dWeight + "\" />\r\n" +
                "   <TRA name=\"FrameWeight\" value=\"" + dFrameWeight + "\" />\r\n" +
                "   <!-- set engine type\r\n" +
                "     -1 = fixed \r\n" +
                "      N = variable point on a plot describing impules value\r\n" +
                "     -->\r\n" +
                "   <TRA name=\"EngineType\" value=\"" + dEngineType + "\" />\r\n" +
                "   <!-- set engine Throttle  value (0.0 - 1.0) -->\r\n" +
                "   <!-- if engine is liquid (EngineType=NN)\r\n" +
                "        set firing time of the engine \r\n" +
                "        otherwise it is ignored -->\r\n" +
                "    <TRA name=\"FireTime\" value=\"" + dFireTime + "\" />\r\n" +
                "   <TRA name=\"Throttle\" value=\"" + dThrottle + "\" />\r\n" +
                "   <!-- iteration per sec from engine's plot -->\r\n" +
                "    <TRA name=\"DeltaT\" value=\"" + dDeltaT + "\" />\r\n";
                String TimeVal;
                String NumberVal;
                double dI = 0;
                foreach (double dd in ListVal)
                {
                    NumberVal = Convert.ToString(dI);
                    TimeVal = Convert.ToString(dI / dDeltaT); dI += 1.0;

                    strXML += " <TRA n=\"" + NumberVal + "\" name=\"ImplVal\" value=\"" + dd + "\" t=\"" + TimeVal + "\"/>\r\n";
                    if (dEngineType == dI)
                        strXML += " <! ---- from that point it will be XXX seconds of firing ------>\r\n";
                }
                return strXML;
            }
            
        } 

        public long intMaxSessionN;
        //string szUsername = "Main";
        //List<double> ListEngineImpuse = new List<double>();
        //List<double> ListWeight = new List<double>();
        //List<double> ListFrameWeight = new List<double>();
        //List<double> ListEngineType = new List<double>();
        //List<double> ListFireTime = new List<double>();
        //List<double> ListThrottle = new List<double>();
        //List<double> ListDeltaT = new List<double>();
        //List<double> ListImpulseVal = new List<double>();
        void UpdateVisibility(CraftParam MyCraft)
        {

            switch (MyCraft.ListEngineImpuse.Count)
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
                    //ButtonDeleteImp1.Visible = !CheckBoxImp1.Checked;
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
                    //ButtonDeleteImp1.Visible = !CheckBoxImp1.Checked;
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
                    //ButtonDeleteImp1.Visible = !CheckBoxImp1.Checked;
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
                    //ButtonDeleteImp1.Visible = !CheckBoxImp1.Checked;
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
                    //ButtonDeleteImp1.Visible = !CheckBoxImp1.Checked;
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
        protected CraftParam GetAndSetAndUpdate(CraftParam MyCraft, object IsList)
        {
            if (IsList != null)
            {
                MyCraft = SatCtrl._TraCalcCraft.Common.GetFromApplication(MyCraft, IsList);
                TextBoxProbM.Text = MyCraft.strProbM;
                TextBoxProbMTarget.Text = MyCraft.strProbMTarget;
                if (MyCraft.ListEngineImpuse.Count > 0)
                {
                    for (int iEngineImpuse = 0; iEngineImpuse < MyCraft.ListEngineImpuse.Count; iEngineImpuse++)
                    {
                        switch (iEngineImpuse)
                        {
                            case 0:
                                TextBoxEngineXML1.Text = MyCraft.TextBoxEngineXML1_Text;
                                ImageImpl1.ImageUrl = "EngineJpgGen.aspx?n=0";
                                //CheckBoxImp1.Checked = true;
                                break;
                            case 1:
                                TextBoxEngineXML2.Text = MyCraft.TextBoxEngineXML2_Text;
                                ImageImpl2.ImageUrl = "EngineJpgGen.aspx?n=1";
                                //CheckBoxImp2.Checked = true;
                                break;
                            case 2:
                                TextBoxEngineXML3.Text = MyCraft.TextBoxEngineXML3_Text;
                                ImageImpl3.ImageUrl = "EngineJpgGen.aspx?n=2";
                                //CheckBoxImp3.Checked = true;
                                break;
                            case 3:
                                TextBoxEngineXML4.Text = MyCraft.TextBoxEngineXML4_Text;
                                ImageImpl4.ImageUrl = "EngineJpgGen.aspx?n=3";
                                //CheckBoxImp4.Checked = true;
                                break;
                            case 4:
                                TextBoxEngineXML5.Text = MyCraft.TextBoxEngineXML5_Text;
                                ImageImpl5.ImageUrl = "EngineJpgGen.aspx?n=4";
                                //CheckBoxImp5.Checked = true;
                                break;
                        }

                    }
                }

            }
            UpdateVisibility(MyCraft);
            return MyCraft;
        }
        String TextBoxEngineXML1Value;
        String TextBoxEngineXML2Value;
        String TextBoxEngineXML3Value;
        String TextBoxEngineXML4Value;
        String TextBoxEngineXML5Value;
        String TextBoxProbMTargetValue;
        CraftParam MyCraft;
        protected void Page_Load(object sender, EventArgs e)
        {
            //String strProbM;
            //String strProbMTarget;
            //String strImpNumber;
            //String strEngineXML;
            MyCraft.ListEngineImpuse = new List<double>();
            MyCraft.ListWeight = new List<double>();
            MyCraft.ListFrameWeight = new List<double>();
            MyCraft.ListEngineType = new List<double>();
            MyCraft.ListFireTime = new List<double>();
            MyCraft.ListThrottle = new List<double>();
            MyCraft.ListDeltaT = new List<double>();
            MyCraft.ListImpulseVal = new List<double>();
            MyCraft.TotalWeight = 0.0;

            string strMaxSessionN = HttpContext.Current.Application["strMaxSessionN"].ToString();
            intMaxSessionN = Convert.ToInt32(strMaxSessionN);
            if (Page.User.Identity.IsAuthenticated)
            {
                MyCraft.szUsername = Page.User.Identity.Name.ToString();
                TextBoxEngineXML1.ReadOnly = false;
                TextBoxEngineXML2.ReadOnly = false;
                TextBoxEngineXML3.ReadOnly = false;
                TextBoxEngineXML4.ReadOnly = false;
                TextBoxEngineXML5.ReadOnly = false;
                TextBoxProbMTarget.ReadOnly = false;
                LabelTextToLogin.Visible = false;
                HyperLinkLogin.Visible = false;
            }
            else
            {
                MyCraft.szUsername = "Main";
                LabelTextToLogin.Visible = true;
                HyperLinkLogin.Visible = true;
            }
            TextBoxEngineXML1Value = TextBoxEngineXML1.Text.ToString();
            TextBoxEngineXML2Value = TextBoxEngineXML2.Text.ToString();
            TextBoxEngineXML3Value = TextBoxEngineXML3.Text.ToString();
            TextBoxEngineXML4Value = TextBoxEngineXML4.Text.ToString();
            TextBoxEngineXML5Value = TextBoxEngineXML5.Text.ToString();
            TextBoxProbMTargetValue = TextBoxProbMTarget.Text.ToString();

            LabelUserName.Text = MyCraft.szUsername;
            object IsList = HttpContext.Current.Application["ProbM" + MyCraft.szUsername];
            if (IsList == null)
            {
                String xml = null;
                String NameFile = "InitCraft" + MyCraft.szUsername + ".xml";
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
                if (xml == null) // Init file is absent try ti use main:
                {
                    NameFile = "InitCraft" + "Main.xml";
                    MapPath = Server.MapPath(NameFile);
                    iDirAccound = MapPath.IndexOf("\\SatCtrl\\");
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
                }
                if (xml != null)
                {
                    MyCraft.ListImpulseVal = new List<double>();
                    MyCraft.strProbM = SatCtrl.Global.Common.GetValue(xml, "ProbM", 0);
                    HttpContext.Current.Application["ProbM" + MyCraft.szUsername] = MyCraft.strProbM;

                    MyCraft.strProbMTarget = SatCtrl.Global.Common.GetValue(xml, "ProbMTarget", 0);
                    HttpContext.Current.Application["ProbMTarget" + MyCraft.szUsername] = MyCraft.strProbMTarget;

                    MyCraft.TotalWeight = Convert.ToDouble(MyCraft.strProbMTarget);
                    // now need to read all engines profiles
                    
                    int iTotalIteration = 0;
                    int iEngineImpuse = 0;
                    int iEngineImpuseUpdt = 0;
                    do
                    {
                        MyCraft = SatCtrl._TraCalcCraft.Common.ProcessOneImpl(MyCraft, xml, iEngineImpuse, iTotalIteration, iEngineImpuseUpdt);
                        iTotalIteration = MyCraft.iTotalIteration;
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
                        iEngineImpuseUpdt += 1;
                        MyCraft.ListImpulseVal = new List<double>(); 
                    }
                    while (iTotalIteration != 0);

                    HttpContext.Current.Application["EngineImpuse" + MyCraft.szUsername] = MyCraft.ListEngineImpuse;
                    HttpContext.Current.Application["Weight" + MyCraft.szUsername] = MyCraft.ListWeight;
                    HttpContext.Current.Application["FrameWeight" + MyCraft.szUsername] = MyCraft.ListFrameWeight;
                    HttpContext.Current.Application["EngineType" + MyCraft.szUsername] = MyCraft.ListEngineType;
                    HttpContext.Current.Application["FireTime" + MyCraft.szUsername] = MyCraft.ListFireTime;
                    HttpContext.Current.Application["Throttle" + MyCraft.szUsername] = MyCraft.ListThrottle;
                    HttpContext.Current.Application["DeltaT" + MyCraft.szUsername] = MyCraft.ListDeltaT;
                    MyCraft.strProbM = Convert.ToString(MyCraft.TotalWeight);
                    HttpContext.Current.Application["ProbM" + MyCraft.szUsername] = MyCraft.strProbM;
                }
                else // file with initial data do not exsists
                {
                    MyCraft.strProbM = "255";
                    HttpContext.Current.Application["ProbM" + MyCraft.szUsername] = MyCraft.strProbM;
                    MyCraft.strProbMTarget = "4";
                    HttpContext.Current.Application["ProbMTarget"] = MyCraft.strProbMTarget;
                }
                IsList = HttpContext.Current.Application["ProbM" + MyCraft.szUsername];
            }
            else // value in memory
            {
            }
            // no take everything
            MyCraft = GetAndSetAndUpdate(MyCraft, IsList);
        }
        
        protected void CheckBoxImp1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateVisibility(MyCraft);
        }
        protected void CheckBoxImp2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateVisibility(MyCraft);
        }

        protected void CheckBoxImp3_CheckedChanged(object sender, EventArgs e)
        {
            UpdateVisibility(MyCraft);
        }

        protected void CheckBoxImp4_CheckedChanged(object sender, EventArgs e)
        {
            UpdateVisibility(MyCraft);
        }

        protected void CheckBoxImp5_CheckedChanged(object sender, EventArgs e)
        {
            UpdateVisibility(MyCraft);
        }
        /// <summary>
        /// update group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonUpdateImp1_Click(object sender, EventArgs e)
        {
            MyCraft.ListImpulseVal = new List<double>();
            String xml = TextBoxEngineXML1Value;
            int iEngineImpuse = 0;
            int iTotalIteration = 0;
            
            MyCraft = SatCtrl._TraCalcCraft.Common.ProcessOneImpl(MyCraft, xml, iEngineImpuse, iTotalIteration, 0, true);
            iTotalIteration = MyCraft.iTotalIteration;
            object IsList = HttpContext.Current.Application["ProbM" + MyCraft.szUsername];
            MyCraft = GetAndSetAndUpdate(MyCraft, IsList);
        }

        protected void ButtonUpdateImp2_Click(object sender, EventArgs e)
        {
            MyCraft.ListImpulseVal = new List<double>();
            String xml = TextBoxEngineXML2Value;
            int iEngineImpuse = 1;
            int iTotalIteration = 0;
            MyCraft = SatCtrl._TraCalcCraft.Common.ProcessOneImpl(MyCraft, xml, iEngineImpuse, iTotalIteration, 0, true);
            iTotalIteration = MyCraft.iTotalIteration;
            object IsList = HttpContext.Current.Application["ProbM" + MyCraft.szUsername];
            MyCraft = GetAndSetAndUpdate(MyCraft, IsList);
        }

        protected void ButtonUpdateImp3_Click(object sender, EventArgs e)
        {
            MyCraft.ListImpulseVal = new List<double>();
            String xml = TextBoxEngineXML3Value;
            int iEngineImpuse = 2;
            int iTotalIteration = 0;
            MyCraft = SatCtrl._TraCalcCraft.Common.ProcessOneImpl(MyCraft, xml, iEngineImpuse, iTotalIteration, 0, true);
            iTotalIteration = MyCraft.iTotalIteration;
            object IsList = HttpContext.Current.Application["ProbM" + MyCraft.szUsername];
            MyCraft = GetAndSetAndUpdate(MyCraft, IsList);
        }

        protected void ButtonUpdateImp4_Click(object sender, EventArgs e)
        {
            MyCraft.ListImpulseVal = new List<double>();
            String xml = TextBoxEngineXML4Value;
            int iEngineImpuse = 2;
            int iTotalIteration = 0;
            MyCraft = SatCtrl._TraCalcCraft.Common.ProcessOneImpl(MyCraft, xml, iEngineImpuse, iTotalIteration, 0, true);
            iTotalIteration = MyCraft.iTotalIteration;
            object IsList = HttpContext.Current.Application["ProbM" + MyCraft.szUsername];
            MyCraft = GetAndSetAndUpdate(MyCraft, IsList);
        }

        protected void ButtonUpdateImp5_Click(object sender, EventArgs e)
        {
            MyCraft.ListImpulseVal = new List<double>();
            String xml = TextBoxEngineXML5Value;
            int iEngineImpuse = 2;
            int iTotalIteration = 0;
            MyCraft = SatCtrl._TraCalcCraft.Common.ProcessOneImpl(MyCraft, xml, iEngineImpuse, iTotalIteration, 0, true);
            iTotalIteration = MyCraft.iTotalIteration;
            object IsList = HttpContext.Current.Application["ProbM" + MyCraft.szUsername];
            MyCraft = GetAndSetAndUpdate(MyCraft, IsList);
        }

        ///////
        // added impulse goup
        protected void ButtonAddImp1_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonAddImp2_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonAddImp3_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonAddImp4_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonAddImp5_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// delete impulse group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonDeleteImp1_Click(object sender, EventArgs e)
        {
        }

        protected void ButtonDeleteImp2_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonDeleteImp3_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonDeleteImp4_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonDeleteImp5_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
