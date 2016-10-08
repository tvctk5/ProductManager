$(document).ready(function () {
    var urlpost = "";

    // Mở popup so sánh đồng thời tải dữ liệu
    $(".div-compare-product-title").click(function () {
        var hide = $(this).attr("hide");
        // Nếu đang ẩn ==> Mở ra
        if (hide == "TRUE") {
            $(this).attr("hide", "FALSE");
            $(this).addClass("div-compare-product-title-showicon");
            $(this).removeClass("div-compare-product-title-hideicon");
            $(".div-compare-product").css("width", "200px");
            $(".div-compare-product-title span").removeClass("hide");
            $(".div-compare-product-content").removeClass("hide");
            $(".div-compare-product-function").removeClass("hide");

            // Tải lại dữ liệu
            Product_Compare_ReloadList();
        }
        // Đóng lại
        else {
            $(this).attr("hide", "TRUE");
            $(this).removeClass("div-compare-product-title-showicon");
            $(this).addClass("div-compare-product-title-hideicon");
            $(".div-compare-product").css("width", "50px");
            $(".div-compare-product-title span").addClass("hide");
            $(".div-compare-product-content").addClass("hide");
            $(".div-compare-product-function").addClass("hide");
        }
    });

    // So sánh sản phẩm
    $(".input-compare-product").change(function () {
        var checked = $(this).is(":checked");
        var ProductID = $(this).attr("productid");
        // Chọn để so sánh
        if (checked)
            Product_Compare(ProductID, true, false);

        // Hủy chọn
        else
            Product_Compare(ProductID, false, false);
    });

    // Sub Action
    $(".action-sub-checkbox-label").click(function () {
        var parent_checkbox = $(this).parents("span");

        var checkbox = parent_checkbox.find("input[type='checkbox']:first");
        if (checkbox.is(":checked"))
            checkbox.removeAttr("checked");
        else
            checkbox.attr("checked", "checked");

        // Thay đổi check
        checkbox.change();
    });

    $(".action-sub-radio-label").click(function () {
        var parent_checkbox = $(this).parents("span");

        var checkbox = parent_checkbox.find("input[type='radio']:first");
        if (checkbox.is(":checked"))
            checkbox.removeAttr("checked");
        else
            checkbox.attr("checked", "checked");

        // Thay đổi check
        checkbox.change();
    });

    // Tìm kiếm sản phẩm
    $(".box-search-icon").click(function () {
        var TextSearch = $("#TextSearch");
        if (TextSearch == null || TextSearch.val() == "") {
            alert("Nhập nội dung cần tìm kiếm.");
            TextSearch.focus();
            return false;
        }

        // Chuyển trang
        window.location.href = "/vn/San-pham/Loc-tim-san-pham.aspx?TextSearch=" + TextSearch.val();
    });

    $("#TextSearch").keypress(function (e) {
        code = e.keyCode ? e.keyCode : e.which;
        if (code.toString() == 13) {
            $(".box-search-icon").click();
        }
    })

    // Ẩn hiện popup Survey: Khi bấm vào tiêu đề popup
    $(".popup-survey .popup-title").click(function () {
        var Opened = $(this).attr("Opened");

        // Đang mở --> ẩn
        if (Opened == "1") {
            $(this).find("a:first").attr("Opened", "0");
            $(this).attr("Opened", "0");
            $(this).parent(".popup-survey").find(".popup-content").addClass("hide");

            // Set cookie xem, có hiển thị popup khi reload lại trang hay không
            var alwayOpen = $(this).attr("alwayOpen");
            if (alwayOpen == "0") {

                //var date = new Date();
                //var minutes = 30;
                //date.setTime(date.getTime() + (minutes * 60 * 1000));
                //$.cookie("VSW.PopupSurvey", 0, { expires: date });

                // Popup sẽ không hiển thị khi reload nữa: Cookie 30'
                UpdateSurvey("0");
            }
        }
        else
        // Đang ẩn --> Hiện
        {
            $(this).find("a:first").attr("Opened", "1");
            $(this).attr("Opened", "1");
            $(this).parent(".popup-survey").find(".popup-content").removeClass("hide");
        }

    });

    // Ẩn hiện popup module: Khi bấm vào tiêu đề popup
    $(".div-module-popup .module-popup-title").click(function () {
        var Opened = $(this).attr("Opened");

        // Đang mở --> ẩn
        if (Opened == "1") {
            $(this).find("a:first").attr("Opened", "0");
            $(this).attr("Opened", "0");
            $(this).parent(".div-module-popup").find(".module-popup-content").addClass("hide");

            // Set cookie xem, có hiển thị popup khi reload lại trang hay không
            var alwayOpen = $(this).attr("alwayOpen");
            var cookiename = $(this).attr("cookiename");
            if (alwayOpen == "0") { 
                // Popup sẽ không hiển thị khi reload nữa: Cookie 30'
                UpdateCookie(cookiename, "0");
            }
        }
        else
        // Đang ẩn --> Hiện
        {
            $(this).find("a:first").attr("Opened", "1");
            $(this).attr("Opened", "1");
            $(this).parent(".div-module-popup").find(".module-popup-content").removeClass("hide");
        }

    });

    // Nút go top 
    $(".go_top").click(function () {
        $('html,body').animate({ scrollTop: 0 }, 1000);
    });
});

