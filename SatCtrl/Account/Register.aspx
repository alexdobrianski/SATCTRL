<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SatCtrl.Account.RegUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        Create new account</h1>
    <p>
        <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" 
            DisplayCancelButton="True" DisplaySideBar="True" 
            FinishDestinationPageUrl="~/About.aspx" LoginCreatedUser="False">
            <MailDefinition BodyFileName="~/Account/userEmailReg.txt" From="adobri@shaw.ca" 
                Subject="user registration">
            </MailDefinition>
            <WizardSteps>
                <asp:CreateUserWizardStep runat="server" />
                <asp:CompleteWizardStep runat="server" />
            </WizardSteps>
        </asp:CreateUserWizard>
    </p>
</asp:Content>
