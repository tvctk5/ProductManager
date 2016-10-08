<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    //var listItem = ViewBag.Data as List<ModProduct_FilterEntity>;
    //var model = ViewBag.Model as MProduct_FilterModel;
    string sTextSearch = ConvertTool.ConvertToString(Request.QueryString["TextSearch"]);
%>
<div class="DefaultModuleContent">
    <script type="text/javascript">
        // Ngôn ngữ hiện tại
        var LanguageId = '<%=ViewPage.CurrentLang.ID %>';
        var ipageindex = '<%=VSW.Core.Global.Config.GetValue("Mod.WebPageSize").ToString() %>';


        $(document).ready(function () {

            $("#Name").val('<%=sTextSearch %>');

            // LẤy số bản ghi
            $("#PageSize").attr("value", ipageindex);

            // Tìm kiếm phân trang: chạy lần đầu tiên vào trang
            ProductFilter();

            // Tìm kiếm khi có sự thay đổi 
            $("#btnSearch").click(function () {
                // Đặt lại page index 
                $("#PageIndex").val("0");
                ProductFilter();
            });

            $("#Orderby,#OrderType").change(function () {
                // Đặt lại page index 
                $("#PageIndex").val("0");
                ProductFilter();
            });

            $(".div-product-filter-left input[type!='text'][type!='button']").change(function () {
                // Đặt lại page index 
                $("#PageIndex").val("0");

                // Nếu là dạng radio thì xóa hết các attribute đi rồi chạy lại
                if ($(this).attr("type") == "radio") {
                    var group = $(this).attr("group");

                    // Duyệt xóa các Attr
                    $("span[group='" + group + "']").each(function () {
                        var text_control = $(this).find("label-value[value_base]");
                        text_control.html(text_control.attr("value_base"));
                        text_control.css("font-weight", "normal");
                        text_control.css("color", "black");
                    });
                }

                // Tìm kiếm
                ProductFilter();

                // Thay đổi text
                var parentactionsubfilter = $(this).parents("span.action-sub:first");
                var status_check = $(this).is(":checked");

                // Gọi hàm
                ChangeText_ProductFilter(parentactionsubfilter, status_check);
            });

            // CLick phân trang
            $(".span-page").click(function () {
                $("#PageIndex").val($(this).attr("pageindex"));

                // Tìm kiếm phân trang
                ProductFilter();
            });
        });

        function ProductFilter() {
            var Orderby = $("#Orderby").val();
            var OrderType = $("#OrderType").val();

            // Tìm kiếm
            Product_Filter($(".div-product-filter-left"), LanguageId, Orderby, OrderType)
        }

        function ChangeText_ProductFilter(parent, status) {
            var text_control = $(parent).find("label-value[value_base]");
            if (text_control == null)
                return;

            // Nếu thuộc tính lọc đang được check
            if (status) {
                text_control.html(text_control.attr("value_base") + "&nbsp;<span class='product-filter-delete'></span>");
                text_control.css("font-weight", "bold");
                text_control.css("color", "blue");
            }
            else {
                text_control.html(text_control.attr("value_base"));
                text_control.css("font-weight", "normal");
                text_control.css("color", "black");
            }
        }

    </script>
    <div class="defaultContentTitle TitleContent title">
        <div class="title">
            Lọc tìm kiếm sản phẩm</div>
    </div>
    <div class="defaultContentDetail defaultContent defaultContent-ProductFilter">
        <div class="div-product-filter">
            <div class="div-product-filter-left">
                <div class="div-product-filter-left-title">
                    Tiêu chí tìm kiếm
                    <input type="hidden" id="PageIndex" name="PageIndex" value="0" />
                    <input type="hidden" id="PageSize" name="PageSize" value="0" />
                </div>
                <div class="div-product-filter-left-content">
                    <fieldset>
                        <legend>Thông tin sản phẩm</legend>
                        <table border="0" cellpadding="2" cellspacing="3" class="div-product-filter-left-content-tbl_infosearch">
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
                        <div class="div-product-filter-group-content-detail">
                        </div>
                    </fieldset>
                    <fieldset>
                        <legend>Hãng sản xuất</legend>
                        <div class="div-product-filter-group-content-detail">
                            <%=ViewBag.ListManufacturer %>
                        </div>
                    </fieldset>
                    <!--Danh sách các nhóm thuộc tính khác-->
                    <%=ViewBag.ListFilterGroups%>
                </div>
            </div>
            <div class="div-product-filter-right">
                <div class="div-product-filter-right-title">
                    Kết quả tìm kiếm</div>
                <div class="div-product-filter-right-content">
                    <div class="div-product-filter-right-content-option">
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
                        <div class="product-filter-loading">
                            <div>
                                <p>
                                    <img class="product-filter-loading-img" alt="Đang tìm kiếm" />&nbsp;&nbsp;Đang tìm
                                    kiếm, xin hãy chờ!</p>
                            </div>
                        </div>
                        <div class="div-product-filter-right-content-detail-result">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