// ---------------------------------------------------------------------------------------
// So sánh hoặc hủy so sánh
// Type: 0: Remove (false) | 1: Add (true)
function Product_Compare(ProductID, type, reloadForm) {
    if (type)
        urlpost = "/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=addcompareproduct";
    else
        urlpost = "/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=removecompareproduct";

    var pageUrl = urlpost + '&RecordID=' + ProductID;
    var dataPostBack = null;

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            // Tải lại dữ liệu
            $(".div-compare-product-content").html(data.MessSuccess);

            // Xem có sản phẩm hay không
            if (data.NoProduct)
                $(".div-compare-product-function a").addClass("hide");
            else
                $(".div-compare-product-function a").removeClass("hide");

            if (reloadForm)
            // Tải lại form
                location.reload(false);
        }
    })
    .done(function () {

        // Tải lại form
        if (reloadForm == true)
            return;

        // Kiểm tra xem, nếu popup đang ẩn thì hiển thị
        var hide = $(".div-compare-product-title").attr("hide");
        if (hide == "TRUE")
            $(".div-compare-product-title").click();
    })
    .fail(function () {
        alert("Phát sinh lỗi trong quá trình thực hiện. Hãy thử lại!");
        return null;
    })
    .always(function () {
    })
    ;
}

function Product_Compare_ReloadList() {
    urlpost = "/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=reloadlistcompareproduct";

    var pageUrl = urlpost;
    var dataPostBack = null;

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            // Tải lại dữ liệu
            $(".div-compare-product-content").html(data.MessSuccess);

            // Xem có sản phẩm hay không
            if (data.NoProduct)
                $(".div-compare-product-function a").addClass("hide");
            else
                $(".div-compare-product-function a").removeClass("hide");
        }
    })
    .done(function () {
    })
    .fail(function () {
        alert("Phát sinh lỗi trong quá trình thực hiện. Hãy thử lại!");
        return null;
    })
    .always(function () {
    })
    ;
}

// Xóa sản phẩm khỏi danh sách
function Product_Compare_Delete(object) {
    var ProductId = $(object).attr("productid");
    // Xóa và tải Lại danh sách
    Product_Compare(ProductId, false, false)

    return false;
}

// Xóa sản phẩm khỏi danh sách: Tải lại form
function Product_Compare_Delete_ReloadForm(object) {
    var ProductId = $(object).attr("productid");
    // Xóa và tải Lại danh sách và Tải lại form
    Product_Compare(ProductId, false, true);
}

// Cập nhật lại trạng thái xem có mở popup survey nữa hay không
function UpdateSurvey(status) {
    urlpost = "/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=updatesurvey&PopupSurvey=" + status;

    var pageUrl = urlpost;
    var dataPostBack = null;

    $.post(pageUrl,
    dataPostBack,
    function (data) {
    })
    .done(function () {
    })
    .fail(function () {
    })
    .always(function () {
    })
    ;
}

// Cập nhật lại trạng thái xem có mở popup nữa hay không
function UpdateCookie(cookieName,status) {
    urlpost = "/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=updatecookie&CookieName=" + cookieName + "&PopupStatus=" + status;

    var pageUrl = urlpost;
    var dataPostBack = null;

    $.post(pageUrl,
    dataPostBack,
    function (data) {
    })
    .done(function () {
    })
    .fail(function () {
    })
    .always(function () {
    })
    ;
}
