<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="TraCalcTarget.aspx.cs" Inherits="SatCtrl._TraCalcTarget" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table style="width:100%;" frame="vsides">    
        <tr>
            <td>
                Status:
                <asp:Label ID="LabelStatus" runat="server" Text="Status"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                Running nodes:
                <asp:Label ID="LabelNodes" runat="server" Text="Nodes"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button4" runat="server" Enabled="False" Text="Cancel" 
                    ToolTip="Cancel Finding impulses or impulses optimization" />
                </td>
        </tr>
        <tr>
            <td class="style9">
                Target<asp:RadioButton 
                    ID="RadioButton5" runat="server" Enabled="False" GroupName="TargetType" 
                    ToolTip="Set target as an orbit" />
            &nbsp;TLE1:<asp:TextBox ID="TextBox21" runat="server" Width="707px" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; TLE2:<asp:TextBox 
                    ID="TextBox22" runat="server" Enabled="False" Width="713px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5">
                Target:<asp:RadioButton ID="RadioButtonTargetLL" 
                    runat="server" GroupName="TargetType" 
                    ToolTip="Set target as location on the lunar surface." />
&nbsp;Latitude&nbsp;&nbsp;
                <asp:TextBox ID="TextBoxLatitude" runat="server" Width="92px">S2</asp:TextBox>
&nbsp;
                Longitude:<asp:TextBox ID="TextBoxLongitude" runat="server" Width="86px">E15</asp:TextBox>
                </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:ImageButton ID="ImageLunarMap" runat="server" Height="575px" 
                    ImageUrl="~/LunarMapGen.aspx" onclick="ImageButton1_Click" 
                    Width="1148px" />
                <asp:Button ID="Button3" runat="server" Text="See on bigger map" 
                    PostBackUrl="~/LunarMapGen.aspx" />
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Button ID="ButtonFindImp" runat="server" Text="Find Impulses" 
                    
                    ToolTip="That will start the search for values of impulses from initial orbit to reach the target on the lunar surface." 
                    Width="190px" />
                &nbsp;from
                <asp:TextBox ID="FindImpulsesFromDate" runat="server" Font-Size="X-Small" 
                    Width="110px">07/05/13 08:28:00:000</asp:TextBox>
                </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Button ID="ButtonImpOptimization" runat="server" Text="Impulses optimization" 
                    
                    ToolTip="That will start the search of possible firing time and directions to reach the target on the lunar surface." />
                &nbsp;from
                <asp:TextBox ID="TextBox20" runat="server" Font-Size="X-Small" Width="108px">07/05/13 08:28:00:000</asp:TextBox>
                </td>
        </tr>
        <tr>
            <td class="style5">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style5">
                </td>
        </tr>
        <tr>
            <td class="style5">
                </td>
        </tr>
        <tr>
            <td class="style5">
                </td>
        </tr>
        <tr>
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

