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

namespace SatCtrl
{
    public partial class _CraftCubeSat : System.Web.UI.Page
    {
        public long intMaxSessionN;
        protected void Page_Load(object sender, EventArgs e)
        {
            string strMaxSessionN = HttpContext.Current.Application["strMaxSessionN"].ToString();
            intMaxSessionN = Convert.ToInt32(strMaxSessionN);
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            WebRequest request;
            Stream dataStream;
            intMaxSessionN = intMaxSessionN + 1;
            string strMaxSessionN = intMaxSessionN.ToString().PadLeft(10, '0');
            HttpContext.Current.Application["strMaxSessionN"] = strMaxSessionN;
            string strUpLoadMsg = MsgUplink.Text.ToString();
            if (strUpLoadMsg.Length != 0)
            {
                strUpLoadMsg = strUpLoadMsg.Replace("=", "%3d");
                strUpLoadMsg = strUpLoadMsg.Replace("#", "%23");
                strUpLoadMsg = strUpLoadMsg.Replace("&", "%26");

                String url = HttpContext.Current.Application["StnURL"].ToString() + "cmd?session_no=" + strMaxSessionN + "&package=" + strUpLoadMsg + "&g_station=" + HttpContext.Current.Application["DefaultMainGrStn"].ToString();
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
                string ResonseString = "";
                try
                {
                    WebResponse resp = request.GetResponse();
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
                    String str_packet_no = "00000";
                    DateTime d = new DateTime();
                    d = DateTime.UtcNow;
                    String szMils = d.Millisecond.ToString();
                    if (szMils.Length == 1)
                        szMils = "00" + szMils;
                    else if (szMils.Length == 2)
                        szMils = "0" + szMils;
                    String str_d_time = d.ToString("MM/dd/yy HH:mm:ss") + "." + szMils;

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

                        try
                        {
                            conn.Open();
                            MySqlCommand cmd = new MySqlCommand("", conn);
                            cmd.CommandText = "INSERT INTO mission_session (session_no, packet_type, packet_no, d_time, g_station, gs_time, package) "
                                + "VALUES ('" + str_session_no + "','" + str_packet_type + "','" + str_packet_no + "','" + str_d_time + "','" + str_g_station + "','" + str_gs_time + "','" + str_package + "');";

                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                        catch (MySqlException ex)
                        {
                            String mmm = (ex.Message);
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
                    for (long StartAddr = FlashAddr; StartAddr < (FlashAddr + FlashLength); StartAddr += 16)
                    {
                        MsgUplink.Text += AddHexString("3=#5   F\x05\x03");
                        MsgUplink.Text += AddHexString(char.ConvertFromUtf32((int)(FlashAddr >> 16)));
                        MsgUplink.Text += AddHexString(char.ConvertFromUtf32((int)(((FlashAddr & 0xff00) >> 8))));
                        MsgUplink.Text += AddHexString(char.ConvertFromUtf32((int)((FlashAddr & 0xff))));
                        MsgUplink.Text += AddHexString("@\x10\x33");
                    }
                }
            }
            else
            {
                ReadFlashAddr.Text = "0000";
                ReadFlashLen.Text = "0010";
            }
        }

        protected void ButtonGrStReadFlash_Click(object sender, EventArgs e)
        {
            if ((GrStReadFlashFrom.Text != "") && (GrStReadFlashLen.Text != ""))
            {
                long FlashAddr = Int32.Parse(GrStReadFlashFrom.Text.ToString(), System.Globalization.NumberStyles.HexNumber);//Convert.ToInt32(ReadFlashAddr.Text.ToString());
                long FlashLength = Int32.Parse(GrStReadFlashLen.Text.ToString(), System.Globalization.NumberStyles.HexNumber);//Convert.ToInt32(ReadFlashLen.Text.ToString());

                if (FlashLength != 0)
                {
                    for (long StartAddr = FlashAddr; StartAddr < (FlashAddr + FlashLength); StartAddr += 16)
                    {
                        //  first command is "=9   " - responce send back to unit 9
                        MsgUplink.Text += AddHexString("2=#9   F\x05\x03");
                        MsgUplink.Text += AddHexString(char.ConvertFromUtf32((int)(FlashAddr >> 16)));
                        MsgUplink.Text += AddHexString(char.ConvertFromUtf32((int)(((FlashAddr & 0xff00) >> 8))));
                        MsgUplink.Text += AddHexString(char.ConvertFromUtf32((int)((FlashAddr & 0xff))));
                        MsgUplink.Text += AddHexString("@\x10\x32");
                    }
                }
            }
            else
            {
                GrStReadFlashFrom.Text = "0000";
                GrStReadFlashLen.Text  = "0010";
            }
        }
    }
}
