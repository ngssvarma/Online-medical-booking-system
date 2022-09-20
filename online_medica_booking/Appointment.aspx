<%@ Page Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="Appointment.aspx.cs" Inherits="Admin_Appointment" Title="Untitled Page" StyleSheetTheme="SkinFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
    <div id="AppointmentGrid" runat="server">
<form id="frmAppointmentGrid" runat="server">
    <table width="100%" border="1" class="table1">
    
    <thead>
    <tr>
    <td colspan="100%" align="right">    
                       Select Doctor&nbsp; 
                       <asp:DropDownList ID="ddlSearchDoctor" runat="server">
                    </asp:DropDownList>
                    &nbsp;Select Patient
                    <asp:DropDownList ID="ddlSearchPatients" runat="server">
                    </asp:DropDownList>
                    &nbsp;Search By
                    <asp:DropDownList ID="ddlSearchBy" runat="server" >
                    <asp:ListItem Text="Doctor" Value="Doctor"></asp:ListItem>
                    <asp:ListItem Text="Patients" Value="Patients"></asp:ListItem>
                    <asp:ListItem Text="Both" Value="Both"></asp:ListItem>
                       </asp:DropDownList>
                       
                       &nbsp;<asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" Text="Go" />
                       </td>                       
                       

    </tr>
    <tr>
        <td colspan="100%" align="right" class="td2">
                            <a href="Appointment.aspx?action=Add" onclick="">New Appointment</a>
                       </td>
    </tr>
    <tr>
    <th colspan="100%" class="td1"> List of Appointments</th>
    </tr>
        <tr>
        <th>Sr.</th> 
        <th> Appt Date </th> 
        <th> Appt Time </th> 
        <th> Patient Name </th> 
        <th>Doctor Name </th> 
        <th> Status</th> 
        <th id="thDoctorRemarks" runat="server">Action</th>          
        </tr>
        </thead>
        <tbody id="trAppointmentGrid" runat="server">
        
        </tbody>    
        
        
    </table>
    </form>
    </div>
    
    <div id="AddNewAppointment" runat="server">
    <form id="frmAppointment" runat="server">
        <table width="400" id="PatientRegistration" class="table1">
        <tbody>
        <tr>
        <th colspan="100" class="td1">New Appointment</th>
        </tr>
        </tbody>
            
            <tr>
                <td align="right" style="width:40%">
                    Date&nbsp;</td>
                <td>
                <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>                
                </td>
            </tr>
            
            <tr>
                <td align="right">
                    Time</td>
                <td>
                    <asp:TextBox ID="txtTime" runat="server"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td align="right">
                    Doctor Name</td>
                <td>
                    <asp:DropDownList ID="ddlDoctor" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Patient Name</td>
                <td>
                    <asp:DropDownList ID="ddlPatients" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Status</td>
                <td>
                    <asp:TextBox ID="txtStatus" runat="server" Height="22px">Pending</asp:TextBox></td>
            </tr>
           <%-- <tr>
                <td align="right">
                    Doctor Remarks</td>
                <td>
                    <asp:TextBox ID="txtDoctorRemarks" runat="server" Height="49px" 
                        ontextchanged="TextBox1_TextChanged"></asp:TextBox></td>
            </tr>
            <tr id="trConfirmPassword">
                <td align="right">
                    Doctor Entry Time</td>
                <td>
                    <asp:TextBox
                        ID="txtDoctorEntryTime" runat="server" Width="99px"></asp:TextBox></td>
            </tr>
--%>            <tr>
                <td align="right">
                </td>
                <td>
                    <asp:Button ID="btnAddAppointment" runat="server" Text="Add" 
                        OnClick="btnPatientRegister_Click" EnableTheming="False" />
                    &nbsp;<input id="Reset1" type="reset" value="Cancel" class="button1" /></td>
            </tr>
            <tr>
                <td align="center" colspan ="100%">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </td>
                
            </tr>
        </table>
        </form>
    </div>
    
    <div id="divAddRemarks" runat="server">
    <form id="frmAddRemarks" runat="server">
    <table width="400" class="table1">
        <tr>
            <th colspan="2">Remarks</th>
            
        </tr>
        <tr>
            <td style="width:50%" align="right">
                &nbsp;DateTime</td>
            <td>
                <asp:TextBox ID="txtRemarksDate" runat="server" BackColor="White" 
                    BorderStyle="None" Enabled="False"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td align="right">
                Remarks
            </td>
            <td>
                <asp:TextBox ID="txtRemarks" runat="server" Height="83px" TextMode="MultiLine" 
                    Width="300px" MaxLength="500"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td align="right">
                &nbsp;</td>
            <td>
                <asp:Button ID="btnAddRemarks" runat="server" onclick="btnAddRemarks_Click" 
                    Text="Add" />
                &nbsp;
                <input id="Reset2" type="reset" value="reset" class="button1" /></td>
            
        </tr>
    </table>
    </form>
    </div>
 </center>   
</asp:Content>

