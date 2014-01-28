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
        _TraCalcCraft.CraftParam MyCraft;
        _TraCalcOrbit.CraftTLE MyTLE;
        protected _TraCalcCraft.CraftParam GetAndSetAndUpdate(_TraCalcCraft.CraftParam MyCraft, object IsList)
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
                                ImageEngine1.Visible = true;
                                ImageEngine1.ImageUrl = "EngineJpgGen.aspx?n=0";
                                break;
                            case 1:
                                ImageEngine2.Visible = true;
                                ImageEngine2.ImageUrl = "EngineJpgGen.aspx?n=1";
                                break;
                            case 2:
                                ImageEngine3.Visible = true;
                                ImageEngine3.ImageUrl = "EngineJpgGen.aspx?n=2";
                                break;
                            case 3:
                                ImageEngine4.Visible = true;
                                ImageEngine4.ImageUrl = "EngineJpgGen.aspx?n=3";
                                break;
                            case 4:
                                ImageEngine5.Visible = true;
                                ImageEngine5.ImageUrl = "EngineJpgGen.aspx?n=4";
                                break;
                        }

                    }
                }

            }
            //UpdateVisibility(MyCraft);
            return MyCraft;
        }
        String TextBoxTimeCalc_txt;
        String TextBoxTotalDays_Text;
        protected void Page_Load(object sender, EventArgs e)
        {
            String strEW;
            String strLongitude;
            String strLatitude;
            String strNS;
            MyCraft.ListEngineImpuse = new List<double>();
            MyCraft.ListWeight = new List<double>();
            MyCraft.ListFrameWeight = new List<double>();
            MyCraft.ListEngineType = new List<double>();
            MyCraft.ListFireTime = new List<double>();
            MyCraft.ListThrottle = new List<double>();
            MyCraft.ListDeltaT = new List<double>();
            MyCraft.ListImpulseVal = new List<double>();
            MyCraft.TotalWeight = 0.0;
            MyCraft.szUsername = "Main";
            if (Page.User.Identity.IsAuthenticated)
            {
                szUsername = Page.User.Identity.Name.ToString();
                MyCraft.szUsername = szUsername;
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
                //ButtonImpOptimization.Enabled = false;
                ButtonFindImp.Enabled = false;
            }


            IsList = HttpContext.Current.Application["ProbM" + MyCraft.szUsername];
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
            MyCraft = GetAndSetAndUpdate(MyCraft, IsList);
            TextBoxProbM.Text = MyCraft.strProbM.ToString();
            TextBoxProbMTarget.Text = MyCraft.strProbMTarget.ToString();



            //////////////////////////////////////////////////////////////////////
            string SVal1 = null;
            string SVal2 = null;
            string SVal3 = null;
            object IsIt = HttpContext.Current.Application["InitKeplerLine1" + szUsername];
            if (IsIt == null)
            {
                String xml = null;
                String NameFile = "InitOrbit" + szUsername + ".xml";
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
                if (xml == null) // init file is absent - use main for now
                {
                    NameFile = "InitOrbit" + "Main.xml";
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
                    SVal1 = SatCtrl.Global.Common.GetValue(xml, "ProbKeplerLine1", 0);
                    HttpContext.Current.Application["InitKeplerLine1" + szUsername] = SVal1;
                    SVal2 = SatCtrl.Global.Common.GetValue(xml, "ProbKeplerLine2", 0);
                    HttpContext.Current.Application["InitKeplerLine2" + szUsername] = SVal2;
                    SVal3 = SatCtrl.Global.Common.GetValue(xml, "ProbKeplerLine3", 0);
                    HttpContext.Current.Application["InitKeplerLine3" + szUsername] = SVal3;
                }
            }
            IsIt = HttpContext.Current.Application["InitKeplerLine1" + szUsername];
            object IsIt2 = HttpContext.Current.Application["InitKeplerLine2" + szUsername];
            object IsIt3 = HttpContext.Current.Application["InitKeplerLine3" + szUsername];
            if ((IsIt != null) && (IsIt2 != null) && (IsIt3 != null))
            {
                SVal1 = IsIt.ToString();
                SVal2 = IsIt2.ToString();
                SVal3 = IsIt3.ToString();
                TextBoxTLE1.Text = SVal2.ToString(); TextBoxTLE2.Text = SVal3.ToString();
            }
            object IsIt4 = HttpContext.Current.Application["StartDate" + szUsername];
            TextBoxTimeCalc_txt = FindImpulsesFromDate.Text.ToString();
            if (IsIt4 != null)
            {
                FindImpulsesFromDate.Text = IsIt4.ToString();
            }
            else
            {
                DateTime d = new DateTime();
                d = DateTime.UtcNow;
                String str_d_time = d.ToString("dd/MM/yy HH:mm:ss") + "." + d.Millisecond.ToString().PadLeft(3, '0');
                FindImpulsesFromDate.Text = str_d_time;
                HttpContext.Current.Application["StartDate" + szUsername] = str_d_time;
                //CheckBoxCurTime.Checked = true;
                //FindImpulsesFromDate.Enabled = false;
            }
            IsIt4 = HttpContext.Current.Application["TotalDays" + szUsername];
            TextBoxTotalDays_Text = TextBoxTotalDays.Text;
            if (IsIt4 != null)
            {
                TextBoxTotalDays.Text = IsIt4.ToString();
            }
            else
            {
                TextBoxTotalDays.Text = "1";
                HttpContext.Current.Application["TotalDays" + szUsername] = TextBoxTotalDays.Text;
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

        protected void FindImpulsesFromDate_TextChanged(object sender, EventArgs e)
        {
            HttpContext.Current.Application["StartDate" + szUsername] = TextBoxTimeCalc_txt;
            FindImpulsesFromDate.Text = TextBoxTimeCalc_txt;
        }

        protected void TextBoxTotalDays_TextChanged(object sender, EventArgs e)
        {
            HttpContext.Current.Application["TotalDays" + szUsername] = TextBoxTotalDays_Text;
            TextBoxTotalDays.Text = TextBoxTotalDays_Text;

        }
    }
}
