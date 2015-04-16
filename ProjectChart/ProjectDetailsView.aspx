<%@ Page Title="" Language="C#" MasterPageFile="~/ProjectChart/ProjectChart.master" AutoEventWireup="true" CodeBehind="ProjectDetailsView.aspx.cs" Inherits="GSDC.ProjectChart.ProjectDetailsView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentProjectChart" runat="server">

    <div id="divFormat" runat="server">

          <br />
    <div class="headerContainer" style="width: 55%">My Profile</div>
    <br />
     <table border="1" style="width: 55%">
         <tr>
             <td style="width: 20%">
                 <asp:Label ID="Label1" runat="server" Text="Employee Code :"></asp:Label> 
             </td>
             <td>
                  <asp:Label ID="lblEmpCode" runat="server" Text=""></asp:Label>
             </td>
         </tr>
         <tr>
           <td style="width: 20%">
                 <asp:Label ID="Label3" runat="server" Text="Employee Name:"></asp:Label>
             </td>
             <td> <asp:Label ID="lblEmpName" runat="server" Text=""></asp:Label></td>
         </tr>

         <tr>
           <td style="width: 20%">
                <asp:Label ID="Label5" runat="server" Text="Designation:"></asp:Label> 
             </td>
             <td> <asp:Label ID="lbEmpDesg" runat="server" Text=""></asp:Label></td>
         </tr>

         <tr>
           <td style="width: 20%">
                 <asp:Label ID="Label7" runat="server" Text="Team Name:"></asp:Label>
             </td>
             <td> <asp:Label ID="lblEmailID" runat="server" Text=""></asp:Label></td>
         </tr>

     </table>
    </div>
</asp:Content>
