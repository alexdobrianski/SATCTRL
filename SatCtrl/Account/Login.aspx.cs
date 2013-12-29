using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SatCtrl.Account
{
    public partial class Login : System.Web.UI.Page
    {
        String szUserName = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            String UrlRq = Request.QueryString.ToString();
            int IndexUsername = UrlRq.IndexOf("Username=");
            if (IndexUsername >= 0)
            {
                szUserName = UrlRq.Substring(IndexUsername + 9);
                if (szUserName != null)
                {
                    int IndexAmp = szUserName.IndexOf('&');
                    if (IndexAmp > 0)
                    {
                        szUserName = szUserName.Substring(0, IndexAmp);
                        LoginUser.UserName = szUserName;
                    }
                }
            }
            //RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            RegisterHyperLink.NavigateUrl = "Register.aspx";
            Recovery.NavigateUrl = "PasswordRecovery.aspx";
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            String UrlRq = Request.QueryString.ToString();
            int IndexUsername = UrlRq.IndexOf("Username=");
            if (IndexUsername >= 0)
            {
                szUserName = UrlRq.Substring(IndexUsername + 9);
                if (szUserName != null)
                {
                    int IndexAmp = szUserName.IndexOf('&');
                    if (IndexAmp > 0)
                    {
                        szUserName = szUserName.Substring(0, IndexAmp);
                        
                    }
                }
            }
            else
                szUserName = LoginUser.UserName.ToString();

            String AppValue = "UserCreation" + szUserName;
            object IsIt = HttpContext.Current.Application[AppValue];
            if (IsIt != null) // present verification data
            {
                String szGUIDData = IsIt.ToString();
                if (szGUIDData == "Done") // verification was done
                {
                }
                else  // the case when verification was not done ( only case not resolved for now == restart of the web setrvice
                // in that case old GUID must be soterd outside of the web service and reloaded in the case of restart)
                {
                    {
                        object IsItUserCreation = HttpContext.Current.Application["UserCreation" + szUserName];
                        if (IsItUserCreation != null)
                        {
                            String szGUIDVal = IsItUserCreation.ToString();
                            int IndexGUID = UrlRq.IndexOf("GUID=");
                            if (IndexGUID >= 0)
                            {
                                String szUserEnteredGUID = UrlRq.Substring(IndexGUID + 5);
                                if (szUserEnteredGUID != null)
                                {
                                    int IndexAmp2 = szUserEnteredGUID.IndexOf('&');
                                    if (IndexAmp2 > 0)
                                    {
                                        szUserEnteredGUID = szUserEnteredGUID.Substring(0, IndexAmp2);
                                    }
                                    if (szUserEnteredGUID == szGUIDVal)
                                    {
                                        //LoginUser.UserName = szUserName;
                                        HttpContext.Current.Application["UserCreation" + szUserName] = "Done";
                                    }
                                    else
                                    {
                                        //LoginUser.Visible = false;
                                        LoginUser.UserName = "un confirmed";
                                    }
                                }
                                else
                                    LoginUser.UserName = "un confirmed user";
                            }
                            else
                                LoginUser.UserName = "un confirmed user";
                        }
                        else
                            LoginUser.UserName = "un confirmed user";
                    }
                }
            }
            else // no verification data
            {

            }

            //LoginUser.UserName = "un confirmed";
        }
    }
}
