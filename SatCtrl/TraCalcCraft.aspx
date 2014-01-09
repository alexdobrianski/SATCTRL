<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="TraCalcCraft.aspx.cs" Inherits="SatCtrl._TraCalcCraft" validateRequest="false" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table style="width:100%;" frame="vsides">    
        <tr>
            <td class="style12">
                Craft 
            </td>
            <td>
                Calculation 
                by:
                <asp:Label ID="LabelUserName" runat="server" Text="Label"></asp:Label>

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LabelTextToLogin" runat="server" Text="To edit fields need to "></asp:Label>
                <asp:HyperLink ID="HyperLinkLogin" runat="server" 
                    NavigateUrl="~/Account/Login.aspx">Login</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td class="style12">
                Total mass&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="TextBoxProbM" 
                    runat="server" ReadOnly="True"></asp:TextBox>
            &nbsp;kg</td>
        </tr>
        <tr>
            <td class="style12">
                Landed mass&nbsp; 
                <asp:TextBox 
                    ID="TextBoxProbMTarget" runat="server" ReadOnly="True"></asp:TextBox>
            &nbsp;kg<asp:Button ID="ButtonSetLandedMass" runat="server" onclick="Button1_Click" 
                    Text="Set Landed mass" />
            </td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
        </tr>

        <tr>
            <td class="style12">
                <asp:Label ID="LabelImpl1" runat="server" Text="Engine (1 impulse) profile:"></asp:Label>
                <asp:CheckBox 
                    ID="CheckBoxImp1" runat="server" Text="use in calculations" AutoPostBack="True" 
                    oncheckedchanged="CheckBoxImp1_CheckedChanged" />
            &nbsp;</td>
            <td>
                <asp:Button ID="ButtonUpdateImp1" runat="server" 
                    onclick="ButtonUpdateImp1_Click" Text="Update" />
            </td>
        </tr>
        <tr>
            <td class="style12">
                    
                    <asp:HyperLink ID="HyperLinkImpl1" runat="server" NavigateUrl="EngineJpgGen.aspx?n=0" >
                    <asp:Image ID="ImageImpl1" runat="server" Height="200px" 
                        ImageUrl="~/Img/engineblank.JPG" Width="350px"  />
                    
                    </asp:HyperLink>
                    
            </td>        
            <td>
                    <asp:TextBox ID="TextBoxEngineXML1" runat="server" Height="200px" ReadOnly="True" 
                    Rows="2" TextMode="MultiLine" Width="430px" Wrap="False" 
                    style="margin-bottom: 0px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style13">
                </td>
        </tr>

        <tr>
            <td class="style12">
                <asp:Label ID="LabelImpl2" runat="server" Text="Engine (2 impulse) profile:"></asp:Label>
                <asp:CheckBox 
                    ID="CheckBoxImp2" runat="server" AutoPostBack="True" 
                    oncheckedchanged="CheckBoxImp2_CheckedChanged" 
                    Text="use in calculations" />
            </td>
            <td>
                <asp:Button ID="ButtonDeleteImp2" runat="server" Text="Delete" 
                    onclick="ButtonDeleteImp2_Click" />
                <asp:Button ID="ButtonAddImp2" runat="server" Text="Add" 
                    ToolTip="Add another impulse" onclick="ButtonAddImp2_Click" />
                <asp:Button ID="ButtonUpdateImp2" runat="server" Text="Update" 
                    onclick="ButtonUpdateImp2_Click" />
            </td>

        </tr>
        <tr>
            <td class="style12">
                    <asp:HyperLink ID="HyperLinkImpl2" runat="server" NavigateUrl="EngineJpgGen.aspx?n=1" >
                    <asp:Image ID="ImageImpl2" runat="server" Height="200px" 
                        ImageUrl="~/Img/engineblank.JPG" Width="350px" />
                        </asp:HyperLink>
                    </td>
            <td>
                    <asp:TextBox ID="TextBoxEngineXML2" runat="server" Height="200px" ReadOnly="True" 
            Rows="2" TextMode="MultiLine" Width="430px" Wrap="False" 
                    style="margin-bottom: 0px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
        </tr>

        <tr>
            <td class="style12">
                <asp:Label ID="LabelImpl3" runat="server" Text="Engine (3 impulse) profile:"></asp:Label>
                <asp:CheckBox 
                    ID="CheckBoxImp3" runat="server" AutoPostBack="True" 
                    oncheckedchanged="CheckBoxImp3_CheckedChanged" 
                    Text="use in calculations" />
            </td>
            <td>
                <asp:Button ID="ButtonDeleteImp3" runat="server" Text="Delete" 
                    onclick="ButtonDeleteImp3_Click" />
                <asp:Button ID="ButtonAddImp3" runat="server" Text="Add" 
                    ToolTip="Add another impulse" onclick="ButtonAddImp3_Click" />
                <asp:Button ID="ButtonUpdateImp3" runat="server" Text="Update" 
                    onclick="ButtonUpdateImp3_Click" />
            </td>

        </tr>
        <tr>
            <td class="style12">
                    <asp:HyperLink ID="HyperLinkImpl3" runat="server" NavigateUrl="EngineJpgGen.aspx?n=2" >
                    <asp:Image ID="ImageImpl3" runat="server" Height="200px" 
                        ImageUrl="~/Img/engineblank.JPG" Width="350px" />
                        </asp:HyperLink>
                    </td>
            <td>
                    <asp:TextBox ID="TextBoxEngineXML3" runat="server" Height="200px" ReadOnly="True" 
            Rows="2" TextMode="MultiLine" Width="430px" Wrap="False" 
                    style="margin-bottom: 0px"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>

        </tr>

        <tr>
            <td class="style12">
                &nbsp;</td>
        </tr>

        <tr>
            <td class="style12">
                <asp:Label ID="LabelImpl4" runat="server" Text="Engine (4 impulse) profile:"></asp:Label>
                <asp:CheckBox 
                    ID="CheckBoxImp4" runat="server" AutoPostBack="True" 
                    oncheckedchanged="CheckBoxImp4_CheckedChanged" 
                    Text="use in calculations" />
            </td>
            <td>
                <asp:Button ID="ButtonDeleteImp4" runat="server" Text="Delete" 
                    onclick="ButtonDeleteImp4_Click" />
                <asp:Button ID="ButtonAddImp4" runat="server" Text="Add" 
                    ToolTip="Add another impulse" onclick="ButtonAddImp4_Click" />
                <asp:Button ID="ButtonUpdateImp4" runat="server" Text="Update" 
                    onclick="ButtonUpdateImp4_Click" />
            </td>
        </tr>
        <tr>
            <td class="style12">
                    <asp:HyperLink ID="HyperLinkImpl4" runat="server" NavigateUrl="EngineJpgGen.aspx?n=3" >
                    <asp:Image ID="ImageImpl4" runat="server" Height="200px" 
                        ImageUrl="~/Img/engineblank.JPG" Width="350px" />
                        </asp:HyperLink>
                    </td>
            <td>
                    <asp:TextBox ID="TextBoxEngineXML4" runat="server" Height="200px" ReadOnly="True" 
            Rows="2" TextMode="MultiLine" Width="430px" Wrap="False" 
                    style="margin-bottom: 0px"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>

        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
        </tr>

        <tr>
            <td class="style12">
                <asp:Label ID="LabelImpl5" runat="server" Text="Engine (5 impulse) profile:"></asp:Label>
                <asp:CheckBox 
                    ID="CheckBoxImp5" runat="server" AutoPostBack="True" 
                    oncheckedchanged="CheckBoxImp5_CheckedChanged" 
                    Text="use in calculations" />
            </td>
            <td>
                <asp:Button ID="ButtonDeleteImp5" runat="server" Text="Delete" 
                    onclick="ButtonDeleteImp5_Click" />
                <asp:Button ID="ButtonAddImp5" runat="server" Text="Add" 
                    ToolTip="Add another impulse" onclick="ButtonAddImp5_Click" />
                <asp:Button ID="ButtonUpdateImp5" runat="server" Text="Update" 
                    onclick="ButtonUpdateImp5_Click" />
            </td>
        </tr>
        <tr>
            <td class="style12">
                    <asp:HyperLink ID="HyperLinkImpl5" runat="server" NavigateUrl="EngineJpgGen.aspx?n=4" >
                    <asp:Image ID="ImageImpl5" runat="server" Height="200px" 
                        ImageUrl="~/Img/engineblank.JPG" Width="350px" />
                        </asp:HyperLink>
                    </td>
            <td>
                    <asp:TextBox ID="TextBoxEngineXML5" runat="server" Height="200px" ReadOnly="True" 
            Rows="2" TextMode="MultiLine" Width="430px" Wrap="False" 
                    style="margin-bottom: 0px"></asp:TextBox>
            </td>

        </tr>

        <tr>
            <td class="style12">
                &nbsp;</td>

        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style12">
                &nbsp;</td>
        </tr>


    </table>
&nbsp; 
    
    <script language="javascript" type="text/javascript">
// <![CDATA[

        function ButtonUpdateIm1_onclick() {

        }

// ]]>
    </script>
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
        #TextArea1
        {
            width: 236px;
        }
        #Select1
        {
            width: 144px;
        }
        .style12
        {
            width: 459px;
        }
        .style13
        {
            width: 459px;
            height: 20px;
        }
    </style>
</asp:Content>

