/// <reference path="../jQuery/jquery-1.8.2.js" />

/*var pageUrl = '<%=ResolveUrl("~/CP/Tools/Ajax/ModProduct_Info/PostData.aspx?Type=1")%>';
var formID = "vswForm";*/

//#region CheckValidationForm
function CheckValidationForm() {

    var bolCode = false;
    var bolName = false;
    var bolInfoBasic = false;
    var bolMenuID = true;
    var bolPrice = false;
    var Mess = "";
    var txtCode = document.getElementById("Code");
    var txtName = document.getElementById("Name");
    var InfoBasic = document.getElementById("InfoBasic");
    var MenuID = document.getElementById("MenuID");
    var txtPrice = document.getElementById("Price");

    //var iIndexMenuSelected = MenuID.selectedIndex;
    bolCode = true;
    //    if (txtCode != null) {
    //        if (txtCode.value.trim() != "") {
    //            if (txtCode.value.trim().length < 3) {
    //                Mess = " - Yêu cầu nhập Mã sản phẩm từ 3 ký tự trở lên";
    //                bolCode = false;
    //            }
    //            else {
    //                bolCode = true;
    //            }
    //        }
    //        else {
    //            Mess = " - Yêu cầu nhập Mã sản phẩm";
    //        }
    //    }

    if (txtName != null) {
        if (txtName.value.trim() != "") {
            bolName = true;
        }
        else {
            if (Mess != "")
            { Mess = Mess + "\r\n"; }
            Mess = Mess + " - Yêu cầu nhập Tên sản phẩm";
        }
    }

    if (InfoBasic != null) {
        if (InfoBasic.value.trim() != "") {
            bolInfoBasic = true;
        }
        else {
            if (Mess != "")
            { Mess = Mess + "\r\n"; }
            Mess = Mess + " - Yêu cầu nhập Thông tin cơ bản cho sản phẩm";
        }
    }

    /*if (MenuID != null) {
    if (iIndexMenuSelected != 0) {
    bolMenuID = true;
    }
    else {
    if (Mess != "")
    { Mess = Mess + "\r\n"; }
    Mess = Mess + " - Yêu cầu chọn danh mục hiển thị của sản phẩm.";
    }
    }*/

    if (txtPrice != null) {
        if (txtPrice.value.trim() != "") {
            var PriceCheck = txtPrice.value.trim().replace(',', '').replace('.', '');
            var CountparseFloat = parseFloat(PriceCheck);
            if (CountparseFloat < 0) {
                if (Mess != "")
                { Mess = Mess + "\r\n"; }
                Mess = Mess + " - Giá bán nhập vào phải lớn hơn hoặc bằng 0.";
            }
            else { bolPrice = true; }

            txtPrice.value = PriceCheck;
        }
        else {
            if (Mess != "")
            { Mess = Mess + "\r\n"; }
            Mess = Mess + " - Yêu cầu nhập giá bán sản phẩm.";
        }
    }


    if (bolName && bolCode && bolInfoBasic && bolPrice && bolMenuID) {
        return true;
    }
    else {
        alert(Mess);
        if (bolCode == false) {
            txtCode.focus(); return false;
        }

        if (bolName == false) {
            txtName.focus(); return false;
        }

        if (bolInfoBasic == false) {
            InfoBasic.focus(); return false;
        }

        if (bolMenuID == false) {
            MenuID.focus(); return false;
        }

        if (bolPrice == false) {
            txtPrice.focus(); return false;
        }
    }
}
//#endregion CheckValidationForm



//#region documentready
$(document).ready(function () {
    /*$("#Code").blur(function () {

    if (this.value.trim() == "")
    return;

    CheckDuplicate(pageUrl, formID, "Code");

    });*/

    $("#SaleOffValue").blur(function () {
        SaleOffExec();
    });


    $("#SaleOffType").change(function () {
        SaleOffExec();
    });
});

function SaleOffExec() {
    var typeSaleOff = $("#SaleOffType").val();
    var saleOffValue = 0;
    var priceValue = ConvertToFloat($("#Price").val());

    var priceSaleOff = 0;
    // Giảm giá kiểu %
    if (typeSaleOff == "1") {
    // Kiểu % thì để dấu '.' thành ','
        $("#SaleOffValue").val(replaceAll($("#SaleOffValue").val(), ".", ","));
        // Đổi sang dạng số
        saleOffValue = parseFloat(replaceAll($("#SaleOffValue").val(), ",", "."));

        // Nếu cao hơn 100%
        if (saleOffValue > 100) {
            saleOffValue = 100;
            $("#SaleOffValue").val("100");
        }
        priceSaleOff = priceValue - ((priceValue * saleOffValue) / 100);
    }
    else {
        saleOffValue = ConvertToFloat($("#SaleOffValue").val());

        // Giảm giá trực tiếp bằng tiền
        if (saleOffValue > priceValue) {
            saleOffValue = priceValue;
            $("#SaleOffValue").val(priceValue.toString());
        }

        priceSaleOff = priceValue - saleOffValue;
    }

    if (priceSaleOff < 0)
        priceSaleOff = 0;

    // Giá giảm
    $("#PriceSale").val(ConvertToPrice(priceSaleOff.toString()));

    // Text giảm giá
    $("#PriceTextSaleView").val("-" + $("#SaleOffValue").val() + " " + $("#SaleOffType option:selected").text());
}
//#endregion documentready

