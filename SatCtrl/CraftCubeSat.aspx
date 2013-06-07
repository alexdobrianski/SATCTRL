<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CraftCubeSat.aspx.cs" Inherits="SatCtrl._CraftCubeSat" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table style="width:100%;" frame="vsides">    
        <tr>
            <td class="style1">
                <span class="style6"><strong>sequence of a communication's commands
                to send:</strong></span>
                <asp:TextBox ID="MsgUplink" runat="server" Height="294px" Width="415px" 
                TextMode="MultiLine"></asp:TextBox>
                <br />
                <asp:Button ID="Button1" runat="server"  onclick="Button1_Click" Text="Send" />
                simbols: '=' %3D
                </td>
            <td class="style2">
                <span class="style6"><strong>Cubesat/Craft&#39;s commands set:</strong></span><br />
                <asp:Button ID="GetStatusViaBackup" runat="server" 
                    Text="Get Status via backup comm" onclick="GetStatusViaBackup_Click" />

                <asp:Button ID="Button12" runat="server" Text="ATDT Cubesat/Craft" 
                    onclick="Button12_Click" />
                <br />
                <asp:Button ID="GetStatus" runat="server" Text="Get Status" 
                    onclick="GetStatus_Click" />

                <asp:Button ID="FindSun" runat="server" Text="Find Sun" Enabled="False" />
                
                <asp:Button ID="RoatateBigAxe" runat="server" Text="Rotate big" 
                    Enabled="False" />
                <asp:Button ID="RotateSmallAxe" runat="server" Text="Rotate small" 
                    Enabled="False" />
                <br />
                <asp:Button ID="SnapShort" runat="server" Text="Get Snapshort position" 
                    Width="226px" Enabled="False" />
                <asp:TextBox ID="TextBox13" runat="server" Width="90px" Enabled="False"></asp:TextBox>
                <br />
                <asp:Button ID="Button3" runat="server" Text="Restore Snapshot position" 
                    Width="226px" Enabled="False" />
                <asp:TextBox ID="TextBox14" runat="server" Width="90px" Enabled="False"></asp:TextBox>
                <br />
                <asp:Button ID="Button4" runat="server" Text="Wait" Enabled="False" />
                <asp:TextBox ID="TextBox15" runat="server" Enabled="False" Width="90px"></asp:TextBox>
&nbsp;seconds<br />
                <asp:Button ID="SetOrientation" runat="server" Text="Set Orientation" 
                    Enabled="False" />
                At time : <asp:TextBox ID="TimeToRunCmd" runat="server" Enabled="False" 
                    Width="90px"></asp:TextBox>
                <br />
                X:<asp:TextBox 
                    ID="TextBox1" runat="server" Width="90px" Enabled="False"></asp:TextBox>
                Y:<asp:TextBox ID="TextBox2" runat="server" Width="90px" Enabled="False"></asp:TextBox>
                Z:<asp:TextBox ID="TextBox3" runat="server" Width="90px" Enabled="False"></asp:TextBox>
                N:<asp:TextBox ID="TextBox4" runat="server" Width="90px" Enabled="False"></asp:TextBox>
                <br />
                <asp:Button ID="SetOrientMF" runat="server" 
                    Text="set orientation in magnetic field" Enabled="False" />
                At time : <asp:TextBox ID="TextBox5" runat="server" Enabled="False" 
                    Width="90px"></asp:TextBox>
                <br />
                X:<asp:TextBox 
                    ID="TextBox6" runat="server" Width="90px" Enabled="False"></asp:TextBox>
                Y:<asp:TextBox ID="TextBox7" runat="server" Width="90px" Enabled="False"></asp:TextBox>
                Z:<asp:TextBox ID="TextBox8" runat="server" Width="90px" Enabled="False"></asp:TextBox>
                N:<asp:TextBox ID="TextBox9" runat="server" Width="90px" Enabled="False"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="ReadFlshComm" runat="server" Text="Read Comm Flash" 
                    onclick="ReadFlshComm_Click" />
&nbsp;from:<asp:TextBox ID="ReadFlashAddr" runat="server" Width="90px"></asp:TextBox>
&nbsp;len:<asp:TextBox ID="ReadFlashLen" runat="server" Width="90px"></asp:TextBox>
                <br />
                <asp:Button ID="Button2" runat="server" Text="Write Comm Flash" 
                    Enabled="False" />
&nbsp;from:<asp:TextBox ID="TextBox12" runat="server" Width="90px" Enabled="False"></asp:TextBox>
<br />
                
            </td>
            <td>
                <span class="style6"><strong>Ground Station&#39;s orientation commands set:</strong></span><br />
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
                    ID="AntQX" runat="server" Width="90px"></asp:TextBox>
                Y:<asp:TextBox ID="AntQY" runat="server" Width="90px"></asp:TextBox>
                Z:<asp:TextBox ID="AntQZ" runat="server" Width="90px"></asp:TextBox>
                N:<asp:TextBox ID="AntQN" runat="server" Width="90px"></asp:TextBox>
                <br />
                <asp:Button ID="SetAntMove" runat="server" Text="Set Movement" />
                &nbsp;from current quternion to quternion:<br />
                X:<asp:TextBox 
                    ID="AntMoveQX" runat="server" Width="90px"></asp:TextBox>
                Y:<asp:TextBox ID="AntMoveQY" runat="server" Width="90px"></asp:TextBox>
                Z:<asp:TextBox ID="AntMoveQZ" runat="server" Width="90px"></asp:TextBox>
                N:<asp:TextBox ID="AntMoveQN" runat="server" Width="90px"></asp:TextBox>
                <br />
                in
                <asp:TextBox ID="AntMoveQSec" runat="server" Width="90px"></asp:TextBox>
