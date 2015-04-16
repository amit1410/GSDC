<%@ Page Title="Work Management" Language="C#" MasterPageFile="WorkManagement.master" AutoEventWireup="true" CodeBehind="EmployeeActivity.aspx.cs" Inherits="GSDC.WorkManagement.EmployeeActivity" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentEmployeeActivity" runat="server">


    <script type="text/javascript">
        var hiddenControl = '<%= inpHide.ClientID %>';
        var count1 = 0;
        var countm1 = 0
        var count = 0, chkfn = null;
        var countm = 0, counth = 0;
        function changeColor() {

                   
                   
            // Call function with 1000 milliseconds gap
            count1 = parseInt(document.getElementById(hiddenControl).value);
            countm = parseInt(Math.floor(count1 / 60));
            counth = Math.floor(countm / 60);
            count = parseInt(count1 % 60);
            chkfn = setInterval(starttimer, 1000);
            
                 
        }
        function starttimer() {
                     
            count = count + 1;
            //countm += 1;
            var D = new Date();
            var oElem = document.getElementById("divtxt");

            oElem.style.color = oElem.style.color == "red" ? "blue":"red";
                    
                   
            if (count1 > 59) {

                     
                if (count > 59) {
                    count = 0;
                    countm = parseInt(countm + 1);
                    document.getElementById("pcount").innerHTML = "Your Time Starts: " + countm + ":" + count;
                }
                else
                {
                           
                    document.getElementById("pcount").innerHTML = "Your Time Starts: " + countm + ":" + count;
                }
            }
            else if(count>59)
            {
                if (count > 59) {
                    count = 0;
                    countm = parseInt(countm + 1);
                    document.getElementById("pcount").innerHTML = "Your Time Starts: " + countm + ":" + count;
                   
                }
                else
                {
                           
                    document.getElementById("pcount").innerHTML = "Your Time Starts: " + countm + ":" + count;
                }
            }
            if (countm > 59) {
                        
                       
                     
                countm = parseInt(countm % 60);
                //  counth += 1;
                // countm = 0;
                // count = 0;
                document.getElementById("pcount").innerHTML = "Your Time Starts: " + counth + ":" + countm + ":" + count;
               
            }
            else {
                document.getElementById("pcount").innerHTML = "Your Time Starts: " + counth + ":" + countm + ":" + count;
              

            }



        }
        function stoptimer() {
            clearInterval(chkfn);
            chkfn = null;
            count = 0;
            countm = 0;
            countmh = 0;
            document.getElementById("pcount").innerHTML = '';
        }

        function getminute() {
            if (count > 60)
                countm += 1;
            count = 0;
        }

        function gethour() {
            if (countm > 60)
                counth += 1;
        }



    </script>


    <script type="text/javascript">
        $(function () {
            $("[id*=imgOrdersShow]").each(function () {
                if ($(this)[0].src.indexOf("minus") != -1) {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                    $(this).next().remove();
                }
            });
            $("[id*=imgProductsShow]").each(function () {
                if ($(this)[0].src.indexOf("minus") != -1) {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                    $(this).next().remove();
                }
            });
        });
    </script>
    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "/Images/minus.gif";
            } else {
                div.style.display = "none";
                img.src = "/Images/plus.gif";
            }
        }

         
        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <br />


            <asp:DataList ID="DlstEmployeeHist" runat="server" BackColor="Gray" BorderColor="#666666"
                BorderStyle="None" BorderWidth="2px" CellPadding="3" CellSpacing="2"
                Font-Names="Verdana" Font-Size="Small" GridLines="Both"
                Width="600px">
                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" Font-Size="Large" ForeColor="White"
                    HorizontalAlign="Center" VerticalAlign="Middle" />
                <HeaderTemplate>
                    Current Status
                </HeaderTemplate>
                <ItemStyle BackColor="White" ForeColor="Black" BorderWidth="2px" />
                <ItemTemplate>
                    <b>Name:</b>
                    <asp:Label ID="lblCName" runat="server" Text='<%# Eval("EmpName") %>'></asp:Label>
                    <br />
                    <b>Role:</b>
                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("RoleName") %>'></asp:Label>
                    <br />
                    <b>Department:</b>
                    <asp:Label ID="lblCity" runat="server" Text=' <%# Eval("TeamName") %>'></asp:Label>
                    <br />
                    <b>Shift From:</b>
                    <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("ShiftTime") %>'></asp:Label>
                    <br />
                    <b>Logged From:</b>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("AttendenceType") %>'></asp:Label>
                    <br />
                </ItemTemplate>
            </asp:DataList>
            <br />

            <asp:Panel ID="pnlActivity" runat="server" HorizontalAlign="Left">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 30%">
                            <asp:DropDownList ID="ddlActivityType" CssClass="dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlActivityType_SelectedIndexChanged" runat="server" DataTextField="Activity_Desc" DataValueField="ActivityID" Style="width: 100%"></asp:DropDownList><br />
                           
                        </td>
                        <td style="width: 30%">
                            <asp:DropDownList ID="ddlRequest" CssClass="dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlRequest_SelectedIndexChanged" runat="server" DataTextField="Request_Desc" DataValueField="RequestID" Style="width: 100%" Visible="false"></asp:DropDownList>
                        </td>
                        <td style="width: 30%">
                           
                            <asp:DropDownList ID="ddlSubRequest" CssClass="dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlSubRequest_SelectedIndexChanged" runat="server" DataTextField="SubRequest_Desc" DataValueField="SubRequestID" Style="width: 100%" Visible="false"></asp:DropDownList>
                        </td>


                    </tr>
                  <br />
                    <br />
                    <tr>
                        <td style="width: 15%">
                            <input id="inpHide" type="hidden" runat="server" />
                            <%--  <Button ID="Button1" runat="server" Text="Start" style="font-size: 18px;border-width:0px;height: 40px;width: 100px;background-color:#E51937;color:white"  onclick="changeColor();" visible="True">Start</Button>--%>
                            <asp:Button ID="btmstart" runat="server" Text="Start" Style="font-size: 18px; border-width: 0px; height: 40px; width: 100px; background-color: #E51937; color: white" OnClick="btmstart_Click" Visible="true" />
                            <asp:Button ID="btnStop" runat="server" Text="Stop" Style="font-size: 18px; border-width: 0px; height: 40px; width: 100px; background-color: #E51937; color: white" OnClick="btnStop_Click" Visible="false" />
                            <asp:Button ID="btnSignOut" runat="server" Text="Sign Out" Style="font-size: 18px; border-width: 0px; height: 40px; width: 100px; background-color: #E51937; color: white" OnClick="btnSignOut_Click" Visible="false" OnClientClick="if (!confirm('Are you sure you want to logout?')) return false;" />

                        </td>
                        <td style="width: 40%" colspan="3"><asp:Label ID="lblCtime" runat="server" Font-Bold="True" Font-Names="Verdana"
                            ForeColor="Red"></asp:Label>
                            <div id="divtxt">
                                <p id="pcount" style="font: bold 24px verdana"></p>
                                <asp:Label ID="lbltrimer" runat="server" Text="" ForeColor="Red" Font-Bold="true" Font-Size="Medium"></asp:Label>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" Font-Bold="true" Font-Size="Medium" Visible="false"></asp:Label></td>
                    </tr>
                </table>
                <br />

                </asp:Panel>
         
            <br />
            <%--  <div id="div1" runat="server" class="headerContainer" style="width: 100%" visible="false">Todays Activity</div>--%>
            <br />
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
                <asp:GridView ID="GdEmpReport" runat="server" AutoGenerateColumns="false" CssClass="gridview" Align="Centre" OnRowDataBound="GdEmpReport_RowDataBound"
                    DataKeyNames="StartTime" AllowPaging="true" PageSize="5" OnPageIndexChanging="GdEmpReport_PageIndexChanging">
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

                        <asp:BoundField DataField="Activity" ControlStyle-Width="86%" HeaderText="Activity" />

                        <asp:BoundField DataField="StartTime" ControlStyle-Width="86%" HeaderText="Start Time" />
                        <asp:BoundField DataField="EndTime" ControlStyle-Width="86%" HeaderText="End Time" />
                        <asp:BoundField DataField="TotalTime" ControlStyle-Width="86%" HeaderText="Total Time(In Minute)" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>

        </ContentTemplate>


    </asp:UpdatePanel>



</asp:Content>

