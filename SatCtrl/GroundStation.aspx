<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="GroundStation.aspx.cs" Inherits="SatCtrl._GroundStation" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <p>
        Ground station location XXX, YYY</p>
    <p>
        <iframe id="I1" frameborder="0" height="350" marginheight="0" marginwidth="0" 
            name="I1" scrolling="no" 
            src="https://maps.google.ca/?ie=UTF8&amp;ll=49.257735,-123.123904&amp;spn=0.153041,0.316887&amp;t=m&amp;z=12&amp;output=embed" 
            width="425"></iframe>
&nbsp;
        <br />
        <small>
        <a href="https://maps.google.ca/?ie=UTF8&amp;ll=49.257735,-123.123904&amp;spn=0.153041,0.316887&amp;t=m&amp;z=12&amp;source=embed" 
            style="color:#0000FF;text-align:left">View Larger Map</a></small></p>
    <p>
        Weather at Ground station location
        <span style="display: block !important; width: 320px; text-align: center; font-family: sans-serif; font-size: 12px;">
        <a href="http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:00000.1.71892&amp;bannertypeclick=wu_clean2day" 
            target="_blank" title="Vancouver, British Columbia Weather Forecast">
        <img alt="Find more about Weather in Vancouver, CA" 
            src="http://weathersticker.wunderground.com/weathersticker/cgi-bin/banner/ban/wxBanner?bannertype=wu_clean2day_metric_cond&amp;pwscode=IBRITSHC3&amp;ForcedCity=Vancouver&amp;ForcedState=Canada&amp;wmo=71892&amp;language=EN" 
            width="300" /></a><br />
        <a href="http://www.wunderground.com/cgi-bin/findweather/getForecast?query=zmw:00000.1.71892&amp;bannertypeclick=wu_clean2day" 
            style="font-family: sans-serif; font-size: 12px" target="_blank" 
            title="Get latest Weather Forecast updates">Click for weather forecast</a></span><br />
    </p>
    <p>
    </p>
</asp:Content>
