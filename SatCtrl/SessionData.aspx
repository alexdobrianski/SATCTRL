<%@ Page Title="SessionData Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="SessionData.aspx.cs" Inherits="SatCtrl._SessionData" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <p>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            DataSourceID="SqlDataSource1" Width="856px" Height="429px" PageSize="25" 
            style="margin-bottom: 106px">
            <Columns>
                <asp:BoundField DataField="session_no" HeaderText="session_no" 
                    SortExpression="session_no" />
                <asp:BoundField DataField="packet_type" HeaderText="packet_type" 
                    SortExpression="packet_type" />
                <asp:BoundField DataField="packet_no" HeaderText="packet_no" 
                    SortExpression="packet_no" />
                <asp:BoundField DataField="d_time" HeaderText="d_time" 
                    SortExpression="d_time" />
                <asp:BoundField DataField="g_station" HeaderText="g_station" 
                    SortExpression="g_station" />
                <asp:BoundField DataField="gs_time" HeaderText="gs_time" 
                    SortExpression="gs_time" />
                <asp:BoundField DataField="package" HeaderText="package" 
                    SortExpression="package" />
            </Columns>
        </asp:GridView>
</p>
    <p>
        data</p>
    <p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:missionlogConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:missionlogConnectionString.ProviderName %>" 
            
            SelectCommand="SELECT session_no, packet_type, packet_no, d_time, g_station, gs_time, package FROM mission_session ORDER BY d_time desc">
        </asp:SqlDataSource>
    <br />
</p>
</asp:Content>
