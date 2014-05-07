<%@ Page Title="SessionData Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="SessionData.aspx.cs" Inherits="SatCtrl._SessionData" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <p>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
            DataSourceID="SqlDataSource1" Width="1200px" Height="16px" 
            onrowdatabound="CustomersGridView_RowDataBound"
            style="margin-bottom: 106px" HorizontalAlign="Center" PageSize="25">
            <Columns>
                <asp:BoundField DataField="session_no" HeaderText="session" 
                    SortExpression="session_no" >
                <ItemStyle Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="packet_type" HeaderText="type" 
                    SortExpression="packet_type" >
                <ItemStyle Width="20px" />
                </asp:BoundField>
                <asp:BoundField DataField="packet_no" HeaderText="packet" 
                    SortExpression="packet_no" >
                <ItemStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="d_time" HeaderText="d&amp;t" 
                    SortExpression="d_time" >
                <ItemStyle Wrap="False" Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="g_station" HeaderText="GrSt" 
                    SortExpression="g_station" >
                <ItemStyle Width="20px" />
                </asp:BoundField>
                <asp:BoundField DataField="gs_time" HeaderText="GrStn d&amp;t" 
                    SortExpression="gs_time" >
                <ItemStyle Wrap="False" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="package" HeaderText="package" 
                    SortExpression="package" >
                <ItemStyle Wrap="True" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:CheckBox ID="CheckBoxHex" runat="server" AutoPostBack="True" Text="Hex" />
</p>
    <p>
        data</p>
    <p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:missionlogConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:missionlogConnectionString.ProviderName %>" 
            
            SelectCommand="SELECT session_no, packet_type, packet_no, d_time, g_station, gs_time, package FROM mission_session ORDER BY d_time desc, packet_no desc">
        </asp:SqlDataSource>
    <br />
</p>
</asp:Content>