//#region OnChangeSaleOff
function OnChangeSaleOff(chkSaleOff) {
    if ($("input[name='SaleOff']:first").is(":checked"))
        $(".SaleOffSetting").removeClass("hide");
    else
        $(".SaleOffSetting").addClass("hide");
}
//#endregion OnChangeSaleOff

//#region Sự kiện trên ListBox
function RToL() {
    var lstModProductGroupsOut = document.getElementById("lstModProductGroupsOut");
    var lstModProductGroupsIn = document.getElementById("lstModProductGroupsIn");
    var GroupOutLength = lstModProductGroupsOut.options.length;
    var GroupInLength = lstModProductGroupsIn.options.length;
    if (GroupOutLength <= 0)
        return;

    for (var i = 0; i < GroupOutLength; i++) {
        if (lstModProductGroupsOut.options[i].selected) {
            var objOption = new Option(lstModProductGroupsOut.options[i].text, lstModProductGroupsOut.options[i].value)

            var AttributeColor = lstModProductGroupsOut.options[i].style.color;
            if (AttributeColor != null) {
                objOption.style.color = AttributeColor;
            }

            lstModProductGroupsIn.add(objOption);
            lstModProductGroupsOut.remove(i);

            i--;
            GroupOutLength = GroupOutLength - 1;
        }
    }

    RefreshCustomersGroupsId();
}

function LToR() {
    var lstModProductGroupsOut = document.getElementById("lstModProductGroupsOut");
    var lstModProductGroupsIn = document.getElementById("lstModProductGroupsIn");
    var GroupOutLength = lstModProductGroupsOut.options.length;
    var GroupInLength = lstModProductGroupsIn.options.length;
    if (GroupInLength <= 0)
        return;

    for (var i = 0; i < GroupInLength; i++) {
        if (lstModProductGroupsIn.options[i].selected) {
            var objOption = new Option(lstModProductGroupsIn.options[i].text, lstModProductGroupsIn.options[i].value)

            var AttributeColor = lstModProductGroupsIn.options[i].style.color;
            if (AttributeColor != null) {
                objOption.style.color = AttributeColor;
            }

            lstModProductGroupsOut.add(objOption);
            lstModProductGroupsIn.remove(i);

            i--;
            GroupInLength = GroupInLength - 1;
        }
    }

    RefreshCustomersGroupsId();
}

function AllRToL() {
    var lstModProductGroupsOut = document.getElementById("lstModProductGroupsOut");
    var lstModProductGroupsIn = document.getElementById("lstModProductGroupsIn");
    var GroupOutLength = lstModProductGroupsOut.options.length;
    var GroupInLength = lstModProductGroupsIn.options.length;
    if (GroupOutLength <= 0)
        return;

    for (var i = 0; i < GroupOutLength; i++) {
        var objOption = new Option(lstModProductGroupsOut.options[i].text, lstModProductGroupsOut.options[i].value)

        var AttributeColor = lstModProductGroupsOut.options[i].style.color;
        if (AttributeColor != null) {
            objOption.style.color = AttributeColor;
        }

        lstModProductGroupsIn.add(objOption);
        lstModProductGroupsOut.remove(i);

        i--;
        GroupOutLength = GroupOutLength - 1;
    }

    RefreshCustomersGroupsId();
}

function AllLToR() {
    var lstModProductGroupsOut = document.getElementById("lstModProductGroupsOut");
    var lstModProductGroupsIn = document.getElementById("lstModProductGroupsIn");
    var GroupOutLength = lstModProductGroupsOut.options.length;
    var GroupInLength = lstModProductGroupsIn.options.length;
    if (GroupInLength <= 0)
        return;

    for (var i = 0; i < GroupInLength; i++) {

        var objOption = new Option(lstModProductGroupsIn.options[i].text, lstModProductGroupsIn.options[i].value)

        var AttributeColor = lstModProductGroupsIn.options[i].style.color;
        if (AttributeColor != null) {
            objOption.style.color = AttributeColor;
        }

        lstModProductGroupsOut.add(objOption);
        lstModProductGroupsIn.remove(i);

        i--;
        GroupInLength = GroupInLength - 1;
    }

    RefreshCustomersGroupsId();
}

// Cập nhật lại Id của các nhóm
function RefreshCustomersGroupsId() {
    var lstModProductGroupsOut = document.getElementById("lstModProductGroupsOut");
    var lstModProductGroupsIn = document.getElementById("lstModProductGroupsIn");

    var sModProductGroupsOutId = "";
    var sModProductGroupsInId = "";

    var GroupInLength = lstModProductGroupsIn.options.length;
    var GroupOutLength = lstModProductGroupsOut.options.length;

    // Id của các nhóm không chứa Khách hàng
    if (GroupOutLength > 0) {
        for (var i = 0; i < GroupOutLength; i++) {
            if (sModProductGroupsOutId.value = "")
            { sModProductGroupsOutId = lstModProductGroupsOut.options[i].value; }
            else {
                sModProductGroupsOutId = sModProductGroupsOutId + "," + lstModProductGroupsOut.options[i].value;
            }

        }
    }
    // Id của các nhóm chứa Khách hàng
    if (GroupInLength > 0) {
        for (var i = 0; i < GroupInLength; i++) {
            if (sModProductGroupsInId.value = "")
            { sModProductGroupsInId = lstModProductGroupsIn.options[i].value; }
            else {
                sModProductGroupsInId = sModProductGroupsInId + "," + lstModProductGroupsIn.options[i].value;
            }

        }
    }

    document.getElementById('ModProductGroupsInId').value = sModProductGroupsInId;
    document.getElementById('ModProductGroupsOutId').value = sModProductGroupsOutId;
}
//#endregion Sự kiện trên ListBox

