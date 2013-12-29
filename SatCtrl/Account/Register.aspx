<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SatCtrl.Account.RegUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        Register new user with Nanosatellite mission control</h1>
    <p>
        &nbsp;</p>
    <p>
        <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" 
            DisplayCancelButton="True" 
            FinishDestinationPageUrl="~/About.aspx" LoginCreatedUser="False"
            onsendingmail="Createuserwizard1_SendingMail" 
            CreateUserButtonText="Register new User" 
            
            DuplicateEmailErrorMessage="The e-mail address and user name that you entered is already in use. Please enter a different e-mail address and user name." 
            DuplicateUserNameErrorMessage="The e-mail address and user name that you entered is already in use. Please enter a different e-mail address and user name." 
            CompleteSuccessText="Your account has been successfully created. Follow E-mail to to finish the registration." InvalidPasswordErrorMessage="Password length minimum: {8}. Non-alphanumeric characters required: {1}."
            >
            <MailDefinition BodyFileName="~/Account/userEmailReg.txt" From="adobri@shaw.ca" 
                Subject="user registration">
            </MailDefinition>
            <WizardSteps>
                <asp:CreateUserWizardStep runat="server" Title="Register Your New Account" />
                <asp:CompleteWizardStep runat="server" 
                    Title="Follow E-mail to to finish the registration." >
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td align="center" colspan="2">
                                    Follow E-mail to to finish the registration.</td>
                            </tr>
                            <tr>
                                <td>
                                    Your account has been successfully created. Follow E-mail to to finish the 
                                    registration.</td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="ContinueButton" runat="server" CausesValidation="False" 
                                        CommandName="Continue" PostBackUrl="~/About.aspx" Text="Continue" 
                                        ValidationGroup="CreateUserWizard1" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:CompleteWizardStep>
            </WizardSteps>

        </asp:CreateUserWizard>
    </p>
</asp:Content>
