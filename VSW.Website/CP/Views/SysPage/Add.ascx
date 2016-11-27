<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    SysPageModel model = ViewBag.Model as SysPageModel;
    SysPageEntity item = ViewBag.Data as SysPageEntity;

    List<SysTemplateEntity> listTemplate = SysTemplateService.Instance.CreateQuery()
        .Where(o => o.LangID == model.LangID)
        .OrderByAsc(o => o.Order)
        .ToList();

    List<VSW.Lib.MVC.ModuleInfo> listModule = VSW.Lib.Web.Application.Modules.Where(o => o.IsControl == false).OrderBy(o => o.Order).ToList();

    List<VSW.Lib.Global.ListItem.Item> listParent = VSW.Lib.Global.ListItem.List.GetList(SysPageService.Instance, model.LangID);

    if (model.RecordID > 0)
    {
        //loai bo danh muc con cua danh muc hien tai
        listParent = VSW.Lib.Global.ListItem.List.GetListForEdit(listParent, model.RecordID);
    }
   
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
        <div class="toolbar-list" id="toolbar">
            <%=GetDefaultAddCommand()%>
        </div>
        <div class="pagetitle icon-48-menu">
            <h2>
                Trang :
                <%=  model.RecordID > 0 ? "Chỉnh sửa" : "Thêm mới"%></h2>
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
<%= ShowMessage()%>
<%if (model.RecordID > 0)
  { %>
<div id="submenu-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="submenu-box">
            <div class="submenu-pad">
                <ul id="submenu" class="page">
                    <li><a id="General" class="active">Thông tin chung</a></li>
                    <li><a id="Design">Thiết kế giao diện</a></li>
                    <%-- <li><a id="Preview">Xem trước</a></li>--%>
                </ul>
                <div class="clr">
                </div>
            </div>
        </div>
        <div class="clr">
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
<%} %>
<div id="element-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div id="tab-document" class="col width-100">
            <div id="page-General" class="tab">
                <table class="admintable">
                    <tr>
                        <td class="key">
                            <label>
                                Tên trang :</label>
                        </td>
                        <td>
                            <input class="text_input" type="text" id="Name" name="Name" value="<%=item.Name %>"
                                maxlength="255" />
                        </td>
                    </tr>
                    <tr>
                        <td class="key">
                            <label>
                                Mã trên URL:</label>
                        </td>
                        <td>
                            <input class="text_input" type="text" name="Code" value="<%=item.Code %>" maxlength="255" />
                        </td>
                    </tr>
                    <tr>
                        <td class="key">
                            <label>
                                [SEO] Tiêu đề trên thẻ [A] :</label>
                        </td>
                        <td>
                            <input class="text_input" type="text" name="LinkTitle" value="<%=item.LinkTitle %>"
                                maxlength="255" />
                        </td>
                    </tr>
                    <tr>
                        <td class="key">
                            <label>
                                [SEO] Tiêu đề trang :</label>
                        </td>
                        <td>
                            <input class="text_input" type="text" name="PageTitle" value="<%=item.PageTitle %>"
                                maxlength="255" />
                        </td>
                    </tr>
                    <tr>
                        <td class="key">
                            <label>
                                [SEO] Mô tả trang:</label>
                        </td>
                        <td>
                            <textarea class="text_input" style="height: 100px;" name="PageDescription" id="PageDescription"><%=item.PageDescription%></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td class="key">
                            <label>
                                [SEO] Từ khóa trang:</label>
                        </td>
                        <td>
                            <textarea class="text_input" style="height: 100px;" name="PageKeywords" id="PageKeywords"><%=item.PageKeywords%></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td class="key">
                            <label>
                                Mã thiết kế:</label>
                        </td>
                        <td>
                            <textarea class="text_input" style="height: 50px;" name="Custom" id="custom"><%=item.Custom %></textarea>
                        </td>
                    </tr>
                    <%if (model.ParentID > 0)
                      {%>
                    <tr>
                        <td class="key">
                            <label>
                                Mãu giao diện :</label>
                        </td>
                        <td>
                            <select class="text_input" name="TemplateID">
                                <option value="0"></option>
                                <%for (int i = 0; listTemplate != null && i < listTemplate.Count; i++)
                                  { %>
                                <option <%if(item.TemplateID==listTemplate[i].ID) {%>selected<%} %> value='<%= listTemplate[i].ID%>'>
                                    &nbsp;
                                    <%= listTemplate[i].Name%></option>
                                <%} %>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="key">
                            <label>
                                Chức năng :</label>
                        </td>
                        <td>
                            <select class="text_input" name="ModuleCode" onchange="page_control_change(this.value)">
                                <option value="0"></option>
                                <%for (int i = 0; listModule != null && i < listModule.Count; i++)
                                  { %>
                                <option <%if(item.ModuleCode==listModule[i].Code) {%>selected<%} %> value='<%= listModule[i].Code%>'>
                                    &nbsp;
                                    <%= listModule[i].Name%></option>
                                <%} %>
                            </select>
                            <div id="control_param">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="key">
                            <label>
                                Liên kết chuyên mục :</label>
                        </td>
                        <td>
                            <div id="list_menu">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="key">
                            <label>
                                Trang cha :</label>
                        </td>
                        <td>
                            <select class="text_input" name="ParentID">
                                <option value="0">Root</option>
                                <%for (int i = 0; listParent != null && i < listParent.Count; i++)
                                  { %>
                                <option <%if(item.ParentID.ToString()==listParent[i].Value) {%>selected<%} %> value='<%= listParent[i].Value%>'>
                                    &nbsp;
                                    <%= listParent[i].Name%></option>
                                <%} %>
                            </select>
                        </td>
                    </tr>
                    <%} %>
                    <tr>
                        <td class="key">
                            <label>
                                Duyệt :</label>
                        </td>
                        <td>
                            <input name="Activity" <%= item.Activity ? "checked" : "" %> type="radio" value='1' />
                            Có
                            <input name="Activity" <%= !item.Activity ? "checked" : "" %> type="radio" value='0' />
                            Không
                        </td>
                    </tr>
                    <tr>
                        <td class="key">
                            <label>
                                Hiển thị trên Menu :</label>
                        </td>
                        <td>
                            <input name="ViewInMenu" <%= item.ViewInMenu ? "checked" : "" %> type="radio" value='1' />
                            Có
                            <input name="ViewInMenu" <%= !item.ViewInMenu ? "checked" : "" %> type="radio" value='0' />
                            Không
                        </td>
                    </tr>
                    <tr>
                        <td class="key">
                            <label>
                                Hiển thị trên Sitemap :</label>
                        </td>
                        <td>
                            <input name="ViewInSiteMap" <%= item.ViewInSiteMap ? "checked" : "" %> type="radio"
                                value='1' />
                            Có
                            <input name="ViewInSiteMap" <%= !item.ViewInSiteMap ? "checked" : "" %> type="radio"
                                value='0' />
                            Không
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <td class="key">
                            <label>
                                Css File:</label>
                        </td>
                        <td>
                            <table class="adminlist">
                                <tr>
                                    <th colspan="4">
                                        Thiết lập các tệp tin css trong mẫu giao diện (chú ý thứ tự)
                                    </th>
                                </tr>
                                <tr>
                                    <th colspan="4"> Thay thế Css gốc : 
                                        <input name="ChangeCss" <%= item.ChangeCss ? "checked" : "" %> type="radio"
                                            value='1' />
                                        Có
                                        <input name="ChangeCss" <%= !item.ChangeCss ? "checked" : "" %> type="radio"
                                            value='0' />
                                        Không
                                    </th>
                                </tr>
                                <tr>
                                    <th nowrap="nowrap" style="text-align: center;">
                                        STT
                                    </th>
                                    <th style="text-align: center;">
                                        Tệp Css
                                    </th>
                                    <th style="text-align: center;">
                                        Xóa
                                    </th>
                                    <th style="text-align: center;">
                                        Thêm
                                    </th>
                                </tr>
                                <% if (model.RecordID <= 0)
                                   {%>
                                <tr cssfile="TRUE" stt="1">
                                    <td nowrap="nowrap" style="text-align: right;">
                                        1.
                                    </td>
                                    <td style="width: 100%;">
                                        <input class="text_input" type="text" name="CssFile" id="CssFile1" style="width: 65%" />
                                        <input class="text_input" style="width: 75px;" type="button" onclick="ShowFileForm('CssFile1');return false;"
                                            value="Chọn tệp" />
                                    </td>
                                    <td nowrap="nowrap">
                                        <input type="button" style="display: none;" title="Xóa" value=" - " onclick="RemoveRowCssFile(1); return false;" />
                                    </td>
                                    <td nowrap="nowrap">
                                        <input type="button" title="Thêm" value=" + " onclick="AddRowCssFile(1); return false;" />
                                    </td>
                                </tr>
                                <%}
                                   else
                                   {
                                       // Duyệt các bản ghi nếu có
                                       if (string.IsNullOrEmpty(item.CssFile))
                                       { 
                                %>
                                <tr cssfile="TRUE" stt="1">
                                    <td nowrap="nowrap" style="text-align: right;">
                                        1.
                                    </td>
                                    <td style="width: 100%;">
                                        <input class="text_input" type="text" name="CssFile" id="CssFile1" style="width: 65%" />
                                        <input class="text_input" style="width: 75px;" type="button" onclick="ShowFileForm('CssFile1');return false;"
                                            value="Chọn tệp" />
                                    </td>
                                    <td nowrap="nowrap">
                                        <input type="button" style="display: none;" title="Xóa" value=" - " onclick="RemoveRowCssFile(1); return false;" />
                                    </td>
                                    <td nowrap="nowrap">
                                        <input type="button" title="Thêm" value=" + " onclick="AddRowCssFile(1); return false;" />
                                    </td>
                                </tr>
                                <%}
                                       else
                                       {
                                           string[] lstCssFile = item.CssFile.Split(',');
                                           for (int i = 0; i < lstCssFile.Length; i++)
                                           {
                                %>
                                <tr cssfile="TRUE" stt="1">
                                    <td nowrap="nowrap" style="text-align: right;">
                                        <%= (i +1).ToString() + "." %>
                                    </td>
                                    <td style="width: 100%;">
                                        <input class="text_input" type="text" name="CssFile" id="CssFile<%= (i +1).ToString() %>"
                                            style="width: 65%" value="<%= lstCssFile[i] %>" />
                                        <input class="text_input" style="width: 75px;" type="button" onclick="ShowFileForm('CssFile<%= (i +1).ToString() %>');return false;"
                                            value="Chọn tệp" />
                                    </td>
                                    <td nowrap="nowrap">
                                        <input type="button" <%= (i==0? "style='display:none;'" : ""  ) %> title="Xóa" value=" - "
                                            onclick="RemoveRowCssFile(<%= (i +1) %>); return false;" />
                                    </td>
                                    <td nowrap="nowrap">
                                        <input type="button" <%= ((i + 1) ==lstCssFile.Length? "" : "style='display:none;'" ) %>
                                            title="Thêm" value=" + " onclick="AddRowCssFile(<%= (i +1) %>); return false;" />
                                    </td>
                                </tr>
                                <%}
                                       }
                                   } %>
                            </table>
                            <script type="text/javascript">
                                $(document).ready(function () { });
                                function AddRowCssFile(currentStt) {
                                    if (currentStt == null)
                                        currentStt = 0;

                                    var nextStt = currentStt + 1;

                                    var rowNext = "";
                                    rowNext += "<tr CssFile=\"TRUE\" stt=\"" + nextStt + "\">";
                                    rowNext += "	<td nowrap=\"nowrap\" style=\"text-align:right;\">";
                                    rowNext += " " + nextStt + ".";
                                    rowNext += "	</td>";
                                    rowNext += "	<td style=\"width: 100%;\">";
                                    rowNext += "		<input class=\"text_input\" type=\"text\" name=\"CssFile\" id=\"CssFile" + nextStt + "\" style=\"width: 65%\" />";
                                    rowNext += "		<input class=\"text_input\" style=\"width: 75px;\" type=\"button\" onclick=\"ShowFileForm('CssFile" + nextStt + "');return false;\"";
                                    rowNext += "			value=\"Chọn tệp\" />";
                                    rowNext += "	</td>";
                                    rowNext += "	<td nowrap=\"nowrap\">";
                                    rowNext += "		<input type=\"button\" title=\"Xóa\" value=\" - \" onclick=\"RemoveRowCssFile(" + nextStt + "); return false;\"/>";
                                    rowNext += "	</td>";
                                    rowNext += "	<td nowrap=\"nowrap\">";
                                    rowNext += "		<input type=\"button\" title=\"Thêm\" value=\" + \" onclick=\"AddRowCssFile(" + nextStt + "); return false;\"/>";
                                    rowNext += "	</td>";
                                    rowNext += "</tr>";
                                    // Thêm dòng
                                    $(rowNext).insertAfter($("tr[CssFile][stt=" + currentStt + "]"));

                                    // Chỉ hiện button cuối cùng
                                    var allButton = $("tr[CssFile] input[title='Thêm']");
                                    if (allButton != null) {
                                        for (var i = 0; i < allButton.length; i++) {
                                            if (i == allButton.length - 1)
                                                break;

                                            $(allButton[i]).hide();
                                        }
                                    }

                                    // Nút xóa
                                    $("tr[CssFile] input[title='Xóa']").first().hide();
                                }

                                function RemoveRowCssFile(currentStt) {
                                    $("tr[CssFile][stt=" + currentStt + "]").remove();

                                    var listRows = $("tr[CssFile][stt]");
                                    var stt = 0;
                                    for (var i = 0; i < listRows.length; i++) {
                                        var rowSelect = $(listRows[i]);
                                        // Số TT
                                        rowSelect.attr("stt", (++stt).toString());
                                        // Giá trị STT
                                        rowSelect.find("td").first().html(stt.toString() + ".");
                                        // Input File rowSelect
                                        rowSelect.find("input[name='CssFile']").first().attr("id", "CssFile" + stt.toString());
                                        rowSelect.find("input.text_input").last().attr("onclick", "ShowFileForm('CssFile" + stt.toString() + "');return false;");
                                        // button 
                                        rowSelect.find("input[type='button'][title='Xóa']").first().attr("onclick", "RemoveRowCssFile(" + stt.toString() + ");return false;");
                                        rowSelect.find("input[type='button'][title='Thêm']").first().attr("onclick", "AddRowCssFile(" + stt.toString() + ");return false;");
                                    }

                                    // Chỉ hiện button cuối cùng
                                    var allButton = $("tr[CssFile] input[title='Thêm']");
                                    if (allButton != null) {
                                        for (var i = 0; i < allButton.length; i++) {
                                            if (i == allButton.length - 1) {
                                                $(allButton[i]).show();
                                                break;
                                            }

                                            $(allButton[i]).hide();
                                        }
                                    }

                                    // Nút xóa
                                    $("tr[CssFile] input[title='Xóa']").first().hide();
                                }
                            </script>
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <td class="key">
                            <label>
                                Js File:</label>
                        </td>
                        <td>
                            <table class="adminlist">
                                <tr>
                                    <th colspan="4">
                                        Thiết lập các tệp tin js trong mẫu giao diện (chú ý thứ tự)
                                    </th>
                                </tr>
                                <tr>
                                    <th colspan="4"> Thay thế Js gốc : 
                                        <input name="ChangeJs" <%= item.ChangeJs ? "checked" : "" %> type="radio"
                                            value='1' />
                                        Có
                                        <input name="ChangeJs" <%= !item.ChangeJs ? "checked" : "" %> type="radio"
                                            value='0' />
                                        Không
                                    </th>
                                </tr>
                                <tr>
                                    <th nowrap="nowrap" style="text-align: center;">
                                        STT
                                    </th>
                                    <th style="text-align: center;">
                                        Tệp Js
                                    </th>
                                    <th style="text-align: center;">
                                        Xóa
                                    </th>
                                    <th style="text-align: center;">
                                        Thêm
                                    </th>
                                </tr>
                                <% if (model.RecordID <= 0)
                                   {%>
                                <tr jsfile="TRUE" stt="1">
                                    <td nowrap="nowrap" style="text-align: right;">
                                        1.
                                    </td>
                                    <td style="width: 100%;">
                                        <input class="text_input" type="text" name="JsFile" id="JsFile1" style="width: 65%" />
                                        <input class="text_input" style="width: 75px;" type="button" onclick="ShowFileForm('JsFile1');return false;"
                                            value="Chọn tệp" />
                                    </td>
                                    <td nowrap="nowrap">
                                        <input type="button" style="display: none;" title="Xóa" value=" - " onclick="RemoveRowJsFile(1); return false;" />
                                    </td>
                                    <td nowrap="nowrap">
                                        <input type="button" title="Thêm" value=" + " onclick="AddRowJsFile(1); return false;" />
                                    </td>
                                </tr>
                                <%}
                                   else
                                   {
                                       // Duyệt các bản ghi nếu có
                                       if (string.IsNullOrEmpty(item.JsFile))
                                       { 
                                %>
                                <tr jsfile="TRUE" stt="1">
                                    <td nowrap="nowrap" style="text-align: right;">
                                        1.
                                    </td>
                                    <td style="width: 100%;">
                                        <input class="text_input" type="text" name="JsFile" id="JsFile1" style="width: 65%" />
                                        <input class="text_input" style="width: 75px;" type="button" onclick="ShowFileForm('JsFile1');return false;"
                                            value="Chọn tệp" />
                                    </td>
                                    <td nowrap="nowrap">
                                        <input type="button" style="display: none;" title="Xóa" value=" - " onclick="RemoveRowJsFile(1); return false;" />
                                    </td>
                                    <td nowrap="nowrap">
                                        <input type="button" title="Thêm" value=" + " onclick="AddRowJsFile(1); return false;" />
                                    </td>
                                </tr>
                                <%}
                                       else
                                       {
                                           string[] lstJsFile = item.JsFile.Split(',');
                                           for (int i = 0; i < lstJsFile.Length; i++)
                                           {
                                %>
                                <tr jsfile="TRUE" stt="1">
                                    <td nowrap="nowrap" style="text-align: right;">
                                        <%= (i +1).ToString() + "." %>
                                    </td>
                                    <td style="width: 100%;">
                                        <input class="text_input" type="text" name="JsFile" id="JsFile<%= (i +1).ToString() %>"
                                            style="width: 65%" value="<%= lstJsFile[i] %>" />
                                        <input class="text_input" style="width: 75px;" type="button" onclick="ShowFileForm('JsFile<%= (i +1).ToString() %>');return false;"
                                            value="Chọn tệp" />
                                    </td>
                                    <td nowrap="nowrap">
                                        <input type="button" <%= (i==0? "style='display:none;'" : ""  ) %> title="Xóa" value=" - "
                                            onclick="RemoveRowJsFile(<%= (i +1) %>); return false;" />
                                    </td>
                                    <td nowrap="nowrap">
                                        <input type="button" <%= ((i + 1) ==lstJsFile.Length? "" : "style='display:none;'" ) %>
                                            title="Thêm" value=" + " onclick="AddRowJsFile(<%= (i +1) %>); return false;" />
                                    </td>
                                </tr>
                                <%}
                                       }
                                   } %>
                            </table>
                            <script type="text/javascript">
                                $(document).ready(function () { });
                                function AddRowJsFile(currentStt) {
                                    if (currentStt == null)
                                        currentStt = 0;

                                    var nextStt = currentStt + 1;

                                    var rowNext = "";
                                    rowNext += "<tr JsFile=\"TRUE\" stt=\"" + nextStt + "\">";
                                    rowNext += "	<td nowrap=\"nowrap\" style=\"text-align:right;\">";
                                    rowNext += " " + nextStt + ".";
                                    rowNext += "	</td>";
                                    rowNext += "	<td style=\"width: 100%;\">";
                                    rowNext += "		<input class=\"text_input\" type=\"text\" name=\"JsFile\" id=\"JsFile" + nextStt + "\" style=\"width: 65%\" />";
                                    rowNext += "		<input class=\"text_input\" style=\"width: 75px;\" type=\"button\" onclick=\"ShowFileForm('JsFile" + nextStt + "');return false;\"";
                                    rowNext += "			value=\"Chọn tệp\" />";
                                    rowNext += "	</td>";
                                    rowNext += "	<td nowrap=\"nowrap\">";
                                    rowNext += "		<input type=\"button\" title=\"Xóa\" value=\" - \" onclick=\"RemoveRowJsFile(" + nextStt + "); return false;\"/>";
                                    rowNext += "	</td>";
                                    rowNext += "	<td nowrap=\"nowrap\">";
                                    rowNext += "		<input type=\"button\" title=\"Thêm\" value=\" + \" onclick=\"AddRowJsFile(" + nextStt + "); return false;\"/>";
                                    rowNext += "	</td>";
                                    rowNext += "</tr>";
                                    // Thêm dòng
                                    $(rowNext).insertAfter($("tr[JsFile][stt=" + currentStt + "]"));

                                    // Chỉ hiện button cuối cùng
                                    var allButton = $("tr[JsFile] input[title='Thêm']");
                                    if (allButton != null) {
                                        for (var i = 0; i < allButton.length; i++) {
                                            if (i == allButton.length - 1)
                                                break;

                                            $(allButton[i]).hide();
                                        }
                                    }

                                    // Nút xóa
                                    $("tr[JsFile] input[title='Xóa']").first().hide();
                                }

                                function RemoveRowJsFile(currentStt) {
                                    $("tr[JsFile][stt=" + currentStt + "]").remove();

                                    var listRows = $("tr[JsFile][stt]");
                                    var stt = 0;
                                    for (var i = 0; i < listRows.length; i++) {
                                        var rowSelect = $(listRows[i]);
                                        // Số TT
                                        rowSelect.attr("stt", (++stt).toString());
                                        // Giá trị STT
                                        rowSelect.find("td").first().html(stt.toString() + ".");
                                        // Input File rowSelect
                                        rowSelect.find("input[name='JsFile']").first().attr("id", "JsFile" + stt.toString());
                                        rowSelect.find("input.text_input").last().attr("onclick", "ShowFileForm('JsFile" + stt.toString() + "');return false;");
                                        // button 
                                        rowSelect.find("input[type='button'][title='Xóa']").first().attr("onclick", "RemoveRowJsFile(" + stt.toString() + ");return false;");
                                        rowSelect.find("input[type='button'][title='Thêm']").first().attr("onclick", "AddRowJsFile(" + stt.toString() + ");return false;");
                                    }

                                    // Chỉ hiện button cuối cùng
                                    var allButton = $("tr[JsFile] input[title='Thêm']");
                                    if (allButton != null) {
                                        for (var i = 0; i < allButton.length; i++) {
                                            if (i == allButton.length - 1) {
                                                $(allButton[i]).show();
                                                break;
                                            }

                                            $(allButton[i]).hide();
                                        }
                                    }

                                    // Nút xóa
                                    $("tr[JsFile] input[title='Xóa']").first().hide();
                                }
                            </script>
                        </td>
                    </tr>
                    <tr>
                        <td class="key">
                            <label>
                                Mã css:</label>
                        </td>
                        <td>
                            <textarea class="text_input" rows="5" name="CssContent" id="CssContent"><%=item.CssContent%></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td class="key">
                            <label>
                                Mã js:</label>
                        </td>
                        <td>
                            <textarea class="text_input" rows="5" name="JsContent" id="JsContent"><%=item.JsContent%></textarea>
                        </td>
                    </tr>
                </table>
            </div>
            <%if (model.RecordID > 0)
              { %>
            <div id="page-Design" class="tab">
                <script type="text/javascript">

                    var ListOnLoad = new Array();
                    var has_update = false;

                    function get_content_design() {
                        var vsw_container = window.frames['frame_design'].document.getElementById('vsw_container');
                        if (vsw_container)
                            document.getElementById('content_design').innerHTML = vsw_container.innerHTML;
                        else
                            document.getElementById('content_design').innerHTML = window.frames['frame_design'].document.childNodes[0].innerHTML;

                        if (has_update)
                            custom_update();

                        for (var i = 0; i < ListOnLoad.length; i++) {
                            layout_change(ListOnLoad[i].pid, ListOnLoad[i].list_param, ListOnLoad[i].layout)
                        }
                    }

                    function custom_update() {
                        var ranNum = Math.floor(Math.random() * 999999);
                        sLink = '/{CPPath}/Ajax/PageGetCustom/<%= item.ID %>.aspx?rnd=' + ranNum;
                        AjaxRequest.get(
	                        {
	                            'url': sLink
	                        , 'onSuccess': function (req) {
	                            with (req.responseXML.getElementsByTagName('Item').item(0)) {
	                                var content = getNodeValue(getElementsByTagName('Html'));
	                                document.getElementById("custom").innerHTML = content;
	                            }
	                        }
	                        , 'onError': function (req) { }
	                        }
                        )
                    }

                    function cp_update(value) {

                        has_update = true;

                        var input = document.createElement('input');
                        input.type = 'hidden';
                        input.name = 'vsw_submit';
                        input.value = value;

                        var cp_form = window.frames['frame_design'].document.forms[0];

                        var arr_tag_input = window.frames['frame_design'].document.getElementsByTagName('input');
                        for (var i = 0; i < arr_tag_input.length; i++) {
                            if (document.getElementById(arr_tag_input[i].id))
                                arr_tag_input[i].value = document.getElementById(arr_tag_input[i].id).value;
                        }

                        var arr_tag_select = window.frames['frame_design'].document.getElementsByTagName('select');
                        for (var i = 0; i < arr_tag_select.length; i++) {
                            if (document.getElementById(arr_tag_select[i].id))
                                arr_tag_select[i].value = document.getElementById(arr_tag_select[i].id).value;
                        }

                        var arr_tag_input = window.frames['frame_design'].document.getElementsByTagName('textarea');
                        for (var i = 0; i < arr_tag_input.length; i++) {
                            if (document.getElementById(arr_tag_input[i].id))
                                arr_tag_input[i].value = document.getElementById(arr_tag_input[i].id).value;
                        }

                        cp_form.appendChild(input);
                        cp_form.submit();

                    }

                </script>
                <script type="text/javascript">
                    function dragStart(ev) {
                        ev.dataTransfer.effectAllowed = 'move';
                        ev.dataTransfer.setData("Text", ev.target.getAttribute('id'));
                        ev.dataTransfer.setDragImage(ev.target, 0, 0);
                        return true;
                    }
                    function dragEnter(ev) {
                        event.preventDefault();
                        return true;
                    }
                    function dragOver(ev) {
                        return false;
                    }
                    function dragDrop(ev) {
                        var src = ev.dataTransfer.getData("Text");

                        var id = ev.target.id;

                        if (id != 'to_hlid_' + src)
                            cp_update(id + '$' + src + '|move');

                        //ev.target.appendChild(document.getElementById(src));

                        ev.stopPropagation();
                        return false;
                    }
                    function do_display(id_tbl) {
                        var o = document.getElementById(id_tbl);
                        if (o.style.display == '')
                            o.style.display = 'none';
                        else
                            o.style.display = '';
                    }
                </script>
                <div id="content_design">
                </div>
                <iframe id="frame_design" name="frame_design" src="/{CPPath}/Design/EditPage.aspx?id=<%= model.RecordID %>"
                    onload="get_content_design()" style="height: 0; width: 0; border-width: 0;">
                </iframe>
            </div>
            <%--<div id="page-Preview"></div>--%>
            <%} %>
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
<script type="text/javascript" src="/{CPPath}/Content/ckeditor/ckeditor.js"></script>
<script type="text/javascript">
    function menu_change(name)
    {
       var txtPageName = document.getElementById('Name');
       if(txtPageName.value == '')
       {
           var i = name.indexOf('---- ');
           if (i > -1) 
             txtPageName.value = name.substr(i + 5);
       }
    }

    function setup_editor()
    {
      if(document.getElementById('Content'))
      {
          var editor = CKEDITOR.instances['Content'];
          if (editor) { editor.destroy(true); }
          CKEDITOR.replace('Content');
      }
    }

    function page_control_change(control_id) { 
     var ranNum = Math.floor(Math.random() * 999999);
     sLink = '/{CPPath}/Ajax/PageGetControl.aspx/LangID/<%= model.LangID %>/PageID/<%=item.ID %>/ModuleCode/'+control_id+'?rnd=' + ranNum;	
     AjaxRequest.get(
	        {
	        'url':sLink
	        ,'onSuccess':function(req){	 
              with(req.responseXML.getElementsByTagName('Item').item(0))
			    {
                    var js = getNodeValue(getElementsByTagName('JS'));
			        var content = getNodeValue(getElementsByTagName('Html'));
                    var list_menu = getNodeValue(getElementsByTagName('Params'));
                    list_menu = '<select onchange="menu_change(this.options[this.selectedIndex].text)" class="text_input" name="MenuID"><option value="0">Root</option>' + list_menu + '</select>';

			        document.getElementById("control_param").innerHTML = content;
                    document.getElementById("list_menu").innerHTML = list_menu;

                    if(js != '') 
                       eval(js);

                    window.setTimeout("setup_editor()", 100);
    		    }
	        }
	        ,'onError':function(req){}
	        }
        )
    }

    <%if(!string.IsNullOrEmpty(item.ModuleCode) ){ %>page_control_change('<%= item.ModuleCode %>');<%} %>

</script>
<script type="text/javascript">
    CKFinder.setupCKEditor(null, '/{CPPath}/Content/ckfinder/');
</script>
</form>
