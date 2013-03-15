using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Text;

namespace SatCtrl
{
    public partial class _CraftCubeSat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //WebRequest request;
            //Stream dataStream;
            //String url = "http://www.adobri.com";
            //request = WebRequest.Create("http://localhost:3159/Post.aspx?session_no=000000003&packet_type=2&packet_no=00000&d_time=03/15/13 07:00:55.734&g_station=3&gs_time=03/15/13 07:00:55.734&package=qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq");
            //request = WebRequest.Create(url);
            //request.Method = "GET";
            //string postData = "";
            //byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            //request.ContentType = "application/x-www-form-urlencoded";

            // Set the ContentLength property of the WebRequest.
            //request.ContentLength = byteArray.Length;

            // Get the request stream.
            
            //dataStream = request.GetRequestStream();

            // Write the data to the request stream.
            //dataStream.Write(byteArray, 0, byteArray.Length);

            // Close the Stream object.
            //dataStream.Close();
        }
    }
}
