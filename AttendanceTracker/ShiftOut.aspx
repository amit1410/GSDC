<%@ Page Title="" Language="C#" MasterPageFile="~/AttendanceTracker/AttendanceTracker.master" AutoEventWireup="true" CodeBehind="ShiftOut.aspx.cs" Inherits="GSDC.AttendanceTracker.ShiftOut" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentAttendanceTracker" runat="server">
      <center>
        <div style="width:50%">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                  <br />
              <br />
              <br />

                <asp:DropDownList ID="ddlAttendanceType" CssClass="dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlAttendanceType_SelectedIndexChanged" runat="server" DataTextField="Short_Desc" DataValueField="ID"></asp:DropDownList><br /><br />
                 <asp:DropDownList ID="ddlShift" CssClass="dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged" runat="server" DataTextField="Shifttime" DataValueField="ShiftID"></asp:DropDownList><br />
                <asp:RequiredFieldValidator ForeColor="Red" ID="rfvLeaveTypes" runat="server" ControlToValidate="ddlAttendanceType" InitialValue="" ErrorMessage="Please select attendance type"></asp:RequiredFieldValidator><br />
                <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label><br />
                <asp:Button ID="btnSubmit" style="font-size: 18px;border-width:0px;height: 40px;width: 100px;background-color:#E51937;color:white" runat="server" CommandArgument="0" Text="IN" OnClick="btnSubmit_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
       </div>
    </center>
   
    <script type="text/javascript" language="javascript">
        $("#cssmenu li").eq(0).addClass('active');
    </script>
</asp:Content>
