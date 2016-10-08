var linkPost = '';
var langCode = 'vn';
// Thêm sản phẩm vào giỏ
function Product_AddToCart(ProductID, Price, redrectToCart) {

    var pageUrl = linkPost + '&RecordID=' + ProductID + "&Price=" + Price;
    var dataPostBack = null;
    var link_viewcart = "/" + langCode + "/gio-hang.aspx";

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            // Ẩn dòng bị xóa đi
            if (redrectToCart)
                window.location.href = link_viewcart;
            else {
                // Nút thêm vào giỏ hàng --> Đã thêm vào giỏ hàng
                $(".btnThemVaoGioHang").unbind("click");
                $(".btnThemVaoGioHang").attr("click", "return false;");
                $(".btnThemVaoGioHang span").html("Đã thêm vào giỏ hàng");

                // Nút Mua ngay --> chuyển thành xem giỏ hàng
                $(".btnMuaNgay").unbind("click");
                $(".btnMuaNgay").attr("onclick", "window.location.href='" + link_viewcart + "';return false;");
                $(".btnMuaNgay span").html("Xem giỏ hàng");

                alert("Thêm vào giỏ hàng thành công.");
            }
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

function Product_RemoveProductFromCart(ProductID, objectCurrent) {

    var pageUrl = linkPost + '&RecordID=' + ProductID;
    var dataPostBack = null;

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            // Ẩn dòng hiện tại đi
            $(objectCurrent).parents("tr").addClass("hide");
            $(objectCurrent).parents("tr").find("td.cart-td-stt").removeClass("cart-td-stt");

            // Đánh lại số thứ tự
            $("td.cart-td-stt").each(function (index) {
                $(this).find("span:first").html(index + 1);
            });

            // Gán lại tổng tiền
            $(".cart-totalprice").html(data.MessSuccess);

            // Chuyển tới trang chủ nếu hết sản phẩm
            if (data.NoProduct)
                window.location.href = "/";
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

// Tạo tài khoản
function CreateAccount(formPostId) {

    var pageUrl = linkPost;
    var dataPostBack = new FormData($("#" + formPostId)[0]);

    $.ajax({
        url: pageUrl,
        type: 'POST',
        contentType: false,
        processData: false,
        data: dataPostBack,
        success: function (data) {
            // Tạo tài khoản thất bại
            if (data.Error) {
                alert(data.MessError);

                // Đã tồn tại tài khoản
                if (data.Type == 0)
                    $("#UserName").focus();

                // Đã tồn tại Email
                if (data.Type == 1)
                    $("#Email").focus();

                return false;
            }
            else {
                // Tạo tài khoản thành công
                //location.reload(false);
                alert(data.MessSuccess);
                window.location.href = "/";
            }
        },
        error: function () {
            alert("Phát sinh lỗi trong quá trình thực hiện. Hãy thử lại!");
            return null;
        }
    });
}

//$(':button').click(function(){
//    var formData = new FormData($('form')[0]);
//    $.ajax({
//        url: 'upload.php',  //Server script to process data
//        type: 'POST',
//        xhr: function() {  // Custom XMLHttpRequest
//            var myXhr = $.ajaxSettings.xhr();
//            if(myXhr.upload){ // Check if upload property exists
//                myXhr.upload.addEventListener('progress',progressHandlingFunction, false); // For handling the progress of the upload
//            }
//            return myXhr;
//        },
//        //Ajax events
//        beforeSend: beforeSendHandler,
//        success: completeHandler,
//        error: errorHandler,
//        // Form data
//        data: formData,
//        //Options to tell jQuery not to process data or worry about content-type.
//        cache: false,
//        contentType: false,
//        processData: false
//    });
//});

// Đăng nhập
function Login(user, pass) {

    var pageUrl = linkPost + '&UserName=' + user + "&Pass=" + pass;
    var dataPostBack = null;

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        // Đăng nhập thất bại
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            // Thông báo đăng nhập thành công
            //alert(data.MessSuccess);
            location.reload(false);
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

// Đăng xuất
function Logout(user, pass) {

    var pageUrl = linkPost;
    var dataPostBack = null;

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        // Đăng xuất thất bại
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            location.reload(false);
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

// Đăng nhập form
function Login_Form(user, pass) {

    var pageUrl = linkPost + '&UserName=' + user + "&Pass=" + pass;
    var dataPostBack = null;

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        // Đăng nhập thất bại
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            // Thông báo đăng nhập thành công
            //alert(data.MessSuccess);
            window.location.href = "/";
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

// Tạo Cập nhật tài khoản
function UpdateAccount_Form(formPostId, Id) {

    var pageUrl = linkPost + "&RecordID=" + Id;
    var dataPostBack = new FormData($("#" + formPostId)[0]);

    $.ajax({
        url: pageUrl,
        type: 'POST',
        contentType: false,
        processData: false,
        data: dataPostBack,
        success: function (data) {
            // Tạo tài khoản thất bại
            if (data.Error) {
                alert(data.MessError);

                // Không tìm thấy tài khoản
                if (data.Type == -1)
                    location.reload(true);

                // Mật khẩu cũ không đúng
                if (data.Type == 0)
                    $("#OldPass").focus();

                // Email mới đã tồn tại
                if (data.Type == 1)
                    $("#Email").focus();


                return false;
            }
            else {
                // Tạo tài khoản thành công
                alert(data.MessSuccess);
                location.reload(false);
            }
        },
        error: function () {
            alert("Phát sinh lỗi trong quá trình thực hiện. Hãy thử lại!");
            return null;
        }
    });
}

// Tìm kiếm sản phẩm theo các thuộc tính lọc
function Product_Filter(framePost, languageId, order, typeorderby) {

    var pageUrl = "/Tools/Ajax/ModProduct_Info/PostData.aspx?type=productfilter" + "&languageId=" + languageId + "&order=" + order + "&typeorderby=" + typeorderby;
    var dataPostBack = framePost.find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

    // Hiện popup
    $(".product-filter-loading").show();
    $(".div-product-filter-right-content-detail-result").hide();

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            // alert(data.MessError);
            $(".div-product-filter-right-content-detail-result").html(data.MessError);
            return false;
        }
        else {
            // Hiển thị dữ liệu tìm thấy
            $(".div-product-filter-right-content-detail-result").html(data.MessSuccess);
            $(".div-product-filter-right").css("height", "auto");
        }
    })
    .done(function () {
    })
    .fail(function () {
        alert("Phát sinh lỗi trong quá trình thực hiện. Hãy thử lại!");
        return null;
    })
    .always(function () {
        // Ẩn popup
        $(".product-filter-loading").hide();
        $(".div-product-filter-right-content-detail-result").show();

        // CLick phân trang
        $(".span-page").click(function () {
            $("#PageIndex").attr("value", $(this).attr("pageindex"));

            // Tìm kiếm phân trang
            ProductFilter();
        });

        // Đăng ký sự kiện cho checkbox So sánh sản phẩm
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

        // Chọn để So sánh sản phẩm
        // Sub Action
        $(".action-sub-checkbox .action-sub-checkbox-label").click(function () {
            var parent_checkbox = $(this).parents("span");

            var checkbox = parent_checkbox.find("input[type='checkbox']:first");
            if (checkbox.is(":checked"))
                checkbox.removeAttr("checked");
            else
                checkbox.attr("checked", "checked");

            // Thay đổi check
            checkbox.change();
        });
    })
    ;
}

// Thêm mới comment sản phẩm
function SentComment(objectPost, objectReloadData, ProductId) {

    linkPost = "/Tools/Ajax/ModProduct_Info/PostData.aspx?type=addcomment";
    var pageUrl = linkPost + '&RecordID=' + ProductId;
    var dataPostBack = objectPost.find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        // Đăng nhập thất bại
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            // Thông báo đăng nhập thành công
            alert(data.MessSuccess);

            // Bỏ các giá trị đi
            objectPost.find("textarea#Comment_Content").val("");

            // Tải lại danh sách comment
            objectReloadData.html(data.ListComment);
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

// Yêu cầu reset mật khẩu
function RequestResetPassword(User, Email) {

    linkPost = "/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=requestresetpass";
    var pageUrl = linkPost + '&User=' + User + "&Email=" + Email + "&langCode=" + langCode;
    var dataPostBack = null;

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        // Đăng nhập thất bại
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            // Thông báo đăng nhập thành công
            alert(data.MessSuccess);

            window.location.href = "/";
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

// Reset mật khẩu
function ResetPassword(CustomerID, Pass) {

    linkPost = "/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=resetpass";
    var pageUrl = linkPost + '&CustomerID=' + CustomerID + "&Pass=" + Pass;
    var dataPostBack = null;

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        // Đăng nhập thất bại
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            // Thông báo đăng nhập thành công
            alert(data.MessSuccess);

            window.location.href = "/" + langCode + "/tai-khoan/dang-nhap.aspx";
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

// Gửi thông tin survey
function SentSurvey(dataPost) {
    // Lấy name của các input
    var Name_Input = dataPost.find("input:first").attr("name");
    linkPost = "/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=sentsurvey&Name=" + Name_Input;
    var pageUrl = linkPost;
    var dataPostBack = dataPost.find("input").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        // Phát sinh lỗi
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            // Thông báo gửi thành công
            alert(data.MessSuccess);

            // Ẩn form popup
            $(".popup-title").click();
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

// Gửi thông tin phản hồi
function SubmitPopup_Feedback(dataPost) {

    var pageUrl = linkPost;
    var dataPostBack = dataPost.find("input,textarea").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        // Phát sinh lỗi
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            // Thông báo thành công
            alert(data.MessSuccess);

            // Ẩn form popup
            dataPost.find(".module-popup-title").click();
            dataPost.find("table input[type='text'],table textarea").val("");
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