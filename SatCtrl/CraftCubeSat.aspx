<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CraftCubeSat.aspx.cs" Inherits="SatCtrl._CraftCubeSat" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <input id="UpLoadMsg" type="text" /><input id="SendToCubeSat" type="submit" 
        value="SendToCubeSat" />
    </asp:Content>
