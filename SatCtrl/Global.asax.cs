using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using MySql.Data.MySqlClient;
using System.IO;

namespace SatCtrl
{

    public class Global : System.Web.HttpApplication
    {
        
        protected String GetValue(String xml, String SearchStr, int iInstance)
        {
            String MySearch = "\"" + SearchStr + "\"";
            int FirstLine = xml.IndexOf(MySearch);
            int iCount = 0;

            if (FirstLine > 0)
            {
                do
                {
                    if (iCount == iInstance)
                    {
                        int FirstValue = xml.IndexOf("value=", FirstLine) + 7;
                        int LastValue = xml.IndexOf('\"', FirstValue) - 1;
                        return xml.Substring(FirstValue, LastValue - FirstValue + 1);
                    }
                    FirstLine = xml.IndexOf(MySearch, FirstLine+1);
                    iCount+=1;
                }
                while (FirstLine > 0);
            }
            return null;
        }

        void Application_Start(object sender, EventArgs e)
        {
            string strMaxSessionN = "0";
            long intMaxSessionN = 0;
            string strMaxPacketNumber = "0";
            long MaxPacketNumber = 0;
            String szUsername = "main";

            MySqlConnection conn =
                new MySqlConnection("server=127.0.0.1;User Id=root;password=azura2samtak;Persist Security Info=True;database=missionlog");
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("", conn);
                //cmd.CommandText = "SELECT MAX(session_no) FROM mission_session;";
                cmd.CommandText = "SELECT MAX(CONVERT(session_no,DECIMAl (10,0))) FROM mission_session;";
                rdr = cmd.ExecuteReader();
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
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("", conn);
                cmd.CommandText = "select MAX(convert(packet_no, decimal (6,0))) from mission_session where convert(session_no, decimal(10,0))="+strMaxSessionN+";";
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    strMaxPacketNumber = rdr.GetString(0);
                }
                conn.Close();
            }
            catch (MySqlException ex)
            {
                String mmm = (ex.Message);
            }
            intMaxSessionN = Convert.ToInt32(strMaxSessionN.ToString());
            MaxPacketNumber = Convert.ToInt32(strMaxPacketNumber.ToString());

            HttpContext.Current.Application["strMaxSessionN"] = strMaxSessionN;
            HttpContext.Current.Application["strPacketN"] = MaxPacketNumber;
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

            string SVal1 = null;
            string SVal2 = null;
            string SVal3 = null;
            object IsIt = null;// HttpContext.Current.Application["ProbKeplerLine1"];
            if (IsIt == null)
            {
                String xml = null;
                String NameFile = "Tra.xml";
                String MapPath = Server.MapPath(NameFile);
                int iDirAccound = MapPath.IndexOf("\\SatCtrl\\");
                if (iDirAccound > 0) // it is dir "account"
                {
                    MapPath = MapPath.Substring(0, iDirAccound );
                    MapPath += "\\SatCtrl\\\\SatCtrl\\\\"+NameFile;
                }
                try
                {
                    xml = File.ReadAllText(MapPath);
                }
                catch (Exception Exs)
                {
                    xml = null;
                }
                if (xml != null)
                {
                    SVal1 = GetValue(xml, "ProbKeplerLine1",0);
                    HttpContext.Current.Application["ProbKeplerLine1"] = SVal1;
                    SVal2 = GetValue(xml, "ProbKeplerLine2",0);
                    HttpContext.Current.Application["ProbKeplerLine2"] = SVal2;
                    SVal3 = GetValue(xml, "ProbKeplerLine3",0);
                    HttpContext.Current.Application["ProbKeplerLine3"] = SVal3;
                }
                int iInteration = 1;
                do
                {
                    SVal1 = GetValue(xml, "ProbKeplerLine1", iInteration);
                    SVal2 = GetValue(xml, "ProbKeplerLine2", iInteration);
                    SVal3 = GetValue(xml, "ProbKeplerLine3", iInteration);
                    if ((SVal1 != null) && (SVal2 != null) && (SVal3 != null))
                    {
                        HttpContext.Current.Application["GPS" + iInteration + "ProbKeplerLine1"] = SVal1;
                        HttpContext.Current.Application["GPS" + iInteration + "ProbKeplerLine2"] = SVal2;
                        HttpContext.Current.Application["GPS" + iInteration + "ProbKeplerLine3"] = SVal3;
                    }
                    else
                        break;
                    iInteration += 1;
                }
                while ((SVal1 != null) && (SVal2 != null) && (SVal3 != null));
            }
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
