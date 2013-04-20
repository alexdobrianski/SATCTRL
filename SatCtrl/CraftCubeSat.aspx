<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CraftCubeSat.aspx.cs" Inherits="SatCtrl._CraftCubeSat" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<table style="width:100%;" frame="vsides">    
        <tr>
            <td class="style1">
                <span class="style6"><strong>sequence of a communication's commands
                to send:</strong></span>
                <asp:TextBox ID="MsgUplink" runat="server" Height="294px" Width="491px" 
                TextMode="MultiLine"></asp:TextBox>
    
                <asp:Button ID="Button1" runat="server"  onclick="Button1_Click" Text="Send" />

                </td>
            <td class="style2">
                <span class="style6"><strong>Cubesat/Craft&#39;s commands set:</strong></span><br />
                <asp:Button ID="GetStatusViaBackup" runat="server" 
                    Text="Get Status via backup comm" />

                <asp:Button ID="Button12" runat="server" Text="ATDT Cubesat/Craft" />
                <br />
                <asp:Button ID="GetStatus" runat="server" Text="Get Status" />

                <asp:Button ID="FindSun" runat="server" Text="Find Sun" />
                
                <asp:Button ID="RoatateBigAxe" runat="server" Text="Rotate big" />
                <asp:Button ID="RotateSmallAxe" runat="server" Text="Rotate small" />
                <br />
                <asp:Button ID="SnapShort" runat="server" Text="Get Snapshort position" 
                    Width="226px" />
                <asp:TextBox ID="TextBox13" runat="server" Width="90px"></asp:TextBox>
                <br />
                <asp:Button ID="Button3" runat="server" Text="Restore Snapshot position" 
                    Width="226px" />
                <asp:TextBox ID="TextBox14" runat="server" Width="90px"></asp:TextBox>
                <br />
                <asp:Button ID="Button4" runat="server" Text="Wait" />
                <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
&nbsp;seconds<br />
                <asp:Button ID="SetOrientation" runat="server" Text="Set Orientation" />
                At time : <asp:TextBox ID="TimeToRunCmd" runat="server"></asp:TextBox>
                <br />
                X:<asp:TextBox 
                    ID="TextBox1" runat="server" Width="120px"></asp:TextBox>
                Y:<asp:TextBox ID="TextBox2" runat="server" Width="120px"></asp:TextBox>
                Z:<asp:TextBox ID="TextBox3" runat="server" Width="120px"></asp:TextBox>
                N:<asp:TextBox ID="TextBox4" runat="server" Width="120px"></asp:TextBox>
                <br />
                <asp:Button ID="SetOrientMF" runat="server" Text="set orientation in magnetic field" />
                At time : <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                <br />
                X:<asp:TextBox 
                    ID="TextBox6" runat="server" Width="120px"></asp:TextBox>
                Y:<asp:TextBox ID="TextBox7" runat="server" Width="119px"></asp:TextBox>
                Z:<asp:TextBox ID="TextBox8" runat="server" Width="120px"></asp:TextBox>
                N:<asp:TextBox ID="TextBox9" runat="server" Width="120px"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="ReadFlshComm" runat="server" Text="Read Comm Flash" />
&nbsp;from addr:<asp:TextBox ID="TextBox10" runat="server" Width="100px"></asp:TextBox>
&nbsp;length:<asp:TextBox ID="TextBox11" runat="server" Width="100px"></asp:TextBox>
                <br />
                <asp:Button ID="Button2" runat="server" Text="Write Comm Flash" />
