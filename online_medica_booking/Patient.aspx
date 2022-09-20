<%@ Page Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="Patient.aspx.cs" Inherits="Admin_Patient" Title="Untitled Page" StyleSheetTheme="SkinFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">  
    .disenable {background-image: url('images/login-bg.jpg');}
      .bold{font-weight: bold;}
</style>
<center>
<div id="PatientGrid" runat="server">
    <table width="100%" border="1" cellpadding="0" cellspacing="0" class="table1">
    
    <thead class=".td3">
    <tr align="right">
    <td colspan="100%" class="td1">
        <a href="Patient.aspx?action=Add">New Patient</a>
    </td>
    </tr>
    <tr>
    <th colspan="100%" class="td2"> List of Patients</th>
    </tr>
        <tr>

            <th>Name</th>
            <th class=".td3">Address</th>
            <th class=".td3">City</th>
            <th class=".td3">Zip</th>
            <th class=".td3">Contact No</th>                        
        </tr>
        </thead>
        <tbody id="trPatientGrid" runat="server">
        
        </tbody>
        
        
        
    </table>
    </div>
    
    <div id="AddNewPatient" runat="server">
    <form id="frmPatientRegister" runat="server">
        <table width="400" id="PatientRegistration" class="table1">
        <tbody>
        <tr>
        <th colspan="100" id="trPatientHeader" runat="server" class="td1">New Patient Registration
        </th>
        </tr>
        </tbody>
            
            <tr>
                <td align="right" style="width:40%">Name
                </td>
                <td>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>                
                </td>
            </tr>
            
            <tr>
                <td align="right">
                    Address&nbsp;</td>
                <td>
                <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>                
                </td>
            </tr>
            
            <tr>
                <td align="right">
                    City&nbsp;</td>
                <td>
                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>                
                </td>
            </tr>
            <tr>
                <td align="right">
                    Zip</td>
                <td>
                    <asp:TextBox ID="txtZip" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    Contact&nbsp; No.</td>
                <td>
                    <asp:TextBox ID="txtContactNo" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    Email&nbsp; Id(Usename)</td>
                <td>
                    <asp:TextBox ID="txtEmailId" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    Password</td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox></td>
            </tr>
            <tr id="trConfirmPassword">
                <td align="right">
                    Confirm Password</td>
                <td>
                    <asp:TextBox ID="txtConfirmPassword" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" 
                        OnClick="btnPatientRegister_Click"/>
                    &nbsp;
                    <input id="Reset1" type="reset" value="Cancel" class="button1" /></td>
            </tr>
            <tr>
                <td align="center" colspan ="100%">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </td>
                
            </tr>
        </table>
        </form>
    </div>
</center>
</asp:Content>

