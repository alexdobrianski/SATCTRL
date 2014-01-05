<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="TraCalcCraft.aspx.cs" Inherits="SatCtrl._TraCalcCraft" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table style="width:100%;" frame="vsides">    
        <tr>
            <td class="style10">
                Craft&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                Calculation:
                <asp:Label ID="LabelUserName" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style7">
                Total mass<asp:TextBox ID="TextBoxProbM" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style11">
                Landed mass<asp:TextBox 
                    ID="TextBoxProbMTarget" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style11">
                &nbsp;</td>
        </tr>

        <tr>
            <td class="style11">
                Engine (1 impulse) profile:<asp:CheckBox 
                    ID="CheckBox1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style11">
                    <img alt="" src="" style="width: 441px; height: 237px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style11">
                    <asp:TextBox ID="TextBox8" runat="server" Height="71px" ReadOnly="True" 
            Rows="2" TextMode="MultiLine" Width="920px" Wrap="False" 
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
        </tr>
        <tr>
            <td class="style11">
                &nbsp;</td>
        </tr>

        <tr>
            <td class="style11">
                Engine (2 impulse) profile:<asp:CheckBox 
                    ID="CheckBox2" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style11">
                    <img alt="" src="" style="width: 441px; height: 237px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:FileUpload ID="FileUpload2" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style11">
                    <asp:TextBox ID="TextBox11" runat="server" Height="71px" ReadOnly="True" 
            Rows="2" TextMode="MultiLine" Width="920px" Wrap="False" 
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
                <input id="Button3" type="button" value="edit" /></td>
        </tr>
        <tr>
            <td class="style11">
                &nbsp;</td>
        </tr>

        <tr>
            <td class="style11">
                Engine (3 impulse) profile:<asp:CheckBox 
                    ID="CheckBox3" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style11">
                    <img alt="" src="" style="width: 441px; height: 237px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:FileUpload ID="FileUpload3" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style11">
                    <asp:TextBox ID="TextBox12" runat="server" Height="71px" ReadOnly="True" 
            Rows="2" TextMode="MultiLine" Width="920px" Wrap="False" 
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
                <input id="Button4" type="button" value="edit" /></td>
        </tr>

        <tr>
            <td class="style11">
                &nbsp;</td>
        </tr>

        <tr>
            <td class="style11">
                Engine (4 impulse) profile:<asp:CheckBox 
                    ID="CheckBox4" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style11">
                    <img alt="" src="" style="width: 441px; height: 237px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:FileUpload ID="FileUpload4" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style11">
                    <asp:TextBox ID="TextBox13" runat="server" Height="71px" ReadOnly="True" 
            Rows="2" TextMode="MultiLine" Width="920px" Wrap="False" 
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
                <input id="Button5" type="button" value="edit" /></td>
        </tr>
        <tr>
            <td class="style11">
                &nbsp;</td>
        </tr>

        <tr>
            <td class="style11">
                Engine (5 impulse) profile:<asp:CheckBox 
                    ID="CheckBox5" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style11">
                    <img alt="" src="" style="width: 441px; height: 237px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:FileUpload ID="FileUpload5" runat="server" />
            </td>
        </tr>

        <tr>
            <td class="style11">
                    <asp:TextBox ID="TextBox14" runat="server" Height="71px" ReadOnly="True" 
            Rows="2" TextMode="MultiLine" Width="920px" Wrap="False" 
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
                <input id="Button6" type="button" value="edit" /></td>
        </tr>
        <tr>
            <td class="style11">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style11">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style11">
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
        .style7
        {
            width: 1285px;
            height: 12px;
        }
        .style10
        {
            width: 1285px;
            font-size: small;
        }
        #TextArea1
        {
            width: 236px;
        }
        #Select1
        {
            width: 144px;
        }
        .style11
        {
            width: 1285px;
            height: 20px;
        }
    </style>
</asp:Content>

