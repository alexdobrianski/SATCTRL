<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="TraCalcOrbit.aspx.cs" Inherits="SatCtrl._TraCalcOrbit" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table style="width:100%;" frame="vsides">    
        <tr>
            <td class="style2">
                Initial Orbit settings (a)
                <asp:RadioButton ID="RadioButtonTLE" runat="server" AutoPostBack="True" 
                    GroupName="TypeOrbit" oncheckedchanged="RadioButton1_CheckedChanged" 
                    Text="TLE" />
                &nbsp; (b)
                <asp:RadioButton ID="RadioButtonOrbita" runat="server" AutoPostBack="True" 
                    GroupName="TypeOrbit" oncheckedchanged="RadioButton2_CheckedChanged" 
                    Text="Orbital Parameters" />
                &nbsp; (c)
                <asp:RadioButton ID="RadioButtonXYZ" runat="server" AutoPostBack="True" 
                    GroupName="TypeOrbit" oncheckedchanged="RadioButton3_CheckedChanged" 
                    Text="Position / Velocity" />
                <asp:Button ID="ButtonConvertXYZ" runat="server" Text="to XYZ VxVyVz" 
                    onclick="ButtonConvertXYZ_Click" />
                <asp:Button ID="ButtonToOrbital" runat="server" Text="to Orbital Prm" />
                <asp:Button ID="ButtonToTLE" runat="server" Text="to TLE" />
</td>
        </tr>
        <tr>
            <td class="style4">
                <hr />
            </td>
        </tr>
        <tr>
            <td class="style8">
                TLE1:<asp:TextBox ID="TextBoxTLE1" runat="server" Width="679px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style4">
                TLE2:<asp:TextBox ID="TextBoxTLE2" 
                    runat="server" Width="679px"></asp:TextBox>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td class="style4">
                <hr />
            </td>
        </tr>

        <tr>
            <td class="style4">
                Orbital parameters:</td>
        </tr>
        <tr>
            <td class="style4">
                inclination&nbsp;&nbsp;
                <asp:TextBox ID="TextBoxOrbitInc" runat="server" Width="100px" ></asp:TextBox>
                <br />
                perigee&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                <asp:TextBox ID="TextBoxOrbitPerigee" 
                    runat="server" Width="100px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; apogee<asp:TextBox 
                    ID="TextBoxApogee" runat="server" Width="100px"></asp:TextBox>
                &nbsp;&nbsp;
                Period (check value):
                <asp:TextBox ID="TextBoxOrbitPeriod" runat="server" Width="100px"></asp:TextBox>
                <br />
                perigee time<asp:TextBox ID="TextBoxOrbitPereegeTime" runat="server" 
                    Width="116px" Font-Size="X-Small"></asp:TextBox>
&nbsp;perigee longitude<asp:TextBox ID="TextBoxOrbitLongitude" runat="server" Width="100px"></asp:TextBox>
&nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                <hr />
            </td>
        </tr>
        <tr>
            <td class="style4">
                Position/Speed at:<asp:CheckBox ID="CheckBoxCurTime" runat="server" 
                    oncheckedchanged="CheckBoxCurTime_CheckedChanged" Text="Curent time" 
                    AutoPostBack="True" CausesValidation="True" />
&nbsp; at:
                <asp:TextBox ID="TextBoxTimeCalc" runat="server" Font-Size="X-Small" 
                    Width="116px">07/05/13 08:28:00:000</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;
                X :&nbsp;<asp:TextBox ID="TextBoxX" runat="server" Width="86px"></asp:TextBox>
                &nbsp;&nbsp;
                Y:&nbsp;<asp:TextBox ID="TextBoxY" runat="server" Width="99px"></asp:TextBox>
                &nbsp;&nbsp;
                Z:&nbsp;<asp:TextBox ID="TextBoxZ" runat="server" Width="80px"></asp:TextBox>
                &nbsp;&nbsp;
                Vx:&nbsp;<asp:TextBox ID="TextBoxVx" runat="server" Width="91px"></asp:TextBox>
                &nbsp;&nbsp;
                Vy:&nbsp;<asp:TextBox ID="TextBoxVy" runat="server" Width="98px"></asp:TextBox>
                &nbsp;&nbsp;
                Vz:&nbsp;<asp:TextBox ID="TextBoxVz" runat="server" Width="85px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style4">
                <hr />
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

