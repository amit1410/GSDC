<%@ Page Title="" Language="C#" MasterPageFile="~/WorkManagement/WorkManagement.master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="GSDC.WorkManagement.Reports" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentEmployeeActivity" runat="server">


    <rsweb:ReportViewer ID="ReportViewer1" runat="server" ProcessingMode="Remote" Width="100%"></rsweb:ReportViewer>
</asp:Content>

