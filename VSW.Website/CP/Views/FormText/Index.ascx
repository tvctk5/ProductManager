<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl"%>

<% 
    var model = ViewBag.Model as FormTextModel;
 %>

<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />

<div id="toolbar-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%= GetListCommand("apply|Lưu,space,cancel|Đóng")%>
        </div>
        <div class="pagetitle icon-48-article">
            <h2>Văn bản</h2>
        </div>
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"> <div class="b"></div></div></div>
</div>
<div class="clr"></div>

<div id="element-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">

         <table class="admintable">  
            <tr>
                <td colspan="2" align="center" style="text-align:center" class="key">
                   NỘI DUNG
                </td>
            </tr>
            <tr>
                <td colspan="2" class="content">
                    <textarea class="ckeditor" style="width:100%;height:500px" name="Value" id="Value"><%=model.Value%></textarea>
                </td>
            </tr>

          </table>
                            
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"><div class="b"></div></div></div>
</div>

<script type="text/javascript" src="/{CPPath}/Content/ckeditor/ckeditor.js"></script>
<script type="text/javascript">
    CKFinder.setupCKEditor(null, '/{CPPath}/Content/ckfinder/');
</script>

</form>