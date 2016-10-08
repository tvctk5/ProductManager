<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<script language="javascript" type="text/javascript">
    function CheckValidationForm() {
        return true;
    }

    function ReturnParent(key) {
        self.parent.tb_remove();
    }

    $(document).ready(function () {
        $("select[class='DropDownList']").change(function () {
            var controlName = $(this).attr("controlset");
            $("#" + controlName).val($(this).find("option:selected").text());
        });
    });
</script>
<%
    var model = ViewBag.Model as ModProduct_Info_DetailsModel;
    var item = ViewBag.Data as ModProduct_Info_DetailsEntity;

    var PropertiesGroup = model.PropertiesGroup;
    var PropertiesList = model.GetPropertiesList;
    var PropertiesList_Value = model.GetPropertiesList_Value;
    if (PropertiesList_Value == null)
        PropertiesList_Value = new List<ModProduct_Info_DetailsEntity>();

    // Danh sách các giá trị đã có sẵn
    var GetProduct_PropertiesList_Values = model.GetProduct_PropertiesList_Values;
     
%>
<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="DanhSachThuTuThuocTinh" name="DanhSachThuTuThuocTinh" value="<%=model.DanhSachThuTuThuocTinh %>" />
<input type="hidden" id="ProductInfoId" name="ProductInfoId" value="<%=model.ProductInfoId %>" />
<input type="hidden" id="PropertiesGroupsId" name="PropertiesGroupsId" value="<%=model.PropertiesGroupsId %>" />
<div id="toolbar-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%=  GetListCommandThickboxValidation("apply|Lưu,space,closepopup|Đóng")%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Thông tin thuộc tính sản phẩm :
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
            <%
                if (PropertiesGroup != null)
                {
            %>
            <table class="admintable">
                <% List<ModProduct_PropertiesListEntity> lstPropertiesList = PropertiesList;
                   ModProduct_PropertiesGroupsEntity itemGroupList = PropertiesGroup;
                %>
                <tr>
                    <td colspan="6" align="left" style="background-color: #CC6600; height: 40px; color: White;
                        font-weight: bold; padding-left: 10px; font-size: large;">
                        <%=itemGroupList.Name %>
                    </td>
                </tr>
                <%
                    if (lstPropertiesList == null || lstPropertiesList.Count <= 0)
                    { 
                %>
                <tr>
                    <td colspan="6" align="left" style="background-color: red; color: White;">
                        Không có thuộc tính nào thuộc nhóm này
                    </td>
                </tr>
                <% return;
                    }
                    else
                    {%>
                <tr style="background-color: #003399; font-weight: bold; color: #FFFFFF; height: 30px;"
                    align="center">
                    <td style="width: 5% !important; padding-left: 2px;display:none;" nowrap="nowrap">
                        <label>
                            Mã thuộc tính</label>
                    </td>
                    <td style="width: 10% !important; padding-left: 2px;" nowrap="nowrap">
                        <label>
                            Tên thuộc tính</label>
                    </td>
                    <td style="width: 10% !important; padding-left: 2px;" nowrap="nowrap">
                        <label>
                            Dữ liệu cũ</label>
                    </td>
                    <td style="width: 100% !important;">
                        <label>
                            Giá trị thuộc tính</label>
                    </td>
                    <td style="width: 50px !important; padding-left: 2px;" nowrap="nowrap">
                        <label>
                            Đơn vị tính</label>
                    </td>
                    <td style="width: 50px !important; padding-left: 2px;">
                        <label>
                            Lưu dữ liệu</label>
                    </td>
                </tr>
                <%
                    }// End Else
                    string sValue = string.Empty;
                    string sCode = string.Empty;
                    string sDonViTinh = string.Empty;
                    int i = -1;
                    string sSTT = string.Empty;

                    ModProduct_Info_DetailsEntity objDetailsEntity = null;
                    foreach (ModProduct_PropertiesListEntity itemPropertiesList in lstPropertiesList)
                    {
                        objDetailsEntity = PropertiesList_Value.Where(o => o.PropertiesListId == itemPropertiesList.ID).SingleOrDefault();
                        if (objDetailsEntity == null)
                            objDetailsEntity = new ModProduct_Info_DetailsEntity();

                        sCode = itemPropertiesList.Code;
                        sDonViTinh = itemPropertiesList.Unit;
                        sValue = objDetailsEntity.Content;
                        i++; sSTT = i.ToString(); 
                %>
                <tr>
                    <td class="key" style="width: 5% !important; text-align: left  !important; padding-left: 2px;display:none;">
                        <label>
                            <%= itemPropertiesList.Code%></label>
                    </td>
                    <td class="key" style="width: 10% !important; text-align: left  !important; padding-left: 2px;">
                        <label>
                            <%= itemPropertiesList.Name%></label>
                    </td>
                    <td class="key" style="width: 30% !important; padding-left: 2px;">
                        <%if (itemPropertiesList.ViewOldData)
                          {%>
                        <select controlset="Value<%=sSTT%>" class="DropDownList">
                            <option value="0"></option>
                            <%
                              var ListValue = GetProduct_PropertiesList_Values.Where(p => p.PropertiesListId == itemPropertiesList.ID).OrderBy(o => o.Content).ToList();

                              if (ListValue != null && ListValue.Count > 0)
                              {
                                  foreach (var itemValue in ListValue)
                                  {
                            %>
                            <option value="<%=itemValue.Content %>" <%=itemValue.Content==sValue?"selected":""%>><%=itemValue.Content %></option>
                            <%}
                              } %>
                        </select>
                        <%} %>
                    </td>
                    <td class="key" style="width: 60% !important;" nowrap="nowrap" align="left">
                        <%
                          // Kiểu hiển thị dữ liệu: 0: Textbox 1 dòng | 1: Mutiline
                          if (itemPropertiesList.Type == 0)
                          { 
                        %>
                        <input type="text" class="text_input" name="Value<%=sSTT%>" id="Value<%=sSTT%>" maxlength="500"
                            style="width: 98%;" value='<%=sValue%>' />
                        <%}
                          else
                          {  %>
                        <textarea rows="2" class="text_input" name="Value<%=sSTT%>" id="Value<%=sSTT%>" maxlength="500"
                            style="width: 98%;"><%=sValue%></textarea>
                        <%} %>
                    </td>
                    <td class="key" style="width: 10% !important; padding-left: 5px;text-align:left !important" nowrap="nowrap">
                        <label>
                            <%=sDonViTinh%></label>
                    </td>
                    <td class="key" style="width: 5% !important; padding-left: 2px;" nowrap="nowrap">
                        <input type="checkbox" name="chkSaveData<%=sSTT %>" id="chkSaveData<%=sSTT %>" checked="checked" />
                    </td>
                </tr>
                <% }// End foreach Properties
                %>
            </table>
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
<div class="toolbar-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="toolbar-list">
            <%=  GetListCommandThickboxValidation("apply|Lưu,space,closepopup|Đóng")%>
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
