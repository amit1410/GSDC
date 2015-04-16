<%@ Page Title="Leave Details" Language="C#" MasterPageFile="~/LeaveTracker/LeaveTracker.master" AutoEventWireup="true" CodeBehind="LeaveDetails.aspx.cs" Inherits="GSDC.LeaveTracker.LeaveDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentLeaveTracker" runat="server">
     <asp:GridView ID="gvdDetailedLeaves" Visible="false" CssClass="gridview" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found">
        <Columns>
            <asp:BoundField DataField="EmployeeName" HeaderText="Name" />
            <asp:BoundField DataField="Start_Date" HeaderText="Leave Start Date" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="End_Date" HeaderText="Leave End Date" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Short_Desc" HeaderText="Leave Type" />
        </Columns>
        <EmptyDataRowStyle CssClass="empty_row" ForeColor="White" />
    </asp:GridView>
    <asp:GridView ID="gvmDetailedLeaves" Visible="false" CssClass="gridview" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found">
        <Columns>
            <asp:BoundField DataField="EmployeeName" HeaderText="Name" />
            <asp:BoundField DataField="Total" HeaderText="Total Leaves" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Balance" HeaderText="Balance Leaves" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Short_Desc" HeaderText="Leave Type" />
        </Columns>
        <EmptyDataRowStyle CssClass="empty_row" ForeColor="White" />
    </asp:GridView>
    <asp:GridView ID="gvOutage" Visible="false" CssClass="gridview" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found">
        <Columns>
            <asp:BoundField DataField="EmployeeName" HeaderText="Name" />
            <asp:BoundField DataField="Start_Date" HeaderText="Leave Start Date" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="End_Date" HeaderText="Leave End Date" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Short_Desc" HeaderText="Leave Type" />
        </Columns>
        <EmptyDataRowStyle CssClass="empty_row" ForeColor="White" />
    </asp:GridView>
    <script type="text/javascript">
        $("#cssmenu li").eq(3).addClass('active');
    </script>
</asp:Content>
