﻿<%@ Page Title="Simulation Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Simulation.aspx.cs" Inherits="SatCtrl._Simulation" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <p>
        <a href="http://java3d.java.net/binary-builds.html">Requare Java 3D - 
        click to install</a><br />
    <APPLET ARCHIVE="/SatCtrl/visualtra.jar" CODE=visualtra.class WIDTH=1280 HEIGHT=800> </APPLET>

</p>
</asp:Content>
