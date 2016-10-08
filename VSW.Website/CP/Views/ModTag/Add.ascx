<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>

<%
    var model = ViewBag.Model as ModTagModel;
    var item = ViewBag.Data as ModTagEntity;
%>

<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />

<div id="toolbar-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%= GetDefaultAddCommand()%>
        </div>
        <div class="pagetitle icon-48-article">
            <h2>Tags : <%=  model.RecordID > 0 ? "Chỉnh sửa" : "Thêm mới"%></h2>
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
                    <label>Tên tag:</label>
                </td>
                <td>
                    <%=item.Name %>
                </td>
            </tr>
           <tr>
                <td class="key">
                    <label>[SEO] Tiêu đề trang :</label>
                </td>
                <td>
                    <input class="text_input" type="text" name="Title" id="Title" value="<%=item.Title %>" maxlength="255" />
                </td>
            </tr>
          
           <tr>
                <td class="key">
                    <label>[SEO] Mô tả trang :</label>
                </td>
                <td>
                    <textarea class="ckeditor" style="height:100px;width:98%" name="Description"><%=item.Description%></textarea>
                </td>
            </tr>
             <tr>
                <td class="key">
                    <label>[SEO] Từ khóa trang :</label>
                </td>
                <td>
                    <textarea class="ckeditor" style="height:100px;width:98%" name="Keywords"><%=item.Keywords%></textarea>
                </td>
            </tr>
          </table>
        </div>                
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"><div class="b"></div></div></div>
</div>

</form>