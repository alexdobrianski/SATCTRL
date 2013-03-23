using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using MySql.Data.MySqlClient;

namespace SatCtrl
{

    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            string strMaxSessionN = "0";
            long intMaxSessionN;

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
                
            }
            catch (MySqlException ex)
            {
                String mmm = (ex.Message);
            }
            intMaxSessionN = Convert.ToInt32(strMaxSessionN.ToString());
            HttpContext.Current.Application["strMaxSessionN"] = strMaxSessionN;
            HttpContext.Current.Application["DefaultMainGrStn"] = System.Configuration.ConfigurationManager.AppSettings["DefaultMainGrStn"];
            

            HttpContext.Current.Application["Stn1URL"] = System.Configuration.ConfigurationManager.AppSettings["Stn1URL"];
            HttpContext.Current.Application["Stn2URL"] = System.Configuration.ConfigurationManager.AppSettings["Stn2URL"];
            HttpContext.Current.Application["Stn3URL"] = System.Configuration.ConfigurationManager.AppSettings["Stn3URL"];
            HttpContext.Current.Application["Stn4URL"] = System.Configuration.ConfigurationManager.AppSettings["Stn4URL"];
            HttpContext.Current.Application["Stn5URL"] = System.Configuration.ConfigurationManager.AppSettings["Stn5URL"];
            HttpContext.Current.Application["Stn6URL"] = System.Configuration.ConfigurationManager.AppSettings["Stn6URL"];
            HttpContext.Current.Application["Stn7URL"] = System.Configuration.ConfigurationManager.AppSettings["Stn7URL"];
            HttpContext.Current.Application["Stn8URL"] = System.Configuration.ConfigurationManager.AppSettings["Stn8URL"];
            HttpContext.Current.Application["Stn9URL"] = System.Configuration.ConfigurationManager.AppSettings["Stn9URL"];
            String StationN = HttpContext.Current.Application["DefaultMainGrStn"].ToString();
            if (StationN == "1")
                HttpContext.Current.Application["StnURL"] = HttpContext.Current.Application["Stn1URL"];
            else if (StationN == "2")
                HttpContext.Current.Application["StnURL"] = HttpContext.Current.Application["Stn2URL"];
            else if (StationN == "3")
                HttpContext.Current.Application["StnURL"] = HttpContext.Current.Application["Stn3URL"];
            else if (StationN == "4")
                HttpContext.Current.Application["StnURL"] = HttpContext.Current.Application["Stn4URL"];
            else if (StationN == "5")
                HttpContext.Current.Application["StnURL"] = HttpContext.Current.Application["Stn5URL"];
            else if (StationN == "6")
                HttpContext.Current.Application["StnURL"] = HttpContext.Current.Application["Stn6URL"];
            else if (StationN == "7")
                HttpContext.Current.Application["StnURL"] = HttpContext.Current.Application["Stn7URL"];
            else if (StationN == "8")
                HttpContext.Current.Application["StnURL"] = HttpContext.Current.Application["Stn8URL"];
            else if (StationN == "9")
                HttpContext.Current.Application["StnURL"] = HttpContext.Current.Application["Stn9URL"];
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
