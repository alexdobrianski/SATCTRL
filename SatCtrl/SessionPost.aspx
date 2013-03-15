<%@ Page Title="SessionData Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="SessionPost.aspx.cs" Inherits="SatCtrl._SessionPost" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <p>
        post</p>
    <p>
        <asp:FormView ID="FormView1" runat="server" DataSourceID="SqlDataSource1" 
            AllowPaging="True" DefaultMode="Insert" 
            Width="174px">
            <EditItemTemplate>
                session_no:
                <asp:TextBox ID="session_noTextBox" runat="server" 
                    Text='<%# Bind("session_no") %>' />
                <br />
                packet_type:
                <asp:TextBox ID="packet_typeTextBox" runat="server" 
                    Text='<%# Bind("packet_type") %>' />
                <br />
                packet_no:
                <asp:TextBox ID="packet_noTextBox" runat="server" 
                    Text='<%# Bind("packet_no") %>' />
                <br />
                d_time:
                <asp:TextBox ID="d_timeTextBox" runat="server" Text='<%# Bind("d_time") %>' />
                <br />

                g_station:
                <asp:TextBox ID="g_stationTextBox" runat="server" 
                    Text='<%# Bind("g_station") %>' />
                <br />
                gs_time:
                <asp:TextBox ID="gs_timeTextBox" runat="server" Text='<%# Bind("gs_time") %>' />
                <br />

                package:
                <asp:TextBox ID="packageTextBox" runat="server" Text='<%# Bind("package") %>' />
                <br />
                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                    CommandName="Update" Text="Update" />
                &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                    CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </EditItemTemplate>
            <InsertItemTemplate>
                session_no:
                <asp:TextBox ID="session_noTextBox" runat="server" 
                    Text='<%# Bind("session_no") %>' />
                <br />
                packet_type:
                <asp:TextBox ID="packet_typeTextBox" runat="server" 
                    Text='<%# Bind("packet_type") %>' />
                <br />
                packet_no:
                <asp:TextBox ID="packet_noTextBox" runat="server" 
                    Text='<%# Bind("packet_no") %>' />
                <br />
                d_time:
                <asp:TextBox ID="d_timeTextBox" runat="server" Text='<%# Bind("d_time") %>' />
                <br />

                g_station:
                <asp:TextBox ID="g_stationTextBox" runat="server" 
                    Text='<%# Bind("g_station") %>' />
                <br />
                gs_time:
                <asp:TextBox ID="gs_timeTextBox" runat="server" Text='<%# Bind("gs_time") %>' />
                <br />

                package:
                <asp:TextBox ID="packageTextBox" runat="server" Text='<%# Bind("package") %>' />
                <br />
                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                    CommandName="Insert" Text="Insert" />
                &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                    CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </InsertItemTemplate>
            <ItemTemplate>
                session_no:
                <asp:Label ID="session_noLabel" runat="server" 
                    Text='<%# Bind("session_no") %>' />
                <br />
                packet_type:
                <asp:Label ID="packet_typeLabel" runat="server" 
                    Text='<%# Bind("packet_type") %>' />
                <br />
                packet_no:
                <asp:Label ID="packet_noLabel" runat="server" Text='<%# Bind("packet_no") %>' />
                <br />
                d_time:
                <asp:Label ID="d_timeLabel" runat="server" Text='<%# Bind("d_time") %>' />
                <br />

                g_station:
                <asp:Label ID="g_stationLabel" runat="server" Text='<%# Bind("g_station") %>' />
                <br />
                gs_time:
                <asp:Label ID="gs_timeLabel" runat="server" Text='<%# Bind("gs_time") %>' />
                <br />

                package:
                <asp:Label ID="packageLabel" runat="server" Text='<%# Bind("package") %>' />
                <br />
                <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" 
                    CommandName="New" Text="New" />
            </ItemTemplate>
        </asp:FormView>
    </p>
    <p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:missionlogConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:missionlogConnectionString.ProviderName %>" 
            SelectCommand="SELECT session_no, packet_type, packet_no, d_time, g_station, gs_time, package FROM mission_session ORDER BY session_no, packet_type, packet_no"
            
            InsertCommand="INSERT INTO mission_session(session_no, packet_type, packet_no, d_time, g_station, gs_time, package) VALUES (@session_no, @packet_type, @packet_no, @d_time, @g_station, @gs_time, @package)">

        <InsertParameters>
            <asp:Parameter Name="session_no" Type="String" />
            <asp:Parameter Name="packet_type" Type="String" />
            <asp:Parameter Name="packet_no" Type="String" />
            <asp:Parameter Name="d_time" Type="String" />
            <asp:Parameter Name="g_station" Type="String" />
            <asp:Parameter Name="gs_time" Type="String" />
            <asp:Parameter Name="package" Type="String" />
        </InsertParameters>


        </asp:SqlDataSource>
        <br />
    </p>
</asp:Content>
