<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<script src="/{CPPath}/Content/add/js/ProductionInfo.js" type="text/javascript"></script>
<%
    var model = ViewBag.Model as ModProduct_NationalModel;
    var item = ViewBag.Data as ModProduct_NationalEntity;
    var listItem = model.ListArea as List<ModProduct_National_AreaEntity>;
%>
<script language="javascript" type="text/javascript">
    function CheckValidationForm() {
        return true;
    }

    // link Xóa khu vực
    var urlArea_Delete = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_National/PostData.aspx?Type=areaDelete")%>';
    var urlArea_GetList = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_National/PostData.aspx?Type=areaGetList")%>';
    function ReloadData_Area() {
        AreaGetList(urlArea_GetList, null, '<%= model.RecordID%>', true);
    }
    function ReLoadDataInParent(ListString) {
        $("tbody[class='tbl-data-area']").html(ListString);
    }

    $(document).ready(function () {
        $("#btnRefresh").click(function () {
            ReloadData_Area();
            alert("Đã cập nhật dữ liệu mới nhất");
        });
    });

</script>
<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="RecordID" name="RecordID" value="<%=model.RecordID %>" />
<div id="toolbar-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <!--GetDefaultAddCommand()-->
            <%= GetDefaultAddCommandValidation()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Quốc gia :
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
<div id="element-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="col width-100">
            <table class="admintable">
                <tr>
                    <td class="key">
                        <label>
                            Mã :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Code" id="Code" value="<%=item.Code %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Tên :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Name" id="Name" value="<%=item.Name %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Create date :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="CreateDate" id="CreateDate" value="<%=item.CreateDate %>"
                            maxlength="255" />
                    </td>
                </tr>
                <%if (CPViewPage.UserPermissions.Approve)
                  {%>
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
                <%}
                  if (model.RecordID > 0)
                  {%>
                <tr>
                    <td class="key">
                        <label>
                            Khu vực :</label>
                    </td>
                    <td>
                        <div style="text-align: right; margin-top: 5px; margin-bottom: 5px;">
                            <input id="btnRefresh" value="Làm mới dữ liệu" class="text_input button-function button-background-image-refresh" style="width: 150px;"
                                type="button"  />
                            <input id="btnAddArea" onclick="tb_show('', '/CP/FormProduct_Area/Add.aspx/ProductNationalId/<%=model.RecordID %>TB_iframe=true;height=300;width=850;', ''); return false;"
                                value="Thêm khu vực" class="text_input button-function button-background-image-add" style="width: 150px;"
                                type="button"  />
                        </div>
                        <div>
                            <table class="adminlist" cellspacing="1">
                                <thead>
                                    <tr>
                                        <th width="1%">
                                            #
                                        </th>
                                        <th width="1%" nowrap="nowrap" class="hide">
                                            ID
                                        </th>
                                        <th width="1%" nowrap="nowrap">
                                            Mã khu vực
                                        </th>
                                        <th class="title">
                                            Tên khu vực
                                        </th>
                                        <th width="1%" nowrap="nowrap">
                                            Ngày tạo
                                        </th>
                                        <th width="1%" nowrap="nowrap">
                                            Trạng thái
                                        </th>
                                        <th width="1%" nowrap="nowrap">
                                            Sửa
                                        </th>
                                        <th width="1%" nowrap="nowrap">
                                            Xóa
                                        </th>
                                    </tr>
                                </thead>
                                <tbody class="tbl-data-area">
                                    <% if (listItem == null || listItem.Count <= 0)
                                       { %>
                                    Chưa có vùng miền nào
                                    <%}
                                       else
                                       {
                                           for (int i = 0; listItem != null && i < listItem.Count; i++)
                                           { %>
                                    <tr class="row<%= i%2 %>">
                                        <td align="center">
                                            <%= i + 1%>
                                        </td>
                                        <td align="center" class="hide">
                                            <%= listItem[i].ID%>
                                        </td>
                                        <td align="left" nowrap="nowrap">
                                            <%= listItem[i].Code%>
                                        </td>
                                        <td>
                                            <%= listItem[i].Name%>
                                        </td>
                                        <td align="center">
                                            <%= string.Format("{0:dd/MM/yyyy HH:mm}", listItem[i].CreateDate)%>
                                        </td>
                                        <td align="center">
                                            <%if (listItem[i].Activity)
                                              {%>
                                            <span class='jgrid'><span class='state activate' title='Đang sử dụng'></span></span>
                                            <%}
                                              else
                                              { %>
                                            <span class='jgrid'><span class='state unpublish' title='Không sử dụng'></span></span>
                                            <%} %>
                                        </td>
                                        <td align="center">
                                            <a class="jgrid" title="Sửa khu vực" href="javascript:void(0);" onclick="tb_show('', '/CP/FormProduct_Area/Add.aspx/RecordID/<%=listItem[i].ID %>/ProductNationalId/<%=model.RecordID %>TB_iframe=true;height=300;width=850;', ''); return false;">
                                                <span class="jgrid"><span class="state edit"></span></span></a>
                                        </td>
                                        <td align="center">
                                            <a class="jgrid" title="Xóa khu vực" href="javascript:void(0);" onclick="AreaDelete_DeleteTr(urlArea_Delete,this,'<%=listItem[i].ID %>');return false;">
                                                <span class="jgrid"><span class="state delete"></span></span></a>
                                        </td>
                                    </tr>
                                    <%}
                                       } %>
                                </tbody>
                            </table>
                        </div>
                    </td>
                </tr>
                <%} %>
            </table>
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
<div class="toolbar-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="toolbar-list">
            <%= GetDefaultAddCommandValidation()%>
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
</form>
