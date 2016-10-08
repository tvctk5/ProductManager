<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    
    var listItem = new List<ModProduct_SurveyGroupEntity>();
    var Data = ViewBag.Data as List<ModProduct_SurveyGroupEntity>;
    var Data_Detail = ViewBag.Data_Detail as List<ModProduct_SurveyGroup_DetailEntity>;

    string sListGroup = string.Empty;

    // Nếu không có dữ liệu
    if (Data == null || Data.Count <= 0 || Data_Detail == null || Data_Detail.Count <= 0)
        return;

    int iType = 0;

    for (int i = 0; i < Data.Count; i++)
    {
        sListGroup += "<div class='popup-content-group popup-content-group-" + i + "'>";

        sListGroup += "<div class='popup-content-group-title'>";
        sListGroup += "<span>" + Data[i].Name + "</span>";
        // End class='popup-content-group-title'
        sListGroup += "</div>";

        sListGroup += "<div class='popup-content-group-content'>";

        sListGroup += "<ul class='popup-content-group-content-ul'>";

        // Lấy thông tin chi tiết thuộc tính khảo sát
        iType = Data[i].Type;
        var ListDetail = Data_Detail.Where(o => o.SurveyGroupId == Data[i].ID).ToList();
        if (ListDetail != null && ListDetail.Count > 0)
        {
            // Duyệt từng bản ghi chi tiết
            foreach (var itemDetail in ListDetail)
            {
                sListGroup += "<li class='popup-content-group-content-ul-li'>";

                // Kiểm tra xem hiển thị theo checkbox hay radio
                if (iType == (int)EnumValue.TypeSurveyGroup.RADIO)
                {
                    sListGroup += "<span class='action-sub'><input type='radio' name='surveydetail" + Data[i].ID + "' value='" + itemDetail.ID + "' />&nbsp;<span class='action-sub-radio-label'>" + itemDetail.Name + "</span></span>";
                }
                // Checkbox
                else
                {
                    sListGroup += "<span class='action-sub'><input type='checkbox' name='surveydetail" + Data[i].ID + "' value='" + itemDetail.ID + "' />&nbsp;<span class='action-sub-checkbox-label'>" + itemDetail.Name + "</span></span>";
                }

                sListGroup += "</li>";
            }

        }

        // End class='popup-content-group-content-ul'
        sListGroup += "</ul>";

        // Tạo nút bấm
        sListGroup += "<div class='popup-content-group-function'>";
        sListGroup += "<input type='button' value='Gửi thông tin khảo sát' dataPostId='popup-content-group-" + i + "' width='100%' class='popup-content-group-function-submit'>";
        sListGroup += "</div>";

        // End class='popup-content-group-content'
        sListGroup += "</div>";

        // End popup-content-group
        sListGroup += "</div>";
    }

    // Biến kiểm tra xem form đang đóng hay mở
    bool Opened = true;
    int iOpened = 1;
    if (VSW.Lib.Global.Cookies.Exists("VSW.PopupSurvey"))
    {
        iOpened = VSW.Lib.Global.ConvertTool.ConvertToInt32(VSW.Lib.Global.Cookies.GetValue("VSW.PopupSurvey"));
        Opened = iOpened == 1 ? true : false;
    }
%>
<%--<form method="post">--%>
<div class="popup-survey">
    <script type="text/javascript">
        $(document).ready(function () {
            // Kiểm tra thông tin
            //var countInputChecked =
            $(".popup-content-group-function-submit").click(function () {
                // Lấy cha
                var parent_Name = "." + $(".popup-content-group-function-submit").attr("dataPostId");
                var parent_check = $(parent_Name);
                // Kiểm tra xem có input nào check hay chưa
                var countInputChecked = parent_check.find("input:checked").length;
                if (countInputChecked <= 0) {
                    alert("Yêu cầu lựa chọn thông tin khảo sát.");
                    return false;
                }

                // Gửi thông tin
                SentSurvey(parent_check);
            });
        });
    </script>
    <div class="popup-title" opened="<%=iOpened %>" alwayOpen="<%=ViewBag.AlwayOpenPopupSurvey %>">
        <a href="javascript:void();" title="Bấm để Đóng/ Mở khảo sát" class="module-popup-title-img" opened="<%=iOpened %>"></a>
        <span>
            <%=ViewBag.Title %></span></div>
    <div class='popup-content<%=(Opened?"":" hide") %>'>
        <%=sListGroup%>
    </div>
    <div class="popup-footer">
    </div>
</div>