//#region Sự kiện trên ListBox Agent
function AgentRToL() {
    var lstModProductAgentOut = document.getElementById("lstModProductAgentOut");
    var lstModProductAgentIn = document.getElementById("lstModProductAgentIn");
    var AgentOutLength = lstModProductAgentOut.options.length;
    var AgentInLength = lstModProductAgentIn.options.length;
    if (AgentOutLength <= 0)
        return;

    for (var i = 0; i < AgentOutLength; i++) {
        if (lstModProductAgentOut.options[i].selected) {
            var objOption = new Option(lstModProductAgentOut.options[i].text, lstModProductAgentOut.options[i].value)

            var AttributeColor = lstModProductAgentOut.options[i].style.color;
            if (AttributeColor != null) {
                objOption.style.color = AttributeColor;
            }

            lstModProductAgentIn.add(objOption);
            lstModProductAgentOut.remove(i);

            i--;
            AgentOutLength = AgentOutLength - 1;
        }
    }

    RefreshAgentId();
}

function AgentLToR() {
    var lstModProductAgentOut = document.getElementById("lstModProductAgentOut");
    var lstModProductAgentIn = document.getElementById("lstModProductAgentIn");
    var AgentOutLength = lstModProductAgentOut.options.length;
    var AgentInLength = lstModProductAgentIn.options.length;
    if (AgentInLength <= 0)
        return;

    for (var i = 0; i < AgentInLength; i++) {
        if (lstModProductAgentIn.options[i].selected) {
            var objOption = new Option(lstModProductAgentIn.options[i].text, lstModProductAgentIn.options[i].value)

            var AttributeColor = lstModProductAgentIn.options[i].style.color;
            if (AttributeColor != null) {
                objOption.style.color = AttributeColor;
            }

            lstModProductAgentOut.add(objOption);
            lstModProductAgentIn.remove(i);

            i--;
            AgentInLength = AgentInLength - 1;
        }
    }

    RefreshAgentId();
}

function AgentAllRToL() {
    var lstModProductAgentOut = document.getElementById("lstModProductAgentOut");
    var lstModProductAgentIn = document.getElementById("lstModProductAgentIn");
    var AgentOutLength = lstModProductAgentOut.options.length;
    var AgentInLength = lstModProductAgentIn.options.length;
    if (AgentOutLength <= 0)
        return;

    for (var i = 0; i < AgentOutLength; i++) {
        var objOption = new Option(lstModProductAgentOut.options[i].text, lstModProductAgentOut.options[i].value)

        var AttributeColor = lstModProductAgentOut.options[i].style.color;
        if (AttributeColor != null) {
            objOption.style.color = AttributeColor;
        }

        lstModProductAgentIn.add(objOption);
        lstModProductAgentOut.remove(i);

        i--;
        AgentOutLength = AgentOutLength - 1;
    }

    RefreshAgentId();
}

function AgentAllLToR() {
    var lstModProductAgentOut = document.getElementById("lstModProductAgentOut");
    var lstModProductAgentIn = document.getElementById("lstModProductAgentIn");
    var AgentOutLength = lstModProductAgentOut.options.length;
    var AgentInLength = lstModProductAgentIn.options.length;
    if (AgentInLength <= 0)
        return;

    for (var i = 0; i < AgentInLength; i++) {

        var objOption = new Option(lstModProductAgentIn.options[i].text, lstModProductAgentIn.options[i].value)

        var AttributeColor = lstModProductAgentIn.options[i].style.color;
        if (AttributeColor != null) {
            objOption.style.color = AttributeColor;
        }

        lstModProductAgentOut.add(objOption);
        lstModProductAgentIn.remove(i);

        i--;
        AgentInLength = AgentInLength - 1;
    }

    RefreshAgentId();
}

// Cập nhật lại Id của các nhóm
function RefreshAgentId() {
    var lstModProductAgentOut = document.getElementById("lstModProductAgentOut");
    var lstModProductAgentIn = document.getElementById("lstModProductAgentIn");

    var sModProductAgentOutId = "";
    var sModProductAgentInId = "";

    var AgentInLength = lstModProductAgentIn.options.length;
    var AgentOutLength = lstModProductAgentOut.options.length;

    // Id của các nhóm không chứa Khách hàng
    if (AgentOutLength > 0) {
        for (var i = 0; i < AgentOutLength; i++) {
            if (sModProductAgentOutId.value = "")
            { sModProductAgentOutId = lstModProductAgentOut.options[i].value; }
            else {
                sModProductAgentOutId = sModProductAgentOutId + "," + lstModProductAgentOut.options[i].value;
            }

        }
    }
    // Id của các nhóm chứa Khách hàng
    if (AgentInLength > 0) {
        for (var i = 0; i < AgentInLength; i++) {
            if (sModProductAgentInId.value = "")
            { sModProductAgentInId = lstModProductAgentIn.options[i].value; }
            else {
                sModProductAgentInId = sModProductAgentInId + "," + lstModProductAgentIn.options[i].value;
            }

        }
    }

    document.getElementById('ModProductAgentInId').value = sModProductAgentInId;
    document.getElementById('ModProductAgentOutId').value = sModProductAgentOutId;
}
//#endregion Sự kiện trên ListBox Agent

