<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<%
    var model = ViewBag.Model as ModProductModel;
    var item = ViewBag.Data as ModProductEntity;
%>

<script type="text/javascript" src="/{CPPath}/Content/ckeditor/ckeditor.js"></script>
<script type="text/javascript">
    CKFinder.setupCKEditor(null, '/{CPPath}/Content/ckfinder/');
</script>

<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />

<div id="toolbar-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%= GetDefaultAddCommand()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>Product : <%=  model.RecordID > 0 ? "Chỉnh sửa" : "Thêm mới"%></h2>
        </div>
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"><div class="b"></div></div></div>
</div>
<div class="clr"></div>

<%= ShowMessage()%>

<div id="element-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="col width-100">
          <table class="admintable">
            <tr>
                <td class="key">
                    <label>Chuyên mục :</label>
                </td>
                <td>
                     <select name="MenuID" id="MenuID" class="text_input">
                         <option value="0">Root</option>
                         <%= Utils.ShowDDLMenuByType("Product", model.LangID, item.MenuID)%> 
                    </select>
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Tên :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Name" id="Name" value="<%=item.Name %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Mã :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Code" id="Code" value="<%=item.Code %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Ảnh :</label>
                </td>
                <td>
                    <%= Utils.GetMedia(item.File, 100, 80)%> <br />
                    <input class="text_input" type="text" name="File" id="File" style="width:80%;" value="<%=item.File %>" maxlength="255" />
                    <input class="text_input" style="width:17%;" type="button" onclick="ShowFileForm('File');return false;" value="Chọn File" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Tóm tắt :</label>
                </td>
                <td>
                    <textarea class="text_input" style="height:100px" name="Summary" id="Summary" ><%=item.Summary %></textarea>
                </td>
            </tr>
            <tr>
                <td class="key">
                    <label>Nội dung :</label>
                </td>
                <td class="content">
                    <textarea class="ckeditor" style="width:100%;height:500px" name="Content" id="Content"><%=item.Content%></textarea>
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Frice :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Frice" id="Frice" value="<%=item.Frice %>" maxlength="255" />
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>Quantity :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Quantity" id="Quantity" value="<%=item.Quantity %>" maxlength="255" />
                </td>
            </tr>
            <%if(CPViewPage.UserPermissions.Approve) {%>
            <tr>
                <td class="key">
                    <label>Duyệt :</label>
                </td>
                <td>
                    <input name="Activity" <%= item.Activity ? "checked" : "" %> type="radio" value='1' /> Có
                    <input name="Activity" <%= !item.Activity ? "checked" : "" %> type="radio" value='0' /> Không
                </td>
            </tr>
            <%} %>
          </table>
        </div>                
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"><div class="b"></div></div></div>
</div>

</form>