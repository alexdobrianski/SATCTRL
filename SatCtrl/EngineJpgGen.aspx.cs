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
    public partial class EngineJpgGen : System.Web.UI.Page
    {
        double dX;
        double dY;
        String szUsername = "Main";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page.User.Identity.IsAuthenticated)
            {
                szUsername = Page.User.Identity.Name.ToString();
            }
            String Param = Request.QueryString["n"];
            int iEngine =1;
            if (Param != null)
                iEngine = Convert.ToInt32(Param);
            iEngine = iEngine - 1;
            int iBrushSize = 1;
            double dLinelenX = 640;
            double dLinelenX0 = 5;
            double dLinelenY = 360;
            double dLinelenY0 = 5;
            string NameMap = "Img/engineblank.jpg";

            Bitmap bitMapImage = new System.Drawing.Bitmap(Server.MapPath(NameMap));
            Graphics graphicImage = Graphics.FromImage(bitMapImage);
            Color MyColor = System.Drawing.Color.Red;
            SolidBrush myBrush = new SolidBrush(Color.Red);
            Pen myPen = new Pen(myBrush, iBrushSize);




            double dWidth = bitMapImage.Width;
            double dHeight = bitMapImage.Height;

            //cImage.DrawLine(myPen, (float)(dLinelenX0), (float)dLinelenY0, (float)(dLinelenX0 + dLinelenX), (float)(dLinelenY0));
            graphicImage.DrawLine(myPen, (float)(dLinelenX0), (float)dLinelenY0, (float)(dLinelenX0), (float)(dLinelenY0 + dLinelenY));
            graphicImage.DrawLine(myPen, (float)(dLinelenX0), (float)dLinelenY0, (float)(dLinelenX0 + dLinelenX), (float)(dLinelenY0));

            graphicImage.DrawLine(myPen, (float)(dLinelenX0 + dLinelenX), (float)dLinelenY0, (float)(dLinelenX0 + dLinelenX), (float)(dLinelenY0 + dLinelenY));
            graphicImage.DrawLine(myPen, (float)(dLinelenX0), (float)(dLinelenY0 + dLinelenY), (float)(dLinelenX0 + dLinelenX), (float)(dLinelenY0 + dLinelenY));

            List<double> ListEngineImpuse;

            int iEngineImpuse = 0;
            ListEngineImpuse = (List<double>)HttpContext.Current.Application["EngineImpuse" + szUsername];
            if (ListEngineImpuse != null)
            {
                if (ListEngineImpuse.Count > 0)
                {
                    List<double> ListWeight;
                    List<double> ListFrameWeight;
                    List<double> ListEngineType;
                    List<double> ListThrottle;
                    List<double> ListDeltaT;
                    List<double> ListImpulseVal;
                    String strImpNumber;

                    if (iEngine >= 0 && iEngine < ListEngineImpuse.Count)
                    {
                        iEngineImpuse = iEngine;
                        strImpNumber = "ImplVal" + Convert.ToString(iEngineImpuse) + szUsername;
                        ListImpulseVal = (List<double>)HttpContext.Current.Application[strImpNumber];
                        ListWeight = (List<double>)HttpContext.Current.Application["Weight" + szUsername];
                        ListFrameWeight = (List<double>)HttpContext.Current.Application["FrameWeight" + szUsername];
                        ListEngineType = (List<double>)HttpContext.Current.Application["EngineType" + szUsername];
                        ListThrottle = (List<double>)HttpContext.Current.Application["Throttle" + szUsername];
                        ListDeltaT = (List<double>)HttpContext.Current.Application["DeltaT" + szUsername];
                        SolidBrush PlotBrush = new SolidBrush(Color.Blue);

                        Pen PlotPen = new Pen(PlotBrush, iBrushSize);
                        double maxImpl = ListImpulseVal.Max();
                        double minImpl = ListImpulseVal.Min();
                        int iCount = ListImpulseVal.Count;
                        double ddOld = 0.0;
                        double X0; double X1; double Y0; double Y1;
                        int iter = 0;
                        foreach (double dd in ListImpulseVal)
                        {
                            if (iter > 0)
                            {
                                // drow line from ddOld to dd 
                                X0 = dLinelenX0 + 640.0 * (double)(iter - 1) / (double)iCount;
                                X1 = dLinelenX0 + 640.0 * (double)(iter) / (double)iCount;

                                Y0 = dLinelenY0 + 360 - 360.0 * ddOld / (maxImpl - minImpl);
                                Y1 = dLinelenY0 + 360 - 360.0 * dd / (maxImpl - minImpl);
                                graphicImage.DrawLine(PlotPen, (float)(X0), (float)Y0, (float)(X1), (float)(Y1));
                            }
                            ddOld = dd;
                            iter++;
                        }
                    }
                }
            }


            Response.ContentType = "image/jpeg";
            //Save the new image to the response output stream.
            bitMapImage.Save(Response.OutputStream, ImageFormat.Jpeg); 
        }
    }
}