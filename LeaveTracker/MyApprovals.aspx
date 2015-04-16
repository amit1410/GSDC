<%@ Page Title="My Approvals" Language="C#" MasterPageFile="~/LeaveTracker/LeaveTracker.master" AutoEventWireup="true" CodeBehind="MyApprovals.aspx.cs" Inherits="GSDC.LeaveTracker.MyApprovals" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentLeaveTracker" runat="Server">
    <script>

        function HideComment(ctrl) {
            $('#<%= txtReason.ClientID %>').val('');

            if ($(ctrl).val() == 'Rejected') {
                $(ctrl).closest('tr').next('tr').show();

            }
            else {
                $(ctrl).closest('tr').next('tr').hide();

            }
        }

    </script>
    
    <br />
    <div class="headerContainer" style="width:100%">All Approval Requests</div>
    <br />
    <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <asp:GridView ID="gvApprovals" CssClass="gridview" runat="server" DataKeyNames="ID" AutoGenerateColumns="false" EmptyDataText="No Records Found">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="Leave ID" />
                    <asp:TemplateField HeaderText="Requestor">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfLeaveCount" Value='<%# Bind("Leave_Count") %>' runat="server" />
                             <asp:HiddenField ID="hfLeaveID" Value='<%# Bind("Leave_ID") %>' runat="server" />
                            <asp:LinkButton ID="lnkRequestor" CausesValidation="false" runat="server" CssClass='<%# Bind("Employee_ID") %>' Text='<%# Bind("Requestor") %>' CommandArgument='<%# Bind("ID") %>' CommandName='<%# Bind("RequestorName") %>' OnClick="lnkRequestor_click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Created_Date" HeaderText="Request Date" DataFormatString="{0:MM/dd/yyyy}" />
                    <asp:BoundField DataField="Start_Date" HeaderText="Leave Start Date" DataFormatString="{0:MM/dd/yyyy}" />
                    <asp:BoundField DataField="End_Date" HeaderText="Leave End Date" DataFormatString="{0:MM/dd/yyyy}" />
                    <asp:BoundField DataField="Leave_Type" HeaderText="Leave Type" />
                </Columns>
                <EmptyDataRowStyle CssClass="empty_row" ForeColor="White" />
            </asp:GridView>
            <br />
            <br />
            <asp:Panel ID="pnlDetails" runat="server" Visible="false">
                <table class="table" style="width: 50%">
                    <thead>
                        <tr>
                            <th style="width: 40%">Name
                            </th>
                            <th style="width: 60%">
                                <asp:Label ID="lblRequestor" runat="server"></asp:Label>
                                <asp:HiddenField ID="hfLeaveType" runat="server" />
                                 <asp:HiddenField ID="hfLeaveTypeID" runat="server" />
                                <asp:HiddenField ID="hfStartDate" runat="server" />
                                <asp:HiddenField ID="hfEndDate" runat="server" />
                                <asp:HiddenField ID="hfCreatedBy" runat="server" />
                                <asp:HiddenField ID="hfLeaveCount" runat="server" />
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Leave ID
                            </td>
                            <td>
                                <asp:Label ID="lblID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Approval Status
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlApprovalStatus" runat="server" onchange="HideComment(this)">
                                    <asp:ListItem Text="Select Status" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Approve" Value="Approved"></asp:ListItem>
                                    <asp:ListItem Text="Reject" Value="Rejected"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ForeColor="Red" ID="rfvApproval" runat="server" ControlToValidate="ddlApprovalStatus" InitialValue="" ErrorMessage="Please select status"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>Reason
                            </td>
                            <td>
                                <asp:TextBox ID="txtReason" runat="server" Columns="15" Rows="10" Width="100%" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_click" />

                <br />

            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $("#cssmenu li").eq(1).addClass('active');
    </script>
</asp:Content>


