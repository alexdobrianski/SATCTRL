<%@ Page Title="Simulation Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Simulation.aspx.cs" Inherits="SatCtrl._Simulation" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
        <table style="width:100%;">
            <tr>
                <td><a href="http://java3d.java.net/binary-builds.html">Requare Java 3D - click to install</a></p>
                    &nbsp;</td>
            </tr>
            <tr>
                <td><APPLET ARCHIVE="/SatCtrl/visualtra.jar" CODE=visualtra.class WIDTH=1280 HEIGHT=800> </APPLET>
                    &nbsp;</td>
                <td>
                
                    <asp:TextBox ID="ProbKeplerLine1" runat="server" Width="500px"></asp:TextBox>
                    <asp:TextBox ID="ProbKeplerLine2" runat="server" Width="500px"></asp:TextBox>
                    <asp:TextBox ID="ProbKeplerLine3" runat="server" Width="500px"></asp:TextBox>
                <asp:CheckBox ID="CheckBoxRealTime" runat="server" Text="Use Real Time" 
                        Checked="True" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;minutes after now<asp:TextBox 
                        ID="TextBoxdMinFromNow" runat="server" Width="30px">3</asp:TextBox>
                    <p/>
                    <asp:TextBox ID="TextBoxTime" runat="server" Width="212px"></asp:TextBox>
                        <asp:Button ID="Set" runat="server" Text="Set" />
                </td>
            </tr>
        </table>
        <br />
</asp:Content>
