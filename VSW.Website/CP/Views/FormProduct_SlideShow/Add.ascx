<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<script type="text/javascript" src="/{CPPath}/Content/ckfinder/ckfinder.js"></script>
<script type="text/javascript" src="/{CPPath}/Content/ckeditor/ckeditor.js"></script>
<script language="javascript" type="text/javascript">

    function CheckValidationForm() {
        return true;
    }

    CKFinder.setupCKEditor(null, '/{CPPath}/Content/ckfinder/');

    function refreshPage(arg) {
        arg = '~' + arg;
        document.getElementById(name_control).value = arg;
        var Arr = name_control.split('_');

        if (document.getElementById("img_view_" + Arr[1].toString()))
            document.getElementById("img_view_" + Arr[1].toString()).src = arg.replace('~/', '/{ApplicationPath}');
    }

    function ReturnParent(key) {
        self.parent.tb_remove();
    }
</script>
<style type="text/css">
    td.key
    {
        width: 0px !important;
        nowrap: nowrap !important;
        padding-left: 5px !important;
    }
</style>
<%
    var model = ViewBag.Model as ModProduct_SlideShowModel;
    var item = ViewBag.Data as ModProduct_SlideShowEntity;
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
            <!--GetDefaultAddCommand()-->
            <%=  GetListCommandThickboxValidation("apply|Lưu,space,closepopup|Đóng")%>
        </div>
        <div class="pagetitle icon-16-Edit" style="padding-left: 20px !important;">
            <h2 style="line-height: normal !important;">
                Ảnh của sản phẩm</h2>
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
                    <td class="key" nowrap="nowrap">
                        <label>
                            Ảnh 01:</label>
                    </td>
                    <td width="100px">
                        <%if (!string.IsNullOrEmpty(item.Image_01))
                          { %>
                        <%= Utils.GetMedia(item.Image_01, 100, 80, string.Empty, true, "id='img_view_01'")%><%}
                          else
                          { %>
                        <img id="img_view_01" width="100" height="80" />
                        <%} %>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Image_01" id="Image_01" value="<%=item.Image_01 %>" />
                        <input class="text_input" style="padding: 2px; width: 75px; background-color: #003366;
                            color: White; margin-top: 2px;" type="button" onclick="ShowFileForm('Image_01');return false;"
                            value="Chọn ảnh" />
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Ảnh 02:</label>
                    </td>
                    <td width="100px">
                        <%if (!string.IsNullOrEmpty(item.Image_02))
                          { %>
                        <%= Utils.GetMedia(item.Image_02, 100, 80, string.Empty, true, "id='img_view_02'")%><%}
                          else
                          { %>
                        <img id="img_view_02" width="100" height="80" />
                        <%} %>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Image_02" id="Image_02" value="<%=item.Image_02 %>"
                            maxlength="255" />
                        <input class="text_input" style="padding: 2px; width: 75px; background-color: #003366;
                            color: White; margin-top: 2px;" type="button" onclick="ShowFileForm('Image_02');return false;"
                            value="Chọn ảnh" />
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Ảnh 03:</label>
                    </td>
                    <td width="100px">
                        <%if (!string.IsNullOrEmpty(item.Image_03))
                          { %>
                        <%= Utils.GetMedia(item.Image_03, 100, 80, string.Empty, true, "id='img_view_03'")%><%}
                          else
                          { %>
                        <img id="img_view_03" width="100" height="80" />
                        <%} %>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Image_03" id="Image_03" value="<%=item.Image_03 %>"
                            maxlength="255" />
                        <input class="text_input" style="padding: 2px; width: 75px; background-color: #003366;
                            color: White; margin-top: 2px;" type="button" onclick="ShowFileForm('Image_03');return false;"
                            value="Chọn ảnh" />
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Ảnh 04:</label>
                    </td>
                    <td width="100px">
                        <%if (!string.IsNullOrEmpty(item.Image_04))
                          { %>
                        <%= Utils.GetMedia(item.Image_04, 100, 80, string.Empty, true, "id='img_view_04'")%><%}
                          else
                          { %>
                        <img id="img_view_04" width="100" height="80" />
                        <%} %>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Image_04" id="Image_04" value="<%=item.Image_04 %>"
                            maxlength="255" />
                        <input class="text_input" style="padding: 2px; width: 75px; background-color: #003366;
                            color: White; margin-top: 2px;" type="button" onclick="ShowFileForm('Image_04');return false;"
                            value="Chọn ảnh" />
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Ảnh 05:</label>
                    </td>
                    <td width="100px">
                        <%if (!string.IsNullOrEmpty(item.Image_05))
                          { %>
                        <%= Utils.GetMedia(item.Image_05, 100, 80, string.Empty, true, "id='img_view_05'")%><%}
                          else
                          { %>
                        <img id="img_view_05" width="100" height="80" />
                        <%} %>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Image_05" id="Image_05" value="<%=item.Image_05 %>"
                            maxlength="255" />
                        <input class="text_input" style="padding: 2px; width: 75px; background-color: #003366;
                            color: White; margin-top: 2px;" type="button" onclick="ShowFileForm('Image_05');return false;"
                            value="Chọn ảnh" />
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Ảnh 06:</label>
                    </td>
                    <td width="100px">
                        <%if (!string.IsNullOrEmpty(item.Image_06))
                          { %>
                        <%= Utils.GetMedia(item.Image_06, 100, 80, string.Empty, true, "id='img_view_06'")%><%}
                          else
                          { %>
                        <img id="img_view_06" width="100" height="80" />
                        <%} %>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Image_06" id="Image_06" value="<%=item.Image_06 %>"
                            maxlength="255" />
                        <input class="text_input" style="padding: 2px; width: 75px; background-color: #003366;
                            color: White; margin-top: 2px;" type="button" onclick="ShowFileForm('Image_06');return false;"
                            value="Chọn ảnh" />
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Ảnh 07:</label>
                    </td>
                    <td width="100px">
                        <%if (!string.IsNullOrEmpty(item.Image_07))
                          { %>
                        <%= Utils.GetMedia(item.Image_07, 100, 80, string.Empty, true, "id='img_view_07'")%><%}
                          else
                          { %>
                        <img id="img_view_07" width="100" height="80" />
                        <%} %>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Image_07" id="Image_07" value="<%=item.Image_07 %>"
                            maxlength="255" />
                        <input class="text_input" style="padding: 2px; width: 75px; background-color: #003366;
                            color: White; margin-top: 2px;" type="button" onclick="ShowFileForm('Image_07');return false;"
                            value="Chọn ảnh" />
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Ảnh 08:</label>
                    </td>
                    <td width="100px">
                        <%if (!string.IsNullOrEmpty(item.Image_08))
                          { %>
                        <%= Utils.GetMedia(item.Image_08, 100, 80, string.Empty, true, "id='img_view_08'")%><%}
                          else
                          { %>
                        <img id="img_view_08" width="100" height="80" />
                        <%} %>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Image_08" id="Image_08" value="<%=item.Image_08 %>"
                            maxlength="255" />
                        <input class="text_input" style="padding: 2px; width: 75px; background-color: #003366;
                            color: White; margin-top: 2px;" type="button" onclick="ShowFileForm('Image_08');return false;"
                            value="Chọn ảnh" />
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Ảnh 09:</label>
                    </td>
                    <td width="100px">
                        <%if (!string.IsNullOrEmpty(item.Image_09))
                          { %>
                        <%= Utils.GetMedia(item.Image_09, 100, 80, string.Empty, true, "id='img_view_09'")%><%}
                          else
                          { %>
                        <img id="img_view_09" width="100" height="80" />
                        <%} %>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Image_09" id="Image_09" value="<%=item.Image_09 %>"
                            maxlength="255" />
                        <input class="text_input" style="padding: 2px; width: 75px; background-color: #003366;
                            color: White; margin-top: 2px;" type="button" onclick="ShowFileForm('Image_09');return false;"
                            value="Chọn ảnh" />
                    </td>
                </tr>
                <tr>
                    <td class="key" nowrap="nowrap">
                        <label>
                            Ảnh 10:</label>
                    </td>
                    <td width="100px">
                        <%if (!string.IsNullOrEmpty(item.Image_10))
                          { %>
                        <%= Utils.GetMedia(item.Image_10, 100, 80, string.Empty, true, "id='img_view_10'")%><%}
                          else
                          { %>
                        <img id="img_view_10" width="100" height="80" />
                        <%} %>
                    </td>
                    <td>
                        <input class="text_input" type="text" name="Image_10" id="Image_10" value="<%=item.Image_10 %>"
                            maxlength="255" />
                        <input class="text_input" style="padding: 2px; width: 75px; background-color: #003366;
                            color: White; margin-top: 2px;" type="button" onclick="ShowFileForm('Image_10');return false;"
                            value="Chọn ảnh" />
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
