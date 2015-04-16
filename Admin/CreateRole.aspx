<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="CreateRole.aspx.cs" Inherits="GSDC.Admin.CreateRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdmin" runat="server">
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
    <table>
        <tr>
            <td>Role Name</td>
            <td>
                <asp:TextBox ID="txtRole" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:RequiredFieldValidator ID="rfvRole" runat="server" ErrorMessage="Please enter role name." Display="Dynamic" ControlToValidate="txtRole"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Is Approver</td>
            <td>
                <asp:CheckBox ID="chkApprover" runat="server"></asp:CheckBox>
            </td>
        </tr>
         <tr>
            <td>Is Approver</td>
            <td>
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>

</asp:Content>
