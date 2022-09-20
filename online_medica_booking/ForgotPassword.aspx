<%@ Page Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" Title="Untitled Page" StyleSheetTheme="SkinFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <form id="frmforgotPassword" runat="server">
            <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" class="table1">
                            <tr>
                                <td align="center" colspan="2" class="td1">
                                    Forgot Your Password?</td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    Enter your User Name to receive your password.</td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="txtUsername">User Name:</asp:Label></td>
                                <td style="width: 225px">
                                    <asp:TextBox ID="txtUsername" runat="server" Width="221px"></asp:TextBox>&nbsp;
                                </td>
                            </tr>

                            <tr>
                                <td align="right">
                                    Type</td>
                                <td style="width: 225px">
                                    <asp:DropDownList ID="lstType" runat="server" AutoPostBack="True">
                                        <asp:ListItem>Admin</asp:ListItem>
                                        <asp:ListItem>Doctor</asp:ListItem>
                                        <asp:ListItem>Patients</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            
                            <tr>
                            <td></td>
                                <td align="left" colspan="2">
                                    <asp:Button ID="cmdSubmit" runat="server" CommandName="Submit" Text="Submit" ValidationGroup="PasswordRecovery1" OnClick="cmdSubmit_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="color: red">
                                    <asp:Literal ID="lblMessage" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </form>
            </center>
</asp:Content>

