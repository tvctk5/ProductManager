<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModProduct_InfoModel;
    var item = ViewBag.Data as ModProduct_InfoEntity;
    var listItem_LienQuan = model.listItem_LienQuan;
    var listItem_BanKem = model.listItem_BanKem;
    var GetListManufacture = model.GetListManufacture;
    var listItem_LienQuan_Current = model.listItem_LienQuan_Current;
    var listItem_BanKem_Current = model.listItem_BanKem_Current;
    var listItem_History = model.listItem_History;
%>
<%--<link href="/{CPPath}/Content/add/css/jquery-ui.1.8.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/{CPPath}/Content/add/css/jquery.min1.5.js"></script>--%>
<script type="text/javascript" src="/{CPPath}/Content/add/css/jquery-ui.min.1.8.js"></script>
<%--<script src="/{CPPath}/Content/add/js/Tabs/basic.js" type="text/javascript"></script>
<script src="/{CPPath}/Content/add/js/Tabs/organictabs.jquery.js" type="text/javascript"></script>
<script src="/{CPPath}/Content/add/js/Tabs/withoutPlugin.js" type="text/javascript"></script>--%>
<link href="/{CPPath}/Content/add/css/tabui.css" rel="stylesheet" type="text/css" />
<%--<link href="/{CPPath}/Content/add/css/datepicker-ui.css" rel="stylesheet" type="text/css" />--%>
<script type="text/javascript" src="/{CPPath}/Content/ckeditor/ckeditor.js"></script>
<%--<script type="text/javascript" src="/{CPPath}/Content/add/jQuery/jquery.ui.datepicker.js"></script>--%>
<link href="/{CPPath}/Content/add/colorpicker/css/colpick.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/{CPPath}/Content/add/colorpicker/js/colpick.js"></script>
<script type="text/javascript" src="/{CPPath}/Content/ckfinder/ckfinder.js"></script>
<script type="text/javascript" src="/{CPPath}/Content/ckeditor/ckeditor.js"></script>
<script src="/{CPPath}/Content/add/js/ProductionInfo.js" type="text/javascript"></script>
<link href="/{CPPath}/Content/add/datetimepickermaster/jquery.datetimepicker.css"
    rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/{CPPath}/Content/add/datetimepickermaster/jquery.datetimepicker.js"></script>
<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="RecordID" name="RecordID" value="<%=model.RecordID %>" />
<input name="TabIndexCurrent" id="TabIndexCurrent" type="hidden" value="<%=model.TabIndexCurrent %>" />
<input type="hidden" id="PageSize" name="PageSize" value="<%=model.PageSize %>" />
<input type="hidden" id="PageIndex" name="PageIndex" value="<%=model.PageIndex %>" />
<input type="hidden" id="Selected_Product_BanKem" name="Selected_Product_BanKem"
    value="<%=model.Selected_Product_BanKem %>" />
<input type="hidden" id="Selected_Product_LienQuan" name="Selected_Product_LienQuan"
    value="<%=model.Selected_Product_LienQuan %>" />
<input type="hidden" id="ModPriceOld" name="ModPriceOld" value="<%=model.ModPriceOld %>" />
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
            <%if (model.RecordID > 0)
              {%>
            <%=GetDefaultAddCommandValidationProductInfo()%>
            <%}
              else
              {%>
            <%= GetDefaultAddCommandValidation()%>
            <%}%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Sản phẩm :
                <%= model.RecordID
