using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace SatCtrl
{
    
    public partial class _SessionData : System.Web.UI.Page
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
                catch(MySqlException ex)
                {
                    String mmm = (ex.Message);
                }
            }
            intMaxSessionN = Convert.ToInt32(strMaxSessionN);
        }
    }
}
