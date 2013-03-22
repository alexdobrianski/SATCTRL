<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CraftCubeSat.aspx.cs" Inherits="SatCtrl._CraftCubeSat" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <asp:TextBox ID="MsgUplink" runat="server" Height="294px" Width="491px" 
        TextMode="MultiLine"></asp:TextBox>
    
    <asp:Button ID="Button1" runat="server" 
    onclick="Button1_Click" Text="Send" />
&nbsp; 
    

</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        #UpLoadMsg
        {
            width: 667px;
            height: 449px;
        }
    </style>
</asp:Content>

