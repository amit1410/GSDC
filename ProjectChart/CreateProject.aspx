<%@ Page Title="Add New" Language="C#" MasterPageFile="~/ProjectChart/ProjectChart.master" AutoEventWireup="true" CodeBehind="CreateProject.aspx.cs" Inherits="GSDC.ProjectChart.CreateProject" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="ContentProjectChart" runat="server">

      
   

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
    <style type="text/css">
        .floatLeft {
            float: left;
        }
    </style>
    <style type="text/css">
        .floatRight {
            float: right;
        }
    </style>
    <br />
    <div class="headerContainer" style="width: 70%">Project Details</div>
    <br />
    <asp:ScriptManagerProxy ID="ddd" runat="server"></asp:ScriptManagerProxy>
    <asp:Panel runat="server" ID="pnldetails" Enabled="true" Style="width: 100%">
        <table style="width: 100%">
            <tr>
                <td style="width: 90px">Project Name:
                </td>
                <td class="tdInput" style="width: 90px">
                    <asp:TextBox ID="txtProjectname" runat="server" MaxLength="50"></asp:TextBox>

                </td>

                <td style="width: 70px">Start Date:
                </td>
                <td class="tdInput" style="width: 90px">
                    <asp:TextBox ID="txtStartDate" runat="server" onkeypress="return false;"></asp:TextBox>
                    <img alt="Icon" class="imgCalender" src="../Images/calendar.gif" id="imgStartDate" />
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" OnClientShowing="DisplayDateToday" CssClass="cal_Theme1" FirstDayOfWeek="Sunday" Format="MM/dd/yyyy" PopupButtonID="imgStartDate" TargetControlID="txtStartDate" PopupPosition="BottomRight"></asp:CalendarExtender>
                </td>
                <td style="width: 90px">End Date:
                </td>
                <td class="tdInput" style="width: 90px">
                    <asp:TextBox ID="txtEndDate" runat="server" onkeypress="return false;"></asp:TextBox>
                    <img alt="Icon" class="imgCalender" src="../Images/calendar.gif" id="imgEndDate" />
                    <asp:CalendarExtender ID="calExtenderStart" runat="server" Enabled="True" OnClientShowing="DisplayDateToday" CssClass="cal_Theme1" FirstDayOfWeek="Sunday" Format="MM/dd/yyyy" PopupButtonID="imgEndDate" TargetControlID="txtEndDate" PopupPosition="BottomRight"></asp:CalendarExtender>
                    <asp:CompareValidator ID="cmpVal1" ControlToCompare="txtStartDate" 
         ControlToValidate="txtEndDate" Type="Date" Operator="GreaterThanEqual"   
         ErrorMessage="*Invalid Data" runat="server" ValidationGroup="btnSubmit"></asp:CompareValidator>
                </td>
            </tr>


            <tr>


                <td style="width: 90px">Create Date:
                </td>
                <td class="tdInput" style="width: 90px">
                    <asp:TextBox ID="txtCreateDate" runat="server" onkeypress="return false;"></asp:TextBox>
                    <img alt="Icon" class="imgCalender" src="../Images/calendar.gif" id="imgCreateDate" />
                    <asp:CalendarExtender ID="calExtenderEnd" runat="server" Enabled="True" OnClientShowing="DisplayDateToday" CssClass="cal_Theme1" FirstDayOfWeek="Sunday" Format="MM/dd/yyyy" PopupButtonID="imgCreateDate" TargetControlID="txtCreateDate" PopupPosition="BottomRight"></asp:CalendarExtender>
                </td>
                <td style="width: 70px">Expected End Date:
                </td>
                <td class="tdInput" style="width: 90px">
                    <asp:TextBox ID="txtExpectedDate" runat="server" onkeypress="return false;"></asp:TextBox>
                    <img alt="Icon" class="imgCalender" src="../Images/calendar.gif" id="imgExpectedDate" />
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" OnClientShowing="DisplayDateToday" CssClass="cal_Theme1" FirstDayOfWeek="Sunday" Format="MM/dd/yyyy" PopupButtonID="imgExpectedDate" TargetControlID="txtExpectedDate" PopupPosition="BottomRight"></asp:CalendarExtender>
                </td>

                <td style="width: 90px">Project Description:
                </td>
                <td class="tdInput" style="width: 90px">

                    <asp:FileUpload ID="fileUpload1" runat="server" ValidateRequestMode="Enabled" /><br />

                </td>
            </tr>



            <tr>
                <td style="width: 90px">
                    <asp:Label ID="lblSS" runat="server" Text="Service Strategy SPOC: "></asp:Label></td>
                <td class="tdInput" style="width: 90px">
                    <asp:TextBox ID="TxtSSPOC" runat="server" AutoCompleteType="Search"></asp:TextBox>
                      <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="TxtSSPOC"
         MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
         ServiceMethod="GetEmployee" >
    </asp:AutoCompleteExtender>
                </td>
                <td style="width: 90px">
                    <asp:Label ID="lbSD" runat="server" Text="Service Design SPOC: "></asp:Label></td>
                <td class="tdInput" style="width: 70px">
                    <asp:TextBox ID="txtSDSPOC" runat="server"></asp:TextBox>
                      <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txtSDSPOC"
         MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
         ServiceMethod="GetEmployee" >
    </asp:AutoCompleteExtender>
                </td>
                <td style="width: 90px">
                    <asp:Label ID="lblST" runat="server" Text="Service Transition SPOC :"></asp:Label></td>
                <td class="tdInput" style="width: 90px">
                    <asp:TextBox ID="txtSTSPOC" runat="server"></asp:TextBox>
                      <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" TargetControlID="txtSTSPOC"
         MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
         ServiceMethod="GetEmployee"  >
    </asp:AutoCompleteExtender>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>

                <td>
                    <asp:Label ID="lblSO" runat="server" Text="Service Operation SPOC :"></asp:Label></td>
                <td class="tdInput">
                      <asp:TextBox ID="txtSOSPOC" runat="server"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSOSPOC"
         MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
         ServiceMethod="GetEmployee" >
    </asp:AutoCompleteExtender>
                  
                    <td>
                        <asp:Label ID="lblCSI" runat="server" Text="Continuous Service Improvement SPOC :"></asp:Label></td>
                    <td class="tdInput">
                        <asp:TextBox ID="txtCSISPOC" runat="server"></asp:TextBox>
                         <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" TargetControlID="txtCSISPOC"
         MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
         ServiceMethod="GetEmployee" >
    </asp:AutoCompleteExtender>
                    </td>
                </td>
            </tr>


            <tr>

                <td colspan="5" align="center">

                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnAdd_click" CausesValidation="false" />
                    <asp:Button ID="btnReset" runat="server" Style="visibility: visible" Text="Reset" />

                </td>

            </tr>
            <tr>
                <td colspan="5" align="left">
                    <asp:Label ID="Label12" runat="server" Text="Note:"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="5" align="left">
                    <br />
                    <asp:Label ID="lblUpload" runat="server" Text="1:Please Download File from Download Option in Menu and update then upload the SOAP File"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="5" align="left">
                    <asp:Label ID="Label11" runat="server" Text="2:It is mendatory to fill all the filed of SOAP(Service on a Page File)"></asp:Label></td>
            </tr>
        </table>
    </asp:Panel>


    <br />

    <asp:Panel ID="pnlStrategy" runat="server" Visible="false" Enabled="false">
        <div class="headerContainer" style="width: 50%">Service Strategy</div>
        <br />
        <asp:Panel ID="pnlServiceStrtegy" runat="server">
            <div id="div" runat="server">
                <table width="100%">
                    <tr width="100%">
                        <td width="100%">
                            <span style="float: left;">
                                <asp:Label ID="Label1" runat="server" Text="Average Completed : " Font-Size="Large"></asp:Label></b>
        <asp:Label ID="lblAvgpercent" runat="server" Font-Size="Large" ForeColor="MediumBlue"></asp:Label></b></span>
                            <span style="float: right;">
                                <asp:Button Text="Update" runat="server" OnClick="Strategy_Click" CausesValidation="false" /></span>
                        </td>
                    </tr>
                </table>

            </div>
            <br />

            <asp:GridView ID="gviewServiceStrategy" runat="server" DataKeyNames="ID" CssClass="gridview" Align="Centre" AutoGenerateColumns="false"
                OnRowDataBound="gviewServiceStrategy_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Status" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Process ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblProcessID" runat="server" Text='<%# Eval("ProcessID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub Process ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblSubProcessID" runat="server" Text='<%# Eval("SubProcessID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Process">
                        <ItemTemplate>
                            <asp:Label ID="lblProcess" runat="server" Text='<%# Eval("Process") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub Process">
                        <ItemTemplate>
                            <asp:Label ID="lblSubProcess" runat="server" Text='<%# Eval("SubProcess") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="30" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">

                        <ItemTemplate>
                            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Text="Select Status" Value=""></asp:ListItem>
                                <asp:ListItem Text="Not Started" Value="0%"></asp:ListItem>
                                <asp:ListItem Text="Initiated" Value="25%"></asp:ListItem>
                                <asp:ListItem Text="Review" Value="50%"></asp:ListItem>
                                <asp:ListItem Text="In Progress" Value="75%"></asp:ListItem>
                                <asp:ListItem Text="Completed" Value="100%"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="%(Percent)">
                        <ItemTemplate>
                            <asp:Label ID="lblStatusPercent" runat="server" Text='<%# Eval("StatusPercent") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comments">
                        <ItemTemplate>
                            <asp:TextBox ID="txtComment" runat="server" Text='<%# Eval("Comment") %>'></asp:TextBox>
                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </asp:Panel>
    </asp:Panel>

    <br />
    <asp:Panel ID="pnlSerDesign" runat="server" Visible="false" Enabled="false">
        <div class="headerContainer" style="width: 50%">Service Design</div>
        <br />
        <asp:Panel ID="pnlDesign" runat="server">
            <div id="div1" runat="server">
                <table width="100%">
                    <tr width="100%">
                        <td width="100%">
                            <span style="float: left;">
                                <asp:Label ID="Label2" runat="server" Text="Average Completed : " Font-Size="Large"></asp:Label></b>
        <asp:Label ID="lblAveragePerctDesign" runat="server" Font-Size="Large" ForeColor="MediumBlue"></asp:Label></b></span>
                            <span style="float: right;">
                                <asp:Button ID="btnServiceDesign" Text="Update" runat="server" OnClick="btnServiceDesign_Click" CausesValidation="false" /></span>
                        </td>
                    </tr>
                </table>

            </div>
            <br />

            <asp:GridView ID="gvServiceDesign" runat="server" DataKeyNames="ID" CssClass="gridview" Align="Centre" AutoGenerateColumns="false"
                OnRowDataBound="gvServiceDesign_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Status" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Process ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblProcessID" runat="server" Text='<%# Eval("ProcessID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub Process ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblSubProcessID" runat="server" Text='<%# Eval("SubProcessID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Process">
                        <ItemTemplate>
                            <asp:Label ID="lblProcess" runat="server" Text='<%# Eval("Process") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub Process">
                        <ItemTemplate>
                            <asp:Label ID="lblSubProcess" runat="server" Text='<%# Eval("SubProcess") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="30" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">

                        <ItemTemplate>
                            <asp:DropDownList ID="ddlDesignStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDesignStatus_SelectedIndexChanged">
                                <asp:ListItem Text="Select Status" Value=""></asp:ListItem>
                                <asp:ListItem Text="Not Started" Value="0%"></asp:ListItem>
                                <asp:ListItem Text="Initiated" Value="25%"></asp:ListItem>
                                <asp:ListItem Text="Review" Value="50%"></asp:ListItem>
                                <asp:ListItem Text="In Progress" Value="75%"></asp:ListItem>
                                <asp:ListItem Text="Completed" Value="100%"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="%(Percent)">
                        <ItemTemplate>
                            <asp:Label ID="lblStatusPercent" runat="server" Text='<%# Eval("StatusPercent") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comments">
                        <ItemTemplate>
                            <asp:TextBox ID="txtComment" runat="server" Text='<%# Eval("Comment") %>'></asp:TextBox>
                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </asp:Panel>
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlTransition" runat="server" Visible="false" Enabled="false">
        <div class="headerContainer" style="width: 50%">Service Transition</div>
        <br />
        <asp:Panel ID="pnlSerTransition" runat="server">
            <div id="div2" runat="server">
                <table width="100%">
                    <tr width="100%">
                        <td width="100%">
                            <span style="float: left;">
                                <asp:Label ID="Label4" runat="server" Text="Average Completed : " Font-Size="Large"></asp:Label></b>
        <asp:Label ID="lblAverageperctTrans" runat="server" Font-Size="Large" ForeColor="MediumBlue"></asp:Label></b></span>
                            <span style="float: right;">
                                <asp:Button ID="btnTransition" Text="Update" runat="server" OnClick="btnTransition_Click" CausesValidation="false" /></span>
                        </td>
                    </tr>
                </table>

            </div>
            <br />

            <asp:GridView ID="gvServiceTransition" runat="server" DataKeyNames="ID" CssClass="gridview" Align="Centre" AutoGenerateColumns="false"
                OnRowDataBound="gvServiceTransition_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Status" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Process ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblProcessID" runat="server" Text='<%# Eval("ProcessID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub Process ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblSubProcessID" runat="server" Text='<%# Eval("SubProcessID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Process">
                        <ItemTemplate>
                            <asp:Label ID="lblProcess" runat="server" Text='<%# Eval("Process") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub Process">
                        <ItemTemplate>
                            <asp:Label ID="lblSubProcess" runat="server" Text='<%# Eval("SubProcess") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="30" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">

                        <ItemTemplate>
                            <asp:DropDownList ID="ddlServiceTrans" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlServiceTrans_SelectedIndexChanged">
                                <asp:ListItem Text="Select Status" Value=""></asp:ListItem>
                                <asp:ListItem Text="Not Started" Value="0%"></asp:ListItem>
                                <asp:ListItem Text="Initiated" Value="25%"></asp:ListItem>
                                <asp:ListItem Text="Review" Value="50%"></asp:ListItem>
                                <asp:ListItem Text="In Progress" Value="75%"></asp:ListItem>
                                <asp:ListItem Text="Completed" Value="100%"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="%(Percent)">
                        <ItemTemplate>
                            <asp:Label ID="lblStatusPercent" runat="server" Text='<%# Eval("StatusPercent") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comments">
                        <ItemTemplate>
                            <asp:TextBox ID="txtComment" runat="server" Text='<%# Eval("Comment") %>'></asp:TextBox>
                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </asp:Panel>
    </asp:Panel>

    <br />
    <asp:Panel ID="pnlOperation" runat="server" Visible="false" Enabled="false">
        <div class="headerContainer" style="width: 50%">Service Operation</div>
        <br />
        <asp:Panel ID="Panel2" runat="server">
            <div id="div3" runat="server">
                <table width="100%">
                    <tr width="100%">
                        <td width="100%">
                            <span style="float: left;">
                                <asp:Label ID="Label6" runat="server" Text="Average Completed : " Font-Size="Large"></asp:Label></b>
        <asp:Label ID="lblAveragePerctOps" runat="server" Font-Size="Large" ForeColor="MediumBlue"></asp:Label></b></span>
                            <span style="float: right;">
                                <asp:Button ID="btnOperation" Text="Update" runat="server" OnClick="btnOperation_Click" CausesValidation="false" /></span>
                        </td>
                    </tr>
                </table>

            </div>
            <br />

            <asp:GridView ID="gvServiceOperation" runat="server" DataKeyNames="ID" CssClass="gridview" Align="Centre" AutoGenerateColumns="false"
                OnRowDataBound="gvServiceOperation_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Status" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Process ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblProcessID" runat="server" Text='<%# Eval("ProcessID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub Process ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblSubProcessID" runat="server" Text='<%# Eval("SubProcessID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Process">
                        <ItemTemplate>
                            <asp:Label ID="lblProcess" runat="server" Text='<%# Eval("Process") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub Process">
                        <ItemTemplate>
                            <asp:Label ID="lblSubProcess" runat="server" Text='<%# Eval("SubProcess") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="30" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">

                        <ItemTemplate>
                            <asp:DropDownList ID="ddlServiceOps" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlServiceOps_SelectedIndexChanged">
                                <asp:ListItem Text="Select Status" Value=""></asp:ListItem>
                                <asp:ListItem Text="Not Started" Value="0%"></asp:ListItem>
                                <asp:ListItem Text="Initiated" Value="25%"></asp:ListItem>
                                <asp:ListItem Text="Review" Value="50%"></asp:ListItem>
                                <asp:ListItem Text="In Progress" Value="75%"></asp:ListItem>
                                <asp:ListItem Text="Completed" Value="100%"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="%(Percent)">
                        <ItemTemplate>
                            <asp:Label ID="lblStatusPercent" runat="server" Text='<%# Eval("StatusPercent") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comments">
                        <ItemTemplate>
                            <asp:TextBox ID="txtComment" runat="server" Text='<%# Eval("Comment") %>'></asp:TextBox>
                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </asp:Panel>
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlCSI" runat="server" Visible="false" Enabled="false">
        <div class="headerContainer" style="width: 50%">Continous Service Improvement</div>
        <br />
        <asp:Panel ID="Panel3" runat="server">
            <div id="div4" runat="server">
                <table width="100%">
                    <tr width="100%">
                        <td width="100%">
                            <span style="float: left;">
                                <asp:Label ID="Label8" runat="server" Text="Average Completed : " Font-Size="Large"></asp:Label></b>
        <asp:Label ID="lblAveragePerctCSI" runat="server" Font-Size="Large" ForeColor="MediumBlue"></asp:Label></b></span>
                            <span style="float: right;">
                                <asp:Button ID="btnCSI" Text="Update" runat="server" OnClick="btnCSI_Click" CausesValidation="false" /></span>
                        </td>
                    </tr>
                </table>

            </div>
            <br />

            <asp:GridView ID="gvCSI" runat="server" DataKeyNames="ID" CssClass="gridview" Align="Centre" AutoGenerateColumns="false"
                OnRowDataBound="gvCSI_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Status" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Process ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblProcessID" runat="server" Text='<%# Eval("ProcessID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub Process ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblSubProcessID" runat="server" Text='<%# Eval("SubProcessID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Process">
                        <ItemTemplate>
                            <asp:Label ID="lblProcess" runat="server" Text='<%# Eval("Process") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub Process">
                        <ItemTemplate>
                            <asp:Label ID="lblSubProcess" runat="server" Text='<%# Eval("SubProcess") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="30" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">

                        <ItemTemplate>
                            <asp:DropDownList ID="ddlCSI" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCSI_SelectedIndexChanged">
                                <asp:ListItem Text="Select Status" Value=""></asp:ListItem>
                                <asp:ListItem Text="Not Started" Value="0%"></asp:ListItem>
                                <asp:ListItem Text="Initiated" Value="25%"></asp:ListItem>
                                <asp:ListItem Text="Review" Value="50%"></asp:ListItem>
                                <asp:ListItem Text="In Progress" Value="75%"></asp:ListItem>
                                <asp:ListItem Text="Completed" Value="100%"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="%(Percent)">
                        <ItemTemplate>
                            <asp:Label ID="lblStatusPercent" runat="server" Text='<%# Eval("StatusPercent") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comments">
                        <ItemTemplate>
                            <asp:TextBox ID="txtComment" runat="server" Text='<%# Eval("Comment") %>'></asp:TextBox>
                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </asp:Panel>
    </asp:Panel>

</asp:Content>
