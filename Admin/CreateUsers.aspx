<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="CreateUsers.aspx.cs" Inherits="GSDC.Admin.CreateUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdmin" runat="server">
    Select Team<asp:DropDownList ID="ddlTeams" runat="server" DataValueField="ID" AutoPostBack="true"  OnSelectedIndexChanged="ddlTeams_SelectedIndexChanged" DataTextField="Team_Name"></asp:DropDownList>
    <br /><br /><asp:UpdatePanel ID="update" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          Select Manager  <asp:DropDownList ID="ddlManagers" runat="server" DataValueField="ID" DataTextField="Name"></asp:DropDownList>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlTeams" />
        </Triggers>
    </asp:UpdatePanel><br />
    <asp:GridView ID="gvEmployees" runat="server" OnRowDataBound="gvEmployees_RowDataBound" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="EmpID">
                <ItemTemplate>
                    <asp:TextBox ID="txtEmpID" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="First Name">
                <ItemTemplate>
                    <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Last Name">
                <ItemTemplate>
                    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Role">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlRole" DataTextField="Role_Name" DataValueField="ID" runat="server"></asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
           
        </Columns>

    </asp:GridView>
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
</asp:Content>
