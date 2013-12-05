using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace SatCtrl
{
    public partial class LunarMapGen : System.Web.UI.Page
    {
        double dX;
        double dY;
        protected void Page_Load(object sender, EventArgs e)
        {
            int iBrushSize = 1;
            double dLinelen = 20;
            string NameMap = "Img/lunarnearsidelarge3.jpg";
            object IsList = HttpContext.Current.Application["strPageUsed"];
            if (IsList != null)
            {
                string strMaxSessionN = HttpContext.Current.Application["strPageUsed"].ToString();
                if (strMaxSessionN == "TraCalc")
                {
                    iBrushSize = 10;
                    dLinelen = 2;
                    NameMap = "Img/lunarnearsidelarge3.jpg";
                }
            }

            Bitmap bitMapImage = new System.Drawing.Bitmap(Server.MapPath(NameMap));
            Graphics graphicImage = Graphics.FromImage(bitMapImage);
            Color MyColor = System.Drawing.Color.Red;
            SolidBrush myBrush = new SolidBrush(Color.Red);
            IsList = HttpContext.Current.Application["TargetLatitude"];
            if (IsList != null)
            {
                string strLongitude = HttpContext.Current.Application["TargetLongitude"].ToString();

                string strEW = strLongitude.Substring(0, 1);
                if (strEW == "W")
                    dX = -Convert.ToDouble(strLongitude.Substring(1));
                else
                    dX = Convert.ToDouble(strLongitude.Substring(1));
            }
            else
                dX = 15.0;

            IsList = HttpContext.Current.Application["TargetLongitude"];
            if (IsList != null)
            {
                string strLatitude = HttpContext.Current.Application["TargetLatitude"].ToString();
                string strNS = strLatitude.Substring(0, 1);
                if (strNS == "N")
                    dY = -Convert.ToDouble(strLatitude.Substring(1));
                else
                    dY = Convert.ToDouble(strLatitude.Substring(1));
            }
            else
                dY = 2.0;
            Pen myPen = new Pen(myBrush, iBrushSize);

            double dWidth = bitMapImage.Width;
            double dHeight = bitMapImage.Height;
            dX = dX * (dWidth-40.0*2)/ 360.0;
            dY = dY * (dHeight-22.0*2) / 180.0;
            dX = dX + dWidth / 2;
            dY = dY + dHeight / 2;

            graphicImage.DrawLine(myPen, (float)(dX - dLinelen * (double)iBrushSize), (float)dY, (float)(dX + dLinelen * (double)iBrushSize), (float)dY);
            graphicImage.DrawLine(myPen, (float)dX, (float)(dY - dLinelen * (double)iBrushSize), (float)dX, (float)(dY + dLinelen * (double)iBrushSize));
            Response.ContentType = "image/jpeg";
            //Save the new image to the response output stream.
            bitMapImage.Save(Response.OutputStream, ImageFormat.Jpeg); 
        }
    }
}