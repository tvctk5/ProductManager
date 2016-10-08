/// <reference path="jquery-1.8.2.js" />

function CheckDate(objDateField, strDateFormat, strMsg) {
    //Check Date of dateobjects.
    //User:NVK
    //Date:29/8/2003
    strDateFormat = strDateFormat.toLowerCase();
    mdateval = eval(objDateField).value;
    switch (strDateFormat) {
        case 'dd/mm/yyyy':
            if (mdateval != "") {
                mday = mdateval.substring(0, mdateval.indexOf("/"));
                mmonth = mdateval.substring(mdateval.indexOf("/") + 1, mdateval.lastIndexOf("/"));
                myear = mdateval.substring(mdateval.lastIndexOf("/") + 1, mdateval.length);
                mdate = new Date(mmonth + "/" + mday + "/" + myear);
                cday = mdate.getDate();
                cmonth = mdate.getMonth() + 1;
                cyear = mdate.getYear();
                if ((parseFloat(mday) != parseFloat(cday)) || (parseFloat(mmonth) != parseFloat(cmonth)) || (isNaN(myear)) || (myear.length < 4) || (myear.length > 4) || (myear < 1753)) {
                    alert(strMsg);
                    eval(objDateField).value = "";
                    eval(objDateField).focus();
                    return false;
                }
                break;
            }
        case 'mm/dd/yyyy':
            if (mdateval != "") {
                mmonth = mdateval.substring(0, mdateval.indexOf("/"));
                mday = mdateval.substring(mdateval.indexOf("/") + 1, mdateval.lastIndexOf("/"))
                myear = mdateval.substring(mdateval.lastIndexOf("/") + 1, mdateval.length);
                mdate = new Date(mmonth + "/" + mday + "/" + myear);
                cday = mdate.getDate();
                cmonth = mdate.getMonth() + 1;
                cyear = mdate.getYear();
                if (parseFloat(mday) != parseFloat(cday) || parseFloat(mmonth) != parseFloat(cmonth) || (myear != cyear) || (myear.length != 4) || (myear < 1753)) {
                    alert(strMsg);
                    eval(objDateField).value = "";
                    eval(objDateField).focus();
                    return false;
                }
                break;
            }
    }
    return true;
}

$(document).ready(function () {

    // Các control yêu cầu nhập dữ liệu
    $("input[require='true'],textarea[require='true'],select[require='true']").after("&nbsp;(<span class='RequireValidate'>*</span>)");

    // Các control có thuộc tính ReadOnly
    $("input[readonly='readonly'][require!='true'],textarea[readonly='readonly'][require!='true']").after("&nbsp;<span class='img-readonly' title='Thuộc tính chỉ xem, không được chỉnh sửa!'></span>");

    $("input[price='true']").each(function () {
        this.value = ConvertToPrice(this.value);
    });

    $("input[price='true']").change(function () {
        this.value = this.value.trim();

        if (this.value.trim() == "")
            return "";

        var thisValue = replaceAll(replaceAll(this.value, ",", ""), ".", "");
        if (thisValue.length <= 3)
            return thisValue;

        var length = thisValue.length;
        var soKyTu = parseInt(length / 3);
        var dataReturn = "";
        var index = 0;

        for (var i = thisValue.length - 1; i >= 0; i--) {

            if (index % 3 == 0 && index != 0)
                dataReturn = "." + dataReturn;

            dataReturn = thisValue[i] + dataReturn;
            index = index + 1;
        }

        this.value = dataReturn;
    });
});

$(document).submit(function () {
    $("input[price='true']").each(function () {
        this.value = replaceAll(replaceAll(this.value, ",", ""), ".", "");
    });
});

function ConvertToPrice(value) {
    value = value.trim();
    if (value == "")
        return "";

    var thisValue = replaceAll(replaceAll(value, ",", ""), ".", "");
    if (thisValue.length <= 3)
        return thisValue;

    var length = thisValue.length;
    var soKyTu = parseInt(length / 3);
    var dataReturn = "";
    var index = 0;

    for (var i = thisValue.length - 1; i >= 0; i--) {
        if (index % 3 == 0 && index != 0)
            dataReturn = "." + dataReturn;

        dataReturn = thisValue[i] + dataReturn;
        index = index + 1;
    }

    value = dataReturn;

    return value;
}

