using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SatCtrl.Account
{
    public partial class RegUser : System.Web.UI.Page
    {
        private String szUserName;
        private string url;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Createuserwizard1_SendingMail(object sender, MailMessageEventArgs e)
        {
            String AppValue = null;
            szUserName = CreateUserWizard1.UserName.ToString();
            url = HttpContext.Current.Request.Url.AbsoluteUri;
            int iPageName = url.IndexOf("Register.aspx");
            if (iPageName > 0)
            {
                url = url.Substring(0, iPageName);
                url += "Login.aspx";
            }

            // Set MailMessage fields.
            e.Message.IsBodyHtml = false;
            e.Message.Subject = "New user on Web site.";
            e.Message.CC.Add("adobri@shaw.ca");
            //e.Message.To.Add(CreateUserWizard1.Email.ToString());
            Guid g;
            // Create and display the value of two GUIDs.
            g = Guid.NewGuid();
            AppValue = "UserCreation" + szUserName;
            HttpContext.Current.Application[AppValue] = g.ToString();
            // Replace placeholder text in message body with information 
            // provided by the user.
            e.Message.Body = e.Message.Body.Replace("<%URL%>", url.ToString());
            e.Message.Body = e.Message.Body.Replace("<%GUID%>", g.ToString());
            //e.Message.Body = e.Message.Body.Replace("<%PasswordAnswer%>", Createuserwizard1.Answer);
        }
    }
}