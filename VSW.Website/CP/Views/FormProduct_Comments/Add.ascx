<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<script language="javascript" type="text/javascript">
    function CheckValidationForm() {
        return true;
    }

    function ReturnParent(Key) {
        self.parent.tb_remove();
    }
</script>
<%
    var model = ViewBag.Model as ModProduct_CommentsModel;
    var item = ViewBag.Data as ModProduct_CommentsEntity;
%>
<script type="text/javascript" src="/{CPPath}/Content/ckeditor/ckeditor.js"></script>
<script type="text/javascript" src="/{CPPath}/Content/ckfinder/ckfinder.js"></script>
<script type="text/javascript">
    CKFinder.setupCKEditor(null, '/{CPPath}/Content/ckfinder/');
</script>
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
            <!--GetDefaultAddCommand()-->
            <%= GetListCommandThickboxValidation("apply|Lưu,space,list|Danh sách,space,closepopup|Đóng")%>
        </div>
        <div class="pagetitle icon-16-Edit" style="padding-left: 20px !important;">
            <h2 style="line-height: normal !important;">
                Nhận xét :
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
                    <td class="key" nowrap="nowrap" style="width: 5% !important;">
                        <label>
                            Sản phẩm :</label>
                    </td>
                    <td>
                        <label name="ProductInfoName" id="ProductInfoName">
                            <input class="text_input" readonly="readonly" type="text" value='<%= model.ProductInfoName %>' />
                            <input type="hidden" name="ProductInfoId" id="ProductInfoId" value="<%=item.ProductInfoId %>" />
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap" style="width: 5% !important;">
                        <label>
                            Khách hàng :</label>
                    </td>
                    <td>
                        <input class="text_input" readonly="readonly" type="text" value='<%= model.CustomerFullName +" ("+ model.CustomerUserName + ")" %>' />
                        <input type="hidden" name="CustomersId" id="CustomersId" value="<%=item.CustomersId %>" />
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="key" nowrap="nowrap" style="width: 5% !important;">
                        <label>
                            User id :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="UserId" id="UserId" value="<%=item.UserId %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap" style="width: 5% !important;">
                        <label>
                            Nội dung :</label>
                    </td>
                    <td class="content">
                        <textarea class="ckeditor" style="width: 100%;" name="Content" id="Content"><%=item.Content%></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap" style="width: 5% !important;">
                        <label>
                            Ngày tạo :</label>
                    </td>
                    <td>
                        <input readonly="readonly" class="text_input" type="text" name="CreateDate" id="CreateDate"
                            value="<%=item.CreateDate %>" maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap" style="width: 5% !important;">
                        <label>
                            Ngày sửa cuối :</label>
                    </td>
                    <td>
                        <input readonly="readonly" class="text_input" type="text" name="ModifiedDate" id="ModifiedDate"
                            value="<%=item.ModifiedDate %>" maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap" style="width: 5% !important;">
                        <label>
                            Người sửa cuối  :</label>
                    </td>
                    <td>
                        <input readonly="readonly" class="text_input" type="text" name="UserNameModified"
                            id="UserNameModified" value="<%=model.UserNameModified%>" maxlength="255" />
                    </td>
                </tr>
                <%if (CPViewPage.UserPermissions.Approve)
                  {%>
                <tr>
                    <td class="key" nowrap="nowrap" style="width: 0px !important;">
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
            <%= GetListCommandThickboxValidation("apply|Lưu,space,list|Danh sách,space,closepopup|Đóng")%>
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
