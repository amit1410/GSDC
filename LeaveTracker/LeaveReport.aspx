<%@ Page Title="Leave Report" Language="C#" MasterPageFile="~/LeaveTracker/LeaveTracker.master" AutoEventWireup="true" CodeBehind="LeaveReport.aspx.cs" Inherits="GSDC.LeaveTracker.LeaveReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentLeaveTracker" runat="server">


    <script type="text/javascript"
        src="https://www.google.com/jsapi?autoload={
            'modules':[{
              'name':'visualization',
              'version':'1',
              'packages':['corechart']
            }]
          }"></script>
    <script type="text/javascript">

        CallTodayHandler();
        CallMonthlyHandler();
        CallMonthlyOutageHandler();
        function CallTodayHandler() {
            $.ajax({
                url: "/Handlers/GetLeaveReport.ashx",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: { 'reportType': 'd' },
                responseType: "json",
                success: function (data) {
                    drawTodayChart(data);
                },
                error: OnFail
            });
            return false;
        }
        function CallMonthlyHandler() {
            $.ajax({
                url: "/Handlers/GetLeaveReport.ashx",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: { 'reportType': 'm' },
                responseType: "json",
                success: function (data) {
                    drawMonthlyChart(data);
                },
                error: OnFail
            });
            return false;
        }
        function CallMonthlyOutageHandler() {
            $.ajax({
                url: "/Handlers/GetMonthlyOutage.ashx",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: {},
                responseType: "json",
                success: function (data) {
                    drawOutageChart(data);
                },
                error: OnFail
            });
            return false;
        }

        function OnComplete(result) {
            drawChart(result);
        }
        function OnFail(result) {
            alert('Request Failed');
        }
        function drawOutageChart(json) {

            var dataTable = new google.visualization.DataTable();

            dataTable.addColumn('string', 'LeaveDate');
            dataTable.addColumn('number', 'Outage');
            dataTable.addRows(json.length);

            for (var j in json) {

                var col = 0;
                for (var k in json[j]) {
                    dataTable.setValue(parseInt(j), col, json[j][k]);
                    col = col + 1;
                }
            }

            var options = {
                title: 'Monthly Outage Report',
                curveType: 'function',
                pointSize: 3,
                colors: ['#e51937', 'green'],
                legend: { position: 'bottom' },
                vAxis: { title: 'Monthly Outage', titleTextStyle: { color: 'red' } },
                hAxis: {

                    slantedText: true,
                    gridlines: { count: 1, color: '#CCC' },
                    slantedTextAngle: 180 // here you can even use 180
                }

            };

            var chart = new google.visualization.LineChart(document.getElementById('chart_monthly_outage'));

            chart.draw(dataTable, options);
            google.visualization.events.addListener(chart, 'select', selectHandler);
            function selectHandler() {
                // alert(dataTable.getValue(chart.getSelection()[0].row, 1));
                var obj = document.getElementById('<%= hfLeaveType.ClientID %>');
                if (dataTable.getValue(chart.getSelection()[0].row, 1) == "0")
                    return;
                $(obj).val(dataTable.getValue(chart.getSelection()[0].row, 0));
                var objCategory = document.getElementById('<%= hfReportCategory.ClientID %>');

                $(objCategory).val("o");
                $('.btnMonthly').click()
                //$('.btnMonthlyOutage').click()

            }

        }
        function drawMonthlyChart(json) {

            var dataTable = new google.visualization.DataTable();

            dataTable.addColumn('string', 'LeaveType');
            dataTable.addColumn('number', 'Leave Count');
            dataTable.addColumn({ type: 'string', role: 'annotation' });
            dataTable.addRows(json.length);
            var count = 0;
            for (var j in json) {

                var col = 0;
                for (var k in json[j]) {
                    dataTable.setValue(parseInt(j), col, json[j][k]);
                    count = json[j][k];
                    col = col + 1;
                    //  if(col==2)
                    //     dataTable.setValue(parseInt(j), col, count);
                }
                // alert(j+' '+count+' '+col);

            }
            var view = new google.visualization.DataView(dataTable);
            view.setColumns([0, 1]);

            var options = {
                title: 'Monthly Leave Report',
                curveType: 'function',
                colors: ['#e51937', 'green'],
                legend: { position: 'bottom' },
                vAxis: { title: 'Leave Count', titleTextStyle: { color: 'red' } }

            };

            var chart = new google.visualization.ColumnChart(document.getElementById('chart_monthly'));

            chart.draw(view, options);
            google.visualization.events.addListener(chart, 'select', selectHandler);
            function selectHandler() {
                var obj = document.getElementById('<%= hfLeaveType.ClientID %>');
                $(obj).val(dataTable.getValue(chart.getSelection()[0].row, 0));
                var objCategory = document.getElementById('<%= hfReportCategory.ClientID %>');

                $(objCategory).val("m");
                $('.btnMonthly').click()

            }

        }
        function drawTodayChart(json) {

            var data = new google.visualization.DataTable();

            data.addColumn('string', 'LeaveType');
            data.addColumn('number', 'Leave Count');
            data.addRows(json.length);

            for (var j in json) {

                var col = 0;
                for (var k in json[j]) {
                    data.setValue(parseInt(j), col, json[j][k]);
                    col = col + 1;
                }
            }

            var options = {
                title: "Today's Leave Report",
                curveType: 'function',
                pointSize: 3,
                colors: ['#e51937', 'green'],
                legend: { position: 'bottom' },
                vAxis: { title: 'Leave Count', titleTextStyle: { color: 'red' } }

            };

            var chart = new google.visualization.ColumnChart(document.getElementById('chart_today'));

            chart.draw(data, options);
            google.visualization.events.addListener(chart, 'select', selectHandler);
            function selectHandler() {
                var obj = document.getElementById('<%= hfLeaveType.ClientID %>');
                var objCategory = document.getElementById('<%= hfReportCategory.ClientID %>');
                $(obj).val(data.getValue(chart.getSelection()[0].row, 0));
                $(objCategory).val("d");
                //  $('.btnToday').click();
                $('.btnMonthly').click()

            }

        }


    </script>
    <br />

    <table style="width: 100%">

        <tr>
            <td style="width: 50%; border: 2px solid #e51937">
                <div id="chart_today" style="width: 100%; height: 400px"></div>
            </td>
            <td style="border: 2px solid red">
                <div id="chart_monthly" style="width: 100%; height: 400px"></div>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border: 2px solid #e51937">
                <div id="chart_monthly_outage" style="width: 100%; height: 400px"></div>
            </td>
        </tr>
    </table>

    <asp:UpdatePanel ID="updateToday" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvdDetailedLeaves" CssClass="gridview" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found">
                <Columns>
                    <asp:BoundField DataField="EmployeeName" HeaderText="Name" />
                    <asp:BoundField DataField="Start_Date" HeaderText="Leave Start Date" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="End_Date" HeaderText="Leave End Date" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Short_Desc" HeaderText="Leave Type" />
                </Columns>
                <EmptyDataRowStyle CssClass="empty_row" ForeColor="White" />
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnToday" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hfLeaveType" runat="server" />
    <asp:HiddenField ID="hfReportCategory" runat="server" />
    <asp:Button ID="btnToday" runat="server" OnClick="btnToday_Click" CssClass="btnToday" Style="visibility: hidden" />

    <%-- <asp:UpdatePanel ID="updateMonthly" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <asp:GridView ID="gvmDetailedLeaves" CssClass="gridview" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found">
        <Columns>
            <asp:BoundField DataField="EmployeeName" HeaderText="Name" />
            <asp:BoundField DataField="Total" HeaderText="Total Leaves" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Balance" HeaderText="Balance Leaves" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Short_Desc" HeaderText="Leave Type" />
        </Columns>
        <EmptyDataRowStyle CssClass="empty_row" ForeColor="White" />
    </asp:GridView>
    <%--</ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnMonthly" />
        </Triggers>
    </asp:UpdatePanel>--%>

    <asp:Button ID="btnMonthly" runat="server" PostBackUrl="~/LeaveTracker/LeaveDetails.aspx" CssClass="btnMonthly" Style="visibility: hidden" />
    <asp:UpdatePanel ID="updateOutage" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvOutage" CssClass="gridview" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found">
                <Columns>
                    <asp:BoundField DataField="EmployeeName" HeaderText="Name" />
                    <asp:BoundField DataField="Start_Date" HeaderText="Leave Start Date" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="End_Date" HeaderText="Leave End Date" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Short_Desc" HeaderText="Leave Type" />
                </Columns>
                <EmptyDataRowStyle CssClass="empty_row" ForeColor="White" />
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnMonthlyOutage" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Button ID="btnMonthlyOutage" runat="server" OnClick="btnMonthlyOutage_Click" CssClass="btnMonthlyOutage" Style="visibility: hidden" />
    <script type="text/javascript">
        $("#cssmenu li").eq(3).addClass('active');
    </script>



</asp:Content>
