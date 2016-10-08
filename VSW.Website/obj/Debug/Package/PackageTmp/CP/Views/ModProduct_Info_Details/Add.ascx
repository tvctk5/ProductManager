<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<script language="javascript" type="text/javascript">
    function CheckValidationForm() {
        return true;
    }
</script>
<%
    var model = ViewBag.Model as ModProduct_Info_DetailsModel;
    var item = ViewBag.Data as ModProduct_Info_DetailsEntity;

    var GroupList = model.GetGroupList;
    var PropertiesList = model.GetPropertiesList;
%>
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
            <%--<%= GetDefaultAddCommandValidation()%>--%>
            <%= GetListCommandValidation("apply|Lưu,save|Lưu  &amp; đóng,space,cancel|Đóng") %>
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
<div id="divMessError" style="display:none;"></div>
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
                if (GroupList != null && GroupList.Count > 0)
                { 
            %>
            <table class="admintable">
                <% List<ModProduct_PropertiesListEntity> lstPropertiesList = null;

                   foreach (ModProduct_PropertiesGroupsEntity itemGroupList in GroupList)
                   {%>
                <tr>
                    <td colspan="3" align="left" style="background-color: #CC6600; height: 40px; color: White;
                        font-weight: bold; padding-left: 10px; font-size: large;">
                        <%=itemGroupList.Name %>
                    </td>
                </tr>
                <%
                       if (PropertiesList == null || PropertiesList.Count <= 0)
                           return;

                       lstPropertiesList = PropertiesList.Where(o => o.PropertiesGroupsId == itemGroupList.ID).ToList();
                       if (lstPropertiesList == null || lstPropertiesList.Count <= 0)
                       { 
                %>
                <tr>
                    <td colspan="3" align="left" style="background-color: red; color: White;">
                        Không có thuộc tính nào thuộc nhóm này
                    </td>
                </tr>
                <% return;
                       }
                       else
                       {%>
                <tr style="background-color: #003399; font-weight: bold; color: #FFFFFF; height: 30px;"
                    align="center">
                    <td style="width: 5% !important; padding-left: 2px;" nowrap="nowrap">
                        <label>
                            Mã thuộc tính</label>
                    </td>
                    <td style="width: 10% !important; padding-left: 2px;" nowrap="nowrap">
                        <label>
                            Tên thuộc tính</label>
                    </td>
                    <td style="width: 100% !important;">
                        <label>
                            Giá trị thuộc tính</label>
                    </td>
                </tr>
                <%
                       }// End Else
                       string sValue = string.Empty;
                       string sCode = string.Empty;
                       foreach (ModProduct_PropertiesListEntity itemPropertiesList in lstPropertiesList)
                       {
                           sCode = itemPropertiesList.Code;
                           sValue = model.GetValueFromColumnName(item, sCode); 
                %>
                <tr>
                    <td class="key" style="width: 5% !important; text-align: left  !important; padding-left: 2px;"
                        nowrap="nowrap">
                        <label>
                            <%= itemPropertiesList.Code%></label>
                    </td>
                    <td class="key" style="width: 10% !important; text-align: left  !important; padding-left: 2px;"
                        nowrap="nowrap">
                        <label>
                            <%= itemPropertiesList.Name%></label>
                    </td>
                    <td style="width: 100% !important;" nowrap="nowrap" align="left">
                        <textarea rows="2" class="text_input" name="<%=sCode%>" id="<%=sCode%>" maxlength="500"
                            style="width: 98%;"><%=sValue %></textarea>
                    </td>
                </tr>
                <% }// End foreach Properties
                   }// End foreach Group Properties
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
            <%-- <%= GetDefaultAddCommandValidation()%>--%>
            <%= GetListCommandValidation("apply|Lưu,save|Lưu  &amp; đóng,space,cancel|Đóng") %>
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
