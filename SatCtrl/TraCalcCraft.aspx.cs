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
        public class Common
        {
            public static double TotalWeight = 0.0;
            public static List<double> ListEngineImpuse = new List<double>();
            public static List<double> ListWeight = new List<double>();
            public static List<double> ListFrameWeight = new List<double>();
            public static List<double> ListEngineType = new List<double>();
            public static List<double> ListFireTime = new List<double>();
            public static List<double> ListThrottle = new List<double>();
            public static List<double> ListDeltaT = new List<double>();
            public static List<double> ListImpulseVal = new List<double>();
            public static string szUsername = "Main";
            public static String strProbM;
            public static String strProbMTarget;
            public static String strEngineXML;
            public static String TextBoxEngineXML1_Text;
            public static String TextBoxEngineXML2_Text;
            public static String TextBoxEngineXML3_Text;
            public static String TextBoxEngineXML4_Text;
            public static String TextBoxEngineXML5_Text;

            public static void GetFromApplication(object IsList)
            {
                String strImpNumber;
                strProbM = IsList.ToString();
                //TextBoxProbM.Text = strProbM;
                IsList = HttpContext.Current.Application["ProbMTarget" + szUsername];
                if (IsList != null)
                {
                    strProbMTarget = IsList.ToString();
                    //TextBoxProbMTarget.Text = strProbMTarget;
                }
                int iEngineImpuse = 0;
                ListEngineImpuse = (List<double>)HttpContext.Current.Application["EngineImpuse" + szUsername];
                if (ListEngineImpuse.Count > 0)
                {
                    for (iEngineImpuse = 0; iEngineImpuse < ListEngineImpuse.Count; iEngineImpuse++)
                    {
                        strImpNumber = "ImplVal" + Convert.ToString(iEngineImpuse) + szUsername;
                        ListImpulseVal = (List<double>)HttpContext.Current.Application[strImpNumber];
                        ListWeight = (List<double>)HttpContext.Current.Application["Weight" + szUsername];
                        ListFrameWeight = (List<double>)HttpContext.Current.Application["FrameWeight" + szUsername];
                        ListEngineType = (List<double>)HttpContext.Current.Application["EngineType" + szUsername];
                        ListFireTime = (List<double>)HttpContext.Current.Application["FireTime" + szUsername];
                        ListThrottle = (List<double>)HttpContext.Current.Application["Throttle" + szUsername];
                        ListDeltaT = (List<double>)HttpContext.Current.Application["DeltaT" + szUsername];

                        strEngineXML = SatCtrl._TraCalcCraft.Common.MakeEngineXMLFile(ListImpulseVal, ListEngineImpuse[iEngineImpuse], ListWeight[iEngineImpuse],
                            ListFrameWeight[iEngineImpuse], ListEngineType[iEngineImpuse],
                            ListFireTime[iEngineImpuse],
                            ListThrottle[iEngineImpuse], ListDeltaT[iEngineImpuse]);
                        switch (iEngineImpuse)
                        {
                            case 0:
                                TextBoxEngineXML1_Text = strEngineXML;
                                //CheckBoxImp1.Checked = true;
                                break;
                            case 1:
                                TextBoxEngineXML2_Text = strEngineXML;
                                //CheckBoxImp2.Checked = true;
                                break;
                            case 2:
                                TextBoxEngineXML3_Text = strEngineXML;
                                //CheckBoxImp3.Checked = true;
                                break;
                            case 3:
                                TextBoxEngineXML4_Text = strEngineXML;
                                //CheckBoxImp4.Checked = true;
                                break;
                            case 4:
                                TextBoxEngineXML5_Text = strEngineXML;
                                //CheckBoxImp5.Checked = true;
                                break;
                        }

                    }
                }
            }
            static public int ProcessOneImpl(String xml, int iEngineImpuse, int iTotalIteration, int iEngineImpuseUpdt, bool update = false)
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

                                ListImpulseVal.Add(dImplVal);
                                strImpNumber = "ImplVal" + Convert.ToString(iEngineImpuse) + szUsername;
                                HttpContext.Current.Application[strImpNumber] = ListImpulseVal;

                                //<TRA:setting name="EngineImpuse" value="1.0" />
                                strEngineImpuse = SatCtrl.Global.Common.GetValue(xml, "EngineImpuse", iEngineImpuseUpdt);
                                dEngineImpuse = Convert.ToDouble(strEngineImpuse);
                                if (update)
                                    ListEngineImpuse[iEngineImpuse] = dEngineImpuse;
                                else
                                    ListEngineImpuse.Add(dEngineImpuse);
                                // <TRA:setting name="Weight" value="17.157" />
                                strWeight = SatCtrl.Global.Common.GetValue(xml, "Weight", iEngineImpuseUpdt);
                                dWeight = Convert.ToDouble(strWeight);
                                TotalWeight += dWeight;
                                if (update)
                                    ListWeight[iEngineImpuse] = dWeight;
                                else
                                    ListWeight.Add(dWeight);
                                // <TRA:setting name="FrameWeight" value="7.0" />
                                strFrameWeight = SatCtrl.Global.Common.GetValue(xml, "FrameWeight", iEngineImpuseUpdt);
                                FrameWeight = Convert.ToDouble(strFrameWeight);
                                ListFrameWeight.Add(FrameWeight);
                                // <!-- set engine type
                                //      -1 = fixed 
                                //      N = variable point on a plot describing impules value
                                //          -->
                                //<TRA:setting name="EngineType" value="-1.0" />
                                strEngineType = SatCtrl.Global.Common.GetValue(xml, "EngineType", iEngineImpuseUpdt);
                                EngineType = Convert.ToDouble(strEngineType);
                                if (update)
                                    ListEngineType[iEngineImpuse] = EngineType;
                                else
                                    ListEngineType.Add(EngineType);



                                strFireTime = SatCtrl.Global.Common.GetValue(xml, "FireTime", iEngineImpuseUpdt);
                                FireTime = Convert.ToDouble(strFireTime);
                                if (update)
                                    ListFireTime[iEngineImpuse] = FireTime;
                                else
                                    ListFireTime.Add(FireTime);

                                //<!-- set engine Throttle  value (0.0 - 1.0) -->    
                                //<TRA:setting name="Throttle" value="1.0" />
                                strThrottle = SatCtrl.Global.Common.GetValue(xml, "Throttle", iEngineImpuseUpdt);
                                dThrottle = Convert.ToDouble(strThrottle);

                                if (update)
                                    ListThrottle[iEngineImpuse] = dThrottle;
                                else
                                    ListThrottle.Add(dThrottle);
                                //<!-- iteration per sec from engine's plot -->
                                //<TRA:setting name="DeltaT" value="5.0" />
                                strDeltaT = SatCtrl.Global.Common.GetValue(xml, "DeltaT", iEngineImpuseUpdt);
                                dDeltaT = Convert.ToDouble(strDeltaT);
                                if (update)
                                    ListDeltaT[iEngineImpuse] = dDeltaT;
                                else
                                    ListDeltaT.Add(dDeltaT);
                                return iTotalIteration;
                            }
                            else
                            {
                                ListImpulseVal.Add(dImplVal);
                            }

                        }

                    }
                    while (strImplVal != null);
                }

                return 0;
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
        void UpdateVisibility()
        {

            switch (SatCtrl._TraCalcCraft.Common.ListEngineImpuse.Count)
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
        /*
        double TotalWeight = 0.0;
        public int ProcessOneImpl(String xml, int iEngineImpuse, int iTotalIteration, int iEngineImpuseUpdt, bool update = false)
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

                            ListImpulseVal.Add(dImplVal);
                            strImpNumber = "ImplVal" + Convert.ToString(iEngineImpuse) + szUsername;
                            HttpContext.Current.Application[strImpNumber] = ListImpulseVal;

                            //<TRA:setting name="EngineImpuse" value="1.0" />
                            strEngineImpuse = SatCtrl.Global.Common.GetValue(xml, "EngineImpuse", iEngineImpuseUpdt);
                            dEngineImpuse = Convert.ToDouble(strEngineImpuse);
                            if (update)
                                ListEngineImpuse[iEngineImpuse] = dEngineImpuse;
                            else
                                ListEngineImpuse.Add(dEngineImpuse);
                            // <TRA:setting name="Weight" value="17.157" />
                            strWeight = SatCtrl.Global.Common.GetValue(xml, "Weight", iEngineImpuseUpdt);
                            dWeight = Convert.ToDouble(strWeight);
                            TotalWeight += dWeight;
                            if (update)
                                ListWeight[iEngineImpuse] = dWeight;
                            else
                                ListWeight.Add(dWeight);
                            // <TRA:setting name="FrameWeight" value="7.0" />
                            strFrameWeight = SatCtrl.Global.Common.GetValue(xml, "FrameWeight", iEngineImpuseUpdt);
                            FrameWeight = Convert.ToDouble(strFrameWeight);
                            ListFrameWeight.Add(FrameWeight);
                            // <!-- set engine type
                            //      -1 = fixed 
                            //      N = variable point on a plot describing impules value
                            //          -->
                            //<TRA:setting name="EngineType" value="-1.0" />
                            strEngineType = SatCtrl.Global.Common.GetValue(xml, "EngineType", iEngineImpuseUpdt);
                            EngineType = Convert.ToDouble(strEngineType);
                            if (update)
                                ListEngineType[iEngineImpuse] = EngineType;
                            else
                                ListEngineType.Add(EngineType);



                            strFireTime = SatCtrl.Global.Common.GetValue(xml, "FireTime", iEngineImpuseUpdt);
                            FireTime = Convert.ToDouble(strFireTime);
                            if (update)
                                ListFireTime[iEngineImpuse] = FireTime;
                            else
                                ListFireTime.Add(FireTime);

                            //<!-- set engine Throttle  value (0.0 - 1.0) -->    
                            //<TRA:setting name="Throttle" value="1.0" />
                            strThrottle = SatCtrl.Global.Common.GetValue(xml, "Throttle", iEngineImpuseUpdt);
                            dThrottle = Convert.ToDouble(strThrottle);

                            if (update)
                                ListThrottle[iEngineImpuse] = dThrottle;
                            else
                                ListThrottle.Add(dThrottle);
                            //<!-- iteration per sec from engine's plot -->
                            //<TRA:setting name="DeltaT" value="5.0" />
                            strDeltaT = SatCtrl.Global.Common.GetValue(xml, "DeltaT", iEngineImpuseUpdt);
                            dDeltaT = Convert.ToDouble(strDeltaT);
                            if (update)
                                ListDeltaT[iEngineImpuse] = dDeltaT;
                            else
                                ListDeltaT.Add(dDeltaT);
                            return iTotalIteration;
                        }
                        else
                        {
                            ListImpulseVal.Add(dImplVal);
                        }

                    }

                }
                while (strImplVal != null);
            }

            return 0;
        }
         */
        /*
        public void GetFromApplication(object IsList)
        {
            String strProbM;
            String strProbMTarget;
            String strImpNumber;
            String strEngineXML;


            strProbM = IsList.ToString();
            TextBoxProbM.Text = strProbM;
            IsList = HttpContext.Current.Application["ProbMTarget" + szUsername];
            if (IsList != null)
            {
                strProbMTarget = IsList.ToString();
                TextBoxProbMTarget.Text = strProbMTarget;
            }
            int iEngineImpuse = 0;
            ListEngineImpuse = (List<double>)HttpContext.Current.Application["EngineImpuse" + szUsername];
            if (ListEngineImpuse.Count > 0)
            {
                for (iEngineImpuse = 0; iEngineImpuse < ListEngineImpuse.Count; iEngineImpuse++)
                {
                    strImpNumber = "ImplVal" + Convert.ToString(iEngineImpuse) + szUsername;
                    ListImpulseVal = (List<double>)HttpContext.Current.Application[strImpNumber];
                    ListWeight = (List<double>)HttpContext.Current.Application["Weight" + szUsername];
                    ListFrameWeight = (List<double>)HttpContext.Current.Application["FrameWeight" + szUsername];
                    ListEngineType = (List<double>)HttpContext.Current.Application["EngineType" + szUsername];
                    ListFireTime = (List<double>)HttpContext.Current.Application["FireTime" + szUsername];
                    ListThrottle = (List<double>)HttpContext.Current.Application["Throttle" + szUsername];
                    ListDeltaT = (List<double>)HttpContext.Current.Application["DeltaT" + szUsername];

                    strEngineXML = SatCtrl._TraCalcCraft.Common.MakeEngineXMLFile(ListImpulseVal, ListEngineImpuse[iEngineImpuse], ListWeight[iEngineImpuse],
                        ListFrameWeight[iEngineImpuse], ListEngineType[iEngineImpuse], 
                        ListFireTime[iEngineImpuse],
                        ListThrottle[iEngineImpuse], ListDeltaT[iEngineImpuse]);
                    switch (iEngineImpuse)
                    {
                        case 0:
                            TextBoxEngineXML1.Text = strEngineXML;
                            ImageImpl1.ImageUrl = "EngineJpgGen.aspx?n=0";
                            //CheckBoxImp1.Checked = true;
                            break;
                        case 1:
                            TextBoxEngineXML2.Text = strEngineXML;
                            ImageImpl2.ImageUrl = "EngineJpgGen.aspx?n=1";
                            //CheckBoxImp2.Checked = true;
                            break;
                        case 2:
                            TextBoxEngineXML3.Text = strEngineXML;
                            ImageImpl3.ImageUrl = "EngineJpgGen.aspx?n=2";
                            //CheckBoxImp3.Checked = true;
                            break;
                        case 3:
                            TextBoxEngineXML4.Text = strEngineXML;
                            ImageImpl4.ImageUrl = "EngineJpgGen.aspx?n=3";
                            //CheckBoxImp4.Checked = true;
                            break;
                        case 4:
                            TextBoxEngineXML5.Text = strEngineXML;
                            ImageImpl5.ImageUrl = "EngineJpgGen.aspx?n=4";
                            //CheckBoxImp5.Checked = true;
                            break;
                    }

                }
            }
        }*/
        protected void GetAndSetAndUpdate(object IsList)
        {
            if (IsList != null)
            {
                SatCtrl._TraCalcCraft.Common.GetFromApplication(IsList);
                TextBoxProbM.Text = SatCtrl._TraCalcCraft.Common.strProbM;
                TextBoxProbMTarget.Text = SatCtrl._TraCalcCraft.Common.strProbMTarget;
                if (SatCtrl._TraCalcCraft.Common.ListEngineImpuse.Count > 0)
                {
                    for (int iEngineImpuse = 0; iEngineImpuse < SatCtrl._TraCalcCraft.Common.ListEngineImpuse.Count; iEngineImpuse++)
                    {
                        switch (iEngineImpuse)
                        {
                            case 0:
                                TextBoxEngineXML1.Text = SatCtrl._TraCalcCraft.Common.TextBoxEngineXML1_Text;
                                ImageImpl1.ImageUrl = "EngineJpgGen.aspx?n=0";
                                //CheckBoxImp1.Checked = true;
                                break;
                            case 1:
                                TextBoxEngineXML2.Text = SatCtrl._TraCalcCraft.Common.TextBoxEngineXML2_Text;
                                ImageImpl2.ImageUrl = "EngineJpgGen.aspx?n=1";
                                //CheckBoxImp2.Checked = true;
                                break;
                            case 2:
                                TextBoxEngineXML3.Text = SatCtrl._TraCalcCraft.Common.TextBoxEngineXML3_Text;
                                ImageImpl3.ImageUrl = "EngineJpgGen.aspx?n=2";
                                //CheckBoxImp3.Checked = true;
                                break;
                            case 3:
                                TextBoxEngineXML4.Text = SatCtrl._TraCalcCraft.Common.TextBoxEngineXML4_Text;
                                ImageImpl4.ImageUrl = "EngineJpgGen.aspx?n=3";
                                //CheckBoxImp4.Checked = true;
                                break;
                            case 4:
                                TextBoxEngineXML5.Text = SatCtrl._TraCalcCraft.Common.TextBoxEngineXML5_Text;
                                ImageImpl5.ImageUrl = "EngineJpgGen.aspx?n=4";
                                //CheckBoxImp5.Checked = true;
                                break;
                        }

                    }
                }

            }
            UpdateVisibility();
        }
        String TextBoxEngineXML1Value;
        String TextBoxEngineXML2Value;
        String TextBoxEngineXML3Value;
        String TextBoxEngineXML4Value;
        String TextBoxEngineXML5Value;
        String TextBoxProbMTargetValue;

        protected void Page_Load(object sender, EventArgs e)
        {
            String strProbM;
            String strProbMTarget;
            //String strImpNumber;
            //String strEngineXML;

            string strMaxSessionN = HttpContext.Current.Application["strMaxSessionN"].ToString();
            intMaxSessionN = Convert.ToInt32(strMaxSessionN);
            if (Page.User.Identity.IsAuthenticated)
            {
                SatCtrl._TraCalcCraft.Common.szUsername = Page.User.Identity.Name.ToString();
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
                LabelTextToLogin.Visible = true;
                HyperLinkLogin.Visible = true;
            }
            TextBoxEngineXML1Value = TextBoxEngineXML1.Text.ToString();
            TextBoxEngineXML2Value = TextBoxEngineXML2.Text.ToString();
            TextBoxEngineXML3Value = TextBoxEngineXML3.Text.ToString();
            TextBoxEngineXML4Value = TextBoxEngineXML4.Text.ToString();
            TextBoxEngineXML5Value = TextBoxEngineXML5.Text.ToString();
            TextBoxProbMTargetValue = TextBoxProbMTarget.Text.ToString();

            LabelUserName.Text = SatCtrl._TraCalcCraft.Common.szUsername;
            object IsList = HttpContext.Current.Application["ProbM" + SatCtrl._TraCalcCraft.Common.szUsername];
            if (IsList == null)
            {
                String xml = null;
                String NameFile = "InitCraft" + SatCtrl._TraCalcCraft.Common.szUsername + ".xml";
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
                    SatCtrl._TraCalcCraft.Common.ListImpulseVal = new List<double>();
                    strProbM = SatCtrl.Global.Common.GetValue(xml, "ProbM", 0);
                    HttpContext.Current.Application["ProbM" + SatCtrl._TraCalcCraft.Common.szUsername] = strProbM;

                    strProbMTarget = SatCtrl.Global.Common.GetValue(xml, "ProbMTarget", 0);
                    HttpContext.Current.Application["ProbMTarget" + SatCtrl._TraCalcCraft.Common.szUsername] = strProbMTarget;

                    SatCtrl._TraCalcCraft.Common.TotalWeight = Convert.ToDouble(strProbMTarget);
                    // now need to read all engines profiles
                    
                    int iTotalIteration = 0;
                    int iEngineImpuse = 0;
                    int iEngineImpuseUpdt = 0;
                    do
                    {
                        iTotalIteration = SatCtrl._TraCalcCraft.Common.ProcessOneImpl(xml, iEngineImpuse, iTotalIteration, iEngineImpuseUpdt);
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
                        SatCtrl._TraCalcCraft.Common.ListImpulseVal = new List<double>(); 
                    }
                    while (iTotalIteration != 0);

                    HttpContext.Current.Application["EngineImpuse" + SatCtrl._TraCalcCraft.Common.szUsername] = SatCtrl._TraCalcCraft.Common.ListEngineImpuse;
                    HttpContext.Current.Application["Weight" + SatCtrl._TraCalcCraft.Common.szUsername] = SatCtrl._TraCalcCraft.Common.ListWeight;
                    HttpContext.Current.Application["FrameWeight" + SatCtrl._TraCalcCraft.Common.szUsername] = SatCtrl._TraCalcCraft.Common.ListFrameWeight;
                    HttpContext.Current.Application["EngineType" + SatCtrl._TraCalcCraft.Common.szUsername] = SatCtrl._TraCalcCraft.Common.ListEngineType;
                    HttpContext.Current.Application["FireTime" + SatCtrl._TraCalcCraft.Common.szUsername] = SatCtrl._TraCalcCraft.Common.ListFireTime;
                    HttpContext.Current.Application["Throttle" + SatCtrl._TraCalcCraft.Common.szUsername] = SatCtrl._TraCalcCraft.Common.ListThrottle;
                    HttpContext.Current.Application["DeltaT" + SatCtrl._TraCalcCraft.Common.szUsername] = SatCtrl._TraCalcCraft.Common.ListDeltaT;
                    strProbM = Convert.ToString(SatCtrl._TraCalcCraft.Common.TotalWeight);
                    HttpContext.Current.Application["ProbM" + SatCtrl._TraCalcCraft.Common.szUsername] = strProbM;
                }
                else // file with initial data do not exsists
                {
                    strProbM = "255";
                    HttpContext.Current.Application["ProbM" + SatCtrl._TraCalcCraft.Common.szUsername] = strProbM;
                    strProbMTarget = "4";
                    HttpContext.Current.Application["ProbMTarget"] = strProbMTarget;
                }
                IsList = HttpContext.Current.Application["ProbM" + SatCtrl._TraCalcCraft.Common.szUsername];
            }
            else // value in memory
            {
            }
            // no take everything
            GetAndSetAndUpdate(IsList);
        }
        
        protected void CheckBoxImp1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateVisibility();
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
        /// <summary>
        /// update group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonUpdateImp1_Click(object sender, EventArgs e)
        {
            SatCtrl._TraCalcCraft.Common.ListImpulseVal = new List<double>();
            String xml = TextBoxEngineXML1Value;
            int iEngineImpuse = 0;
            int iTotalIteration = 0;
            iTotalIteration = SatCtrl._TraCalcCraft.Common.ProcessOneImpl(xml, iEngineImpuse, iTotalIteration, 0, true);
            object IsList = HttpContext.Current.Application["ProbM" + SatCtrl._TraCalcCraft.Common.szUsername];
            GetAndSetAndUpdate(IsList);
        }

        protected void ButtonUpdateImp2_Click(object sender, EventArgs e)
        {
            SatCtrl._TraCalcCraft.Common.ListImpulseVal = new List<double>();
            String xml = TextBoxEngineXML2Value;
            int iEngineImpuse = 1;
            int iTotalIteration = 0;
            iTotalIteration = SatCtrl._TraCalcCraft.Common.ProcessOneImpl(xml, iEngineImpuse, iTotalIteration, 0, true);
            object IsList = HttpContext.Current.Application["ProbM" + SatCtrl._TraCalcCraft.Common.szUsername];
            GetAndSetAndUpdate(IsList);
        }

        protected void ButtonUpdateImp3_Click(object sender, EventArgs e)
        {
            SatCtrl._TraCalcCraft.Common.ListImpulseVal = new List<double>();
            String xml = TextBoxEngineXML3Value;
            int iEngineImpuse = 2;
            int iTotalIteration = 0;
            iTotalIteration = SatCtrl._TraCalcCraft.Common.ProcessOneImpl(xml, iEngineImpuse, iTotalIteration, 0, true);
            object IsList = HttpContext.Current.Application["ProbM" + SatCtrl._TraCalcCraft.Common.szUsername];
            GetAndSetAndUpdate(IsList);
        }

        protected void ButtonUpdateImp4_Click(object sender, EventArgs e)
        {
            SatCtrl._TraCalcCraft.Common.ListImpulseVal = new List<double>();
            String xml = TextBoxEngineXML4Value;
            int iEngineImpuse = 2;
            int iTotalIteration = 0;
            iTotalIteration = SatCtrl._TraCalcCraft.Common.ProcessOneImpl(xml, iEngineImpuse, iTotalIteration, 0, true);
            object IsList = HttpContext.Current.Application["ProbM" + SatCtrl._TraCalcCraft.Common.szUsername];
            GetAndSetAndUpdate(IsList);
        }

        protected void ButtonUpdateImp5_Click(object sender, EventArgs e)
        {
            SatCtrl._TraCalcCraft.Common.ListImpulseVal = new List<double>();
            String xml = TextBoxEngineXML5Value;
            int iEngineImpuse = 2;
            int iTotalIteration = 0;
            iTotalIteration = SatCtrl._TraCalcCraft.Common.ProcessOneImpl(xml, iEngineImpuse, iTotalIteration, 0, true);
            object IsList = HttpContext.Current.Application["ProbM" + SatCtrl._TraCalcCraft.Common.szUsername];
            GetAndSetAndUpdate(IsList);
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
