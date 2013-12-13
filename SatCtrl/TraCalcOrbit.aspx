<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="TraCalcOrbit.aspx.cs" Inherits="SatCtrl._TraCalcOrbit" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table style="width:100%;" frame="vsides">    
        <tr>
            <td class="style2">
                Initial Orbit
                <asp:RadioButton ID="RadioButton1" runat="server" Text="Two Line Element" 
                    GroupName="InitialOrbbit" />
                <asp:RadioButton ID="RadioButton2" runat="server" Text="Inc/Pe/Ap" 
                    GroupName="InitialOrbbit" />
                <asp:RadioButton ID="RadioButton3" runat="server" Text="Speed and vel" 
                    GroupName="InitialOrbbit" />
            </td>
        </tr>
        <tr>
            <td class="style8">
                TLE1:<asp:TextBox ID="TextBoxTLE1" runat="server" 
                    Width="842px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style4">
                TLE2:<asp:TextBox ID="TextBoxTLE2" 
                    runat="server" Width="843px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style4">
                Orbital parameters:</td>
        </tr>
        <tr>
            <td class="style4">
                inclination
                <asp:TextBox ID="TextBox5" runat="server" Width="100px"></asp:TextBox>
                <br />
                perigee&nbsp;&nbsp;&nbsp; <asp:TextBox ID="TextBox6" 
                    runat="server" Width="103px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; apogee<asp:TextBox ID="TextBox7" runat="server" Width="104px"></asp:TextBox>
                &nbsp;&nbsp;
                Period(check):
                <asp:TextBox ID="TextBox9" runat="server" Width="99px"></asp:TextBox>
                <br />
                perigee time<asp:TextBox ID="TextBox10" runat="server" Width="87px"></asp:TextBox>
&nbsp;perigee longitude<asp:TextBox ID="TextBox11" runat="server" Width="97px"></asp:TextBox>
&nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                Position/Speed</td>
        </tr>
        <tr>
            <td class="style4">
                X :<asp:TextBox ID="TextBox12" runat="server" Width="86px"></asp:TextBox>
                &nbsp;
                Y:<asp:TextBox ID="TextBox13" runat="server" Width="99px"></asp:TextBox>
                &nbsp;
                Z<asp:TextBox ID="TextBox14" runat="server" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style4">
                Vx<asp:TextBox ID="TextBox15" runat="server" Width="91px"></asp:TextBox>
                Vy<asp:TextBox ID="TextBox16" runat="server" Width="98px"></asp:TextBox>
                Vz<asp:TextBox ID="TextBox17" runat="server" Width="85px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
        </tr>


    </table>
&nbsp; 
    
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        #UpLoadMsg
        {
            width: 667px;
            height: 449px;
        }
        .style1
        {
            width: 432px;
        }
        .style2
        {
            width: 1093px;
        }
        .style4
        {
            width: 1093px;
            height: 20px;
        }
        .style8
        {
            width: 1093px;
            height: 12px;
        }
        #TextArea1
        {
            width: 236px;
        }
        #Select1
        {
            width: 144px;
        }
    </style>
</asp:Content>

