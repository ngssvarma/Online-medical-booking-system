<%@ Page Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="Doctor.aspx.cs" Inherits="Admin_Doctor" Title="Untitled Page" StyleSheetTheme="SkinFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <style type="text/css">  
     .disenable {background-image: url('images/login-bg.jpg');}
      .bold{font-weight: bold;}
    </style>

    <center>

<script type="text/javascript" language="javascript">
    function Reset1_onclick() {

    }

</script>

    


    <div id="DoctorGrid" runat="server">
    <table width="100%" border="1" class="table1">
    
    <thead>
    <tr align="right">
    <td colspan="100%" class="td1">
    <a href="Doctor.aspx?action=Add" onclick="">New Doctor</a>
    </td>
    </tr>
    <tr>
    <th colspan="100%" class="td2"> List of Doctors</th>
    </tr>
        <tr>
            <th>Name</th>
            <th>Degree</th>
            <th>Available Days</th>
            <th>Available Time</th>
            <th>Action</th>
        </tr>
        </thead>
        <tbody id="trDoctorGrid" runat="server">
        
        </tbody>
        
        
        
    </table>
    </div>
    
    <div id="AddNewDoctor" runat="server">
       <form id="frmPatientRegister" runat="server">
        <table width="400" id="PatientRegistration" class="table1">
        <tbody>
        <tr>
        <th colspan="100" id="trDoctorHeader" runat="server" class="td1">New Doctor</th>
        </tr>
        </tbody>
            
            <tr>
                <td align="right" style="width:30%">Name
                </td>
                <td>
                <asp:TextBox ID="txtName" runat="server" ></asp:TextBox>                
                </td>
            </tr>
            
            <tr>
                <td align="right">
                    Degree&nbsp;</td>
                <td>
                <asp:TextBox ID="txtDegree" runat="server"></asp:TextBox>                
                </td>
            </tr>
            
            <tr>
                <td align="right">
                    Available days&nbsp;</td>
                <td>
                    <asp:CheckBox ID="chkSunday" runat="server" Text="Sunday" /><br />
                    <asp:CheckBox ID="chkMonday" runat="server" Text="Monday" /><br />
                    <asp:CheckBox ID="chkTuesday" runat="server" Text="Tuesday" /><br />
                    <asp:CheckBox ID="chkWednesday" runat="server" Text="Wednesday" /><br />
                    <asp:CheckBox ID="chkThursday" runat="server" Text="Thursday" /><br />
                    <asp:CheckBox ID="chkFriday" runat="server" Text="Friday" /><br />
                    <asp:CheckBox ID="chkSaturday" runat="server" Text="Saturday" /></td>
            </tr>
            <tr>
                <td align="right">
                    Available time</td>
                <td>
                    &nbsp;<asp:TextBox ID="txtAvailableTime" runat="server"></asp:TextBox></td>
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
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr id="trConfirmPassword">
                <td align="right">
                    Confirm Password</td>
                <td>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                    &nbsp;
                    <input id="Reset1" type="reset" value="Cancel" 
                        onclick="return Reset1_onclick()" class="button1" /></td>
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

