<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="ManageLeaves.aspx.cs" Inherits="GSDC.Admin.ManageLeaves" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentAdmin" runat="server">
   
    
     <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>

    <style>
        
        .ui-autocomplete .ui-widget {
            padding: 0px;
            border: 1px solid black;
            background-color: white;
            overflow: hidden;
            z-index: 99999;
        }

            .ui-autocomplete .ui-widget ul {
                width: 100%;
                list-style-position: outside;
                list-style: none;
                padding: 0;
                margin: 0;
            }

            .ui-autocomplete .ui-widget li {
                margin: 0px;
                padding: 2px 5px;
                cursor: default;
                display: block;
                /*

if width will be 100% horizontal scrollbar will apear

when scroll mode will be used

*/
                /*width: 100%;*/
                font: menu;
                font-size: 12px;
                /*

it is very important, if line-height not setted or setted

in relative units scroll will be broken in firefox

*/
                line-height: 16px;
                overflow: hidden;
            }

        .ac_loading {
            background: white url('indicator.gif') right center no-repeat;
        }

        .ac_over {
            background-color: #0A246A;
            color: white;
        }

        .ui-widget strong {
            font-weight: bold;
            color: #E51937;
        }
       .ui-widget strong:hover {
            font-weight: bold;
            color: #E51937;
        }
        .ui-menu .ui-menu-item a:hover
        {
            display: block;
            padding: 3px 3px 3px 3px;
            text-decoration: none;
            color: white;
           
            
        }
        .ui-menu .ui-menu-item ul:hover
        {
            display: block;
            padding: 3px 3px 3px 3px;
            text-decoration: none;
            color: red;
           background-color:red;
            
        }
        .ui-state-hover, .ui-widget-content .ui-state-hover, .ui-widget-header .ui-state-hover, .ui-state-focus, .ui-widget-content .ui-state-focus, .ui-widget-header .ui-state-focus {
         background:#808080!important;
        }
        /*.ui-autocomplete.ui-widget {
  font-family: Verdana,Arial,sans-serif;
  font-size: 10px;
}*/
    
    </style>
   
    <asp:TextBox ID="txtSearchName" CssClass="autosuggest" runat="server"></asp:TextBox>
    <asp:Button runat="server" Text="Search" OnClick="btnSearch_Click" ID="btnSearch" />
    <asp:UpdatePanel ID="update" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <asp:GridView ID="gvLeaves" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="Short_Desc" HeaderText="Leave Type" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:TextBox ID="txtLeaveCount" runat="server"></asp:TextBox>
                            <asp:CompareValidator ID="cmpLeave" runat="server" Display="Dynamic" ControlToValidate="txtLeaveCount" Operator="DataTypeCheck" Type="Integer" ErrorMessage="Enter valid value."></asp:CompareValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">

        $(document).ready(function () {
           // $("#MainContent_ContentAdmin_txtSearchName").autocomplete("AutoComplete.ashx");
            SearchText();
        });
        function SearchText() {
           
        $(".autosuggest").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "ManageLeaves.aspx/GetAutoCompleteData",
                    data: "{'name':'" + $('#MainContent_ContentAdmin_txtSearchName').val() + "'}",
                    dataType: "json",
                    success: function (data) {
                        response(data.d);
                    },
                    error: function (result) {
                        alert("Error");
                    }
                });
            }
        });
        $.ui.autocomplete.prototype._renderItem = function (ul, item) {
            var highlighted = item.label.split(this.term).join("<strong>" + this.term + "</strong>");
            return $("<li></li>")
                .data("item.autocomplete", item)
                .append("<a>" + highlighted + "</a>")
                .appendTo(ul);
        };
    }
</script>
</asp:Content>
