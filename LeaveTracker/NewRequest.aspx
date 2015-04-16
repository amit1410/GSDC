<%@ Page Title="Leave Request" Language="C#" MasterPageFile="~/LeaveTracker/LeaveTracker.master" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="GSDC.LeaveTracker.NewRequest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentLeaveTracker" runat="Server">
    <script>
        function DisplayDateToday(sender, args) {
            if (sender._selectedDate == null) {
                sender._selectedDate = new Date();
                /* For Sunday*/
                sender._daysTableHeaderRow.cells[0].style.color = "Red";
                sender._daysTableHeaderRow.cells[6].style.color = "Red";
                for (var i = 0; i < 7; i++) {
                    sender._daysTable.rows[i].cells[0].style.color = "Red";
                    sender._daysTable.rows[i].cells[6].style.color = "Red";

                }
            }
        }

    </script>

    <br />
    <div class="headerContainer" style="width: 50%">Personal Details</div>
    <br />
    <table style="width: 50%">

        <tr>
            <td class="tdLabel" style="width: 55px">Name
            </td>
            <td style="width: 10px; font-weight: bold">:</td>
            <td class="tdInput">
                <asp:Label runat="server" ID="lblName" Text="Upender Kumar"></asp:Label>

            </td>
            <td style="width: 15px"></td>
            <td class="tdLabel" style="width: 85px">Role
            </td>
            <td style="width: 10px; font-weight: bold">:</td>
            <td class="tdInput">
                <asp:Label ID="lblRole" runat="server" Text="Admin"></asp:Label>

            </td>
        </tr>
        <tr>
            <td class="tdLabel">Manager
            </td>
            <td style="width: 10px; font-weight: bold">:</td>
            <td class="tdInput">
                <asp:Label runat="server" ID="lblManager" Text="Mathur, Sakshi"></asp:Label>

            </td>
            <td style="width: 15px"></td>
            <td class="tdLabel">Service Team
            </td>
            <td style="width: 10px; font-weight: bold">:</td>
            <td class="tdInput">
                <asp:Label ID="lblTeam" runat="server" Text="KCS"></asp:Label>

            </td>
        </tr>
    </table>
    <br />
    <div class="headerContainer" style="width: 50%">Balance Leaves</div>
    <br />
    <asp:GridView ID="gvLeaves" runat="server" CssClass="gridview" AutoGenerateColumns="false" ShowHeader="true" Width="500px">
        <Columns>
            <asp:BoundField DataField="Leave" ControlStyle-Width="86%" HeaderText="Leave Title" />
            <asp:BoundField DataField="Total_Leaves" ControlStyle-CssClass="tdCenter" HeaderText="Total Leaves" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Balance_Leaves" ControlStyle-CssClass="tdCenter" HeaderText="Balance Leaves" ItemStyle-HorizontalAlign="Center" />

        </Columns>
    </asp:GridView>
    <br />
    <table style="width: 50%">
        <tr style="height: 30px">
            <td colspan="5" class="headerContainer">Apply For Leave</td>
        </tr>
        <tr style="height: 20px">
            <td colspan="5"></td>
        </tr>
        <tr>
            <td>Leave Type
            </td>
            <td class="tdInput">
                <asp:DropDownList ID="ddlLeaveTypes" runat="server" DataTextField="Leave" DataValueField="ID" Width="100%"></asp:DropDownList>

            </td>
            <td style="width: 15px"></td>
            <td>Deputy
            </td>
            <td class="tdInput">
                <asp:DropDownList ID="ddlDeputy" runat="server" DataMember="Email" DataTextField="Deputy" DataValueField="ID" Width="100%"></asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td></td>
            <td class="tdInput">
                <asp:RequiredFieldValidator ForeColor="Red" ID="rfvLeaveTypes" runat="server" ControlToValidate="ddlLeaveTypes" InitialValue="" ErrorMessage="Please select leave type"></asp:RequiredFieldValidator>
            </td>
            <td style="width: 15px"></td>
            <td></td>
            <td class="tdInput">
                <asp:RequiredFieldValidator ForeColor="Red" ID="rfvDeputy" runat="server" ControlToValidate="ddlDeputy" InitialValue="" ErrorMessage="Please select deputy"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Start Date
            </td>
            <td class="tdInput">
                <asp:TextBox ID="txtStartDate" runat="server" onkeypress="return false;"></asp:TextBox>
                <img alt="Icon" class="imgCalender" src="../Images/calendar.gif" id="imgStartDate" />
                <asp:CalendarExtender ID="calExtenderStart" runat="server" Enabled="True" OnClientShowing="DisplayDateToday" CssClass="cal_Theme1" FirstDayOfWeek="Sunday" Format="MM/dd/yyyy" PopupButtonID="imgStartDate" TargetControlID="txtStartDate" PopupPosition="BottomRight"></asp:CalendarExtender>
            </td>
            <td style="width: 15px"></td>
            <td>End Date
            </td>
            <td class="tdInput">
                <asp:TextBox ID="txtEndDate" runat="server" onkeypress="return false;"></asp:TextBox>
                <img alt="Icon" class="imgCalender" src="../Images/calendar.gif" id="imgEndDate" />
                <asp:CalendarExtender ID="calExtenderEnd" runat="server" Enabled="True" OnClientShowing="DisplayDateToday" CssClass="cal_Theme1" FirstDayOfWeek="Sunday" Format="MM/dd/yyyy" PopupButtonID="imgEndDate" TargetControlID="txtEndDate" PopupPosition="BottomRight"></asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td></td>
            <td class="tdInput">
                <asp:RequiredFieldValidator ForeColor="Red" ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate" Display="Dynamic" ErrorMessage="Please enter start date"></asp:RequiredFieldValidator>
           </td>
            <td style="width: 15px"></td>
            <td></td>
            <td class="tdInput">
                <asp:RequiredFieldValidator ForeColor="Red" ID="rfvEndDate" runat="server" ControlToValidate="txtEndDate" Display="Dynamic" ErrorMessage="Please enter end date"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cmpEndDate" ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" Type="Date" Operator="GreaterThanEqual" Display="Dynamic" ErrorMessage="Please enter valid date" ForeColor="Red" runat="server"></asp:CompareValidator></td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:UpdatePanel ID="upDeputy" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lblDeputyStatus" runat="server" ForeColor="Red"></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnCheckDeputy" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:Button ID="btnSubmit" runat="server" Style="visibility: hidden" Text="Submit" OnClick="btnSubmit_click" />
                <asp:Button ID="btnCheckDeputy" runat="server" Text="Submit" OnClick="btnCheckDeputy_click" />
            </td>
        </tr>
    </table>
   
    <script type="text/javascript">
        $("#cssmenu li").eq(2).addClass('active');
    </script>
</asp:Content>
