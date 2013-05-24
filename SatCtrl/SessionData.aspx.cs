using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
//using System.Windows.Forms;

namespace SatCtrl
{
    
    public partial class _SessionData : System.Web.UI.Page
    {
        public long intMaxSessionN;
        protected void Page_Load(object sender, EventArgs e)
        {
            string strMaxSessionN = HttpContext.Current.Application["strMaxSessionN"].ToString();
            intMaxSessionN = Convert.ToInt32(strMaxSessionN);
        }
        /*protected void GridView1_RowDataBound(object sender,    GridViewRowEventArgs e)
        {
            System.Data.DataRowView drv;
            drv = (System.Data.DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (drv != null)
                {
                    String catName = drv[1].ToString();
                    Response.Write(catName + "/");

                    int catNameLen = catName.Length;
                    //if (catNameLen > widestData)
                    //{
                    //    widestData = catNameLen;
                    //    GridView1.Columns[2].ItemStyle.Width =
                    //      widestData * 30;
                    //    GridView1.Columns[2].ItemStyle.Wrap = false;
                    //}

                }
            }
        }*/
        protected void CustomersGridView_RowDataBound(Object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[5].Text = "<font size=\"-3\">" + e.Row.Cells[5].Text.ToString().Substring(9) + "</font>";
                String Original = e.Row.Cells[6].Text;
                byte[] BinData = new byte[e.Row.Cells[6].Text.Length*2];
                System.Buffer.BlockCopy(e.Row.Cells[6].Text.ToArray(), 0, BinData, 0, e.Row.Cells[6].Text.Length*2);

                
                String strOut = "";
                if (e.Row.Cells[1].Text.ToString() == "2") // that is from grouns station
                {
                    for (int i = 0; i < BinData.Length; i+=2)
                    {
                        if ((BinData[i] >= ' ') && (BinData[i] <= '~'))
                        {
                            //strOut += "<b>";
                            strOut += (char)BinData[i];
                            //strOut += "</b>";
                        }
                        else
                        {
                            strOut += "<font size=\"-3\"> ";
                            int Simb1 = BinData[i] >> 4;
                            int Simb2 = BinData[i] & 0x0f;
                            if (Simb1 >= 10)
                                Simb1 = 'A' + Simb1 - 10;
                            else
                                Simb1 = '0' + Simb1;
                            if (Simb2 >= 10)
                                Simb2 = 'A' + Simb2 - 10;
                            else
                                Simb2 = '0' + Simb2;
                            strOut += (char)Simb1;
                            strOut += (char)Simb2;
                            strOut += "</font>";
                        }
                    }
                    e.Row.Cells[6].Text = strOut;
                }
                if (e.Row.Cells[1].Text.ToString() == "1")
                {
                    int Simb1 = 0;
                    int Simb2 = 0;
                    int FullByte = 0;
                    for (int i = 0; i < BinData.Length; i += 2)
                    {
                        if (Simb1 < 0)
                        {
                            Simb1 = BinData[i];
                            Simb2 = -1;
                            continue;
                        }
                        if (Simb2 < 0)
                        {
                            Simb2 = BinData[i];
                            if (Simb1 <= '9')
                                FullByte = Simb1 -'0';
                            else if (Simb1 <= 'F')
                                FullByte = Simb1 - 'A' + 10;
                            else
                                FullByte = Simb1 - 'a' + 10;
                            FullByte <<= 4;
                            if (Simb2 <= '9')
                                FullByte |= Simb2 - '0';
                            else if (Simb2 <= 'F')
                                FullByte |= Simb2 - 'A' + 10;
                            else
                                FullByte |= Simb2 - 'a' + 10;

                            if ((FullByte >= ' ') && (FullByte <= '~'))
                            {
                                strOut += (char)FullByte;
                            }
                            else
                            {
                                strOut += "<font size=\"-3\"> ";
                                strOut += (char)Simb1;
                                strOut += (char)Simb2;
                                strOut += "</font>";
                            }
                            continue;
                        }
                        if (BinData[i] == '%')
                        {
                            Simb1 = -1;
                            continue;
                        }
                        else
                            strOut += (char)BinData[i];
                    }
                    e.Row.Cells[6].Text = strOut;
                }
                // TBD error packets has to be show in RED
            }

        }




    }
}