//#region: SearchProduct_BanKem
function SearchProduct_BanKem() {

    var bolCode = false;
    var bolName = false;
    var Mess = "";
    var txtCode = document.getElementById("ModProductInfoSearch_Code_BanKem");
    var txtName = document.getElementById("ModProductInfoSearch_Name_BanKem");

    if (txtCode != null) {
        if (txtCode.value.trim() != "") {
            if (txtCode.value.trim().length < 1) {
                bolCode = false;
            }
            else {
                bolCode = true;
            }
        }
        else {
            bolCode = false;
        }
    }

    if (txtName != null) {
        if (txtName.value.trim() != "") {
            bolName = true;
        }
        else {
            bolName = false;
        }
    }

    if (bolName || bolCode) {
        vsw_exec_cmd("SearchProduct_BanKem");
    }
    else {
        alert('Yêu cầu nhập điều kiện tìm kiếm sản phẩm.');
        txtCode.focus(); return false;
    }
}
//#endregion    

function OnChangePageSize_LienQuan(ddlPageSize, FunctionName) {
    var PageSize = document.getElementById("PageSize");
    var iPageSize = ddlPageSize.value;
    PageSize.value = iPageSize;

    var PageIndex = document.getElementById("PageIndex");
    PageIndex.value = 1;

    vsw_exec_cmd(FunctionName);
}

function OnChangePageIndex_LienQuan(FunctionName, PageIndexInput) {
    var PageIndex = document.getElementById("PageIndex");
    PageIndex.value = PageIndexInput;

    vsw_exec_cmd(FunctionName);
}

function OnChangePageSize_BanKem(ddlPageSize, FunctionName) {
    var PageSize = document.getElementById("PageSize");
    var iPageSize = ddlPageSize.value;
    PageSize.value = iPageSize;

    var PageIndex = document.getElementById("PageIndex");
    PageIndex.value = 1;

    vsw_exec_cmd(FunctionName);
}

function OnChangePageIndex_BanKem(FunctionName, PageIndexInput) {
    var PageIndex = document.getElementById("PageIndex");
    PageIndex.value = PageIndexInput;

    vsw_exec_cmd(FunctionName);
}

function FunctionOnchangePage(ControllerName, iPageIndex, Parameter) {
    var PageIndex = document.getElementById("PageIndex");

    PageIndex.value = iPageIndex;

    vsw_exec_cmd(ControllerName);
}

function ChangeTabIndex(iTabIndex) {
    var TabIndex = document.getElementById("TabIndexCurrent");
    TabIndex.value = iTabIndex;
}

function NotExitProduct_LienQuan(ProductId) {
    var ProductInIdList = document.getElementById("ListIdProduct_LienQuan").value.split(",");
    var bolExitsProduct = false;

    for (var i = 0; i < ProductInIdList.length; i++) {
        if (ProductId == ProductInIdList[i]) {
            alert("Sản phẩm đã nằm trong danh sách tài liệu liên quan.");
            return false;
            break;
        }
    }

    var Selected_Product_LienQuan = document.getElementById("Selected_Product_LienQuan");
    Selected_Product_LienQuan.value = ProductId;

    return true;
}

function NotExitProduct_BanKem(ProductId) {
    var ProductInIdList = document.getElementById("ListIdProduct_BanKem").value.split(",");
    var bolExitsProduct = false;

    for (var i = 0; i < ProductInIdList.length; i++) {
        if (ProductId == ProductInIdList[i]) {
            alert("Sản phẩm đã nằm trong danh sách tài liệu bán kèm.");
            return false;
            break;
        }
    }

    var Selected_Product_BanKem = document.getElementById("Selected_Product_BanKem");
    Selected_Product_BanKem.value = ProductId;

    return true;
}

function ShowSearchDinhKem() {
    var SearchDinhKem = document.getElementById("SearchDinhKem");
    SearchDinhKem.style.display = "";
}

function HideSearchDinhKem() {
    var SearchDinhKem = document.getElementById("SearchDinhKem");
    SearchDinhKem.style.display = "none";
}

// ------------ Thêm, sửa, xóa Color

