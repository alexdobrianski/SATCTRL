<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="SatCtrl.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
    .style2
    {
        width: 163px;
    }
    .style3
    {
        width: 25px;
    }
    .style4
    {
        width: 172px;
    }
    .style5
    {
        width: 130px;
    }
    .style6
    {
        width: 38px;
    }
</style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        About
    </h2>
    <p>
        &nbsp;Main goal for Cubest testing flight is to confirm orientation of the 
        Cubesat in particular moment of a time.</p>
<p>
        <table frame="box" rules="all" style="width: 100%;">
            <tr>
                <td class="style2">
                <img alt="" src="Img/mission_ctrl.JPG" 
                        width="320" />
                <br />
                    Mission Control WebSite</td>
                <td class="style4">
                    Session to Ground Station</td>
                <td class="style3">
                    &lt;-&gt;</td>
                <td class="style5">
                
                    <img alt="" height="320" src="Img/GrStn.JPG" /><br />
                    <a href=GrStn.exe> GrStn (Vancouver) host software</a></td>
                <td class="style6">
                    &lt;-&gt;</td>
                <td>
                    Cubesat/Craft</td>
            </tr>
            <tr>
                <td class="style2">
                    </td>
                <td class="style4">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td class="style5">
                    </td>
                <td class="style6">
                    &lt;-&gt;</td>
                <td>
                    <img alt="" dir="rtl" height="320" src="Img/planb.jpg" /> <br />
                    Ground station controller</td>
            </tr>

            <tr>
                <td class="style2">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    |&nbsp;&nbsp;&nbsp; &nbsp;</td>
                <td class="style4">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td class="style5">
                    Another GrStn</td>
                <td class="style6">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    dasd<img 
                        alt="" src="Img/cubesat_ctrl.JPG" width="320" />asd
                    <br />
                    Sattelite Control Panel</td>
                <td class="style4">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td class="style5">
                    &nbsp;</td>
                <td class="style6">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <img alt="" src="Img/sessionDB.JPG" width="320" /><br />
                    Sessions DB</td>
                <td class="style4">
                    -&gt; (pictures, telemetry, etc.)</td>
                <td class="style3">
                    &lt;-&gt;</td>
                <td class="style5">
                    Fixing broken packets</td>
                <td class="style6">
                    &lt;-&gt;</td>
                <td>
                    packet&#39;s fixing distributed stations</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style4">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td class="style5">
                    &nbsp;</td>
                <td class="style6">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <img alt="" src="Img/tra_sim.JPG" width="320" /><br />
                    Simulation engine - runs separatly<a href="tra.exe">1. click to download and save executable TRA.EXE</a>.<br />
                    <a href="trabat.bat">2.Click to download and save TRABAT.BAT file</a> to run simulation in loop.<br />
                    Batch file requare to adjust IP adress.<br />
                    <a href="bin410.bin">3.Click to download JPL BIN410.BIN (sabe in same directory)</a><br />
                </td>
                <td class="style4">
                    GPS satelites</td>
                <td class="style3">
                    &lt;-&gt;</td>
                <td class="style5">
                    official TLE</td>
                <td class="style6">
                    &lt;-&gt;</td>
                <td>
                    simulation for a Cubesat position&#39;s calculations</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style4">
                    Cubesat simulation</td>
                <td class="style3">
                    &lt;-&gt;</td>
                <td class="style5">
                    testing equipment</td>
                <td class="style6">
                    -&gt;</td>
                <td>
                    Mission Control WebSite</td>
            </tr>
        </table>
    </p>
</asp:Content>
