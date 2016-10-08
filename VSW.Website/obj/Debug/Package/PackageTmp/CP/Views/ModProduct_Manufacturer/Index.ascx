<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_ManufacturerModel;
    var listItem = ViewBag.Data as List<ModProduct_ManufacturerEntity>;
%>
<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="boxchecked" name="boxchecked" value="0" />
<div id="toolbar-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%=GetDefaultListCommand()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Nhà sản xuất: Danh sách</h2>
        </div>
        <div class="clr">
        </div>
    </div>
    <div class="b">
        <div class="b">
            <div class="b">
            </div>
        </div>
    </div>
</div>
<div class="clr">
</div>
<script type="text/javascript">

    var VSWController = 'ModProduct_Manufacturer';

    var VSWArrVar = [
                        'limit', 'PageSize'
                   ];


    var VSWArrVar_QS = [
                        'filter_search', 'SearchText'
                      ];

    var VSWArrQT = [
                      '<%= model.PageIndex + 1 %>', 'PageIndex',
                      '<%= model.Sort %>', 'Sort'
                  ];

    var VSWArrDefault =
                  [
                    '1', 'PageIndex',
                    '20', 'PageSize'
                  ];

    function DisplayMessageCall() {

        var pageUrl = '<%=ResolveUrl("~/Tools/Test.asmx")%>'

        $.ajax({
            type: "POST",
            url: pageUrl + "/DisplayMessage",
            //data: '{}',
            data: "{'NameInfo':'" + $('#<%=txtName.ClientID%>').val() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessCall,
            error: OnErrorCall
        });

    }

    function OnSuccessCall(response) {
        $('#<%=lblOutput.ClientID%>').html(response.d);
    }

    function OnErrorCall(response) {
        alert(response.status + " " + response.statusText);
    }


    function GetDataTable() {

        var pageUrl = '<%=ResolveUrl("~/Tools/Test.asmx")%>'

        $.ajax({
            type: "POST",
            url: pageUrl + "/GetDataTable",
            //data: '{}',
            data: "{'iTop':'" + $('#<%=txtName.ClientID%>').val() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessCall_Table,
            error: OnErrorCall_Table
        });

    }

    function OnSuccessCall_Table(response) {
        // Cách 1
        //var sDataTable = JSON.stringify(eval('(' + response.d + ')'));

        //var DataTable = eval('(' + sDataTable + ')')

        // Cách 2
        var DataTable = JSON.parse(response.d);

        for (var i = 0; i < DataTable.DataRow.length; i++) {
            var Data = '';
            Data += "Column1: " + DataTable.DataRow[i].Column1;
            Data += "\tColumn2: " + DataTable.DataRow[i].Column2;

            alert(Data);
        }


    }

    function OnErrorCall_Table(response) {
        alert(response.status + " " + response.statusText);
    }

</script>
<%= ShowMessage()%>
<div id="element-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div style="border: 1 solid red">
        <input type="button" value="click me" onclick="DisplayMessageCall();return false;" />
        <input type="button" value="click me get DataTable" onclick="GetDataTable();return false;" />
        <div>
            <asp:TextBox ID="txtName" runat="server" Text="Enter your name"></asp:TextBox>
            <br />
            <asp:Label ID="lblOutput" runat="server" Text=""></asp:Label>
        </div>
    </div>
    <div class="m">
        <table>
            <tr>
                <td width="100%">
                    Lọc:
                    <input type="text" id="filter_search" value="<%= model.SearchText %>" class="text_area"
                        onchange="VSWRedirect();return false;" />
                    <button onclick="VSWRedirect();return false;">
                        Tìm kiếm</button>
                </td>
                <td nowrap="nowrap">
                </td>
            </tr>
        </table>
        <table class="adminlist" cellspacing="1">
            <thead>
                <tr>
                    <th width="1%">
                        STT
                    </th>
                    <th width="1%">
                        <input type="checkbox" name="toggle" value="" onclick="checkAll(<%= model.PageSize %>);" />
                    </th>
                    <th width="5%" nowrap="nowrap">
                        <%= GetSortLink("ID", "ID")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Trạng thái", "Activity")%>
                    </th>
                    <th width="10%" nowrap="nowrap">
                        <%= GetSortLink("Mã Nhà sản xuất", "Code")%>
                    </th>
                    <th class="title">
                        <%= GetSortLink("Tên Nhà sản xuất", "Name")%>
                    </th>
                    <th width="20%" nowrap="nowrap">
                        <%= GetSortLink("Địa chỉ", "Address")%>
                    </th>
                    <th width="10%" nowrap="nowrap">
                        <%= GetSortLink("Số điện thoại", "PhoneNumber")%>
                    </th>
                    <th width="10%" nowrap="nowrap">
                        <%= GetSortLink("Email", "Email")%>
                    </th>
                    <th width="10%" nowrap="nowrap">
                        <%= GetSortLink("Ngày tạo", "CreateDate")%>
                    </th>
                </tr>
            </thead>
            <tfoot>
                <tr>
                    <td colspan="15">
                        <del class="container">
                            <%= GetPagination(model.PageIndex, model.PageSize, model.TotalRecord)%>
                        </del>
                    </td>
                </tr>
            </tfoot>
            <tbody>
                <%for (int i = 0; listItem != null && i < listItem.Count; i++)
                  { %>
                <tr class="row<%= i%2 %>">
                    <td align="center">
                        <%= i + 1%>
                    </td>
                    <td align="center">
                        <%= GetCheckbox(listItem[i].ID, i)%>
                    </td>
                    <td align="center">
                        <%= listItem[i].ID%>
                    </td>
                    <td align="center">
                        <%= GetPublish_ActivateBasic(listItem[i].ID, listItem[i].Activity)%>
                    </td>
                    <td align="left">
                        <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)">
                            <%= listItem[i].Code%></a>
                    </td>
                    <td>
                        <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)">
                            <%= listItem[i].Name%></a>
                    </td>
                    <td align="left">
                        <%= listItem[i].Address%>
                    </td>
                    <td align="left">
                        <%= listItem[i].PhoneNumber%>
                    </td>
                    <td align="left">
                        <%= listItem[i].Email%>
                    </td>
                    <td align="center">
                        <%= string.Format("{0:dd/MM/yyyy HH:mm}", listItem[i].CreateDate) %>
                    </td>
                </tr>
                <%} %>
            </tbody>
        </table>
        <div class="clr">
        </div>
    </div>
    <div class="b">
        <div class="b">
            <div class="b">
            </div>
        </div>
    </div>
</div>
</form>
