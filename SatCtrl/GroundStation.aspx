<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="GroundStation.aspx.cs" Inherits="SatCtrl._GroundStation" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <p>
        &nbsp;Ground station <%=LatitudeName%>, location <%=Latitude%>, <%=Longitude%> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        Sky view from Ground Station Camera. </p>
    <p>
        <iframe id="I1" frameborder="0" height="350" marginheight="0" marginwidth="0" 
            name="I1" scrolling="no" 
            src="https://maps.google.ca/?ie=UTF8&amp;ll=<%=Latitude%>,<%=Longitude%>&amp;spn=0.153041,0.316887&amp;t=m&amp;z=12&amp;output=embed" 
            width="425"></iframe>
&nbsp;
        <asp:Image ID="Image1" runat="server" Height="350px" 
            ImageUrl="~/Img/SDC10316.JPG" />
        <br />
        <small>
        <a href="https://maps.google.ca/?ie=UTF8&amp;ll=<%=Latitude%>,<%=Longitude%>&amp;spn=0.153041,0.316887&amp;t=m&amp;z=12&amp;source=embed" 
            style="color:#0000FF;text-align:left">View Larger Map</a></small></p>
    <p>
        Antenna position Azimuth=???, Altitude=???&nbsp; URL address:&nbsp;
        <asp:TextBox ID="TextBox3" runat="server" Width="215px">http://192.168.0.1:6897/</asp:TextBox>
&nbsp;(make port 6897 visible from internet)</p>
    <p>
        <asp:CheckBox ID="CheckBox1" runat="server" 
            oncheckedchanged="CheckBox1_CheckedChanged" Text="Enable Main Communication." />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Text="Call Cubesat" />
    </p>
    <p>
        Uplink&nbsp;&nbsp;&nbsp; : 
        <asp:TextBox ID="TextBox1" runat="server" Height="57px" Width="810px"></asp:TextBox>
    </p>
    <p>
        Downlink:<asp:TextBox ID="TextBox2" runat="server" Height="58px" Width="822px"></asp:TextBox>
    </p>
    <p>
        Weather at Ground station location
        <span style="display: block !important; width: 320px; text-align: center; font-family: sans-serif; font-size: 12px;">
        <a href="<%=Weather%>" 
            target="_blank" title="<%=WeatherTitle%>"">
        <img alt="<%=WeatherTitle2%>" 
            src="<%=WeatherTitlepwscode%>"
            width="300" /></a><br />
        <a href="<%=Weather%>"
            style="font-family: sans-serif; font-size: 12px" target="_blank" 
            title="Get latest Weather Forecast updates">Click for weather forecast</a></span><br />
    </p>
    <p>
    </p>
</asp:Content>
