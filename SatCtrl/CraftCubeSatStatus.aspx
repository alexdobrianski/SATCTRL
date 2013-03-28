<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CraftCubeSatStatus.aspx.cs" Inherits="SatCtrl._CraftCubeSatStatus" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    Status page:<br />
<br />
<br />
    
&nbsp;Position: X:
<asp:Label ID="XCur" runat="server" Text="123.123456"></asp:Label>
&nbsp;Y:<asp:Label ID="YCur" runat="server" Text="1234.5678"></asp:Label>
&nbsp;Z:
<asp:Label ID="ZCur" runat="server" Text="123456.13234124"></asp:Label>
&nbsp;VX:<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
&nbsp;VY:
<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
&nbsp;VZ:<asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
<br />
3 punch cards (TLE) calculated:<br />
old position calculated<br />
old position calculated<br />
old position calulated<br />
<br />
Quatenion orientation:<br />
Quaternion magnetic field<br />
Temparature
<br />
Quterion sun position:<br />
Quternion center of the earth<br />
Volatge C1, C2, C3<br />
<br />
Last picture<br />
<br />
&nbsp;<br />
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