> 0 ? "Chỉnh sửa" : "Thêm mới"%></h2>
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
<div id="divMessError" style="display: none;">
</div>
<div id="element-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m" style="height: auto !important;">
        <div class="col width-100">
            <div id="page-wrap" style="background-color: #F4F4F4 !important; width: 100% !important;">
                <div id="ebooktabs">
                    <fieldset class="adminform" style="background-color: #F4F4F4 !important; height: 100% !important;">
                        <legend>CHỨC NĂNG</legend>
                        <ul class="nav">
                            <li><a href="dvtabInfo" class="current">Thông tin tổng quan</a></li>
                            <li><a href="dvtabColorSize">Màu sắc/ Kích thước</a></li>
                            <li><a href="dvtabImageSEO">Ảnh & SEO</a></li>
                            <li><a href="dvtabProductGroup">Chủng loại liên quan</a></li>
                            <li><a href="dvtabProductRelative">Sản phẩm liên quan</a></li>
                            <li><a href="dvtabProductAttach">Sản phẩm bán kèm</a></li>
                            <li><a href="dvtabAgent">Khu vực bán/ Đại lý</a></li>
                            <li><a href="dvtabFilter">Thuộc tính lọc</a></li>
                            <li><a href="dvtabSlideShow">Ảnh sản phẩm</a></li>
                            <li><a href="dvtabProperties">Thông tin kỹ thuật</a></li>
                            <li><a href="dvtabComments">Bình luận</a></li>
                            <li><a href="dvtabType">Loại sản phẩm</a></li>
                            <% if (model.RecordID > 0)
                               {
                                   string sHref = string.Empty;
                                   switch (model.TabIndexCurrent)
                                   {
                                       case 1: sHref = "href=\"#dvtab1\""; break;
                                       case 2: sHref = "href=\"#dvtab2\""; break;
                                       case 3: sHref = "href=\"#dvtab3\""; break;
                                       case 4: sHref = "href=\"#dvtab4\""; break;
                                       case 5: sHref = "href=\"#dvtab5\""; break;
                                       case 6: sHref = "href=\"#dvtab6\""; break;
                                       //default: sHref = "href=\"#dvtab1\""; break;
                                   } %>
                            <li class="nav-Six"><a <%= sHref %> onclick="<%=model.UrlHistory %>">Lịch sử thay đổi
                                giá</a></li>
                            <li class="nav-Six1"><a <%= sHref %> onclick="<%=model.UrlPriceSaleOffHistory %>">Lịch
                                sử khuyến mãi</a></li>
                            <li class="nav-Seven"><a <%= sHref %> onclick="<%=model.UrlSlideShow %>">Ảnh sản phẩm</a></li>
                            <li class="nav-Eatch"><a <%= sHref %> onclick="<%=model.UrlProperties %>">Thuộc tính
                                sản phẩm</a></li>
                            <li class="nav-Nine"><a <%= sHref %> onclick="<%=model.UrlComment %>">Các bình luận</a></li>
                            <%} %>
                        </ul>
                    </fieldset>
                    <div class="list-wrap">
                        <div id="dvtabInfo">
                            <div class="divebookcontent">
                                <table class="admintable">
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Chủng loại :</label>
                                        </td>
                                        <td style="width: 100% !important;">
                                            <select name="MenuID" id="MenuID" class="DropDownList">
                                                <%= Utils.ShowDDLMenuByType("Product", model.LangID, item.MenuID)%>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Nhà sản xuất :</label>
                                        </td>
                                        <td>
                                            <select name="ModelManufacturerId" id="ModelManufacturerId" class="DropDownList">
                                                <option value="0">--------------- Chọn nhà sản xuất ---------------</option>
                                                <%= Utils.ShowDDLManufacturer(item.ManufacturerId.ToString())%>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Kiểu sản phẩm :</label>
                                        </td>
                                        <td>
                                            <select name="Type" id="Type" class="DropDownList">
                                                <option value="0" <%=item.Type==0?"selected":"" %>>Sản phẩm đơn</option>
                                                <option value="1" <%=item.Type==1?"selected":"" %>>Sản phẩm nhóm</option>
                                            </select>
                                            <%=ShowRequireValidation()%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Mã sản phẩm :</label>
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="Code" id="Code" value="<%=item.Code %>"
                                                maxlength="255" require="true" /><%=ShowRequireValidation()%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Tên sản phẩm :</label>
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="Name" id="Name" value="<%=item.Name %>"
                                                maxlength="255" require="true" /><%=ShowRequireValidation()%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Thông tin cơ bản :</label>
                                        </td>
                                        <td>
                                            <textarea class="text_input" name="InfoBasic" id="InfoBasic" style="height: 50px"
                                                cols="8" maxlength="1000" require="true"><%=item.InfoBasic %></textarea><%=ShowRequireValidation()%>
                                        </td>
                                    </tr>
                                    <%
                                        if (model.RecordID > 0)
                                        {
                                    %>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Số lượng :</label>
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="CountNumber" id="CountNumber" value="<%=item.CountNumber %>"
                                                maxlength="255" />
                                        </td>
                                    </tr>
                                    <%} %>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Giá nhập :</label>
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="PriceInput" id="PriceInput" value="<%=item.PriceInput %>"
                                                maxlength="255" require="true" price="true" /><%=ShowRequireValidation()%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Giá bán :</label>
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="Price" id="Price" value="<%=item.Price %>"
                                                maxlength="255" require="true" price="true" /><%=ShowRequireValidation()%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Hiển thị giá :</label>
                                        </td>
                                        <td>
                                            <input name="ShowPrice" <%= item.ShowPrice ? "checked" : "" %> type="radio" value='1' />
                                            Có
                                            <input name="ShowPrice" <%= !item.ShowPrice ? "checked" : "" %> type="radio" value='0' />
                                            Không
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                VAT :</label>
                                        </td>
                                        <td>
                                            <input name="VAT" <%= item.VAT ? "checked" : "" %> type="radio" value='1' />
                                            Đã có VAT
                                            <input name="VAT" <%= !item.VAT ? "checked" : "" %> type="radio" value='0' />
                                            Chưa có VAT &nbsp;&nbsp;&nbsp;Hiển thị VAT:&nbsp;
                                            <input name="ShowVAT" <%= item.ShowVAT ? "checked" : "" %> type="radio" value='1' />
                                            Có
                                            <input name="ShowVAT" <%= !item.ShowVAT ? "checked" : "" %> type="radio" value='0' />
                                            Không
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Giảm giá :</label>
                                        </td>
                                        <td>
                                            <input name="SaleOff" <%= item.SaleOff ? "checked" : "" %> type="radio" value='1'
                                                onclick="OnChangeSaleOff(this);" />
                                            Có
                                            <input name="SaleOff" <%= !item.SaleOff ? "checked" : "" %> type="radio" value='0'
                                                onclick="OnChangeSaleOff(this);" />
                                            Không
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <table width="97%" style="background-color: #9999FF; font-weight: bold;">
                                                <tr <% if(!item.SaleOff) {%> class="SaleOffSetting hide" <%}else{ %> class="SaleOffSetting"
                                                    <%} %>>
                                                    <td style="width: 0% !important; padding-left: 5px;" nowrap="nowrap">
                                                        Số tiền (%) giảm :
                                                    </td>
                                                    <td>
                                                        <input type="text" class="text_input" style="width: 70px !important;" name="SaleOffValue"
                                                            id="SaleOffValue" value="<%=item.SaleOffValue %>" maxlength="255" price="true" />
                                                        <select class="text_input" style="width: 70px !important;" name="SaleOffType" id="SaleOffType">
                                                            <option <%= item.SaleOffType ? "selected='selected'" : "" %> value="1">%</option>
                                                            <option <%= !item.SaleOffType ? "selected='selected'" : "" %> value="0">VNĐ</option>
                                                        </select>
                                                    </td>
                                                    <td style="width: 100%;">
                                                    </td>
                                                </tr>
                                                <tr <% if(!item.SaleOff) {%> class="SaleOffSetting hide" <%}else{ %> class="SaleOffSetting"
                                                    <%} %>>
                                                    <td style="width: 0% !important; padding-left: 5px;" nowrap="nowrap">
                                                        Thông tin hiển thị :
                                                    </td>
                                                    <td>
                                                        <input class="text_input" type="text" name="PriceTextSaleView" id="PriceTextSaleView"
                                                            value="<%=item.PriceTextSaleView %>" maxlength="50" style="width: 144px !important;"/>
                                                    </td>
                                                    <td style="width: 100%;">
                                                    </td>
                                                </tr>
                                                <tr <% if(!item.SaleOff) {%> class="SaleOffSetting hide" <%}else{ %> class="SaleOffSetting"
                                                    <%} %>>
                                                    <td style="width: 0% !important; padding-left: 5px;" nowrap="nowrap">
                                                        Ngày bắt đầu :
                                                    </td>
                                                    <td nowrap="nowrap">
                                                        <input class="text_input" type="text" name="StartDate" id="StartDate" maxlength="20"
                                                            style="width: 144px !important;" /> 
                                                    </td>
                                                    <td style="width: 100%;">
                                                    </td>
                                                </tr>
                                                <tr <% if(!item.SaleOff) {%> class="SaleOffSetting hide" <%}else{ %> class="SaleOffSetting"
                                                    <%} %>>
                                                    <td style="width: 0% !important; padding-left: 5px;" nowrap="nowrap">
                                                        Ngày kết thúc :
                                                    </td>
                                                    <td>
                                                        <input class="text_input" type="text" name="FinishDate" id="FinishDate" maxlength="20"
                                                            style="width: 144px !important;" /> 
                                                    </td>
                                                    <td style="width: 100%;">
                                                    </td>
                                                </tr>
                                                <tr <% if(!item.SaleOff) {%> class="SaleOffSetting hide" <%}else{ %> class="SaleOffSetting"
                                                    <%} %>>
                                                    <td style="width: 0% !important; padding-left: 5px;" nowrap="nowrap">
                                                        Giá sau giảm :
                                                    </td>
                                                    <td nowrap="nowrap">
                                                        <input type="text" class="text_input" style="width: 144px !important;" id="PriceSale"
                                                            name="PriceSale" value="<%=item.PriceSale %>" price="true" />
                                                        VNĐ
                                                    </td>
                                                    <td style="width: 100%;">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <% if (model.ModMinuteFinish == 0)
                                       {%>
                                    <%} %>
                                    <%
                                        if (model.RecordID > 0)
                                        {
                                    %>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Số lượt xem :</label>
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="Preview" id="Preview" value="<%=item.Preview %>"
                                                maxlength="255" />
                                            <div class="margin-top-5px margin-bottom-5px">
                                                Hiển thị số lượt xem:&nbsp;
                                                <input name="ShowPreview" <%= item.ShowPreview ? "checked" : "" %> type="radio" value='1' />
                                                Có
                                                <input name="ShowPreview" <%= !item.ShowPreview ? "checked" : "" %> type="radio"
                                                    value='0' />
                                                Không
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Số lượt mua :</label>
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="BuyCount" id="BuyCount" value="<%=item.BuyCount %>"
                                                maxlength="255" />
                                            <div class="margin-top-5px margin-bottom-5px">
                                                Hiển thị số lượt mua:&nbsp;
                                                <input name="ShowBuyCount" <%= item.ShowBuyCount ? "checked" : "" %> type="radio"
                                                    value='1' />
                                                Có
                                                <input name="ShowBuyCount" <%= !item.ShowBuyCount ? "checked" : "" %> type="radio"
                                                    value='0' />
                                                Không
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Số bình luận :</label>
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="CommentCount" id="CommentCount" value="<%=item.CommentCount %>"
                                                maxlength="255" />
                                            <div class="margin-top-5px margin-bottom-5px">
                                                Hiển thị số lượt bình luận:&nbsp;
                                                <input name="ShowCommentCount" <%= item.ShowCommentCount ? "checked" : "" %> type="radio"
                                                    value='1' />
                                                Có
                                                <input name="ShowCommentCount" <%= !item.ShowCommentCount ? "checked" : "" %> type="radio"
                                                    value='0' />
                                                Không
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Số lượt thích :</label>
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="LikeCount" id="LikeCount" value="<%=item.LikeCount %>"
                                                maxlength="255" />
                                            <div class="margin-top-5px margin-bottom-5px">
                                                Hiển thị số lượt thích:&nbsp;
                                                <input name="ShowLike" <%= item.ShowLike ? "checked" : "" %> type="radio"
                                                    value='1' />
                                                Có
                                                <input name="ShowLike" <%= !item.ShowLike ? "checked" : "" %> type="radio"
                                                    value='0' />
                                                Không
                                            </div>
                                        </td>
                                    </tr>
                                    <%} %>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Thời gian bảo hành :</label>
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="Warranty" id="Warranty" value="<%=item.Warranty %>"
                                                maxlength="255" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Tình trạng :</label>
                                        </td>
                                        <td>
                                            <input name="New" <%= item.Status ? "checked" : "" %> type="radio" value='1' />
                                            Sản phẩm mới
                                            <input name="New" <%= !item.Status ? "checked" : "" %> type="radio" value='0' />
                                            Sản phẩm cũ &nbsp;&nbsp;&nbsp;Hiển thị thông tin tình trạng:&nbsp;
                                            <input name="ShowStatus" <%= item.ShowStatus ? "checked" : "" %> type="radio" value='1' />
                                            Có
                                            <input name="ShowStatus" <%= !item.ShowStatus ? "checked" : "" %> type="radio" value='0' />
                                            Không
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Gán Icon :</label>
                                        </td>
                                        <td>
                                            <input name="ShowIcon" <%= item.ShowIcon==0 ? "checked" : "" %> type="radio" value='0' />
                                            Không có
                                            <input name="ShowIcon" <%= item.ShowIcon==1 ? "checked" : "" %> type="radio" value='1' />
                                            New
                                            <input name="ShowIcon" <%= item.ShowIcon==2 ? "checked" : "" %> type="radio" value='2' />
                                            Hot
                                            <input name="ShowIcon" <%= item.ShowIcon==3 ? "checked" : "" %> type="radio" value='3' />
                                            Khuyến mại
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Thông tin tình trạng :</label>
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="StatusNote" id="StatusNote" value="<%=item.StatusNote %>"
                                                maxlength="255" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Quà tặng đi kèm :</label>
                                        </td>
                                        <td>
                                            <textarea rows="2" class="text_input" name="Gifts" id="Gifts" maxlength="500"><%=item.Gifts %></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Khuyến mãi :</label>
                                        </td>
                                        <td>
                                            <textarea rows="2" class="text_input" name="Promotion" id="Promotion" maxlength="500"><%=item.Promotion%></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Chính sách :</label>
                                        </td>
                                        <td>
                                            <textarea rows="2" class="text_input" name="Policy" id="Policy" maxlength="500"><%=item.Policy%></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Thông tin nổi bật khác :</label>
                                        </td>
                                        <td>
                                            <textarea rows="2" class="text_input" name="InfoHighlight" id="InfoHighlight" maxlength="500"><%=item.InfoHighlight%></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Ngày bắt đầu bán :</label>
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="RuntimeDateStart" id="RuntimeDateStart"
                                                maxlength="20" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Ngày kết thúc bán :</label>
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="RuntimeDateFinish" id="RuntimeDateFinish"
                                                maxlength="20" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Ngày đăng hiển thị :</label>
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="PostDate" id="PostDate" maxlength="20" />
                                        </td>
                                    </tr>
                                    <%
                                        if (model.RecordID > 0)
                                        {
                                    %>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Ngày tạo :</label>
                                        </td>
                                        <td>
                                            <input class="text_input" readonly="readonly" type="text" name="CreateDate" id="CreateDate"
                                                value="<%=item.CreateDate %>" maxlength="255" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Ngày sửa cuối :</label>
                                        </td>
                                        <td>
                                            <input class="text_input" readonly="readonly" type="text" name="ModifiedDate" id="ModifiedDate"
                                                value="<%=item.ModifiedDate %>" maxlength="255" />
                                        </td>
                                    </tr>
                                    <%
                                        }
                                        else
                                        {  %>
                                    <tr style="display: none;">
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Ngày tạo :</label>
                                        </td>
                                        <td>
                                            <input class="text_input" type="text" name="CreateDate" name="CreateDate" id="CreateDate"
                                                value="<%=item.CreateDate %>" maxlength="255" />
                                        </td>
                                    </tr>
                                    <tr style="display: none;">
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Ngày sửa cuối :</label>
                                        </td>
                                        <td>
                                            <input class="text_input" readonly="readonly" type="text" name="ModifiedDate" id="ModifiedDate"
                                                value="<%=item.ModifiedDate %>" maxlength="255" />
                                        </td>
                                    </tr>
                                    <%}%>
                                    <%--<tr>
                                                <td class="key" style="width:0% !important;" nowrap="nowrap">
                                                    <label>
                                                        Chuyên mục :</label>
                                                </td>
                                                <td>
                                                    <select name="MenuID" id="MenuID" class="DropDownList">
                                                        <option value="0">Root</option>
                                                        <%= Utils.ShowDDLMenuByType("Product_Info", model.LangID, item.MenuID)%>
                                                    </select>
                                                </td>
                                            </tr>--%>
                                    <%if (CPViewPage.UserPermissions.Approve)
                                      {%>
                                    <tr>
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap">
                                            <label>
                                                Trạng thái :</label>
                                        </td>
                                        <td>
                                            <input name="Activity" <%= item.Activity ? "checked" : "" %> type="radio" value='1' />
                                            Bán
                                            <input name="Activity" <%= !item.Activity ? "checked" : "" %> type="radio" value='0' />
                                            Ngừng bán
                                        </td>
                                    </tr>
                                    <%} %>
                                    <tr style="display:none;">
                                        <td class="key" style="width: 0% !important;" nowrap="nowrap" valign="top">
                                            <label>
                                                Bảng mô tả size :</label>
                                        </td>
                                        <td>
                                            <textarea rows="2" class="ckeditor" style="width: 100%; height: 500px" name="SizeInfo"
                                                id="SizeInfo"><%=item.SizeInfo%></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="key" style="width: 100% !important;">
                                            <br />
                                            <div style="text-align: left !important;">
                                                Bài giới thiệu sản phẩm :</div>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100% !important;" colspan="2" align="left">
                                            <textarea cols="50" class="ckeditor" style="width: 100%; height: 500px" name="NewsPost"
                                                id="NewsPost"><%=item.NewsPost%></textarea>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="divtabfooter">
                                &nbsp;
                            </div>
                        </div>
                        <div id="dvtabImageSEO" class="hide">
                            <div class="divebookcontent" style="padding-top: 0px !important; margin-top: 0px !important;">
                                <div id="content-sliders-" class="pane-sliders">
                                    <div class="panel">
                                        <h3 class="pane-toggler title" id="publishing-details">
                                            <a href="javascript:void(0);"><span>THUỘC TÍNH</span></a></h3>
                                        <div class="pane-slider content" style="padding-left: 40px !important;">
                                            <table class="admintable">
                                                <tr>
                                                    <td align="center" style="text-align: left" class="key">
                                                        Hình minh họa
                                                    </td>
                                                </tr>
                                                <tr>
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
                                                <%-- <tr>
                                                    <td align="center" style="text-align: left" class="key">
                                                        Chuyên mục
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <select name="MenuID" id="MenuID" class="text_input">
                                                            <option value="0">Root</option>
                                                            <%= Utils.ShowDDLMenuByType("Product_Info", model.LangID, item.MenuID)%>
                                                        </select>
                                                    </td>
                                                </tr>--%>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="panel">
                                        <h3 class="pane-toggler title" id="publishing-details">
                                            <a href="javascript:void(0);"><span>SEO</span></a></h3>
                                        <div class="pane-slider content" style="padding-left: 40px !important;">
                                            <table class="admintable">
                                                <tr>
                                                    <td align="center" style="text-align: left" class="key">
                                                        <label>
                                                            [SEO] Tiêu đề trang :</label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input class="text_input" type="text" name="PageTitle" value="<%=item.PageTitle %>"
                                                            maxlength="255" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="text-align: left" class="key">
                                                        <label>
                                                            [SEO] Mô tả trang:</label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <textarea class="text_input" style="height: 100px;" name="PageDescription" id="PageDescription"><%=item.PageDescription%></textarea>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="text-align: left" class="key">
                                                        <label>
                                                            [SEO] Từ khóa trang:</label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <textarea class="text_input" style="height: 100px;" name="PageKeywords" id="PageKeywords"><%=item.PageKeywords%></textarea>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="divtabfooter">
                                &nbsp;
                            </div>
                        </div>
                        <div id="dvtabProductGroup" class="hide">
                            <div class="divebookcontent">
                                <table class="adminlist tbl-productgroups" cellspacing="1">
                                    <thead>
                                        <tr>
                                            <th width="1%">
                                                STT
                                            </th>
                                            <th class="title">
                                                Chủng loại
                                            </th>
                                            <th width="5%" nowrap="nowrap" class="title">
                                                Trạng thái
                                            </th>
                                            <th width="6%" class="title">
                                                Chủng loại chính
                                            </th>
                                            <th width="5%" class="title">
                                                Chọn
                                            </th>
                                            <th width="1%" nowrap="nowrap" class="title">
                                                Xóa
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody class="tbl-data-productgroups">
                                        <%=model.ListProductGroups%>
                                    </tbody>
                                </table>
                            </div>
                            <div class="divtabfooter">
                                &nbsp;
                            </div>
                        </div>
                        <div id="dvtabProductRelative" class="hide">
                            <div class="divebookcontent">
                                <div class="col width-100">
                                    <table class="admintable">
                                        <tr>
                                            <td colspan="2" style="width: 100%; color: White; font-weight: bold; background-color: #336699;
                                                height: 32px; padding-left: 5px;" align="left">
                                                DANH SÁCH CÁC SẢN PHẨM LIÊN QUAN
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div style="text-align: right; margin-top: 5px; margin-bottom: 5px;">
                                                <input id="btnThemSanPhamBanKem" onclick="tb_show('', '/CP/FormProduct_Relative/AddRelativeProduct.aspx/RecordID/<%=model.RecordID %>?TB_iframe=true;height=500;width=850;', ''); return false;"
                                                        value="Thêm sản phẩm" class="text_input button-function button-background-image-add"
                                                        style="width: 150px" type="button" />
                                                </div>
                                                <table class="adminlist" cellspacing="1">
                                                    <thead>
                                                        <tr>
                                                            <th width="1%">
                                                                STT
                                                            </th>
                                                            <th width="1%" nowrap="nowrap">
                                                                ID
                                                            </th>
                                                            <th width="20px">
                                                                Trạng thái
                                                            </th>
                                                            <th style="width: 40px" nowrap="nowrap">
                                                                Ảnh
                                                            </th>
                                                            <th width="10%" nowrap="nowrap">
                                                                Mã sản phẩm
                                                            </th>
                                                            <th class="title">
                                                                Tên sản phẩm
                                                            </th>
                                                            <th width="1%" nowrap="nowrap">
                                                                Số lượng
                                                            </th>
                                                            <th width="5%" nowrap="nowrap">
                                                                Giá
                                                            </th>
                                                            <th width="20%" nowrap="nowrap">
                                                                Nhà sản xuất
                                                            </th>
                                                            <th width="1%" nowrap="nowrap">
                                                                Ngày tạo
                                                            </th>
                                                            <th width="1%" nowrap="nowrap">
                                                                Xóa
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="tbl-data-productrelative">
                                                        <% 
                                                            if (listItem_LienQuan_Current != null && listItem_LienQuan_Current.Count > 0)
                                                            { 
                                                        %>
                                                        <%--<tfoot>
                                                            <tr>
                                                                <td colspan="16">
                                                                    <del class="container">
                                                                        <%= GetPagination(model.PageIndex, model.PageSize, model.TotalRecord, "SearchProduct_LienQuan", "OnChangePageSize_LienQuan(this,'SearchProduct_LienQuan');", "FunctionOnchangePage")%>
                                                                    </del>
                                                                </td>
                                                            </tr>
                                                        </tfoot>--%>
                                                        <%for (int i = 0; listItem_LienQuan_Current != null && i < listItem_LienQuan_Current.Count; i++)
                                                          { %>
                                                        <tr class="row<%= i%2 %>">
                                                            <td align="center">
                                                                <%= i + 1%>
                                                            </td>
                                                            <td align="center">
                                                                <%= listItem_LienQuan_Current[i].ID%>
                                                            </td>
                                                            <td align="center">
                                                                <%  if (listItem_LienQuan_Current[i].Activity)
                                                                    {%>
                                                                <span class='jgrid'><span class='state activate' title='Đang sử dụng'></span></span>
                                                                <%}
                                                                    else
                                                                    { %>
                                                                <span class='jgrid'><span class='state unpublish' title='Không sử dụng'></span></span>
                                                                <%} %>
                                                            </td>
                                                            <td align="center">
                                                                <%= Utils.GetMedia(listItem_LienQuan_Current[i].File, 60, 60)%>
                                                            </td>
                                                            <td align="left">
                                                                <%= listItem_LienQuan_Current[i].Code%>
                                                            </td>
                                                            <td>
                                                                <%= listItem_LienQuan_Current[i].Name%>
                                                            </td>
                                                            <td align="right" nowrap="nowrap">
                                                                <%= string.Format("{0:#,##0}", listItem_LienQuan_Current[i].CountNumber)%>
                                                            </td>
                                                            <td align="right">
                                                                <%= string.Format("{0:#,##0}", listItem_LienQuan_Current[i].Price)%>
                                                            </td>
                                                            <td align="left">
                                                                <%if (listItem_LienQuan_Current[i].ManufacturerId == null || listItem_LienQuan_Current[i].ManufacturerId == 0)
                                                                  {%>
                                                                Không xác định
                                                                <%}
                                                                  else
                                                                  { %>
                                                                <% if (GetListManufacture != null && GetListManufacture.Count > 0)
                                                                   {
                                                                       ModProduct_ManufacturerEntity objManufacturerEntity = GetListManufacture.Where(o => o.ID == listItem_LienQuan_Current[i].ManufacturerId).SingleOrDefault();
                                                                       if (objManufacturerEntity == null)
                                                                       { 
                                                                %>
                                                                Không xác định
                                                                <%}
                                                                       else
                                                                       { %>
                                                                <%= objManufacturerEntity.Name%>
                                                                <%}
                                                                   }
                                                                  }%>
                                                            </td>
                                                            <td align="center">
                                                                <%= string.Format("{0:dd/MM/yyyy HH:mm}", listItem_LienQuan_Current[i].CreateDate)%>
                                                            </td>
                                                            <td align="center">
                                                                <a class="jgrid" href="javascript:void(0);" onclick="RelativeDelete_DeleteTr(urlProductRelative_Delete,this,'<%=listItem_LienQuan_Current[i].ID %>');return false;">
                                                                    <span class="jgrid"><span class="state delete"></span></span></a>
                                                            </td>
                                                        </tr>
                                                        <%} %>
                                                        <%}
                                                            else
                                                            { 
                                                        %>
                                                        Chưa có sản phẩm nào liên quan
                                                        <%} %>
                                                    </tbody>
                                                </table>
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="divtabfooter">
                            </div>
                        </div>
                        <div id="dvtabProductAttach" class="hide">
                            <div class="divebookcontent">
                                <div class="col width-100">
                                    <table class="admintable">
                                        <tr>
                                            <td colspan="2" style="width: 100%; color: White; font-weight: bold; background-color: #336699;
                                                height: 32px; padding-left: 5px" align="left">
                                                DANH SÁCH CÁC SẢN PHẨM BÁN KÈM
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div style="text-align: right; margin-top: 5px; margin-bottom: 5px;">
                                                    <input id="Button1" onclick="tb_show('', '/CP/FormProduct_Attach/AddAttachProduct.aspx/RecordID/<%=model.RecordID %>?TB_iframe=true;height=500;width=850;', ''); return false;"
                                                        value="Thêm sản phẩm" class="text_input button-function button-background-image-add"
                                                        style="width: 150px" type="button" />
                                                </div>
                                                <table class="adminlist" cellspacing="1">
                                                    <thead>
                                                        <tr>
                                                            <th width="1%">
                                                                STT
                                                            </th>
                                                            <th width="1%" nowrap="nowrap">
                                                                ID
                                                            </th>
                                                            <th width="20px">
                                                                Trạng thái
                                                            </th>
                                                            <th style="width: 40px" nowrap="nowrap">
                                                                Ảnh
                                                            </th>
                                                            <th width="10%" nowrap="nowrap">
                                                                Mã sản phẩm
                                                            </th>
                                                            <th class="title">
                                                                Tên sản phẩm
                                                            </th>
                                                            <th width="1%" nowrap="nowrap">
                                                                Số lượng
                                                            </th>
                                                            <th width="5%" nowrap="nowrap">
                                                                Giá
                                                            </th>
                                                            <th width="20%" nowrap="nowrap">
                                                                Nhà sản xuất
                                                            </th>
                                                            <th width="1%" nowrap="nowrap">
                                                                Ngày tạo
                                                            </th>
                                                            <th width="1%" nowrap="nowrap">
                                                                Xóa
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="tbl-data-productattach">
                                                        <% 
                                                            if (listItem_BanKem_Current != null && listItem_BanKem_Current.Count > 0)
                                                            { 
                                                        %>
                                                        <%--<tfoot>
                                                            <tr>
                                                                <td colspan="16">
                                                                    <del class="container">
                                                                        <%= GetPagination(model.PageIndex, model.PageSize, model.TotalRecord, "SearchProduct_LienQuan", "OnChangePageSize_LienQuan(this,'SearchProduct_LienQuan');", "FunctionOnchangePage")%>
                                                                    </del>
                                                                </td>
                                                            </tr>
                                                        </tfoot>--%>
                                                        <%for (int i = 0; listItem_BanKem_Current != null && i < listItem_BanKem_Current.Count; i++)
                                                          { %>
                                                        <tr class="row<%= i%2 %>">
                                                            <td align="center">
                                                                <%= i + 1%>
                                                            </td>
                                                            <td align="center">
                                                                <%= listItem_BanKem_Current[i].ID%>
                                                            </td>
                                                            <td align="center">
                                                                <%  if (listItem_BanKem_Current[i].Activity)
                                                                    {%>
                                                                <span class='jgrid'><span class='state activate' title='Đang sử dụng'></span></span>
                                                                <%}
                                                                    else
                                                                    { %>
                                                                <span class='jgrid'><span class='state unpublish' title='Không sử dụng'></span></span>
                                                                <%} %>
                                                            </td>
                                                            <td align="center">
                                                                <%= Utils.GetMedia(listItem_BanKem_Current[i].File, 60, 60)%>
                                                            </td>
                                                            <td align="left">
                                                                <%= listItem_BanKem_Current[i].Code%>
                                                            </td>
                                                            <td>
                                                                <%= listItem_BanKem_Current[i].Name%>
                                                            </td>
                                                            <td align="right" nowrap="nowrap">
                                                                <%= string.Format("{0:#,##0}", listItem_BanKem_Current[i].CountNumber)%>
                                                            </td>
                                                            <td align="right">
                                                                <%= string.Format("{0:#,##0}", listItem_BanKem_Current[i].Price)%>
                                                            </td>
                                                            <td align="left">
                                                                <%if (listItem_BanKem_Current[i].ManufacturerId == null || listItem_BanKem_Current[i].ManufacturerId == 0)
                                                                  {%>
                                                                Không xác định
                                                                <%}
                                                                  else
                                                                  { %>
                                                                <% if (GetListManufacture != null && GetListManufacture.Count > 0)
                                                                   {
                                                                       ModProduct_ManufacturerEntity objManufacturerEntity = GetListManufacture.Where(o => o.ID == listItem_BanKem_Current[i].ManufacturerId).SingleOrDefault();
                                                                       if (objManufacturerEntity == null)
                                                                       { 
                                                                %>
                                                                Không xác định
                                                                <%}
                                                                       else
                                                                       { %>
                                                                <%= objManufacturerEntity.Name%>
                                                                <%}
                                                                   }
                                                                  }%>
                                                            </td>
                                                            <td align="center">
                                                                <%= string.Format("{0:dd/MM/yyyy HH:mm}", listItem_BanKem_Current[i].CreateDate)%>
                                                            </td>
                                                            <td align="center">
                                                                <a class="jgrid" href="javascript:void(0);" onclick="AttachDelete_DeleteTr(urlProductAttach_Delete,this,'<%=listItem_BanKem_Current[i].ID %>');return false;">
                                                                    <span class="jgrid"><span class="state delete"></span></span></a>
                                                            </td>
                                                        </tr>
                                                        <%} %>
                                                        <%}
                                                            else
                                                            { 
                                                        %>
                                                        Chưa có sản phẩm nào bán kèm
                                                        <%} %>
                                                    </tbody>
                                                </table>
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="divtabfooter">
                            </div>
                        </div>
                        <div id="dvtabColorSize" class="hide">
                            <div class="divebookcontent">
                                <div class="col width-100">
                                    <table class="admintable">
                                        <tr>
                                            <th style="width: 49%; color: White; font-weight: bold; background-color: #336699;
                                                height: 32px; padding-left: 5px">
                                                THÔNG TIN MÀU
                                            </th>
                                            <th style="width: 2%">
                                            </th>
                                            <th style="width: 49%; color: White; font-weight: bold; background-color: #336699;
                                                height: 32px; padding-left: 5px">
                                                THÔNG TIN SIZE
                                            </th>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <table class="adminlist tbl-color-size" cellspacing="1">
                                                    <thead>
                                                        <tr>
                                                            <th width="1%">
                                                                STT
                                                            </th>
                                                            <th width="20%" nowrap="nowrap">
                                                                Mã màu
                                                            </th>
                                                            <th width="60%" class="title">
                                                                Tên màu
                                                            </th>
                                                            <th width="5%" nowrap="nowrap" class="title">
                                                                Số lượng
                                                            </th>
                                                            <th width="15%" class="title">
                                                                Cập nhật/ Thêm mới
                                                            </th>
                                                            <th width="1%" nowrap="nowrap" class="title">
                                                                Xóa
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="tbl-data-color">
                                                        <%=model.ListColor%>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td>
                                            </td>
                                            <td valign="top">
                                                <table class="adminlist tbl-color-size" cellspacing="1">
                                                    <thead>
                                                        <tr>
                                                            <th width="1%">
                                                                STT
                                                            </th>
                                                            <th width="10%" nowrap="nowrap">
                                                                Tên Size
                                                            </th>
                                                            <th class="title">
                                                                Mô tả chi tiết
                                                            </th>
                                                            <th width="1%" nowrap="nowrap">
                                                                Số lượng
                                                            </th>
                                                            <th width="10%" nowrap="nowrap">
                                                                Giá
                                                            </th>
                                                            <th width="15%" class="title">
                                                                Cập nhật/ Thêm mới
                                                            </th>
                                                            <th width="1%" nowrap="nowrap" class="title">
                                                                Xóa
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="tbl-data-size">
                                                        <%=model.ListSize%>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="divtabfooter">
                            </div>
                        </div>
                        <div id="dvtabAgent" class="hide">
                            <div class="divebookcontent">
                                <table class="admintable">
                                    <tr>
                                        <th style="width: 49%; color: White; font-weight: bold; background-color: #336699;
                                            height: 32px; padding-left: 5px">
                                            PHẠM VI BÁN SẢN PHẨM
                                        </th>
                                        <th style="width: 2%">
                                        </th>
                                        <th style="width: 49%; color: White; font-weight: bold; background-color: #336699;
                                            height: 32px; padding-left: 5px">
                                            ĐẠI LÝ BÁN SẢN PHẨM
                                        </th>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <table class="adminlist tbl-color-area" cellspacing="1">
                                                <thead>
                                                    <tr>
                                                        <th width="1%">
                                                            STT
                                                        </th>
                                                        <th width="20%" nowrap="nowrap">
                                                            Mã khu vực
                                                        </th>
                                                        <th width="80%" class="title">
                                                            Tên khu vực
                                                        </th>
                                                        <th width="1%" nowrap="nowrap" class="title">
                                                            Chọn
                                                        </th>
                                                        <th width="1%" nowrap="nowrap" class="title">
                                                            Xóa
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody class="tbl-data-color">
                                                    <%=model.ListArea%>
                                                </tbody>
                                            </table>
                                        </td>
                                        <td>
                                        </td>
                                        <td valign="top">
                                            <table class="adminlist tbl-color-agent" cellspacing="1">
                                                <thead>
                                                    <tr>
                                                        <th width="1%">
                                                            STT
                                                        </th>
                                                        <th width="1%" nowrap="nowrap">
                                                            Code
                                                        </th>
                                                        <th width="20%">
                                                            Tên đại lý
                                                        </th>
                                                        <th width="5%" nowrap="nowrap" class="title">
                                                            Quốc gia
                                                        </th>
                                                        <th width="5%" nowrap="nowrap">
                                                            Tỉnh thành
                                                        </th>
                                                        <th width="30%" nowrap="nowrap">
                                                            Địa chỉ
                                                        </th>
                                                        <th width="1%" nowrap="nowrap" class="title">
                                                            Chọn
                                                        </th>
                                                        <th width="1%" nowrap="nowrap" class="title">
                                                            Xóa
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody class="tbl-data-agent">
                                                    <%=model.ListAgent%>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="divtabfooter">
                                &nbsp;
                            </div>
                        </div>
                        <div id="dvtabFilter" class="hide">
                            <div class="divebookcontent">
                                <table class="adminlist tbl-filter" cellspacing="1">
                                    <thead>
                                        <tr>
                                            <th width="5%" nowrap="nowrap">
                                                STT
                                            </th>
                                            <th width="40%" nowrap="nowrap">
                                                Giá trị
                                            </th>
                                            <th width="30%">
                                                Ghi chú
                                            </th>
                                            <th style="width: 40px" nowrap="nowrap">
                                                Ảnh
                                            </th>
                                            <th width="1%" nowrap="nowrap">
                                                Thêm
                                            </th>
                                            <th width="1%" nowrap="nowrap">
                                                Xóa
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody class="tbl-data-filter">
                                        <%=model.ListFilter%>
                                    </tbody>
                                </table>
                            </div>
                            <div class="divtabfooter">
                                &nbsp;
                            </div>
                        </div>
                        <div id="dvtabSlideShow" class="hide">
                            <div class="divebookcontent">
                                <div class="col width-100">
                                    <table class="admintable">
                                        <tr>
                                            <td>
                                                <table class="adminlist tbl-slideshow" cellspacing="1">
                                                    <thead>
                                                        <tr>
                                                            <th width="1%">
                                                                STT
                                                            </th>
                                                            <th width="15%" class="title">
                                                                Ảnh
                                                            </th>
                                                            <th width="25%" nowrap="nowrap">
                                                                Tên ảnh/ Mô tả
                                                            </th>
                                                            <th width="45%" nowrap="nowrap" class="title">
                                                                Đường dẫn
                                                            </th>
                                                            <th width="3%" nowrap="nowrap" class="title">
                                                                Thứ tự
                                                            </th>
                                                            <th width="1%" nowrap="nowrap" class="title">
                                                                Cập nhật
                                                            </th>
                                                            <th width="1%" nowrap="nowrap" class="title">
                                                                Xóa
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="tbl-data-slideshow">
                                                        <%=model.ListSlideShow%>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="divtabfooter">
                            </div>
                        </div>
                        <div id="dvtabProperties" class="hide">
                            <div class="divebookcontent">
                                <div class="col width-100">
                                    <table class="admintable">
                                        <tr>
                                            <td>
                                                <table class="adminlist tbl-properties" cellspacing="1">
                                                    <thead>
                                                        <tr>
                                                            <th width="1%">
                                                                STT
                                                            </th>
                                                            <th width="15%" class="title">
                                                                Tên thuộc tính
                                                            </th>
                                                            <th width="25%" nowrap="nowrap">
                                                                Dữ liệu cũ
                                                            </th>
                                                            <th width="45%" nowrap="nowrap" class="title">
                                                                Giá trị thuộc tính
                                                            </th>
                                                            <th width="3%" nowrap="nowrap" class="title">
                                                                Đơn vị tính
                                                            </th>
                                                            <th width="1%" nowrap="nowrap" class="title">
                                                                Lưu dữ liệu
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="tbl-data-properties">
                                                        <%=model.ListProperties%>
                                                        <input type="hidden" name="ListPropertiesId" id="ListPropertiesId" value="<%=model.ListPropertiesId %>" />
                                                        <input type="hidden" name="ListPropretyGroupId_ByPropertiesListId" id="ListPropretyGroupId_ByPropertiesListId"
                                                            value="<%=model.ListPropretyGroupId_ByPropertiesListId %>" />
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="divtabfooter">
                            </div>
                        </div>
                        <div id="dvtabComments" class="hide">
                            <div class="divebookcontent">
                                <div class="col width-100">
                                    <table class="admintable">
                                        <tr>
                                            <td>
                                                <table class="adminlist tbl-comment" cellspacing="1">
                                                    <thead>
                                                        <tr>
                                                            <th width="1%">
                                                                STT
                                                            </th>
                                                            <th width="1%" class="title">
                                                                Ngày tạo
                                                            </th>
                                                            <th width="1%" nowrap="nowrap" class="title">
                                                                Họ tên
                                                            </th>
                                                            <th width="1%" nowrap="nowrap" class="title">
                                                                Số ĐT
                                                            </th>
                                                            <th width="1%" nowrap="nowrap" class="title">
                                                                Email
                                                            </th>
                                                            <th nowrap="nowrap" class="title">
                                                                Nội dung
                                                            </th>
                                                            <th width="1%" nowrap="nowrap" class="title">
                                                                Sửa/ Lưu
                                                            </th>
                                                            <th width="1%" nowrap="nowrap" class="title">
                                                                Duyệt/ Hủy duyệt
                                                            </th>
                                                            <th width="1%" nowrap="nowrap" class="title">
                                                                Xóa
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="tbl-data-comment">
                                                        <%=model.ListComments%>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="divtabfooter">
                            </div>
                        </div>
                        <div id="dvtabType" class="hide">
                            <div class="divebookcontent">
                                <div class="col width-100">
                                    <table class="adminlist tbl-color-type" cellspacing="1">
                                        <thead>
                                            <tr>
                                                <th width="1%">
                                                    STT
                                                </th>
                                                <th width="30%" nowrap="nowrap">
                                                    Mã loại
                                                </th>
                                                <th width="65%">
                                                    Tên loại
                                                </th>
                                                <th width="1%" nowrap="nowrap" class="title">
                                                    Chọn
                                                </th>
                                                <th width="1%" nowrap="nowrap" class="title">
                                                    Xóa
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody class="tbl-data-type">
                                            <%=model.ListTypes%>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="divtabfooter">
                            </div>
                        </div>
                        <!-- END List Wrap -->
                    </div>
                    <!-- END Organic Tabs (Example One) -->
                </div>
            </div>
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
<%--<div class="toolbar-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="toolbar-list">
            <%if (model.RecordID >
0)
              {%>
            <%=GetDefaultAddCommandValidationProductInfo()%>
            <%}
              else
              {%>
            <%= GetDefaultAddCommandValidation()%>
            <%}%>
        </div>
    </div>
    <div class="clr">
    </div>