// Replaces all instances of the given substring.
function replaceAll(Source, strTarget, // The substring you want to replace
                                            strSubString // The string you want to replace in.
                                            ) {
    var strText = Source;
    var intIndexOfMatch = strText.indexOf(strTarget);

    // Keep looping while an instance of the target string
    // still exists in the string.
    while (intIndexOfMatch != -1) {
        // Relace out the current instance.
        strText = strText.replace(strTarget, strSubString)

        // Get the index of any next matching substring.
        intIndexOfMatch = strText.indexOf(strTarget);
    }

    // Return the updated string with ALL the target strings
    // replaced out with the new substring.
    return (strText);
}

function GetDataJsonPost(pageUrl, formID) {
    var pageUrl = pageUrl;
    var dataPostBack = null;

    if (formID == "")
        dataPostBack = $("#vswForm").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
    else
        dataPostBack = $("#" + formID).find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

    //dataPostBack
    $.post(pageUrl,
            dataPostBack,
            function (data) {
                if (data.Error)
                // Dữ liệu trả ra lỗi
                    return null;
                else {
                    // Trả ra dữ liệu thực hiện được
                    return data;
                }
            })
            .done(function () {
            })
            .fail(function () {
                return null;
            })
            .always(function () {
            })
            ;
}

function CheckDuplicate(pageUrl, formID, IdFocus) {

    var pageUrl = pageUrl;
    var dataPostBack = null;

    if (formID == "")
        dataPostBack = $("#vswForm").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
    else
        dataPostBack = $("#" + formID).find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

    //dataPostBack
    $.post(pageUrl,
            dataPostBack,
            function (data) {
                if (data.Error) {
                    // Dữ liệu trả ra lỗi
                    $("#divMessError").html(data.MessError);
                    if (IdFocus != "")
                        $("#" + IdFocus).removeClass("DuplicateError");

                    return false;
                }
                else {
                    // KIểm tra dữ liệu lấy được
                    if (data.NotDuplicate == false) {

                        $("#divMessError").html(data.MessSuccess).show();
                        alert(data.MessSuccess);

                        // focus to control
                        if (IdFocus != "") {
                            $("#" + IdFocus).addClass("DuplicateError").focus();
                            $("#" + IdFocus).val("");
                        }

                        return false;
                    }
                    else {
                        $("#divMessError").html("").hide();
                        if (IdFocus != "")
                            $("#" + IdFocus).removeClass("DuplicateError");

                        return true;
                    }
                }
            })
            .done(function () {
            })
            .fail(function () {
                return null;
            })
            .always(function () {
            })
            ;
}

function GetData(pageUrl, formID, IdFocus, ControlViewData) {

    var pageUrl = pageUrl;
    var dataPostBack = null;

    if (formID == "")
        dataPostBack = $("#vswForm").find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();
    else
        dataPostBack = $("#" + formID).find("input,textarea,select,hidden").not("#__VIEWSTATE,#__EVENTVALIDATION").serialize();

    //dataPostBack
    $.post(pageUrl,
            dataPostBack,
            function (data) {
                if (data.Error) {
                    // Dữ liệu trả ra lỗi
                    $("#divMessError").html(data.MessError);
                    if (IdFocus != "")
                        $("#" + IdFocus).removeClass("DuplicateError");

                    return false;
                }
                else {
                    // KIểm tra dữ liệu lấy được
                    if (data.NotDuplicate == true) {

                        $("#" + ControlViewData).html(data.MessSuccess);

                        // focus to control
                        if (IdFocus != "") {
                            $("#" + IdFocus).addClass("DuplicateError").focus();
                            $("#" + IdFocus).val("");
                        }

                        return false;
                    }
                    else {
                        $("#divMessError").html("").hide();
                        if (IdFocus != "")
                            $("#" + IdFocus).removeClass("DuplicateError");

                        return true;
                    }
                }
            })
            .done(function () {
            })
            .fail(function () {
                return null;
            })
            .always(function () {
            })
            ;
}


function ConvertToFloat(input) {
    if (input == "")
        return 0;

    var giatri = replaceAll(replaceAll(input, ",", ""), ".", "");

    return parseFloat(giatri);
}
