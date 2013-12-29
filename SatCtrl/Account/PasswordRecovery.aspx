<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PasswordRecovery.aspx.cs" Inherits="SatCtrl.Account.PasswordRecovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
    <span style="color: rgb(42, 42, 42); font-family: 'Segoe UI', 'Lucida Grande', Verdana, Arial, Helvetica, sans-serif; font-size: 13px; font-style: normal; font-variant: normal; font-weight: bold; letter-spacing: normal; line-height: 18px; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;">
    Forgot Password</span></h1>
<p>
    <asp:PasswordRecovery ID="PasswordRecovery1" runat="server">
        <MailDefinition CC="adobri@shaw.ca" From="adobri@shaw.ca"
        BodyFileName="~/Account/userPassrecovery.txt" 
            Subject="reset password">
        </MailDefinition>
    </asp:PasswordRecovery>
</p>
</asp:Content>
