<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<% 
    var model = ViewBag.Model as SysMenuModel;
    var item = ViewBag.Data as WebMenuEntity;

    List<VSW.Lib.Global.ListItem.Item> listParent = null;
    List<VSW.Lib.Global.ListItem.Item> listArea = null;

    bool bolShowRoot = true; // Hiển thị trong chuyên mục sản phẩm thì không cần Item Root

    if (item.Type.ToUpper().Equals("Product".ToUpper())) // Nếu là chuyên mục sản phẩm thì chuyên đề cha chỉ lấy danh mục sp
    {
        listParent = VSW.Lib.Global.ListItem.List.GetList(WebMenuService.Instance, model.LangID, "Product");
        bolShowRoot = false;
    }
    else
        listParent = VSW.Lib.Global.ListItem.List.GetList(WebMenuService.Instance, model.LangID);

    if (model.RecordID > 0)
    {
        //loai bo danh muc con cua danh muc hien tai
        listParent = VSW.Lib.Global.ListItem.List.GetListForEdit(listParent, model.RecordID);

        // Lấy tất cả dữ liệu
        listArea = VSW.Lib.Global.ListItem.List.GetList_TypeList("Mod_Product_Area", "Name,ID", "", "Name");
    }
    else
        // Chỉ lấy những dữ liệu đang sử dụng
        listArea = VSW.Lib.Global.ListItem.List.GetList_TypeList("Mod_Product_Area", "Name,ID", "Activity=1", "Name");
%>
<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="ParentCode" name="ParentCode" value="<%=item.ParentCode %>" />
<input type="hidden" id="CurrentCode" name="CurrentCode" value="<%=item.CurrentCode %>" />
<input type="hidden" id="ParentID_Save" name="ParentID_Save" value="<%=model.ParentID_Save%>" />

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
        <div class="pagetitle icon-48-categories">
            <h2>
                Chuyên mục :
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
                            Tên chuyên mục :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Name" value="<%=item.Name %>" maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Mã :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Code" value="<%=item.Code %>" maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Hình minh họa :</label>
                    </td>
                    <td>
                        <%if (!string.IsNullOrEmpty(item.File))
                            { %>
                        <%= Utils.GetMedia(item.File, 100, 80, string.Empty, true, "id='img_view'")%><%}
                            else
                            { %>
                        <img id="img_view" width="100" height="80" />
                        <%} %>
                        <br />
                        <input class="text_input" type="text" name="File" id="File" style="width: 65%" value="<%=item.File %>" />
                        <input class="text_input" style="width: 75px;" type="button" onclick="ShowFileForm('File');return false;"
                            value="Chọn ảnh" />
                    </td>
                </tr>
                <%if (model.ParentID == 0)
                  { %>
                <tr>
                    <td class="key">
                        <label>
                            Loại :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Type" value="<%=item.Type %>" maxlength="50" />
                    </td>
                </tr>
                <%} %>
                <tr>
                    <td class="key">
                        <label>
                            Chuyên mục cha :</label>
                    </td>
                    <td>
                        <select class="DropDownList" name="ParentID">
                            <%if (bolShowRoot)
                              {  %>
                            <option value="0">Root</option>
                            <%
                              } for (int i = 0; listParent != null && i < listParent.Count; i++)
                              { %>
                            <option <%if(item.ParentID.ToString()==listParent[i].Value) {%>selected<%} %> value='<%= listParent[i].Value%>'>
                                &nbsp;
                                <%= listParent[i].Name%></option>
                            <%} %>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Lĩnh vực :</label>
                    </td>
                    <td>
                        <select class="DropDownList" name="ModProductAreaId" id="ModProductAreaId">
                            <%for (int i = 0; listArea != null && i < listArea.Count; i++)
                                { %>
                            <option <%if(item.ProductAreaId.ToString()==listArea[i].Value) {%>selected<%} %>
                                value='<%= listArea[i].Value%>'>&nbsp;
                                <%= listArea[i].Name%></option>
                            <%} %>
                        </select>
                    </td>
                </tr>
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
</form>