&nbsp;from addr<asp:TextBox ID="TextBox12" runat="server" Width="100px"></asp:TextBox>
                <asp:FileUpload ID="FileUpload1" runat="server" Width="220px" />
            </td>
            <td>
                <span class="style6"><strong>Ground Station&#39;s commands set:</strong></span><br />
                <asp:CheckBox ID="CheckBoxAutoAntenna" runat="server" 
                    oncheckedchanged="CheckBoxAutoAntenna_CheckedChanged" 
                    Text="Send constantly Antenna position" />
                <asp:Button ID="Button13" runat="server" Text="Set" />
                <br />
                <asp:Button ID="CalcAntnOrientation" runat="server" 
                    Text="Calculate Antenna orintation" />
                <br />
                <asp:Button ID="SetOrientation1" runat="server" Text="Set Orientation" />
                <br />
                <br />
                X:<asp:TextBox 
                    ID="AntQX" runat="server" Width="120px"></asp:TextBox>
                Y:<asp:TextBox ID="AntQY" runat="server" Width="120px"></asp:TextBox>
                Z:<asp:TextBox ID="AntQZ" runat="server" Width="120px"></asp:TextBox>
                N:<asp:TextBox ID="AntQN" runat="server" Width="120px"></asp:TextBox>
                <br />
                <asp:Button ID="SetAntMove" runat="server" Text="Set Movement" />
                &nbsp;from current quternion to quternion:<br />
                X:<asp:TextBox 
                    ID="AntMoveQX" runat="server" Width="120px"></asp:TextBox>
                Y:<asp:TextBox ID="AntMoveQY" runat="server" Width="120px"></asp:TextBox>
                Z:<asp:TextBox ID="AntMoveQZ" runat="server" Width="120px"></asp:TextBox>
                N:<asp:TextBox ID="AntMoveQN" runat="server" Width="120px"></asp:TextBox>
                <br />
                in
                <asp:TextBox ID="AntMoveQSec" runat="server"></asp:TextBox>
&nbsp;seconds.</td>
        </tr>
        <tr>
            <td class="style7">
                <span class="style6"><strong>responce:</strong></span><br />
                <asp:TextBox ID="TextBox19" runat="server" Height="169px" ReadOnly="True" 
                    TextMode="MultiLine" Width="492px"></asp:TextBox>
                </td>
            <td class="style8">
                <span class="style6"><strong>flash storage commands:</strong></span><br />
                <br />
                <asp:Button ID="Button5" runat="server" Text="Get Dir from flash" />
                <asp:Button ID="Button6" runat="server" Text="Upload file to flush" />
                <asp:FileUpload ID="FileUpload2" runat="server" />
                <br />
                <asp:Button ID="Button10" runat="server" Text="CD" />
                <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="Button11" runat="server" Text="RM" />
                <asp:TextBox ID="TextBox18" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="Button7" runat="server" Text="Make HD picture" />
                <br />
                <asp:Button ID="Button8" runat="server" Text="Record HD video" /> for a 
                <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox> seconds
                <br />
                <asp:Button ID="Button9" runat="server" Text="Make regular picture" />
            </td>
            <td class="style9">
                </td>
        </tr>
        <tr>
            <td class="style3">
                <span class="style6"><strong>sequence of&nbsp; commands send via backup comm</strong></span><br />
                <asp:TextBox ID="TextBox25" runat="server" TextMode="MultiLine" Height="131px" 
                    Width="489px" ></asp:TextBox>
                <asp:Button ID="Button14" runat="server" Text="bckp" />
                </td>
            <td class="style4">
                <span class="style6"><strong>rotate&#39;s commands:<br />
                </strong></span>
                <br />
&nbsp;<asp:Button ID="SetOrientation0" runat="server" Text="Set Movement" />
                &nbsp;from current quternion to quternion:<br />
                X:<asp:TextBox 
                    ID="TextBox20" runat="server" Width="120px"></asp:TextBox>
                Y:<asp:TextBox ID="TextBox21" runat="server" Width="120px"></asp:TextBox>
                Z:<asp:TextBox ID="TextBox22" runat="server" Width="120px"></asp:TextBox>
                N:<asp:TextBox ID="TextBox23" runat="server" Width="120px"></asp:TextBox>
                <br />
                in
                <asp:TextBox ID="TextBox24" runat="server"></asp:TextBox>
&nbsp;seconds.<br />
                </td>
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
            width: 545px;
        }
        .style2
        {
            width: 571px;
        }
        .style3
        {
            width: 545px;
            height: 20px;
        }
        .style4
        {
            width: 571px;
            height: 20px;
        }
        .style5
        {
            height: 20px;
        }
        .style6
        {
            font-size: medium;
        }
        .style7
        {
            width: 545px;
            height: 12px;
        }
        .style8
        {
            width: 571px;
            height: 12px;
        }
        .style9
        {
            height: 12px;
        }
    </style>
</asp:Content>

