using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SatCtrl
{
    public partial class _SessionPost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox tb = this.FormView1.FindControl("session_noTextBox") as TextBox;

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
                tb = this.FormView1.FindControl("session_noTextBox") as TextBox;
                tb.Text = str_session_no;

                tb = this.FormView1.FindControl("packet_typeTextBox") as TextBox;
                tb.Text = str_packet_type;

                tb = this.FormView1.FindControl("packet_noTextBox") as TextBox;
                tb.Text = str_packet_no;

                tb = this.FormView1.FindControl("d_timeTextBox") as TextBox;
                tb.Text = str_d_time;

                tb = this.FormView1.FindControl("g_stationTextBox") as TextBox;
                tb.Text = str_g_station;

                tb = this.FormView1.FindControl("gs_timeTextBox") as TextBox;
                tb.Text = str_gs_time;

                tb = this.FormView1.FindControl("packageTextBox") as TextBox;
                tb.Text = str_package;
                tb = this.FormView1.FindControl("d_timeTextBox") as TextBox;
                DateTime d = new DateTime();
                d = DateTime.UtcNow;

                tb.Text = d.ToString("MM/dd/yy HH:mm:ss") + "." + d.Millisecond.ToString();


            }
            else
            {

                tb = this.FormView1.FindControl("d_timeTextBox") as TextBox;
                if (tb != null)
                {
                    DateTime d = new DateTime();
                    d = DateTime.UtcNow;

                    tb.Text = d.ToString("MM/dd/yy HH:mm:ss") + "." + d.Millisecond.ToString();




                    //                String mtime = d.Day + "/" + d.Month + "/" + d.Year + " " + d.Hour + ":" + d.Minute + ":" + d.Second + "." + d.Millisecond;
                    //               tb.Text = mtime;
                }
            }
            
        }
    }
}