function colorAdd(linkPage, TagA, index) {
    var RecordID = $("#RecordID").val();
    var tr = $(TagA).parents("tr:first");
    var pageUrl = linkPage + '&Index=' + index + "&RecordID=" + RecordID
    var dataPostBack = tr.find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

    // Kiểm tra dữ liệu
    var txtColorCode = tr.find("#txtColorCode" + index);
    if (txtColorCode == null || txtColorCode.val() == "") {
        alert("Yêu cầu nhập mã màu.");
        txtColorCode.focus();
        return false;
    }

    var txtColorName = tr.find("#txtColorName" + index);
    if (txtColorName == null || txtColorName.val() == "") {
        alert("Yêu cầu nhập tên màu.");
        txtColorName.focus();
        return false;
    }

    $.post(pageUrl,
            dataPostBack,
            function (data) {
                if (data.Error) {
                    alert(data.MessError);
                    return false;
                }
                else {
                    // KIểm tra dữ liệu lấy được
                    $("tbody[class='tbl-data-color']").html(data.MessSuccess);

                    // Đăng ký lựa chọn màu
                    $("input[type='text'][id*='txtColorCode']").colpick({
                        layout: 'hex',
                        submit: 0,
                        colorScheme: 'dark',
                        onChange: function (hsb, hex, rgb, el, bySetColor) {
                            //$(el).removeAttr('style');
                            $(el).css('background-color', '#' + hex);
                            // Fill the text box just if the color was set using the picker, and not the colpickSetColor function.
                            if (!bySetColor) $(el).val('#' + hex);
                        }
                    }).keyup(function () {
                        $(this).colpickSetColor(this.value);
                    });
                    //alert("Thêm mới màu thành công");
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

function colorSave(linkPage, TagA, index, ID) {
    var RecordID = $("#RecordID").val();
    var tr = $(TagA).parents("tr:first");
    var pageUrl = linkPage + '&Index=' + index + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = tr.find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

    // Kiểm tra dữ liệu
    var txtColorCode = tr.find("#txtColorCode" + index);
    if (txtColorCode == null || txtColorCode.val() == "") {
        alert("Yêu cầu nhập mã màu.");
        txtColorCode.focus();

        return false;
    }

    var txtColorName = tr.find("#txtColorName" + index);
    if (txtColorName == null || txtColorName.val() == "") {
        alert("Yêu cầu nhập tên màu.");
        txtColorName.focus();
        return false;
    }

    $.post(pageUrl,
            dataPostBack,
            function (data) {
                if (data.Error) {
                    alert(data.MessError);
                    return false;
                }
                else {
                    // KIểm tra dữ liệu lấy được
                    $("tbody[class='tbl-data-color']").html(data.MessSuccess);

                    alert("Cập nhật màu thành công");

                    // Đăng ký lựa chọn màu
                    $("input[type='text'][id*='txtColorCode']").colpick({
                        layout: 'hex',
                        submit: 0,
                        colorScheme: 'dark',
                        onChange: function (hsb, hex, rgb, el, bySetColor) {
                            //$(el).removeAttr('style');
                            $(el).css('background-color', '#' + hex);
                            // Fill the text box just if the color was set using the picker, and not the colpickSetColor function.
                            if (!bySetColor) $(el).val('#' + hex);
                        }
                    }).keyup(function () {
                        $(this).colpickSetColor(this.value);
                    });
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

function colorDelete(linkPage, ID) {
    if (!confirm("Bạn muốn xóa màu của sản phẩm?"))
        return false;

    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;

    $.post(pageUrl,
            dataPostBack,
            function (data) {
                if (data.Error) {
                    alert(data.MessError);
                    return false;
                }
                else {
                    // KIểm tra dữ liệu lấy được
                    $("tbody[class='tbl-data-color']").html(data.MessSuccess);

                    alert("Xóa màu thành công");

                    // Đăng ký lựa chọn màu
                    $("input[type='text'][id*='txtColorCode']").colpick({
                        layout: 'hex',
                        submit: 0,
                        colorScheme: 'dark',
                        onChange: function (hsb, hex, rgb, el, bySetColor) {
                            //$(el).removeAttr('style');
                            $(el).css('background-color', '#' + hex);
                            // Fill the text box just if the color was set using the picker, and not the colpickSetColor function.
                            if (!bySetColor) $(el).val('#' + hex);
                        }
                    }).keyup(function () {
                        $(this).colpickSetColor(this.value);
                    });
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

// ------------ Thêm, sửa, xóa Size

function SizeAdd(linkPage, TagA, index) {
    var RecordID = $("#RecordID").val();
    var tr = $(TagA).parents("tr:first");
    var pageUrl = linkPage + '&Index=' + index + "&RecordID=" + RecordID
    var dataPostBack = tr.find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

    // Kiểm tra dữ liệu
    var txtSizeCode = tr.find("#txtSizeCode" + index);
    if (txtSizeCode == null || txtSizeCode.val() == "") {
        alert("Yêu cầu nhập tên Size.");
        txtSizeCode.focus();
        return false;
    }

    var txtSizeName = tr.find("#txtSizeName" + index);
    if (txtSizeName == null || txtSizeName.val() == "") {
        alert("Yêu cầu nhập mô tả Size.");
        txtSizeName.focus();
        return false;
    }

    $.post(pageUrl,
            dataPostBack,
            function (data) {
                if (data.Error) {
                    alert(data.MessError);
                    return false;
                }
                else {
                    // KIểm tra dữ liệu lấy được
                    $("tbody[class='tbl-data-size']").html(data.MessSuccess);

                    //alert("Thực hiện thành công");
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

function SizeSave(linkPage, TagA, index, ID) {
    var RecordID = $("#RecordID").val();
    var tr = $(TagA).parents("tr:first");
    var pageUrl = linkPage + '&Index=' + index + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = tr.find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

    // Kiểm tra dữ liệu
    var txtSizeCode = tr.find("#txtSizeCode" + index);
    if (txtSizeCode == null || txtSizeCode.val() == "") {
        alert("Yêu cầu nhập tên Size.");
        txtSizeCode.focus();

        return false;
    }

    var txtSizeName = tr.find("#txtSizeName" + index);
    if (txtSizeName == null || txtSizeName.val() == "") {
        alert("Yêu cầu nhập mô tả Size.");
        txtSizeName.focus();
        return false;
    }

    $.post(pageUrl,
            dataPostBack,
            function (data) {
                if (data.Error) {
                    alert(data.MessError);
                    return false;
                }
                else {
                    // KIểm tra dữ liệu lấy được
                    $("tbody[class='tbl-data-size']").html(data.MessSuccess);

                    alert("Cập nhật Size thành công");
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

function SizeDelete(linkPage, ID) {
    if (!confirm("Bạn muốn xóa Size của sản phẩm?"))
        return false;

    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;

    $.post(pageUrl,
            dataPostBack,
            function (data) {
                if (data.Error) {
                    alert(data.MessError);
                    return false;
                }
                else {
                    // KIểm tra dữ liệu lấy được
                    $("tbody[class='tbl-data-size']").html(data.MessSuccess);

                    alert("Xóa Size thành công");
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

function ProductGroupsAdd(linkPage, TagA, ID) {

    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;
    var tr = $(TagA).parents("tr:first");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            tr.find("a[class*='jgrid Add']").addClass("a-hide");
            tr.find("a[class*='jgrid Delete']").removeClass("a-hide");
            //alert("Thực hiện thành công");
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

function ProductGroupsDelete(linkPage, TagA, ID) {
    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;
    var tr = $(TagA).parents("tr:first");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            tr.find("a[class*='jgrid Delete']").addClass("a-hide");
            tr.find("a[class*='jgrid Add']").removeClass("a-hide");
            //alert("Thực hiện thành công");
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

// Thêm và hủy sản phẩm liên quan
function RelativeAdd(linkPage, TagA, ID, ToGetList) {

    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;
    var tr = $(TagA).parents("tr:first");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            tr.find("a[class*='jgrid Add']").addClass("a-hide");
            tr.find("a[class*='jgrid Delete']").removeClass("a-hide");

            // Tải lại dữ liệu ở Parent nếu có
            if (ToGetList)
                ReLoadDataInParent(data.MessSuccess);
            //alert("Thực hiện thành công");
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

function RelativeDelete(linkPage, TagA, ID, ToGetList) {
    if (!confirm("Xóa sản phẩm khỏi danh sách liên quan?"))
        return false;

    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;
    var tr = $(TagA).parents("tr:first");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            tr.find("a[class*='jgrid Delete']").addClass("a-hide");
            tr.find("a[class*='jgrid Add']").removeClass("a-hide");

            // Tải lại dữ liệu ở Parent nếu có
            if (ToGetList)
                ReLoadDataInParent(data.MessSuccess);
            //alert("Thực hiện thành công");
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

function RelativeDelete_DeleteTr(linkPage, TagA, ID) {
    if (!confirm("Xóa sản phẩm khỏi danh sách liên quan?"))
        return false;

    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;
    var tr = $(TagA).parents("tr:first");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            // Ẩn dòng bị xóa đi
            tr.css("display", "none");
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

// Sản phẩm bán kèm

// Thêm và hủy sản phẩm bán kèm
function AttachAdd(linkPage, TagA, ID, ToGetList) {

    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;
    var tr = $(TagA).parents("tr:first");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            tr.find("a[class*='jgrid Add']").addClass("a-hide");
            tr.find("a[class*='jgrid Delete']").removeClass("a-hide");

            // Tải lại dữ liệu ở Parent nếu có
            if (ToGetList)
                ReLoadDataInParent(data.MessSuccess);
            //alert("Thực hiện thành công");
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

function AttachDelete(linkPage, TagA, ID, ToGetList) {
    if (!confirm("Xóa sản phẩm khỏi danh sách bán kèm?"))
        return false;

    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;
    var tr = $(TagA).parents("tr:first");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            tr.find("a[class*='jgrid Delete']").addClass("a-hide");
            tr.find("a[class*='jgrid Add']").removeClass("a-hide");

            // Tải lại dữ liệu ở Parent nếu có
            if (ToGetList)
                ReLoadDataInParent(data.MessSuccess);
            //alert("Thực hiện thành công");
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

function AttachDelete_DeleteTr(linkPage, TagA, ID) {
    if (!confirm("Xóa sản phẩm khỏi danh sách bán kèm?"))
        return false;

    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;
    var tr = $(TagA).parents("tr:first");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            // Ẩn dòng bị xóa đi
            tr.css("display", "none");
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

// Khu vực trong quốc gia
function AreaDelete_DeleteTr(linkPage, TagA, ID) {
    if (!confirm("Xóa khu vực?"))
        return false;

    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;
    var tr = $(TagA).parents("tr:first");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            // Ẩn dòng bị xóa đi
            tr.css("display", "none");
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

function AreaGetList(linkPage, TagA, ID, ToGetList) {
    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;
    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            // Tải lại dữ liệu ở Parent nếu có
            if (ToGetList)
                ReLoadDataInParent(data.MessSuccess);
            //alert("Thực hiện thành công");
        }
    })
    .done(function () {
    })
    //.fail(function (ex) {
    //alert("Phát sinh lỗi trong quá trình thực hiện. Hãy thử lại!");
    //return null;
    //})
    .error(function (xhr, textStatus, error) {
        console.log(xhr.statusText);
        console.log(textStatus);
        console.log(error);
    })
    .always(function () {
    })
    ;
}

/* Thông tin về đại lý*/

function AgentAdd(linkPage, TagA, ID) {

    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;
    var tr = $(TagA).parents("tr:first");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            tr.find("a[class*='jgrid Add']").addClass("a-hide");
            tr.find("a[class*='jgrid Delete']").removeClass("a-hide");
            //alert("Thực hiện thành công");
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

function AgentDelete(linkPage, TagA, ID) {
    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;
    var tr = $(TagA).parents("tr:first");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            tr.find("a[class*='jgrid Delete']").addClass("a-hide");
            tr.find("a[class*='jgrid Add']").removeClass("a-hide");
            //alert("Thực hiện thành công");
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

function AreaAdd(linkPage, TagA, ID) {

    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;
    var tr = $(TagA).parents("tr:first");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            tr.find("a[class*='jgrid Add']").addClass("a-hide");
            tr.find("a[class*='jgrid Delete']").removeClass("a-hide");
            //alert("Thực hiện thành công");
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

function AreaDelete(linkPage, TagA, ID) {
    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;
    var tr = $(TagA).parents("tr:first");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            tr.find("a[class*='jgrid Delete']").addClass("a-hide");
            tr.find("a[class*='jgrid Add']").removeClass("a-hide");
            //alert("Thực hiện thành công");
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

function FilterAdd(linkPage, TagA, ID) {

    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;
    var tr = $(TagA).parents("tr:first");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            tr.find("a[class*='jgrid Add']").addClass("a-hide");
            tr.find("a[class*='jgrid Delete']").removeClass("a-hide");
            //alert("Thực hiện thành công");
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

function FilterDelete(linkPage, TagA, ID) {
    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;
    var tr = $(TagA).parents("tr:first");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            tr.find("a[class*='jgrid Delete']").addClass("a-hide");
            tr.find("a[class*='jgrid Add']").removeClass("a-hide");
            //alert("Thực hiện thành công");
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

// ------------ Thêm, sửa, xóa Ảnh của sản phẩm

function ImageSlideShowAdd(linkPage, TagA, index) {
    var RecordID = $("#RecordID").val();
    var tr = $(TagA).parents("tr:first");
    var pageUrl = linkPage + '&Index=' + index + "&RecordID=" + RecordID
    var dataPostBack = tr.find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

    // Kiểm tra dữ liệu
    var txtSlideShowUrl = tr.find("#txtSlideShowUrl" + index);
    if (txtSlideShowUrl == null || txtSlideShowUrl.val() == "") {
        alert("Yêu cầu chọn ảnh.");
        txtSlideShowUrl.focus();
        return false;
    }

    $.post(pageUrl,
            dataPostBack,
            function (data) {
                if (data.Error) {
                    alert(data.MessError);
                    return false;
                }
                else {
                    // KIểm tra dữ liệu lấy được
                    $("tbody[class='tbl-data-slideshow']").html(data.MessSuccess);

                    //alert("Thêm mới màu thành công");
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

function ImageSlideShowSave(linkPage, TagA, index, ID) {
    var RecordID = $("#RecordID").val();
    var tr = $(TagA).parents("tr:first");
    var pageUrl = linkPage + '&Index=' + index + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = tr.find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

    // Kiểm tra dữ liệu
    var txtSlideShowUrl = tr.find("#txtSlideShowUrl" + index);
    if (txtSlideShowUrl == null || txtSlideShowUrl.val() == "") {
        alert("Yêu cầu chọn ảnh.");
        txtSlideShowUrl.focus();
        return false;
    }

    $.post(pageUrl,
            dataPostBack,
            function (data) {
                if (data.Error) {
                    alert(data.MessError);
                    return false;
                }
                else {
                    // KIểm tra dữ liệu lấy được
                    $("tbody[class='tbl-data-slideshow']").html(data.MessSuccess);

                    alert("Cập nhật màu thành công");
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

function ImageSlideShowDelete(linkPage, ID) {
    if (!confirm("Bạn muốn xóa ảnh của sản phẩm?"))
        return false;

    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;

    $.post(pageUrl,
            dataPostBack,
            function (data) {
                if (data.Error) {
                    alert(data.MessError);
                    return false;
                }
                else {
                    // KIểm tra dữ liệu lấy được
                    $("tbody[class='tbl-data-slideshow']").html(data.MessSuccess);
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

// Cập nhật các thuộc tính (Thông tin kỹ thuật cho sản phẩm)
function PropertiesSave(linkPage, DataPost) {

    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + "&RecordID=" + RecordID;
    var dataPostBack = $(DataPost).find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            alert("Cập nhật thuộc tính sản phẩm thành công.")
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

// Cập nhật thông tin các bình luận của sản phẩm -------------------------------------------------------------
function CommentSave(linkPage, TagA, ID, txtCommentContent, lblCommentContent) {
    var RecordID = $("#RecordID").val();
    var tr = $(TagA).parents("tr:first");
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID

    // Kiểm tra dữ liệu
    var txtCommentContent_Control = $("#" + txtCommentContent);
    if (txtCommentContent_Control == null || txtCommentContent_Control.val() == "") {
        alert("Yêu cầu nhập nội dung bình luận.");
        txtCommentContent_Control.focus();
        return false;
    }

    var dataPostBack = $("#" + txtCommentContent).serialize();

    $.post(pageUrl,
            dataPostBack,
            function (data) {
                if (data.Error) {
                    alert(data.MessError);
                    return false;
                }
                else {

                    // Ẩn nút save
                    $(TagA).addClass("hide");

                    // Nút hủy cập nhật
                    $(TagA).parent().find("a[class*='jgrid comment-cancel']").addClass("hide");

                    // Nút lưu thay đổi
                    $(TagA).parent().find("a[class*='jgrid comment-edit']").removeClass("hide");

                    // Ẩn textbox comment
                    $("#" + txtCommentContent).addClass("hide");

                    // Hiện label comment
                    $("#" + lblCommentContent).html(data.MessSuccess).removeClass("hide");

                    //alert("Thêm mới màu thành công");
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

function CommentApprove(linkPage, TagA, ID) {
    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID
    if (!confirm("Đồng ý duyệt bình luận?"))
        return false;

    var dataPostBack = null;
    $.post(pageUrl,
            dataPostBack,
            function (data) {
                if (data.Error) {
                    alert(data.MessError);
                    return false;
                }
                else {

                    // Ẩn nút duyệt
                    $(TagA).addClass("hide");

                    // Nút hủy duyệt
                    $(TagA).parent().find("a[class*='jgrid comment-unapprove']").removeClass("hide");
                    //alert("Thêm mới màu thành công");
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

function CommentUnApprove(linkPage, TagA, ID) {
    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID
    if (!confirm("Đồng ý hủy duyệt bình luận?"))
        return false;
    var dataPostBack = null;

    $.post(pageUrl,
            dataPostBack,
            function (data) {
                if (data.Error) {
                    alert(data.MessError);
                    return false;
                }
                else {

                    // Ẩn nút duyệt
                    $(TagA).addClass("hide");

                    // Nút hủy duyệt
                    $(TagA).parent().find("a[class*='jgrid comment-approve']").removeClass("hide");
                    //alert("Thêm mới màu thành công");
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

function CommentDelete(linkPage, TagA, ID) {
    if (!confirm("Bạn muốn xóa bình luận?"))
        return false;

    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;

    $.post(pageUrl,
            dataPostBack,
            function (data) {
                if (data.Error) {
                    alert(data.MessError);
                    return false;
                }
                else {
                    // Ẩn dòng đã bị xóa
                    $(TagA).parents("tr:first").addClass("hide");
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

/* Thông tin về loại sản phẩm*/

function TypesAdd(linkPage, TagA, ID) {

    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;
    var tr = $(TagA).parents("tr:first");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            tr.find("a[class*='jgrid Add']").addClass("a-hide");
            tr.find("a[class*='jgrid Delete']").removeClass("a-hide");
            //alert("Thực hiện thành công");
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

function TypesDelete(linkPage, TagA, ID) {
    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;
    var tr = $(TagA).parents("tr:first");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            tr.find("a[class*='jgrid Delete']").addClass("a-hide");
            tr.find("a[class*='jgrid Add']").removeClass("a-hide");
            //alert("Thực hiện thành công");
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

// Thêm và hủy sản phẩm trong đơn hàng của việc tính Doanh thu
function KyDaiLyDonHangSanPhamAdd(linkPage, TagA, ID, ToGetList) {

    var RecordID = $("#RecordID").val();
    var txtSoLuong = $("#txt" + ID + "_SoLuong").val();
    var txtDonGia = $("#txt" + ID + "_DonGia").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID + "&SoLuong=" + txtSoLuong + "&DonGia=" + txtDonGia;

    var dataPostBack = null;  //txtSoLuong + "|" + txtDonGia;
    var tr = $(TagA).parents("tr:first");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            tr.find("a[class*='jgrid Add']").addClass("a-hide");
            tr.find("a[class*='jgrid Delete']").removeClass("a-hide");

            // Tải lại các thông tin của đơn hàng
            self.parent.refreshDataDonHang(data.TongSanPham, data.TongTien, data.TongTienSauGiam);

            // Tải lại dữ liệu ở Parent nếu có
            if (ToGetList)
                ReLoadDataInParent(data.MessSuccess);

            //alert("Thực hiện thành công");
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

function KyDaiLyDonHangSanPhamDelete(linkPage, TagA, ID, ToGetList) {
    if (!confirm("Xóa sản phẩm khỏi đơn hàng?"))
        return false;

    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;
    var tr = $(TagA).parents("tr:first");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            tr.find("a[class*='jgrid Delete']").addClass("a-hide");
            tr.find("a[class*='jgrid Add']").removeClass("a-hide");

            // Tải lại các thông tin của đơn hàng
            self.parent.refreshDataDonHang(data.TongSanPham, data.TongTien, data.TongTienSauGiam);

            // Tải lại dữ liệu ở Parent nếu có
            if (ToGetList)
                ReLoadDataInParent(data.MessSuccess);
            //alert("Thực hiện thành công");
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

function KyDaiLyDonHangSanPhamDelete_DeleteTr(linkPage, TagA, ID, ToGetList) {
    if (!confirm("Xóa sản phẩm khỏi đơn hàng?"))
        return false;

    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID;
    var dataPostBack = null;
    var tr = $(TagA).parents("tr:first");

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            // Ẩn dòng bị xóa đi
            tr.css("display", "none");

            // Tải lại các thông tin của đơn hàng
            refreshDataDonHang(data.TongSanPham, data.TongTien, data.TongTienSauGiam);

            // Tải lại dữ liệu ở Parent nếu có
            if (ToGetList)
                ReLoadDataInParent(data.MessSuccess);
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

function KyDaiLyDonHangSanPhamSave(linkPage, ID, txtSoLuong, txtDonGia, ToGetList) {
    if (!confirm("Cập nhật thông tin sản phẩm trong đơn hàng?"))
        return false;

    var RecordID = $("#RecordID").val();
    var pageUrl = linkPage + '&ID=' + ID + "&RecordID=" + RecordID + "&txtSoLuong=" + $(txtSoLuong).val() + "&txtDonGia=" + $(txtDonGia).val();
    var dataPostBack = null;

    $.post(pageUrl,
    dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            // Thành công: data.MessSuccess
            //SoLuong_DonGiaSaveResult();

            // Tải lại các thông tin của đơn hàng
            refreshDataDonHang(data.TongSanPham, data.TongTien, data.TongTienSauGiam);

            // Tải lại dữ liệu ở Parent nếu có
            if (ToGetList)
                ReLoadDataInParent(data.MessSuccess);
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
