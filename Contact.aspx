<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="GSDC.Contact" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <script type="text/javascript"
          src="https://www.google.com/jsapi?autoload={
            'modules':[{
              'name':'visualization',
              'version':'1',
              'packages':['corechart']
            }]
          }"></script>
<script type="text/javascript">
    google.setOnLoadCallback(drawChart);

    function drawChart() {
        var data = google.visualization.arrayToDataTable([
          ['LeaveTye', 'Count','ID'],

          ['EL', 6, ],
          ['Sl', 3,2],
          ['CL', 4, 5],
          
        ]);

        var options = {
            title: 'Current Week Performance',
            curveType: 'function',
            pointSize: 3,
          
            legend: { position: 'bottom' },
            vAxis: { title: 'LeaveTye', titleTextStyle: { color: 'red' } }

        };

        var chart = new google.visualization.BarChart(document.getElementById('curve_chart'));

        chart.draw(data, options);
        google.visualization.events.addListener(chart, 'select', selectHandler);
        function selectHandler() {
            alert(data.getValue(chart.getSelection()[0].row, 0));
        }
    }
    </script>
<div id="curve_chart" style="width: 450px; height: 300px"></div>
</asp:Content>