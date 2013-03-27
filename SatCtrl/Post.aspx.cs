using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace SatCtrl
{
    public partial class _Post : System.Web.UI.Page
    {
        public string MAX_session_no;
        protected void Page_Load(object sender, EventArgs e)
        {

            MAX_session_no = HttpContext.Current.Application["strMaxSessionN"].ToString();
            String str_session_no = Page.Request.QueryString["session_no"];
            String str_packet_type = Page.Request.QueryString["packet_type"];
            String str_packet_no = Page.Request.QueryString["packet_no"];
            String str_d_time = Page.Request.QueryString["d_time"];
            String str_g_station = Page.Request.QueryString["g_station"];
            String str_gs_time = Page.Request.QueryString["gs_time"];
            String str_package = Page.Request.QueryString["package"];
            if ((str_session_no != null) &&
                (str_packet_type != null) &&
                (str_packet_no != null) &&
                (str_d_time != null) &&
                (str_g_station != null) &&
                (str_gs_time != null) &&
                (str_package != null))
            {
                if (str_session_no != "-000000001") // ping packets does not stored
                {
                    DateTime d = new DateTime();
                    d = DateTime.UtcNow;
                    str_d_time = d.ToString("MM/dd/yy HH:mm:ss") + "." + d.Millisecond.ToString();
                    //String ConnStr = System.Configuration.ConfigurationManager.ConnectionStrings.ConnectionStrings["missionlogConnectionString"];

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

                    //tb.Text = d.ToString("MM/dd/yy HH:mm:ss") + "." + d.Millisecond.ToString();
                }
            }
        }
    }
}
