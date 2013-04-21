<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="GroundStation.aspx.cs" Inherits="SatCtrl._GroundStation" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css" id="ID_COLOR_RED">
        .style1
        {
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <p>
        &nbsp;Ground Station <%=LatitudeName%>, location <%=Latitude%>, <%=Longitude%> 
        <asp:Label ID="StatusText" runat="server"><strong>&nbsp;&nbsp;&nbsp;&nbsp;  <%=GrStnStatus %></strong></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        Sky view from Ground Station Camera. </p>
    <p>
        <iframe id="I1" frameborder="0" height="350" marginheight="0" marginwidth="0" 
            name="I1" scrolling="no" 
            src="https://maps.google.ca/?ie=UTF8&amp;ll=<%=Latitude%>,<%=Longitude%>&amp;spn=0.153041,0.316887&amp;t=m&amp;z=12&amp;output=embed" 
            width="425"></iframe>
&nbsp;
        <asp:Image ID="ImagePoint" runat="server" Height="350px" 
            ImageUrl="~/Img/SDC10316.JPG" />
        <br />
        <small>
        <a href="https://maps.google.ca/?ie=UTF8&amp;ll=<%=Latitude%>,<%=Longitude%>&amp;spn=0.153041,0.316887&amp;t=m&amp;z=12&amp;source=embed" 
            style="color:#0000FF;text-align:left">View Larger Map</a></small></p>

    <p>
        Antenna Azimuth=<%=AntennaAzimuth%>(<%=CalulatedAzimuth%>), Altitude=<%=AntennaAltitude%>(<%=CalulatedAltitude%>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        </p>
    <p>
        Use as
        <asp:CheckBox ID="MainGroundStation" runat="server" 
            oncheckedchanged="CheckBox1_CheckedChanged" 
            Text="main GrStn." />
        ;&nbsp;IP address:&nbsp;
        <asp:TextBox ID="SationIpAddress" runat="server" Width="215px">http://192.168.0.1:6897/</asp:TextBox>
&nbsp;<asp:Button ID="Button2" runat="server" Text="Test GrStnConnection" 
            onclick="Button2_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="TestCallCubesat" runat="server" Text="Test Call Cubesat" 
            onclick="Button1_Click" />
    </p>
    <p>
        Uplink&nbsp;&nbsp;&nbsp; : 
        <asp:TextBox ID="TextBox1" runat="server" Height="57px" Width="810px" 
            ReadOnly="True"></asp:TextBox>
    </p>
    <p>
        Downlink:<asp:TextBox ID="TextBox2" runat="server" Height="58px" 
            Width="822px" ReadOnly="True"></asp:TextBox>
    </p>
    <p>

       <span style="display: block !important; width: 320px; text-align: center; font-family: sans-serif; font-size: 12px;">
            <a href='<%=Weather%>' title='<%=WeatherTitle%>'>
            <img src='<%=WeatherTitlepwscode%>'
             alt='<%=WeatherTitle2%>' /></a><br/>
             <a href='<%=WeatherMore%>' title="Get latest Weather Forecast updates"  style='font-family: sans-serif; font-size: 12px;'>
                 Click for weather forecast</a>
       </span>
<br />
    </p>
    <p>
    </p>
</asp:Content>
