<%@ Page Title="My Requests" Language="C#" MasterPageFile="~/LeaveTracker/LeaveTracker.master" AutoEventWireup="true" CodeBehind="MyRequests.aspx.cs" Inherits="GSDC.LeaveTracker.MyRequests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentLeaveTracker" Runat="Server">
    <script>
        function ConfirmCancel(ctrl) {
            if (confirm("Are you sure you want to Cancel")) {
                return true;
            }
            else {
                return false;
            }

        }
    </script>
    <br />
    <div class="headerContainer" style="width:100%">All Leave Requests</div>
    <br />
    <asp:GridView ID="gvRequests" CssClass="gridview" OnRowDataBound="gvRequests_RowDataBound" DataKeyNames="ID" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found">
        <Columns>
             <asp:BoundField DataField="ID" HeaderText="Leave ID" />
            <asp:BoundField DataField="Created_Date" HeaderText="Request Date" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Start_Date" HeaderText="Leave Start Date" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="End_Date" HeaderText="Leave End Date" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="Leave_Type" HeaderText="Leave Type" />
            <asp:TemplateField HeaderText="Deputy Approval">
                <ItemTemplate>
                    <%# GetDate(Eval("Deputy_Status").ToString(),Eval("Deputy_Comment").ToString(),Eval("Deputy_Updated_Date").ToString()) %>                   
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Manager Approval">
                <ItemTemplate>
                    <%# GetDate(Eval("Manager_Status").ToString(),Eval("Manager_Comment").ToString(),Eval("Manager_Updated_Date").ToString()) %>                
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="lblRequestStatus" runat="server"></asp:Label>
                    <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel"  Visible="false" CommandName='<%# Bind("Request_Status") %>' CommandArgument='<%# Bind("ID") %>' OnClientClick="return ConfirmCancel(this);" OnClick="lnkCancel_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataRowStyle CssClass="empty_row" ForeColor="White" />
    </asp:GridView>

    <script type="text/javascript" language="javascript">
        $("#cssmenu li").eq(0).addClass('active');
    </script>
</asp:Content>
