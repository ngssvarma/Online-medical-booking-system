<%@ Page Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Admin_ChangePassword" Title="Untitled Page" StyleSheetTheme="SkinFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <div>
    <form id="frmLogin" runat="server">
        <table width="400" class="table1">
            <tr>
                <td style="width: 100%" colspan="2" align="center" class="td1">
                    Change Password</td>
            </tr>
            <tr>
                <td style="width: 50%" align="right">
                    &nbsp;</td>
                <td style="width: 50%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 50%" align="right">
                    Old Password:
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="txtOldPassword" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 50%" align="right">
                    New
                    Password
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="txtNewPassword" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 50%; height: 19px;" align="right">
                    Confirm Password&nbsp;</td>
                <td style="width: 50%; height: 19px;">
                    <asp:TextBox ID="txtConfirmPassword" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
            <td></td>
            <td>
                <asp:Button ID="cmdSubmit" runat="server" Text="Change"  
                    onclick="cmdSubmit_Click" />
                &nbsp;
                <input id="Reset1" type="reset" value="Reset" onclick="return Reset1_onclick()" class ="button1" /></td>
            </tr>
            <tr>
                <td style="width: 100%" colspan="2">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        </form>
    </div>    
</center>
</asp:Content>

