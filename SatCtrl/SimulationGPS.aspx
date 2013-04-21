<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="SimulationGPS.aspx.cs" Inherits="SatCtrl._SimulationGPS" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            width: 645px;
        }
        

        .style2
        {
            font-size: medium;
        }
        

    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <p>

</p>
    <p>
        <table style="width:100%;">
            <tr>
                <td class="style1">
        <a href="http://java3d.java.net/binary-builds.html">Requare install of Java 3D - 
        click to install</a><br />
    <APPLET ARCHIVE="/SatCtrl/visualtra.jar" CODE=visualtra.class WIDTH=640 HEIGHT=480>
    </applet>
                    </td>
                <td> <span class="style2"><strong>THREE PUNCH CARDS ELEMENTS (modern TLE)= Two Line 
                    Element</strong></span> <br />
                    <asp:RadioButton ID="RBGPSManual" runat="server" Text="Manual" 
                        GroupName="Obtain" />
                    <asp:RadioButton ID="RBGPSExtract" runat="server" Text="Extract from signal" 
                        GroupName="Obtain" />
                    <asp:RadioButton ID="RBGPSObtain" runat="server" Text="Obtain latest" 
                        GroupName="Obtain" />

                <br />GPS1:<asp:TextBox ID="GPS1Line1" runat="server" 
                        ontextchanged="GPS1Line1_TextChanged"></asp:TextBox>
                    <br />Line2 :<asp:TextBox ID="GPS1Line2" runat="server" Width="588px" 
                        ontextchanged="GPS1Line2_TextChanged"></asp:TextBox>
                    <br />Line3 :<asp:TextBox ID="GPS1Line3" runat="server" Width="588px" 
                        ontextchanged="GPS1Line3_TextChanged"></asp:TextBox>
                    <br />
                <br />GPS2:<asp:TextBox ID="GPS2Line1" runat="server"></asp:TextBox>
                    <br />Line2 :<asp:TextBox ID="GPS2Line2" runat="server" Width="588px"></asp:TextBox>
                    <br />Line3 :<asp:TextBox ID="GPS2Line3" runat="server" Width="588px"></asp:TextBox>
                    <br />
                <br />GPS3:<asp:TextBox ID="GPS3Line1" runat="server"></asp:TextBox>
                    <br />Line2 :<asp:TextBox ID="GPS3Line2" runat="server" Width="588px"></asp:TextBox>
                    <br />Line3 :<asp:TextBox ID="GPS3Line3" runat="server" Width="588px"></asp:TextBox>
                    <br />
                <br />GPS4:<asp:TextBox ID="GPS4Line1" runat="server"></asp:TextBox>
                    <br />Line2 :<asp:TextBox ID="GPS4Line2" runat="server" Width="588px"></asp:TextBox>
                    <br />Line3 :<asp:TextBox ID="GPS4Line3" runat="server" Width="588px"></asp:TextBox>
                    <br />
                <br />GPS5:<asp:TextBox ID="GPS5Line1" runat="server"></asp:TextBox>
                    <br />Line2 :<asp:TextBox ID="GPS5Line2" runat="server" Width="588px"></asp:TextBox>
                    <br />Line3 :<asp:TextBox ID="GPS5Line3" runat="server" Width="588px"></asp:TextBox>
                    <br />

                    </td>
            </tr>
        </table>
                    </asp:Content>
