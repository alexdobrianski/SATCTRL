<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="TraCalc.aspx.cs" Inherits="SatCtrl._TraCalc" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table style="width:100%;" frame="vsides">    
        <tr>
            <td class="style10">
                Craft</td>
            <td class="style2">
                Initial Orbit</td>
            <td>
                Target</td>
        </tr>
        <tr>
            <td class="style7">
                Total mass</td>
            <td class="style8">
                TLE1:<asp:TextBox ID="TextBox3" runat="server" Width="473px"></asp:TextBox>
            </td>
            <td class="style9">
                Latitude&nbsp;&nbsp;
                <asp:TextBox ID="TextBox1" runat="server" Width="92px"></asp:TextBox>
&nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                Landed mass</td>
            <td class="style4">
                TLE2:<asp:TextBox ID="TextBox4" 
                    runat="server" Width="471px"></asp:TextBox>
            </td>
            <td class="style5">
                Longitude:<asp:TextBox ID="TextBox2" runat="server" Width="86px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td class="style3">
                Engine 1 (impulse) profile:<input 
                    id="Checkbox1" checked="checked" type="checkbox" /></td>
            <td class="style4">
                Orbital parameters:</td>
            <td class="style5">
                </td>
        </tr>
        <tr>
            <td class="style3">
                    <img alt="" src="" style="width: 288px; height: 114px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button1" type="button" value="upload graph" /></td>
            <td class="style4">
                inclination
                <asp:TextBox ID="TextBox5" runat="server" Width="100px"></asp:TextBox>
                <br />
                perigee<asp:TextBox ID="TextBox6" 
                    runat="server" Width="103px"></asp:TextBox>
&nbsp;apogee<asp:TextBox ID="TextBox7" runat="server" Width="104px"></asp:TextBox>
                Period:
                <asp:TextBox ID="TextBox9" runat="server" Width="99px"></asp:TextBox>
                <br />
                perigee time<asp:TextBox ID="TextBox10" runat="server" Width="87px"></asp:TextBox>
&nbsp;perigee longitude<asp:TextBox ID="TextBox11" runat="server" Width="97px"></asp:TextBox>
&nbsp;</td>
            <td class="style5">
                <img alt="" src="" style="height: 109px; width: 255px" /></td>
        </tr>
        <tr>
            <td class="style3">
                    <asp:TextBox ID="TextBox8" runat="server" Height="33px" ReadOnly="True" 
            Rows="2" TextMode="MultiLine" Width="420px" Wrap="False" 
                    style="margin-bottom: 0px">&lt;CT:setting name=&quot;EngineImpuse&quot; value=&quot;1.0&quot; /&gt;
    &lt;CT:setting name=&quot;EngineOnSatellite&quot; value=&quot;0&quot; /&gt;
    &lt;CT:setting name=&quot;PropCoeff&quot; value=&quot;0.25&quot; /&gt;
    &lt;!-- search for ==1 Apogee ==0 Perigee --&gt;
    &lt;CT:setting name=&quot;SearchFor&quot; value=&quot;1.0&quot; /&gt;
    &lt;!-- set impulses in a time --&gt;
    &lt;CT:setting name=&quot;Weight&quot; value=&quot;17.157&quot; /&gt;
    &lt;!-- iteration per sec from engine&#39;s plot --&gt;
    &lt;CT:setting name=&quot;DeltaT&quot; value=&quot;5.0&quot; /&gt;
    &lt;CT:setting name=&quot;FireTime&quot; value=&quot;19000.0&quot; /&gt;   
    &lt;CT:setting name=&quot;FireTime&quot; value=&quot;18845.0&quot; /&gt;   
    &lt;CT:setting name=&quot;FireTime&quot; value=&quot;16109.0&quot; /&gt;   


    &lt;!-- first angle from center of closest body to fire direction 
         in plane over centre and vector velocity --&gt;
    &lt;CT:setting name=&quot;FireAng1&quot; value=&quot;90.0&quot; /&gt;
    &lt;!-- second angle from vector of velocity  
         in plane perpendicular to direction centre of body --&gt;
    &lt;CT:setting name=&quot;FireAng1&quot; value=&quot;0.0&quot; /&gt;
    &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;0.0&quot; /&gt;
    &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3400.0&quot; /&gt;
    &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3000.0&quot; /&gt;
    &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3100.0&quot; /&gt;
    &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3500.0&quot; /&gt;

    &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3550.0&quot; /&gt;
    &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3600.0&quot; /&gt;
    &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3600.0&quot; /&gt;
    &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3700.0&quot; /&gt;
    &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3750.0&quot; /&gt;

    &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3800.0&quot; /&gt;
    &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3850.0&quot; /&gt;
    &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3900.0&quot; /&gt;                                      
    &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3950.0&quot; /&gt;
    &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;4000.0&quot; /&gt;

    &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;4000.0&quot; /&gt;
    &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;4000.0&quot; /&gt;
    &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;4000.0&quot; /&gt;
    &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;4000.0&quot; /&gt;
    &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;4000.0&quot; /&gt;
       
  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3980.0&quot; /&gt;
  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3960.0&quot; /&gt;
  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3940.0&quot; /&gt;
  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3920.0&quot; /&gt;
  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3900.0&quot; /&gt;

  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3885.0&quot; /&gt;
  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3850.0&quot; /&gt;
  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3815.0&quot; /&gt;
  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3780.0&quot; /&gt;
  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3745.0&quot; /&gt;

  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3710.0&quot; /&gt;
  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3695.0&quot; /&gt;
  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3680.0&quot; /&gt;
  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3665.0&quot; /&gt;
  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3650.0&quot; /&gt;

  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3600.0&quot; /&gt;
  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3500.0&quot; /&gt;
  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3500.0&quot; /&gt;
  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3400.0&quot; /&gt;
  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;3150.0&quot; /&gt;

  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;2000.0&quot; /&gt;
  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;250.0&quot; /&gt;
  &lt;CT:setting name=&quot;ImplVal&quot; value=&quot;0.0&quot; /&gt;
  </asp:TextBox>
                <input id="Button2" type="button" value="edit" /></td>
            <td class="style4">
                Position/Speed</td>
            <td class="style5">
                </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style4">
                X:<asp:TextBox ID="TextBox12" runat="server" Width="99px"></asp:TextBox>
                Y:<asp:TextBox ID="TextBox13" runat="server" Width="99px"></asp:TextBox>
                Z<asp:TextBox ID="TextBox14" runat="server" Width="96px"></asp:TextBox>
            </td>
            <td class="style5">
                </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style4">
                Vx<asp:TextBox ID="TextBox15" runat="server" Width="91px"></asp:TextBox>
                Vy<asp:TextBox ID="TextBox16" runat="server" Width="98px"></asp:TextBox>
                Vz<asp:TextBox ID="TextBox17" runat="server" Width="85px"></asp:TextBox>
            </td>
            <td class="style5">
                </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
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
        .style3
        {
            width: 476px;
            height: 20px;
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
        .style7
        {
            width: 476px;
            height: 12px;
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
        .style10
        {
            width: 476px;
            font-size: small;
        }
        #TextArea1
        {
            width: 236px;
        }
        #Select1
        {
            width: 412px;
        }
    </style>
</asp:Content>

