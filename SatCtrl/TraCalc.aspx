<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="TraCalc.aspx.cs" Inherits="SatCtrl._TraCalc" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table style="width:100%;" frame="vsides">    
        <tr>
            <td class="style2">
                Initial Orbit
                </td>
            <td>
                Status:
                <asp:Label ID="LabelStatus" runat="server" Text="Status"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                Running nodes:
                <asp:Label ID="LabelNodes" runat="server" Text="Nodes"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button4" runat="server" Enabled="False" Text="Cancel" 
                    ToolTip="Cancel Finding impulses or impulses optimization" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Calculation:
                <asp:Label ID="LabelUserName" runat="server" Text="Label"></asp:Label>
                </td>
        </tr>
        <tr>
            <td class="style8">
                TLE1:<asp:TextBox ID="TextBoxTLE1" runat="server" Width="473px" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="style9">
                Target<asp:RadioButton ID="RadioButton5" runat="server" Enabled="False" 
                    GroupName="TargetType" ToolTip="Set target as an orbit" />
            &nbsp;TLE1:<asp:TextBox ID="TextBox21" runat="server" Width="463px" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style4">
                TLE2:<asp:TextBox ID="TextBoxTLE2" 
                    runat="server" Width="471px" ReadOnly="True"></asp:TextBox>
            </td>
            <td class="style5">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                TLE2:<asp:TextBox ID="TextBox22" runat="server" Enabled="False" Width="462px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style4">
                Craft parameters:</td>
            <td class="style5">
                Target:<asp:RadioButton ID="RadioButtonTargetLL" runat="server" 
                    GroupName="TargetType" ToolTip="Set target as location on the lunar surface." />
&nbsp;Latitude&nbsp;&nbsp;
                <asp:TextBox ID="TextBoxLatitude" runat="server" Width="92px">S2</asp:TextBox>
&nbsp;
                Longitude:<asp:TextBox ID="TextBoxLongitude" runat="server" Width="86px">E15</asp:TextBox>
                </td>
        </tr>
        <tr>
            <td class="style4">
                <br />
            Total mass&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="TextBoxProbM" 
                    runat="server" ReadOnly="True"></asp:TextBox>
            Landed mass&nbsp; 
                <asp:TextBox 
                    ID="TextBoxProbMTarget" runat="server" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Image ID="ImageEngine1" runat="server" Visible="False" Height="100px" />
                <asp:Image ID="ImageEngine2" runat="server" Visible="False" Height="100px" />
                <br />
                <asp:Image ID="ImageEngine3" runat="server" Visible="False" Height="100px" />
                <asp:Image ID="ImageEngine4" runat="server" Visible="False" Height="100px" />
                <br />
                <asp:Image ID="ImageEngine5" runat="server" Visible="False" Height="100px" />
            </td>
            <td class="style5">
                <asp:ImageButton ID="ImageLunarMap" runat="server" Height="180px" 
                    ImageUrl="~/LunarMapGen.aspx" onclick="ImageButton1_Click" 
                    Width="360px" />
                <asp:Button ID="Button3" runat="server" Text="Set on bigger map" 
                    PostBackUrl="~/TraCalcTarget.aspx" />
            </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style5">
                <asp:Button ID="ButtonFindImp" runat="server" Text="Find Impulses" 
                    
                    ToolTip="That will start the search for values of impulses from initial orbit to reach the target on the lunar surface." 
                    Width="190px" />
                
                </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style5">
                &nbsp;Start treajectory calculation at: ,&nbsp; or at: &nbsp;<asp:TextBox 
                    ID="FindImpulsesFromDate" runat="server" Font-Size="X-Small" 
                    ontextchanged="FindImpulsesFromDate_TextChanged" Width="110px">07/05/13 08:28:00:000</asp:TextBox>
&nbsp;till
                <asp:TextBox ID="TextBoxTotalDays" runat="server" 
                    ontextchanged="TextBoxTotalDays_TextChanged">1</asp:TextBox>
&nbsp;days</td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style5">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style5">
                </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style5">
                </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style5">
                </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style5">
                </td>
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
            width: 522px;
        }
        .style4
        {
            width: 522px;
            height: 20px;
        }
        .style5
        {
            height: 20px;
        }
        .style8
        {
            width: 522px;
            height: 12px;
        }
        .style9
        {
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