</div>--%>
<div class="b">
    <div class="b">
        <div class="b">
        </div>
    </div>
</div>
</form>
<script language="javascript" type="text/javascript">

    var VSWController = 'ModProduct_Info';

    var VSWArrVar = [
                        'limit', 'PageSize'
                   ];


    var VSWArrQT = [
                      '<%= model.PageIndex + 1 %>', 'PageIndex',
                      '<%= model.Sort %>', 'Sort'
                  ];

    var VSWArrDefault =
                  [
                    '1', 'PageIndex',
                    '20', 'PageSize'
                  ];

    var pageUrlCheckCode = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=1")%>';
    var formID = "vswForm";

    // Link check màu sản phẩm
    var urlColor_Add = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=colorAdd")%>';
    var urlColor_Save = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=colorSave")%>';
    var urlColor_Delete = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=colorDelete")%>';

    // Link check Size sản phẩm
    var urlSize_Add = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=sizeAdd")%>';
    var urlSize_Save = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=sizeSave")%>';
    var urlSize_Delete = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=sizeDelete")%>';

    // Link chủng loại liên quan
    var urlProductGroup_Add = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=productGroupAdd")%>';
    var urlProductGroup_Delete = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=productGroupDelete")%>';

    // Link Sản phẩm liên quan
    var urlProductRelative_Delete = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=relativeDelete")%>';
    function ReloadData_SanPhamLienQuan(ListString) {
        $("tbody[class='tbl-data-productrelative']").html(ListString);
    }

    // Sản phẩm bán kèm
    var urlProductAttach_Delete = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=attachDelete")%>';
    function ReloadData_SanPhamBanKem(ListString) {
        $("tbody[class='tbl-data-productattach']").html(ListString);
    }

    // Link Đại lý
    var urlAgent_Add = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=agentAdd")%>';
    var urlAgent_Delete = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=agentDelete")%>';

    // Link Khu vực bán
    var urlArea_Add = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=areaAdd")%>';
    var urlArea_Delete = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=areaDelete")%>';

    // Thuộc tính lọc
    var urlFilter_Add = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=filterAdd")%>';
    var urlFilter_Delete = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=filterDelete")%>';

    // Link ảnh SlideShow
    var urlImageSlideShow_Add = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=imageSlideShowAdd")%>';
    var urlImageSlideShow_Save = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=imageSlideShowSave")%>';
    var urlImageSlideShow_Delete = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=imageSlideShowDelete")%>';

    // Link cập nhật thuộc tính (thông tin kỹ thuật) cho sản phẩm
    var urlProperties_Save = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=properties_Save")%>';

    // Link cập nhật thông tin Bình luận sản phẩm
    var urlComment_Save = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=comment_Save")%>';
    var urlComment_Approve = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=comment_Approve")%>';
    var urlComment_UnApprove = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=comment_UnApprove")%>';
    var urlComment_Delete = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=comment_Delete")%>';

    // Link Cập nhật loại sản phẩm
    var urlTypes_Add = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=typesAdd")%>';
    var urlTypes_Delete = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=typesDelete")%>';


    CKFinder.setupCKEditor(null, '/{CPPath}/Content/ckfinder/');

    function refreshPage(arg) {
        arg = '~' + arg;
        document.getElementById(name_control).value = arg;
        var Arr = name_control.split('_');

        if (document.getElementById("img_view_" + Arr[1].toString()))
            document.getElementById("img_view_" + Arr[1].toString()).src = arg.replace('~/', '/{ApplicationPath}');
    }


    //#region documentready
    $(document).ready(function () {

        // Thay đổi tab
        $("a[href*='dvtab']").click(function () {
            $("div[id*='dvtab']").addClass("hide");
            $("a[href*='dvtab']").removeClass("current");
            $("div[id*='" + $(this).attr("href") + "']").removeClass("hide");
            $(this).addClass("current");
            return false;
        });

        // Kiểm tra mã sản phẩm
        $("#Code").blur(function () {

            if (this.value.trim() == "")
                return;

            CheckDuplicate(pageUrlCheckCode, formID, "Code");

        });


        // Ngày bắt đầu khuyến mại
        $("#StartDate").datetimepicker({
            lang: 'vi',
            format: 'd/m/Y H:i',
            mask: true,
            allowBlank: true,
            validateOnBlur: false,
            value: '<%=item.StartDate==null?DateTime.Now.ToString("dd/MM/yyyy HH:mm"):(item.StartDate.Year==0001?DateTime.Now.ToString("dd/MM/yyyy HH:mm"):item.StartDate.ToString("dd/MM/yyyy HH:mm")) %>'
            //,onShow: function (ct) {
            //    this.setOptions({
            //        maxDate: $("#FinishDate").val() ? $("#FinishDate").val() : false
            //    })
            //}
        });
        // Ngày kết thúc khuyến mại
        $("#FinishDate").datetimepicker({
            lang: 'vi',
            format: 'd/m/Y H:i',
            mask: true,
            allowBlank: true,
            validateOnBlur: false,
            value: '<%=item.FinishDate==null?string.Empty:(item.FinishDate.Year==0001?string.Empty:item.FinishDate.ToString("dd/MM/yyyy HH:mm")) %>'
            //, onShow: function (ct) {
            //    this.setOptions({
            //        minDate: $('#StartDate').val() ? $('#StartDate').val() : false
            //    })
            //}
        });


        // Đăng ký lịch
        // Ngày bắt đầu bán
        $('#RuntimeDateStart').datetimepicker({
            lang: 'vi',
            format: 'd/m/Y H:i',
            mask: true,
            allowBlank: true,
            validateOnBlur: false,
            value: '<%=item.RuntimeDateStart==null?string.Empty:(item.RuntimeDateStart.Year==0001?string.Empty:item.RuntimeDateStart.ToString("dd/MM/yyyy HH:mm")) %>'
        });
        // Ngày kết thúc bán
        $('#RuntimeDateFinish').datetimepicker({
            lang: 'vi',
            format: 'd/m/Y H:i',
            mask: true,
            allowBlank: true,
            validateOnBlur: false,
            value: '<%=item.RuntimeDateFinish==null?string.Empty:(item.RuntimeDateFinish.Year==0001?string.Empty:item.RuntimeDateFinish.ToString("dd/MM/yyyy HH:mm")) %>'
        });
        // Ngày đăng
        $('#PostDate').datetimepicker({
            lang: 'vi',
            format: 'd/m/Y H:i',
            mask: true,
            value: '<%=item.PostDate==null?string.Empty:(item.PostDate.Year==0001?string.Empty:item.PostDate.ToString("dd/MM/yyyy HH:mm")) %>'
        });
        
        // Đăng ký lựa chọn màu
        $("input[type='text'][id*='txtColorCode']").colpick({
            layout: 'hex',
            submit: 0,
            colorScheme: 'dark',
            onChange: function (hsb, hex, rgb, el, bySetColor) {
                $(el).css('background-color', '#' + hex);
                // Fill the text box just if the color was set using the picker, and not the colpickSetColor function.
                if (!bySetColor) $(el).val('#' + hex);
            }
        }).keyup(function () {
            $(this).colpickSetColor(this.value);
        });

        // Khi chọn thuộc tính cũ của sản phẩm
        $("select[controlset][class='DropDownList']").change(function () {
            var controlName = $(this).attr("controlset");
            $("#" + controlName).val($(this).find("option:selected").text());
        });

        // Khi chọn lưu dữ liệu hay không lưu dữ liệu (Thuộc tính sản phẩm
        $("input[type='checkbox'][name*='chkPropertySaveData']").change(function () {
            var isChecked = $(this).is(":checked");
            if (isChecked == true)
                $(this).parent().find("input[type='hidden'][name*='hidPropertySaveData']").val("1");
            else
                $(this).parent().find("input[type='hidden'][name*='hidPropertySaveData']").val("0");
        });

        // Khi cập nhật thuộc tính cho sản phẩm
        $("#btnPropertiesSave").click(function () {
            PropertiesSave(urlProperties_Save, ".tbl-data-properties");

            return false;
        });

        // Sự kiện liên quan đến bình luận của sản phẩm
        // Nút save cách nút Cancel 6px
        $("a[class*='jgrid comment-save']").css("margin-right", "6px");

        $("a[class*='jgrid comment-edit']").click(function () {
            // Ẩn nút Edit
            $(this).addClass("hide");

            // Nút lưu thay đổi
            $(this).parent().find("a[class*='jgrid comment-save']").removeClass("hide");

            // Nút Hủy lưu thay đổi
            $(this).parent().find("a[class*='jgrid comment-cancel']").removeClass("hide");

            // Ẩn label conntent
            $("#" + $(this).attr("controlhide")).addClass("hide");

            // Hiện text chỉnh sửa
            $("#" + $(this).attr("controlshow")).removeClass("hide");

        });

        // Hủy cập nhật
        $("a[class*='jgrid comment-cancel']").click(function () {
            // Ẩn nút Cancel Edit
            $(this).addClass("hide");

            // Nút lưu thay đổi
            $(this).parent().find("a[class*='jgrid comment-save']").addClass("hide");

            // Nút lưu thay đổi
            $(this).parent().find("a[class*='jgrid comment-edit']").removeClass("hide");

            // Ẩn label conntent
            $("#" + $(this).attr("controlhide")).addClass("hide");

            // Hiện text chỉnh sửa
            $("#" + $(this).attr("controlshow")).removeClass("hide");

        });
    });
     
</script>
<style type="text/css">
    td.key
    {
        width: 0px !important;
        nowrap: nowrap !important;
        padding-left: 5px !important;
    }
    #txtMauHienThi
    {
        width: 70px;
        border-right-width: 20px;
        border-right-style: solid; /*border-right: 20px solid green;*/
    }
    .tbl-color-size td input
    {
        width: 100% !important;
        padding-top: 2px;
        padding-bottom: 1px;
        padding-right: 2px;
    }
    .tbl-color-size td.text-right input
    {
        text-align: right !important;
    }
</style>
