<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl"%>

<%
    var listItem = ViewBag.Data as List<ModChat_HistoryEntity>;
    var model = ViewBag.Model as MChat_HistoryModel;
%>
Request.Cookies["UserName_Chat"]
<div class="DefaultModuleContent">
<div class="defaultContentTitle TitleContent title">
        <div class="title">
            Chát</div>
    </div>
        <div class="defaultContentDetail defaultContent defaultContent-chat">
        <div class="div-chat-history">
            <div class="div-chat-history-left">
                <div class="div-chat-history-left-title">
                    Tiêu chí tìm kiếm
                    <input type="hidden" id="PageIndex" name="PageIndex" value="0" />
                    <input type="hidden" id="PageSize" name="PageSize" value="0" />
                </div>
                <div class="div-chat-history-left-content">
                    <fieldset>
                        <legend>Thông tin sản phẩm</legend>
                        <table border="0" cellpadding="2" cellspacing="3" class="div-chat-history-left-content-tbl_infochat">
                            <tr>
                                <td>
                                    Mã/ Tên/ Từ khóa:
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input class="text_input_search_name" type="text" name="Name" id="Name" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input class="text_input_search_function" type="button" name="btnSearch" id="btnSearch"
                                        value="Tìm kiếm" />
                                </td>
                            </tr>
                        </table>
                        <div class="div-chat-history-group-content-detail">
                        </div>
                    </fieldset>
                    <fieldset>
                        <legend>Hãng sản xuất</legend>
                        <div class="div-chat-history-group-content-detail">
                             ViewBag.ListManufacturer  
                        </div>
                    </fieldset>
                    <!--Danh sách các nhóm thuộc tính khác-->
                     ViewBag.ListFilterGroups 
                </div>
            </div>
            <div class="div-chat-history-right">
                <div class="div-chat-history-right-title">
                    Kết quả tìm kiếm</div>
                <div class="div-chat-history-right-content">
                    <div class="div-chat-history-right-content-option">
                        Sắp xếp theo:
                        <select id="Orderby">
                            <option value="BuyCount">Số lượt mua</option>
                            <option value="PostDate">Ngày đăng</option>
                            <option value="Price">Giá</option>
                        </select>
                        &nbsp;
                        <select id="OrderType">
                            <option value="ASC">Tăng dần</option>
                            <option value="DESC">Giảm dần</option>
                        </select>
                    </div>
                    <div>
                        <div class="chat-history-loading">
                            <div>
                                <p>
                                    <img class="chat-history-loading-img" alt="Đang tìm kiếm" />&nbsp;&nbsp;Đang tìm
                                    kiếm, xin hãy chờ!</p>
                            </div>
                        </div>
                        <div class="div-chat-history-right-content-detail-result">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>