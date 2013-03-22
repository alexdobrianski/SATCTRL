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
            
            string strMaxSessionN = "0";
            if (HttpContext.Current.Session["MaxSessionN"] != null)
            {
                strMaxSessionN = HttpContext.Current.Session["MaxSessionN"].ToString();
            }
            else
            {
                MySqlConnection conn =
                    new MySqlConnection("server=127.0.0.1;User Id=root;password=azura2samtak;Persist Security Info=True;database=missionlog");
                MySqlDataReader rdr = null;
                try
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("", conn);

                    cmd.CommandText = "SELECT MAX(session_no) FROM mission_session;";
                    rdr = cmd.ExecuteReader();

                    //cmd.ExecuteNonQuery();
                    while (rdr.Read())
                    {
                        strMaxSessionN = rdr.GetString(0);
                    }


                    conn.Close();
                    HttpContext.Current.Session["MaxSessionN"] = strMaxSessionN;
                }
                catch (MySqlException ex)
                {
                    String mmm = (ex.Message);
                }
            }
            intMaxSessionN = Convert.ToInt32(strMaxSessionN);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            WebRequest request;
            Stream dataStream;
            intMaxSessionN = intMaxSessionN + 1;
            string strMaxSessionN = intMaxSessionN.ToString().PadLeft(10,'0');
            HttpContext.Current.Session["MaxSessionN"] = strMaxSessionN;
            string strUpLoadMsg = MsgUplink.Text.ToString();

            //String url = "http://www.adobri.com";
            //request = WebRequest.Create("http://localhost:3159/Post.aspx?session_no=000000003&packet_type=2&packet_no=00000&d_time=03/15/13 07:00:55.734&g_station=3&gs_time=03/15/13 07:00:55.734&package=qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq");
            String url = "http://localhost:6921/cmd?session_no="+strMaxSessionN + "&package="+strUpLoadMsg;
            //request = HttpWebRequest.Create("http://localhost:6921/cmd?session_no=000000003&packet_type=2&packet_no=00000&d_time=03/15/1307:00:55.734&g_station=3&gs_time=03/15/1307:00:55.734&package=qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq");
            /////////////////////////////////////////////////////////////////////////////////////////
            // param
            /////////////////////////////////////////////////////////////////////////////////////////
            // session_no=0000000000
            // package=xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
            // 
            request = WebRequest.Create(url);
            request.Method = "GET";
            //string postData = "cmd?session_no=000000003&packet_type=2&packet_no=00000&d_time=03/15/1307:00:55.734&g_station=3&gs_time=03/15/1307:00:55.734&package=qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq";
            //byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            //request.ContentType = "application/x-www-form-urlencoded";
            request.ContentType = "text/html";

            // Set the ContentLength property of the WebRequest.
            //request.ContentLength = byteArray.Length;

            // Get the request stream.

            //dataStream = request.GetRequestStream();

            // Write the data to the request stream.
            //dataStream.Write(byteArray, 0, byteArray.Length);



            // Close the Stream object.
            //dataStream.Close();
            string ResonseString = "";
            //try
            //{
                WebResponse resp = request.GetResponse();
                long sizeresp = resp.ContentLength;
                dataStream = resp.GetResponseStream();
                byte[] byteArray = new byte[sizeresp];

                dataStream.Read(byteArray, 0, (int)sizeresp);
                dataStream.Close();
                ResonseString = Convert.ToString(byteArray);
            //}
            //catch()
            //{
            //}
            if (ResonseString.IndexOf("Page OK") != null)
            {
                // Uplink was download to Ground Station 
                // needs to post upling data to database 
                String str_session_no = strMaxSessionN;
                String str_packet_type = "1";
                String str_packet_no = "00000";
                DateTime d = new DateTime();
                d = DateTime.UtcNow;
                String str_d_time = d.ToString("MM/dd/yy HH:mm:ss") + "." + d.Millisecond.ToString(); 

                String str_g_station = "V";
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
                HttpContext.Current.Session["MaxSessionN"] = strMaxSessionN;
            }
        }
    }
}
