<%@ Page Title="Attendance Details" Language="C#" MasterPageFile="~/AttendanceTracker/AttendanceTracker.master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="GSDC.AttendanceTracker.Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentAttendanceTracker" runat="server">
    <h3> <asp:Label ID="lblEmpName" runat="server"></asp:Label></h3>
    <asp:UpdatePanel ID="update" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvAttendanceList" DataKeyNames="ID" OnRowDataBound="gvAttendanceList_RowDataBound" CssClass="gridview" runat="server" AutoGenerateColumns="false">
                <Columns>

                    <asp:BoundField HeaderText="Date" DataField="In_Time" DataFormatString="{0:MM/dd/yyyy}" />
                    <asp:TemplateField HeaderText="Deputy Approval" Visible="false">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfAttedanceType" Value='<%# Bind("atID") %>' runat="server" />
                            <asp:DropDownList ID="ddlAttendanceTypes" OnSelectedIndexChanged="ddlAttendanceType_SelectedIndexChanged" Width="100%" runat="server" AutoPostBack="true" DataTextField="Short_Desc" DataValueField="ID"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Status" DataField="Short_Desc" />
                    <asp:BoundField HeaderText="IN" DataField="InTime" NullDisplayText="0" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:hh:mm tt}" />
                    <asp:BoundField HeaderText="OUT" DataField="OutTime" NullDisplayText="0" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:hh:mm tt}" />
                    <asp:BoundField HeaderText="Duration" DataField="Duration" NullDisplayText="0" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" />

                </Columns>
                <EmptyDataRowStyle CssClass="empty_row" ForeColor="White" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button ID="btnSave" Text="Save" CssClass="button" Visible="false" runat="server" OnClick="btnSave_Click" />
    <script type="text/javascript" language="javascript">
        $("#cssmenu li").eq(1).addClass('active');
    </script>
</asp:Content>
