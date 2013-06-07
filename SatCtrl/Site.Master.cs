using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SatCtrl
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        //String tb;
        public String MissionYear;
        public String MissionMonth;
        public String MissionDay;
        public String MissionHour;
        public String MissionMin;
        public String MissionSec;
        public String MissionMls;
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime d = new DateTime();
            d = DateTime.UtcNow;

            //DateAndTime.Text = d.ToString("yy/MM/dd HH:mm:ss") +"." + d.Millisecond.ToString();
            string strDefaultMainGrStn = HttpContext.Current.Application["DefaultMainGrStn"].ToString();
            string GrStName = "";
            if (strDefaultMainGrStn == "1")
                GrStName = "Vancouver";
            DateAndTime.Text = "Ground station "+ GrStName+ " - ";
            MissionYear = d.ToString("yy");
            MissionMonth = d.ToString("MM");
            MissionDay = d.ToString("dd");
            MissionHour = d.ToString("HH");
            MissionMin = d.ToString("mm");
            MissionSec = d.ToString("ss");
            MissionMls = d.Millisecond.ToString();
        }
    }
}
