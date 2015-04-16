<%@ Page Title="Project Chart" Language="C#" MasterPageFile="~/ProjectChart/ProjectChart.master" AutoEventWireup="true" CodeBehind="ListProjectChart.aspx.cs" Inherits="GSDC.ProjectChart.ListProjectChart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentProjectChart" runat="server">

    <style type="text/css">
        .modalBackground {
            background-color: gray;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }


        .popup {
            background-color: #ddd;
            margin: 0px auto;
            width: 330px;
            position: relative;
            border: Gray 2px inset;
        }

        body {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 13px;
        }

        .info, .success, .warning, .error, .validation {
            border: 1px solid;
            margin: 10px 0px;
            padding: 15px 10px 15px 50px;
            background-repeat: no-repeat;
            background-position: 10px center;
        }

        .info {
            color: #00529B;
            background-color: #BDE5F8;
            background-image: url('info.png');
        }

        .success {
            color: #4F8A10;
            background-color: #DFF2BF;
            background-image: url('success.png');
        }

        .warning {
            color: #9F6000;
            background-color: #FEEFB3;
            background-image: url('warning.png');
        }

        .error {
            color: #D8000C;
            background-color: #FFBABA;
            background-image: url('error.png');
        }
    </style>

    <asp:Label ID="lblMessage" runat="server" class="headerContainer" Style="width: 50%" Visible="false"></asp:Label>

    <br />
    <div class="headerContainer">Project Summary</div>
    <br />
    <div id="divMessage" runat="server" class="success" visible="false"></div>
    <br />
    <%--<table style="width: 100%">

        <tr>
            <td colspan="1" align="Centre">
               

            </td>
        </tr>
    </table>--%>
    <asp:GridView ID="GdViewProjectChart" runat="server" CssClass="gridview" Align="Centre" AutoGenerateColumns="false" DataKeyNames="FilePath,ProjectID" OnRowDataBound="GdViewProjectChart_RowDataBound" AllowPaging="true" PageSize="10" OnPageIndexChanging="GdViewProjectChart_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="Project ID" ItemStyle-Width="30">
                <ItemTemplate>
                    <asp:Label ID="lblProjID" runat="server" Text='<%# Eval("ProjectID") %>'></asp:Label>

                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="User ID" ItemStyle-Width="30" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("UserID") %>'></asp:Label>

                </ItemTemplate>
            </asp:TemplateField>



            <asp:BoundField DataField="ProjectName" ControlStyle-Width="100" HeaderText="Project Name" />
            <asp:BoundField DataField="ProjectStartDate" ControlStyle-Width="20" HeaderText="Start Date" />
            <asp:BoundField DataField="ProjectEndTime" ControlStyle-Width="20" HeaderText="End Date" />
            <asp:BoundField DataField="ProjectCreationDate" ControlStyle-Width="20" HeaderText="Create Date" />
            <asp:BoundField DataField="ProjectExpectedEnd" ControlStyle-Width="20" HeaderText="Expected Date" />
            <asp:BoundField DataField="ProjectOwner" ControlStyle-Width="100" HeaderText="Owner" />

            <%--<asp:TemplateField HeaderText="Progress (%)" ItemStyle-Width="30" Visible="true">
                <ItemTemplate>
                    <asp:Label ID="lblProgress" runat="server" Text='<%# String.Format("{0:d} %", Eval("ProgressPercent")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Progress Chart">
                <ItemTemplate>
                    <asp:Label ID="LabelProgressBar2" runat="server"
                        Height="80%"
                        BackColor="LightSkyBlue" Font-Bold="true">
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:BoundField DataField="ProgressPercent" ControlStyle-Width="100" HeaderText="Progress Chart" NullDisplayText="0"/>
            <asp:TemplateField HeaderText="SOAP(Servie on a Page)" ItemStyle-Width="100">
                <ItemTemplate>
                    <asp:LinkButton ID="popSOAP" runat="server" CommandName="arg" CausesValidation="false" OnClick="popSOAP_Click">SOAP</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="View" ItemStyle-Width="30">
                <ItemTemplate>
                    <asp:LinkButton ID="popup1" runat="server" CommandName="arg" CausesValidation="false" OnClick="popup1_Click">View</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit" ItemStyle-Width="30">
                <ItemTemplate>
                    <asp:LinkButton ID="popup" runat="server" CommandName="arg" CausesValidation="false" OnClick="popup_Click" Enabled="false">Edit</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        <PagerStyle CssClass="headerContainer" ForeColor="White" />
    </asp:GridView>
    <asp:Button ID="btn1" runat="server" Visible="false" />


    <br />


</asp:Content>

