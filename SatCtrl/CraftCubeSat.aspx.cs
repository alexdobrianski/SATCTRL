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

        protected void Button1_Click(object sender, EventArgs e)
        {
            WebRequest request;
            Stream dataStream;
            intMaxSessionN = intMaxSessionN + 1;
            string strMaxSessionN = intMaxSessionN.ToString().PadLeft(10,'0');
            HttpContext.Current.Application["strMaxSessionN"] = strMaxSessionN;
            string strUpLoadMsg = MsgUplink.Text.ToString();

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
            if (ResonseString.IndexOf("Page OK") >0)
            {
                // Uplink was download to Ground Station 
                // needs to post upling data to database 
                String str_session_no = strMaxSessionN;
                String str_packet_type = "1";
                String str_packet_no = "00000";
                DateTime d = new DateTime();
                d = DateTime.UtcNow;
                String str_d_time = d.ToString("MM/dd/yy HH:mm:ss") + "." + d.Millisecond.ToString(); 

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
                            + "VALUES ('" + str_session_no + "','" + str_packet_type + "','" + str_packet_no +"','" + str_d_time + "','" + str_g_station+"','"+ str_gs_time+"','"+ str_package+ "');";

                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch(MySqlException ex)
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
}
