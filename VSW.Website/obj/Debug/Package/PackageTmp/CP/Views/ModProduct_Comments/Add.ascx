<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_CommentsModel;
    var item = ViewBag.Data as ModProduct_CommentsEntity;
    var objProduct = ViewBag.ProductCurrent as ModProduct_InfoEntity;
%>
<script type="text/javascript" src="/{CPPath}/Content/ckeditor/ckeditor.js"></script>
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
            <%= GetDefaultAddCommand()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Bình luận :
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
                            Sản phẩm :</label>
                    </td>
                    <td>
                        <%if (model.RecordID > 0)
                          { %>
                        <a href="javascript:RedirectController('ModProduct_Info','Add', <%= item.ProductInfoId %>)">
                            <%= objProduct.Name%>
                        </a>
                        <%}
                          else
                          { %>
                        <input class="text_input" type="text" name="ProductInfoId" id="ProductInfoId" value="<%=item.ProductInfoId %>"
                            maxlength="255" />
                        <%} %>
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
                            Số điện thoại :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="PhoneNumber" id="PhoneNumber" value="<%=item.PhoneNumber %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Email :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Email" id="Email" value="<%=item.Email %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Địa chỉ :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Address" id="Address" value="<%=item.Address %>"
                            maxlength="255" />
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Nội dung :</label>
                    </td>
                    <td class="content">
                        <textarea class="ckeditor" style="width: 100%; height: 500px" name="Content" id="Content"><%=item.Content%></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="key">
                        <label>
                            Approved :</label>
                    </td>
                    <td>
                        <input name="Approved" <%= item.Approved ? "checked" : "" %> type="radio" value='1' />
                        Có
                        <input name="Approved" <%= !item.Approved ? "checked" : "" %> type="radio" value='0' />
                        Không
                    </td>
                </tr>
                <%if (model.RecordID > 0)
                  {%>
                <tr>
                    <td class="key">
                        <label>
                            Ngày tạo :</label>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="CreateDate" id="CreateDate" value="<%=item.CreateDate %>"
                            maxlength="255" readonly="readonly" />
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
</form>
