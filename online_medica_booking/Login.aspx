<%@ Page Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" Title="Untitled Page" StyleSheetTheme="SkinFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">  
     #trmenu
     {
     	display:none;
     }
    </style>
    <center>
    <div>
        <form id="frmLogin" runat="server">
        <table width="400" class="table1">
            <tr>
                <td style="width: 100%" colspan="2" align="center" class="td1">
                
                    Login</td>
            </tr>
            

            <tr>
                <td style="width: 50%" align="right">
                    &nbsp;</td>
                <td style="width: 50%">
                    &nbsp;</td>
            </tr>
            
            <tr>
                <td style="width: 50%" align="right">
                Username:                    
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 50%" align="right">
                    Password
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 50%; height: 19px;" align="right">
                    Type
                </td>
                <td style="width: 50%; height: 19px;">
                    &nbsp;<asp:DropDownList ID="lstType" runat="server" 
                        onselectedindexchanged="lstType_SelectedIndexChanged">
                        <asp:ListItem>Admin</asp:ListItem>
                        <asp:ListItem>Doctor</asp:ListItem>
                        <asp:ListItem>Patients</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
            <td></td>
            <td>
                <asp:Button ID="cmdSubmit" runat="server" Text="Login" OnClick="cmdSubmit_Click" />
                &nbsp;<input id="Reset1" type="reset" value="reset" class="button1" /></td>
            </tr>
            <tr>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 100%; border-top-style: solid; border-top-width: 1px;" 
                    colspan="2" align="center">
                
                    &nbsp;<asp:Label ID="lblMessage" runat="server"></asp:Label>
                &nbsp;</td>
            </tr>            
            <tr>
                <td style="width: 100%" colspan="2" align="center">
                
                    Forgot Password Click <a href="ForgotPassword.aspx">Here</a></td>
            </tr>
            <tr>
                <td style="width: 100%" colspan="2" align="center">
                    New Patient Click <a href="Patient.aspx?action=Register">Here</a>
                    </td>
            </tr>
        </table>
        </form>
    </div>
    
</center>
</asp:Content>

