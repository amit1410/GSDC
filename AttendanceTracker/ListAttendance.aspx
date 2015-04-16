<%@ Page Title="List Attendance" Language="C#" MasterPageFile="~/AttendanceTracker/AttendanceTracker.master" AutoEventWireup="true" CodeBehind="ListAttendance.aspx.cs" Inherits="GSDC.AttendanceTracker.ListAttendance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentAttendanceTracker" runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#chk").click(function (event) {

                alert(this.checked);
                $(".chk :checkbox").prop('checked', this.checked);
            });
        });
    </script>
    <center>
    <asp:DropDownList ID="ddlMonths" CssClass="dropdown" runat="server" OnSelectedIndexChanged="ddlMonths_SelectedIndexChanged" AutoPostBack="true">
        
    </asp:DropDownList><br /><br />
        <asp:HiddenField ID="hfEmpID" runat="server" />
        <asp:HiddenField ID="hfEmpName" runat="server" />
        <asp:Button ID="btnApprove" runat="server" CssClass="button" Height="40px" Width="100px" Text="Approve" OnClick="btnApprove_Click" /><br /><br />
    <asp:GridView ID="gvAttendanceList" CssClass="gridview" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>
                    <input type="checkbox" id="chk" />                   
                </HeaderTemplate>
                <ItemTemplate>
                   
                    <asp:CheckBox ID="chk" CssClass="chk" Visible='<%# Bind("Approved") %>' ValidationGroup='<%# Bind("EmployeeID") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Code" ItemStyle-Width="150">
                <ItemTemplate>
                   <asp:LinkButton ID="lnkEmpID" OnClick="lnkEmpID_Click" PostBackUrl="~/AttendanceTracker/Details.aspx" CommandName='<%# Bind("EmployeeName") %>' runat="server" Text='<%# Bind("Employee_Code") %>' CommandArgument='<%# Bind("EmployeeID") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Name" DataField="EmployeeName" />
           <asp:BoundField HeaderText="Email" DataField="Email" />
             <asp:BoundField HeaderText="Days Present" DataField="Office" NullDisplayText="0" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField HeaderText="EL" DataField="EL" NullDisplayText="0" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center"/>
             <asp:BoundField HeaderText="CL" DataField="CL" NullDisplayText="0" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField HeaderText="SL" DataField="SL" NullDisplayText="0" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center"/>
             <asp:BoundField HeaderText="WFH" DataField="WFH" NullDisplayText="0" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <EmptyDataRowStyle CssClass="empty_row" ForeColor="White" />
    </asp:GridView>
        </center>
    <script type="text/javascript" language="javascript">
        $("#cssmenu li").eq(1).addClass('active');
    </script>
</asp:Content>
