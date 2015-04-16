<%@ Page Title="Dash Board" Language="C#" MasterPageFile="~/WorkManagement/WorkManagement.master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="GSDC.WorkManagement.DashBoard" %>


<asp:Content ID="Content4" ContentPlaceHolderID="ContentEmployeeActivity" runat="server">
    <style type="text/css">
.floatLeft { float: left; }
</style>
    <style type="text/css">
.floatRight { float: right; }
</style>
    <center>
           <br />   
    <div class="headerContainer" style="width: 100%; align-items:center" id="msg" runat="server" >Employee Activity Dash Board</div>
              
      <div style="width:100%">
                   
              
           
    <br />   
          <asp:GridView ID="gvDashBoard" runat="server" CssClass="gridview" Align="Centre" emptydatatext="No data Found." AutoGenerateColumns="false" OnRowDataBound="GdEmpReport_RowDataBound" AllowPaging="true"
              DataKeyNames="StartTime"  PageSize="10">
                <Columns>
                    <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgOrdersShow" runat="server" OnClick="imgOrdersShow_Click" ImageUrl="~/images/plus.png"
                                    CommandArgument="Show" />
                                <asp:Panel ID="pnlOrders" runat="server" Visible="false" Style="position: relative">
                                    <tr>
                                        <td colspan="100%">
                                            <asp:GridView ID="gvChildGrid" runat="server" AutoGenerateColumns="false" PageSize="5"
                                                AllowPaging="true" OnPageIndexChanging="gvChildGrid_PageIndexChanging" CssClass="gridview" Align="Centre"
                                                DataKeyNames="StartTime,RequestID">
                                                <Columns>
                                               
                                                    <asp:TemplateField ItemStyle-Width="20px" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblActID" runat="server" Text='<%# Eval("ActivityID") %>'></asp:Label>
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Request" ControlStyle-Width="86%" HeaderText="Activity" />
                                                    <asp:BoundField DataField="StartTime" ControlStyle-Width="86%" HeaderText="Start Time" />

                                                    <asp:BoundField DataField="EndTime" ControlStyle-Width="86%" HeaderText="End Time" />
                                                    <asp:BoundField DataField="TotalTime" ControlStyle-Width="86%" HeaderText="Total Time(In Minute)" />
                                                         <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgProductsShow" runat="server" OnClick="imgProductsShow_Click" ImageUrl="~/images/plus.png"
                                                                CommandArgument="Show" />
                                                            <asp:Panel ID="pnlProducts" runat="server" Visible="false" Style="position: relative">
                                                                <tr>
                                                                    <td colspan="100%">
                                                                        <asp:GridView ID="gvSubChildGrid" runat="server" AutoGenerateColumns="false" PageSize="2"
                                                                            AllowPaging="true" OnPageIndexChanging="gvSubChildGrid_PageIndexChanging" CssClass="gridview" Align="Centre">
                                                                            <Columns>

                                                                                <asp:BoundField DataField="SubRequest" ControlStyle-Width="86%" HeaderText="Activity" />
                                                                                <asp:BoundField DataField="StartTime" ControlStyle-Width="86%" HeaderText="Start Time" />
                                                                                <asp:BoundField DataField="EndTime" ControlStyle-Width="86%" HeaderText="End Time" />
                                                                                <asp:BoundField DataField="TotalTime" ControlStyle-Width="86%" HeaderText="Total Time(In Minute)" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>

                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                      <asp:TemplateField ItemStyle-Width="20px" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblActID" runat="server" Text='<%# Eval("ActivityID") %>'></asp:Label>
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
               <asp:BoundField DataField="EmpName" ControlStyle-Width="86%" HeaderText="Name" />
                 <asp:BoundField DataField="ActDesc" ControlStyle-Width="86%" HeaderText="Activity" />
                 <asp:BoundField DataField="starttime" ControlStyle-Width="86%" HeaderText="Start Time" />
                 <asp:BoundField DataField="Totaltime" ControlStyle-Width="86%" HeaderText="Total Time(In Minute)" />
                    </Columns>
             
          </asp:GridView>
          <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" Font-Bold="true" Font-Size="Medium" Visible="false"></asp:Label>
          </div>
         </center>
</asp:Content>