&nbsp;seconds.</td>
        </tr>
        <tr>
            <td class="style7">
                <span class="style6"><strong>responce:</strong></span><br />
                <asp:TextBox ID="TextBox19" runat="server" Height="169px" ReadOnly="True" 
                    TextMode="MultiLine" Width="415px"></asp:TextBox>
                </td>
            <td class="style8">
                <span class="style6"><strong>flash storage commands:</strong></span><br />
                <br />
                <asp:Button ID="Button5" runat="server" Text="Get Dir from flash" 
                    Enabled="False" />
                <asp:Button ID="Button6" runat="server" Text="Upload file to flush" 
                    Enabled="False" />
                <br />
                <br />
                <asp:Button ID="Button10" runat="server" Text="CD" Enabled="False" />
                <asp:TextBox ID="TextBox17" runat="server" Enabled="False" Width="90px"></asp:TextBox>
                <br />
                <asp:Button ID="Button11" runat="server" Text="RM" Enabled="False" />
                <asp:TextBox ID="TextBox18" runat="server" Enabled="False" Width="90px"></asp:TextBox>
                <br />
                <asp:Button ID="Button7" runat="server" Text="Make HD picture" 
                    Enabled="False" />
                <br />
                <asp:Button ID="Button8" runat="server" Text="Record HD video" 
                    Enabled="False" /> for a 
                <asp:TextBox ID="TextBox16" runat="server" Enabled="False" Width="90px"></asp:TextBox> seconds
                <br />
                <asp:Button ID="Button9" runat="server" Text="Make regular picture" 
                    Enabled="False" />
            </td>
            <td class="style9">
                <span class="style6"><strong>GrStn commands:</strong></span><br /><br />
                <asp:Button ID="ButtonGrStReadFlash" runat="server" Text="Read GrStn Flash" 
                    onclick="ButtonGrStReadFlash_Click" />
                    &nbsp;from:<asp:TextBox ID="GrStReadFlashFrom" runat="server" Width="90px"></asp:TextBox>
                    &nbsp;len:<asp:TextBox ID="GrStReadFlashLen" runat="server" 
                    Width="90px"></asp:TextBox>
                <br />
                <asp:Button ID="ButtonGrStFlasherace" runat="server" 
                    onclick="ButtonGrStFlasherace_Click" Text="Erace Intetrnal FLash" /><br />
                <asp:FileUpload ID="GrStFileUploadFlash" runat="server" />
                <asp:Button ID="ButtonUploadGrStFlash" runat="server" Text="Upload Flash File" 
                       onclick="ButtonUploadGrStFlash_Click" />
                <br />
                <asp:Button ID="ButtonSetClkGrStn" runat="server" 
                    onclick="ButtonSetClkGrStn_Click" Text="SetClock " />
                &lt;-from mission ctrl.
                <asp:Button ID="ButtonGrStnGetClock" runat="server" 
                    onclick="ButtonGrStnGetClock_Click" Text="GetClock" />
&nbsp;-&gt;to mission ctrl</td>
        </tr>
        <tr>
            <td class="style3">
                <span class="style6"><strong>sequence of&nbsp; commands send via backup comm</strong></span><br />
                <asp:TextBox ID="BackUpM10" runat="server" TextMode="MultiLine" Height="131px" 
                    Width="415px" ></asp:TextBox>
                    <br />
                <asp:Button ID="Button14" runat="server" Text="bckp" />
                </td>
            <td class="style4">
                <span class="style6"><strong>rotate&#39;s commands:<br />
                </strong></span>
                <br />
&nbsp;<asp:Button ID="SetOrientation0" runat="server" Text="Set Movement" Enabled="False" />
                &nbsp;from current quternion to quternion:<br />
                X:<asp:TextBox 
                    ID="TextBox20" runat="server" Width="90px" Enabled="False"></asp:TextBox>
                Y:<asp:TextBox ID="TextBox21" runat="server" Width="90px" Enabled="False"></asp:TextBox>
                Z:<asp:TextBox ID="TextBox22" runat="server" Width="90px" Enabled="False"></asp:TextBox>
                N:<asp:TextBox ID="TextBox23" runat="server" Width="90px" Enabled="False"></asp:TextBox>
                <br />
                in
                <asp:TextBox ID="TextBox24" runat="server" Enabled="False" Width="90px"></asp:TextBox>
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
            width: 432px;
        }
        .style2
        {
            width: 451px;
        }
        .style3
        {
            width: 432px;
            height: 20px;
        }
        .style4
        {
            width: 451px;
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
            width: 432px;
            height: 12px;
        }
        .style8
        {
            width: 451px;
            height: 12px;
        }
        .style9
        {
            height: 12px;
        }
    </style>
</asp:Content>

