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
        public string MAX_packet_no;
        protected void Page_Load(object sender, EventArgs e)
        {

            MAX_session_no = HttpContext.Current.Application["strMaxSessionN"].ToString();
            MAX_packet_no = HttpContext.Current.Application["strPacketN"].ToString();
            String str_session_no = Page.Request.QueryString["session_no"];
            String str_packet_type = Page.Request.QueryString["packet_type"];
            String str_packet_no = Page.Request.QueryString["packet_no"];
            String str_d_time = Page.Request.QueryString["d_time"];
            String str_g_station = Page.Request.QueryString["g_station"];
            String str_gs_time = Page.Request.QueryString["gs_time"];
            String str_package = null;// Page.Request.QueryString["package"];
            String str_originalUrl = Page.Request.RawUrl.ToString();
            String shorttime = "";
            int iPackPosition = str_originalUrl.IndexOf("package=");
            if (iPackPosition > 0)
            {
                str_package = str_originalUrl.Substring(iPackPosition + 8);
                // manual convert
                byte[] BinData = new byte[str_package.Length*2];
                System.Buffer.BlockCopy(str_package.ToArray(), 0, BinData, 0, str_package.Length*2);
                str_package = "";
                int Simb1 = 0;
                int Simb2 = 0;
                for (int i = 0; i < BinData.Length; i+=2)
                {
                    int Orig = BinData[i];
                    if (Simb1 < 0)
                    {
                        Simb1 = Orig;
                        Simb2 = -1;
                        continue;
                    }
                    if (Simb2 < 0)
                    {
                        Simb2 = Orig;
                        if (Simb1 <= '9')
                            Orig = Simb1 - '0';
                        else if (Simb1 <= 'F')
                            Orig = Simb1 - 'A' + 10;
                        else
                            Orig = Simb1 - 'a' + 10;
                        Orig <<= 4;
                        if (Simb2 <= '9')
                            Orig |= Simb2 - '0';
                        else if (Simb2 <= 'F')
                            Orig |= Simb2 - 'A' + 10;
                        else
                            Orig |= Simb2 - 'a' + 10;
                        switch (Orig)
                        {
                        case '>': str_package += "&gt;"; break;
                        case '\'': str_package += "&#39;"; break;
                        case '\"': str_package += "&#34;"; break;
                        case '&': str_package += "&amp;"; break;
                        default: str_package += (char)Orig;  break;
                        }
                        continue;
                    }
                    if (Orig != '%')
                        str_package += (char)Orig;
                    else
                        Simb1 = -1;
                }
            }
            if ((str_session_no != null) &&
                (str_packet_type != null) &&
                (str_packet_no != null) &&
                (str_d_time != null) &&
                (str_g_station != null) &&
                (str_gs_time != null) &&
                (str_package != null))
            {
                if (str_packet_no != "-0001")
                {
                    HttpContext.Current.Application["strPacketN"] = str_packet_no;
                }
                if (str_session_no != "-000000001") // ping packets does not stored
                {
                    
                    DateTime d = new DateTime();
                    d = DateTime.UtcNow;
                    str_d_time = d.ToString("yy/MM/dd HH:mm:ss") + "." + d.Millisecond.ToString().PadLeft(3, '0');
                    shorttime = d.ToString("HH:mm:ss");
                    //String ConnStr = System.Configuration.ConfigurationManager.ConnectionStrings.ConnectionStrings["missionlogConnectionString"];

                    //str_package = str_package.Substring(1);
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
                // store responce for visualization
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
                Str1 = Str2;
                Str2 = Str3;
                Str3 = Str4;
                Str4 = Str5;
                Str5 = shorttime+ ":>"+str_package;
                HttpContext.Current.Application["resp1"] = Str1;
                HttpContext.Current.Application["resp2"] = Str2;
                HttpContext.Current.Application["resp3"] = Str3;
                HttpContext.Current.Application["resp4"] = Str4;
                HttpContext.Current.Application["resp5"] = Str5;

            }
        }
    }
}
