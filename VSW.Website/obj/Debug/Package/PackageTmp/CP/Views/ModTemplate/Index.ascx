<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<% 
    var StyleActivate = ViewBag.StyleActivate as string;
    var ListTemplateStyle = ViewBag.ListTemplateStyle as System.IO.DirectoryInfo[];
%>
<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<div id="toolbar-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="toolbar-list hide" id="toolbar">
            <%=GetDefaultListCommand()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Danh sách các kiểu giao diện</h2>
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

    var VSWController = 'ModTemplate';

    var VSWArrVar = [
                        'limit', 'PageSize'
                   ];


    var VSWArrVar_QS = [
                        'filter_search', 'SearchText'
                      ];

    var VSWArrQT = [
                       1, 'PageIndex',
                      '', 'Sort'
                  ];

    var VSWArrDefault =
                  [
                    '1', 'PageIndex',
                    '20', 'PageSize'
                  ];

    function ActivateStyle(Control) {
    //$(document).ready(function () {
        //$("#btnActivate").click(function () {
        var NameStyle = $(Control).attr("NameStyle");
            $("#NameStyle").val(NameStyle);

            vsw_exec_cmd("[ActivateStyle]");

            return false;
        //});
        //});
    }
</script>
<%= ShowMessage()%>
<div id="element-box">
    <div class="t">
        <div class="t">
            <div class="t">
                <input type="hidden" id="NameStyle" name="NameStyle" value="<%=StyleActivate %>" />
            </div>
        </div>
    </div>
    <div class="m">
        <table class="adminlist" cellspacing="1">
            <thead>
                <tr>
                    <th width="1%">
                        #
                    </th>
                    <th class="title">
                        Tên kiểu hiển thị
                    </th>
                    <th width="1%" nowrap="nowrap">
                        Sử dụng
                    </th>
                </tr>
            </thead>
            <tbody>
                <%for (int i = 0; ListTemplateStyle != null && i < ListTemplateStyle.Count(); i++)
                  { %>
                <tr class="row<%= i%2 %>">
                    <td align="center">
                        <%= i + 1%>
                    </td>
                    <td align="left">
                        <%= ListTemplateStyle[i].Name%>
                    </td>
                    <td align="center">
                        <a namestyle="<%=ListTemplateStyle[i].Name %>" class="jgrid hasTip" href="javascript:void(0);" onclick="<%=(StyleActivate == ListTemplateStyle[i].Name)?"return false;" : "ActivateStyle(this);" %>"
                            id="btnActivate" title="Bấm để sử dụng hoặc không sử dụng."><span class="state <%=(StyleActivate == ListTemplateStyle[i].Name)?"publish" : "unpublish" %>">
                            </span></a>
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
