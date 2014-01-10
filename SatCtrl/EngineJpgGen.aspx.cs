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
            int iEngine =0;
            if (Param != null)
                iEngine = Convert.ToInt32(Param);
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
                    List<double> ListFireTime;
                    List<double> ListThrottle;
                    List<double> ListDeltaT;
                    List<double> ListImpulseVal;
                    String strImpNumber;
                    int iDeltaCount = 0;

                    if (iEngine >= 0 && iEngine < ListEngineImpuse.Count)
                    {
                        iEngineImpuse = iEngine;
                        strImpNumber = "ImplVal" + Convert.ToString(iEngineImpuse) + szUsername;
                        ListImpulseVal = (List<double>)HttpContext.Current.Application[strImpNumber];
                        ListWeight = (List<double>)HttpContext.Current.Application["Weight" + szUsername];
                        ListFrameWeight = (List<double>)HttpContext.Current.Application["FrameWeight" + szUsername];
                        ListEngineType = (List<double>)HttpContext.Current.Application["EngineType" + szUsername];
                        ListFireTime = (List<double>)HttpContext.Current.Application["FireTime" + szUsername];
                        ListThrottle = (List<double>)HttpContext.Current.Application["Throttle" + szUsername];
                        ListDeltaT = (List<double>)HttpContext.Current.Application["DeltaT" + szUsername];
                        iDeltaCount = (int) ListDeltaT[iEngine];
                        SolidBrush PlotBrush = new SolidBrush(Color.Blue);
                        Pen PlotPen = new Pen(PlotBrush, iBrushSize);

                        SolidBrush meshBrush = new SolidBrush(Color.Green);
                        Pen meshPen = new Pen(meshBrush, iBrushSize);
                        Font meshFont = new Font("Arial", 9);

                        SolidBrush FireBrush = new SolidBrush(Color.LightSalmon);
                        Pen FirePen = new Pen(FireBrush, iBrushSize);

                        double maxImpl = ListImpulseVal.Max();
                        double minImpl = ListImpulseVal.Min();
                        double CoefMax = maxImpl - minImpl;

                        if (CoefMax < 10.0)
                            CoefMax = 1.0;
                        else if (CoefMax < 100.0)
                            CoefMax = 10.0;
                        else if (CoefMax < 1000.0)
                            CoefMax = 100.0;
                        else if (CoefMax < 10000.0)
                            CoefMax = 1000.0;
                        else if (CoefMax < 100000.0)
                            CoefMax = 10000.0;
                        else if (CoefMax < 1000000.0)
                            CoefMax = 100000.0;
                        else if (CoefMax < 10000000.0)
                            CoefMax = 1000000.0;
                        else if (CoefMax < 100000000.0)
                            CoefMax = 10000000.0;
                        else if (CoefMax < 1000000000.0)
                            CoefMax = 100000000.0;

                        double CorrectedMax = (maxImpl - minImpl) / CoefMax;
                        double LevelMax = CorrectedMax;
                        int iLines = 2;
                        if (CorrectedMax <= 2.0)
                        {
                            LevelMax = 2.0;
                            iLines = 2;
                        }
                        else if (CorrectedMax <= 3.0)
                        {
                            LevelMax = 3.0;
                            iLines = 3;
                        }
                        else if (CorrectedMax <= 4.0)
                        {
                            LevelMax = 4.0;
                            iLines = 4;
                        }
                        else if (CorrectedMax <= 5.0)
                        {
                            LevelMax = 5.0;
                            iLines = 5;
                        }
                        else if (CorrectedMax <= 6.0)
                        {
                            LevelMax = 6.0;
                            iLines = 6;
                        }
                        else if (CorrectedMax <= 7.0)
                        {
                            LevelMax = 7.0;
                            iLines = 7;
                        }
                        else if (CorrectedMax <= 8.0)
                        {
                            LevelMax = 8.0;
                            iLines = 8;
                        }
                        else if (CorrectedMax <= 9.0)
                        {
                            LevelMax = 9.0;
                            iLines = 9;
                        }
                        else if (CorrectedMax <= 10.0)
                        {
                            LevelMax = 10.0;
                            iLines = 10;
                        }
                        String Value;
                        for (int ii = 0; ii < iLines; ii++)
                        {
                            Value = Convert.ToString((iLines-ii) * CoefMax);
                            graphicImage.DrawLine(meshPen, (float)(dLinelenX0), (float)dLinelenY0 + ii * (360 / iLines), (float)(dLinelenX), (float)(dLinelenY0 + ii * (360 / iLines)));
                            graphicImage.DrawString(Value, meshFont, meshBrush, (float)(dLinelenX+3), (float)((float)dLinelenY0 + ii * (360 / iLines)));

                        }
                        maxImpl = minImpl + LevelMax * CoefMax;

                        int iCount = ListImpulseVal.Count;
                        double ddOld = 0.0;
                        double X0; double X1; double Y0; double Y1;
                        int iter = 0;
                        //iDeltaCount = 1;
                        int isec = 0;
                        int nSeconds = 0;
                        String strSecond;
                        SizeF stringSize = new SizeF();
                        int TextPos = (int)dLinelenX0;
                        int iLiquidEngine = (int)ListEngineType[iEngineImpuse];
                        int iFireTime = (int) ListFireTime[iEngineImpuse];
                        if (iLiquidEngine >0)
                        {
                            if (iFireTime < 0 && iFireTime > 10000)
                                iFireTime = 0;
                        }
                        int iLiquidFireTime = 0;
                        foreach (double dd in ListImpulseVal)
                        {
                            if (iter > 0)
                            {
                                // drow line from ddOld to dd 
                                X0 = dLinelenX0 + 640.0 * (double)(iter + iLiquidFireTime  - 1) / (double)iCount;
                                X1 = dLinelenX0 + 640.0 * (double)(iter + iLiquidFireTime) / (double)iCount;

                                Y0 = dLinelenY0 + 360 - 360.0 * ddOld / (maxImpl - minImpl);
                                Y1 = dLinelenY0 + 360 - 360.0 * dd / (maxImpl - minImpl);
                                isec += 1;
                                if (isec >= iDeltaCount)
                                {
                                    isec = 0;
                                    graphicImage.DrawLine(meshPen, (float)(X1), (float)dLinelenY0, (float)(X1), (float)(dLinelenY));
                                    nSeconds += 1;
                                    strSecond = Convert.ToString(nSeconds);
                                    if (X1 > TextPos)
                                    {
                                        graphicImage.DrawString(strSecond, meshFont, meshBrush, (float)(X1), (float)(dLinelenY0 + dLinelenY));
                                        stringSize = graphicImage.MeasureString(strSecond, meshFont);
                                        TextPos = (int)X1 + (int)stringSize.Width * 2;
                                    }
                                    
                                }
                                if (iLiquidEngine == iter)
                                {
                                    graphicImage.DrawLine(FirePen, (float)(X1), (float)dLinelenY0, (float)(X1), (float)(dLinelenY));
                                    if (iFireTime > 0)
                                    {
                                        iLiquidFireTime += 1;
                                        iFireTime -= 1;
                                    }
                                    else
                                        iter++;

                                    
                                }
                                graphicImage.DrawLine(PlotPen, (float)(X0), (float)Y0, (float)(X1), (float)(Y1));
                                
                            }
                            ddOld = dd;
                            if (iLiquidEngine == iter)
                            {
                            }
                            else
                            {
                                iter++;
                            }
                        }
                    }
                }
            }
            //cImage.DrawLine(myPen, (float)(dLinelenX0), (float)dLinelenY0, (float)(dLinelenX0 + dLinelenX), (float)(dLinelenY0));
            graphicImage.DrawLine(myPen, (float)(dLinelenX0), (float)dLinelenY0, (float)(dLinelenX0), (float)(dLinelenY0 + dLinelenY));
            graphicImage.DrawLine(myPen, (float)(dLinelenX0), (float)dLinelenY0, (float)(dLinelenX0 + dLinelenX), (float)(dLinelenY0));

            graphicImage.DrawLine(myPen, (float)(dLinelenX0 + dLinelenX), (float)dLinelenY0, (float)(dLinelenX0 + dLinelenX), (float)(dLinelenY0 + dLinelenY));
            graphicImage.DrawLine(myPen, (float)(dLinelenX0), (float)(dLinelenY0 + dLinelenY), (float)(dLinelenX0 + dLinelenX), (float)(dLinelenY0 + dLinelenY));



            Response.ContentType = "image/jpeg";
            //Save the new image to the response output stream.
            bitMapImage.Save(Response.OutputStream, ImageFormat.Jpeg); 
        }
    }
}