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
    public partial class _CraftCubeSat : System.Web.UI.Page
    {
        public class Common
        {
            public static String AddHexString(String Str2)
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
        public long intMaxSessionN;
        protected void Page_Load(object sender, EventArgs e)
        {
            string strMaxSessionN = HttpContext.Current.Application["strMaxSessionN"].ToString();
            intMaxSessionN = Convert.ToInt32(strMaxSessionN);
            
            String Str1 = "";
            String Str2 = "";
            String Str3 = "";
            String Str4 = "";
            String Str5 = "";
            object oStr1 = HttpContext.Current.Application["resp1"];
            object oStr2 = HttpContext.Current.Application["resp2"];
            object oStr3 = HttpContext.Current.Application["resp3"];
            object oStr4 = HttpContext.Current.Application["resp4"];
            object oStr5 = HttpContext.Current.Application["resp5"];

            if (oStr1 != null)
                Str1 = oStr1.ToString();
            if (oStr2 != null)
                Str2 = oStr2.ToString();
            if (oStr3 != null)
                Str3 = oStr3.ToString();
            if (oStr4 != null)
                Str4 = oStr4.ToString();
            if (oStr5 != null)
                Str5 = oStr5.ToString();
            String MainOut = "";
            if (Str1 != "")
                MainOut = Str1 + "\x0d\x0a";
            if (Str2 != "")
                MainOut = MainOut+ Str2 + "\x0d\x0a";
            if (Str3 != "")
                MainOut = MainOut + Str3 + "\x0d\x0a";
            if (Str4 != "")
                MainOut = MainOut + Str4 + "\x0d\x0a";
            if (Str5 != "")
                MainOut = MainOut + Str5 + "\x0d\x0a";
            TextBoxResponce.Text = MainOut;
            
        }
        
        public void SendToStationPostToDB(string UpLoadMsg)
        {
            DateTime CtrlDate = new DateTime();
            DateTime Timesend = new DateTime();
            DateTime TimeResp = new DateTime();
            

            WebRequest request;
            Stream dataStream;
            intMaxSessionN = intMaxSessionN + 1;
            string strMaxSessionN = intMaxSessionN.ToString().PadLeft(10, '0');
            HttpContext.Current.Application["strMaxSessionN"] = strMaxSessionN;
            if (UpLoadMsg == null)
                return;
            string strUpLoadMsg = UpLoadMsg;
            if (strUpLoadMsg.Length != 0)
            {
                strUpLoadMsg = strUpLoadMsg.Replace("=", "%3d");
                strUpLoadMsg = strUpLoadMsg.Replace("#", "%23");
                strUpLoadMsg = strUpLoadMsg.Replace("&", "%26");

                CtrlDate = DateTime.UtcNow;
                String szCtrlMils = CtrlDate.Millisecond.ToString().PadLeft(3, '0');
                String str_Ctrl_time = CtrlDate.ToString("yy/MM/dd HH:mm:ss") + "." + szCtrlMils;
                String Delta1 = "00/00/00 00:00:00.000";
                String Delta2 = "00/00/00 00:00:00.000";
                String url = HttpContext.Current.Application["StnURL"].ToString() + "cmd?session_no=" + strMaxSessionN + "&package=" + strUpLoadMsg + "&g_station=" + HttpContext.Current.Application["DefaultMainGrStn"].ToString() +
                    "&ctrlt=" + str_Ctrl_time + "&dl1=" + Delta1 + "&dl2=" + Delta2;
                /////////////////////////////////////////////////////////////////////////////////////////
                // param
                /////////////////////////////////////////////////////////////////////////////////////////
                // session_no=0000000000
                // package=xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                // 
                request = WebRequest.Create(url);
                request.Method = "GET";
                request.Timeout = 5000;
                request.ContentType = "text/html";
                HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                request.CachePolicy = noCachePolicy;

                string ResonseString = "";
                try
                {
                    Timesend = DateTime.UtcNow;
                    WebResponse resp = request.GetResponse();
                    TimeResp = DateTime.UtcNow;
                    long sizeresp = resp.ContentLength;
                    dataStream = resp.GetResponseStream();
                    byte[] byteArray = new byte[sizeresp];

                    dataStream.Read(byteArray, 0, (int)sizeresp);
                    dataStream.Close();
                    UTF8Encoding encoding = new UTF8Encoding();
                    ResonseString = encoding.GetString(byteArray);
                }
                catch (WebException)
                {
                }

                if (ResonseString.IndexOf("Page OK") > 0)
                {
                    // Uplink was download to Ground Station 
                    // needs to post upling data to database 
                    String str_session_no = strMaxSessionN;
                    String str_packet_type = "1";
                    int i_packet_no = 0;
                    String str_packet_no = i_packet_no.ToString().PadLeft(5, '0'); //"00000";
                    DateTime d = new DateTime();
                    d = DateTime.UtcNow;
                    String szMils = d.Millisecond.ToString().PadLeft(3, '0');
                    String str_d_time = d.ToString("yy/MM/dd HH:mm:ss") + "." + szMils;

                    String str_g_station = HttpContext.Current.Application["DefaultMainGrStn"].ToString();
                    String str_gs_time = str_d_time;
                    String str_package = strUpLoadMsg;
                    if ((str_session_no != null) &&
                        (str_packet_type != null) &&
                        (str_packet_no != null) &&
                        (str_d_time != null) &&
                        (str_g_station != null) &&
                        (str_gs_time != null) &&
                        (str_package != null))
                    {
                        MySqlConnection conn =
                            new MySqlConnection("server=127.0.0.1;User Id=root;password=azura2samtak;Persist Security Info=True;database=missionlog");
                        // loop to split message to a parts of 512 bytes but not on a boundary of a hex byte representation
                        int i = 0;
                        int iDelta = 512;
                        while (i < str_package.Length)
                        {
                            iDelta = 512;
                            if ((i + iDelta) < str_package.Length)
                            {
                                // that packed has to be splited - now needs to check on wich byte to split
                                // cases       |split point
                                // 1     ABCD%00... -> ABCD%00<split>....
                                // 2      ABCD%0... -> ABCD<split>%0....
                                // 3       abcd%... -> ABCD<split>%....
                                // 4       ABCD0... -> ABCD<split>0....
                                if (str_package.Substring(i + iDelta, 1) == "%") // 3
                                {
                                }
                                else if (str_package.Substring(i + iDelta - 1, 1) == "%") // 2
                                {
                                    iDelta--;
                                }
                                else if (str_package.Substring(i + iDelta - 2, 1) == "%") // 1
                                {
                                    iDelta -= 2;
                                }
                                // last case 4
                            }
                            else
                            {
                                iDelta = (str_package.Length - i);
                            }
                            String PartMsg = str_package.Substring(i, iDelta);
                            str_packet_no = i_packet_no.ToString().PadLeft(5, '0'); //"00000";
                            try
                            {
                                conn.Open();
                                MySqlCommand cmd = new MySqlCommand("", conn);
                                cmd.CommandText = "INSERT INTO mission_session (session_no, packet_type, packet_no, d_time, g_station, gs_time, package) "
                                    + "VALUES ('" + str_session_no + "','" + str_packet_type + "','" + str_packet_no + "','" + str_d_time + "','" + str_g_station + "','" + str_gs_time + "','" + PartMsg + "');";

                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                            catch (MySqlException ex)
                            {
                                String mmm = (ex.Message);
                            }
                            i_packet_no++;
                            i += iDelta;
                        }
                    }
                    MsgUplink.Text = "";
                }
                else // GrounStation hardware is not avalable
                {
                    // reset SessionNumber to prev one
                    intMaxSessionN = intMaxSessionN - 1;
                    strMaxSessionN = intMaxSessionN.ToString().PadLeft(10, '0');
                    HttpContext.Current.Application["strMaxSessionN"] = strMaxSessionN;
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string strUpLoadMsg = MsgUplink.Text.ToString();
            SendToStationPostToDB(strUpLoadMsg);
        }

        protected void CheckBoxAutoAntenna_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxAutoAntenna.Checked)
            {
                CalcAntnOrientation.Enabled = false;
                SetOrientation1.Enabled = false;
                AntQX.Enabled = false;
                AntQY.Enabled = false;
                AntQZ.Enabled = false;
                AntQN.Enabled = false;
                SetAntMove.Enabled = false;
                AntMoveQX.Enabled = false;
                AntMoveQY.Enabled = false;
                AntMoveQZ.Enabled = false;
                AntMoveQN.Enabled = false;
                AntMoveQSec.Enabled = false;
            }
            else
            {
                CalcAntnOrientation.Enabled = true;
                SetOrientation1.Enabled = true;
                AntQX.Enabled = true;
                AntQY.Enabled = true;
                AntQZ.Enabled = true;
                AntQN.Enabled = true;
                SetAntMove.Enabled = true;
                AntMoveQX.Enabled = true;
                AntMoveQY.Enabled = true;
                AntMoveQZ.Enabled = true;
                AntMoveQN.Enabled = true;
                AntMoveQSec.Enabled = true;

            }
        }

        protected void Button12_Click(object sender, EventArgs e)
        {
            MsgUplink.Text += "3atdtluna\x0d\x0a\x33" ;
        }

        protected void GetStatusViaBackup_Click(object sender, EventArgs e)
        {
            BackUpM10.Text += "1N1";
        }

        protected void GetStatus_Click(object sender, EventArgs e)
        {
            MsgUplink.Text += "3#1N#1\x33";
        }

        protected void ReadFlshComm_Click(object sender, EventArgs e)
        {
            if ((ReadFlashAddr.Text != "") && (ReadFlashLen.Text != ""))
            {
                long FlashAddr   = Int32.Parse(ReadFlashAddr.Text.ToString(), System.Globalization.NumberStyles.HexNumber);//Convert.ToInt32(ReadFlashAddr.Text.ToString());
                long FlashLength = Int32.Parse(ReadFlashLen.Text.ToString(), System.Globalization.NumberStyles.HexNumber);//Convert.ToInt32(ReadFlashLen.Text.ToString());

                
                if (FlashLength != 0)
                {
                    long StartAddr = FlashAddr;
                    long Bndr = 0;
                    long Delta = 0;
                    
                    while(StartAddr < (FlashAddr + FlashLength))
                    {
                        // how long to read: from current position till boundary of 256 bytes
                        Bndr = (StartAddr + 128) & 0xffffff80;
                        Delta = Bndr - StartAddr;
                        if ((StartAddr + Delta) < (FlashAddr + FlashLength))
                        {
                        }
                        else
                            Delta = (FlashAddr + FlashLength) - StartAddr;
 
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString("3=#5   F\x05\x03");
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(char.ConvertFromUtf32((int)(StartAddr >> 16)));
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(char.ConvertFromUtf32((int)(((StartAddr & 0xff00) >> 8))));
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(char.ConvertFromUtf32((int)((StartAddr & 0xff))));
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString("@");
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(((char)Delta).ToString());
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString("\x33");
                        StartAddr += Delta;
                    }
                }
            }
            else
            {
                ReadFlashAddr.Text = "000000";
                ReadFlashLen.Text = "000010";
            }
        }

        protected void ButtonGrStReadFlash_Click(object sender, EventArgs e)
        {
            string szUnit = TextBoxGrStnUnit.Text.ToString();

            if ((GrStReadFlashFrom.Text != "") && (GrStReadFlashLen.Text != ""))
            {
                long FlashAddr = Int32.Parse(GrStReadFlashFrom.Text.ToString(), System.Globalization.NumberStyles.HexNumber);
                long FlashLength = Int32.Parse(GrStReadFlashLen.Text.ToString(), System.Globalization.NumberStyles.HexNumber);

                if (FlashLength != 0)
                {
                    long StartAddr = FlashAddr;
                    long Bndr = 0;
                    long Delta = 0;

                    while (StartAddr < (FlashAddr + FlashLength))
                    {
                        // how long to read: from current position till boundary of 256 bytes
                        Bndr = (StartAddr + 128) & 0xffffff80;
                        Delta = Bndr - StartAddr;
                        if ((StartAddr + Delta) < (FlashAddr + FlashLength))
                        {
                        }
                        else
                            Delta = (FlashAddr + FlashLength) - StartAddr;
                        //  first command is "=9   " - responce send back to unit 9
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(szUnit + "=#9   F\x05\x03");
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(char.ConvertFromUtf32((int)(StartAddr >> 16)));
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(char.ConvertFromUtf32((int)(((StartAddr & 0xff00) >> 8))));
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(char.ConvertFromUtf32((int)((StartAddr & 0xff))));
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString("@");
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(((char)Delta).ToString());
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(szUnit);
                        StartAddr += Delta;
                    }
                }
            }
            else
            {
                GrStReadFlashFrom.Text = "000000";
                GrStReadFlashLen.Text  = "000010";
            }
        }

        protected void ButtonGrStFlasherace_Click(object sender, EventArgs e)
        {
            string szUnit = TextBoxGrStnUnit.Text.ToString();
            MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(szUnit + "=#9   F\x01\x06\x46\x01\xC7" + szUnit);
        }

        protected void ButtonUploadGrStFlash_Click(object sender, EventArgs e)
        {
            string szUnit = TextBoxGrStnUnit.Text.ToString();
            if (GrStFileUploadFlash.HasFile)
                try
                {
                    // split file for a lines:
                    Byte [] ByteAllFIle = GrStFileUploadFlash.FileBytes.ToArray();
                    String AllFile = Encoding.UTF8.GetString(ByteAllFIle);
                    string[] lines = AllFile.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    String Address = "00000000";
                    String ByteHex = "00";
                    int iByteHex;
                    int iAdr = 0;
                    long FlashAddr = 0;
                    foreach (string istr in lines)
                    {
                        Address = istr.Substring(0, 8);
                        iAdr = Int32.Parse(Address.Substring(7,1), System.Globalization.NumberStyles.HexNumber);
                        FlashAddr = Int32.Parse(Address, System.Globalization.NumberStyles.HexNumber);
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(szUnit + "=#9   F\x01\x06\x46");//
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(((char)(16 - iAdr + 4)).ToString());
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString("\x02");
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(char.ConvertFromUtf32((int)(FlashAddr >> 16)));
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(char.ConvertFromUtf32((int)(((FlashAddr & 0xff00) >> 8))));
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(char.ConvertFromUtf32((int)((FlashAddr & 0xff))));
                        for (int i = iAdr; i < 16; i++)
                        {
                            if (i < 8)
                                ByteHex = istr.Substring(11+i*3, 2);
                            else
                                ByteHex = istr.Substring(37 - 24 + i*3, 2);
                            iByteHex = Int32.Parse(ByteHex, System.Globalization.NumberStyles.HexNumber);
                            if ((iByteHex >= '0' && iByteHex <= '9') || (iByteHex == '#')) 
                                MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString("#");
                            MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(((char)iByteHex).ToString());
                        }
                        MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(szUnit);
                        //break;
                    }
                    //GrStFileUploadFlash.SaveAs("C:\\Uploads\\" +
                    //     GrStFileUploadFlash.FileName);
                    //Label1.Text = "File name: " +
                    //     FileUpload1.PostedFile.FileName + "<br>" +
                    //     FileUpload1.PostedFile.ContentLength + " kb<br>" +
                    //     "Content type: " +
                    //     FileUpload1.PostedFile.ContentType;
                }
                catch (Exception ex)
                {
                    //Label1.Text = "ERROR: " + ex.Message.ToString();
                }
            else
            {
                //Label1.Text = "You have not specified a file.";
            }

        }

        protected void ButtonSetClkGrStn_Click(object sender, EventArgs e)
        {
            // command 2tXSMHDMY2
            DateTime d = new DateTime();
            d = DateTime.UtcNow;
            String szMils = d.Millisecond.ToString().PadLeft(3, '0');
            int Mils1 = d.Millisecond/256;
            int Mils2 = d.Millisecond - Mils1*256;
            int Sec = d.Second; Sec = Sec / 10; Sec = Sec * 16 + (d.Second - Sec * 10); 
            int Min = d.Minute; Min = Min / 10; Min = Min * 16 + (d.Minute - Min * 10);
            int Hour = d.Hour; Hour = Hour / 10; Hour = Hour * 16 + (d.Hour - Hour * 10);
            int Day = d.Day; Day = Day / 10; Day = Day * 16 + (d.Day - Day * 10);
            int Month = d.Month; Month = Month / 10; Month = Month * 16 + (d.Month - Month * 10);
            int Year = d.Year - 2000; Year = Year / 10; Year = Year * 16 + (d.Year - 2000 - Year * 10);
            string message = "222t" + SatCtrl._CraftCubeSat.Common.AddHexString(((char)(Mils1)).ToString());
            message += SatCtrl._CraftCubeSat.Common.AddHexString(((char)(Mils2)).ToString());
            message += SatCtrl._CraftCubeSat.Common.AddHexString(((char)(Sec)).ToString());
            message += SatCtrl._CraftCubeSat.Common.AddHexString(((char)(Min)).ToString());
            message += SatCtrl._CraftCubeSat.Common.AddHexString(((char)(Hour)).ToString());
            message += SatCtrl._CraftCubeSat.Common.AddHexString(((char)(Day)).ToString());
            message += SatCtrl._CraftCubeSat.Common.AddHexString(((char)(Month)).ToString());
            message += SatCtrl._CraftCubeSat.Common.AddHexString(((char)(Year)).ToString());
            message += "2";
            SendToStationPostToDB(message);
        }

        protected void ButtonGrStnGetClock_Click(object sender, EventArgs e)
        {
            SendToStationPostToDB("222=#9 T2");
        }

        protected void ButtonATDTL_Click(object sender, EventArgs e)
        {
            string szUnit = TextBoxGrStnUnit.Text.ToString();
            MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(szUnit + "atdtl\x0d\x0a" + szUnit);
        }

        protected void ButtonATD_Click(object sender, EventArgs e)
        {
            string szUnit = TextBoxGrStnUnit.Text.ToString();
            MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(szUnit + "ATD" + szUnit);
        }

        protected void ButtonATP_Click(object sender, EventArgs e)
        {
            string szUnit = TextBoxGrStnUnit.Text.ToString();
            MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(szUnit + "ATP" + szUnit);
        }

        protected void ButtonATC_Click(object sender, EventArgs e)
        {
            string szUnit = TextBoxGrStnUnit.Text.ToString();
            MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(szUnit + "ATC" + szUnit);
        }

        protected void ButtonATX_Click(object sender, EventArgs e)
        {
            string szUnit = TextBoxGrStnUnit.Text.ToString();
            MsgUplink.Text += SatCtrl._CraftCubeSat.Common.AddHexString(szUnit + "ATX" + szUnit);
        }


        /*
        protected void ButtonUploadGrStFlash_Click(object sender, EventArgs e)
        {
            if (GrStFileUploadFlash.HasFile)
                try
                {
                    GrStFileUploadFlash.SaveAs("C:\\Uploads\\" +
                         GrStFileUploadFlash.FileName);
                    //Label1.Text = "File name: " +
                    //     FileUpload1.PostedFile.FileName + "<br>" +
                    //     FileUpload1.PostedFile.ContentLength + " kb<br>" +
                    //     "Content type: " +
                    //     FileUpload1.PostedFile.ContentType;
                }
                catch (Exception ex)
                {
                    //Label1.Text = "ERROR: " + ex.Message.ToString();
                }
            else
            {
                //Label1.Text = "You have not specified a file.";
            }
        }*/
    }
}
