<%@ Page Title="Post Page" Language="C#" MasterPageFile="~/Empty.master" AutoEventWireup="true"
    CodeBehind="Post.aspx.cs" Inherits="SatCtrl._Post" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<p>post MAX_session_no=<%=MAX_session_no%></p><p>MAX_packet_no=<%=MAX_packet_no%>&nbsp;</p><p>&nbsp;</p><p><br /></p>
</asp:Content>
